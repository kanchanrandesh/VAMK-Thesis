angular.module('MetronicApp').controller('PettyCashRequestAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal,
    $stateParams, $state, pettyCashService, FileUploader, companyService, projectUnitService, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.iouId = $stateParams.iouId;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.headerTitle;
    $scope.selectedCompany;
    $scope.selectedProject;
    $scope.animationsEnabled = true;

    (function () {
        loadCompanies().then(loadAttachments).then(loadProjectUnits).then(loadTxList).then(loadPettyCashRequest);
    }());

    function loadCompanies() {
        var defer = $.Deferred();
        companyService.getAll().then(function (res) {
            $scope.companies = res;
            defer.resolve();
        });
        return defer;
    };

    function loadProjectUnits() {
        var defer = $.Deferred();
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        projectUnitService.getAll(employeeId).then(function (res) {
            $scope.projectUnits = res;
            defer.resolve();
        });
        return defer;
    };

    function loadTxList() {
        var defer = $.Deferred();
        pettyCashService.getTxHistory($stateParams.id).then(function (res) {
            $scope.txHistoryList = res;
            defer.resolve();
        });
        return defer;
    };

    function loadPettyCashRequest() {
        var defer = $.Deferred();
        if ($stateParams.id != 0)
            $scope.headerTitle = "Edit Petty Cash Request";
        else
            $scope.headerTitle = "New Petty Cash Request";
        if ($stateParams.iouId) {
            pettyCashService.getForIOU($stateParams.iouId).then(function (res) {
                $scope.pettyCashRequest = res;
                if (!res.companyId) {
                    var employeeCompanyId = document.getElementById('HiddenEmployeeCompanyID').value;
                    $scope.selectedCompany = $scope.companies.find(x => x.id === employeeCompanyId)
                }
                else
                    $scope.selectedCompany = $scope.companies.find(x => x.id === res.companyId)
                if (res.projectId)
                    $scope.selectedProjectUnit = 'P' + res.projectId;
                else
                    $scope.selectedProjectUnit = 'U' + res.unitId;
                defer.resolve();
            });
        }
        else {
            pettyCashService.getById($stateParams.id).then(function (res) {
                $scope.pettyCashRequest = res;
                if (!res.companyId) {
                    var employeeCompanyId = document.getElementById('HiddenEmployeeCompanyID').value;
                    $scope.selectedCompany = $scope.companies.find(x => x.id === employeeCompanyId)
                }
                else
                    $scope.selectedCompany = $scope.companies.find(x => x.id === res.companyId)
                if (res.projectId)
                    $scope.selectedProjectUnit = 'P' + res.projectId;
                else
                    $scope.selectedProjectUnit = 'U' + res.unitId;
                defer.resolve();
            });
        }
        return defer;
    };

    function loadAttachments() {
        var defer = $.Deferred();
        pettyCashService.getPettyCashVoucherAttachments($scope.id).then(function (res) {
            console.log(res);
            $scope.attachments = res.files;
            defer.resolve();
        });
        return defer;
    };

    $scope.addNewListItem = function () {
        $scope.pettyCashRequest.itemList.push({
            "pettyCashVoucherId": null,
            "description": null,
            "categoryId": null,
            "amount": null,
        });
    }

    $scope.deleteListItem = function (item) {
        var i = $scope.pettyCashRequest.itemList.indexOf(item);
        if (i != -1) {
            $scope.pettyCashRequest.itemList.splice(i, 1);
        }
        $scope.calcAmount();
    }

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            var employeeId = document.getElementById('HiddenEmployeeID').value;
            if ($scope.selectedProjectUnit) {
                var cha = $scope.selectedProjectUnit.substring(0, 1);
                var length = $scope.selectedProjectUnit.length;
                if (cha == 'P') {
                    obj.projectId = $scope.selectedProjectUnit.substring(1, length)
                    obj.unitId = '';
                }
                else {
                    obj.unitId = $scope.selectedProjectUnit.substring(1, length)
                    obj.projectId = '';
                }
            }
            obj.payeeId = employeeId;
            obj.requestedById = employeeId;
            obj.pettyCashStatus = "Requested";
            if ($scope.selectedCompany)
                obj.companyId = $scope.selectedCompany.id
            else
                obj.companyId = null

            $scope.pettyCashRequest.attachmentList = [];
            for (var i = 0; i < $scope.attachments.length; i++) {
                $scope.pettyCashRequest.attachmentList.push({
                    "fileName": $scope.attachments[i].fileName
                });
            }

            pettyCashService.saveRequst(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Petty Cash saved successfully');
                    $state.go('pettyCashRequestList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('pettyCashRequestList', {});
    }

    $scope.calcAmount = function () {
        $scope.pettyCashRequest.amount = 0;
        for (var i = 0; i < $scope.pettyCashRequest.itemList.length; i++) {
            if ($scope.pettyCashRequest.itemList[i].amount)
                $scope.pettyCashRequest.amount += parseFloat($scope.pettyCashRequest.itemList[i].amount);
        }
    }

    $scope.openModel = function (size, parentSelector, currentItemIndex) {
        var parentElem = parentSelector ?
            angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'pettyCashAttachment.html',
            controller: 'PettyCashAttachmentInstanceCtrl',
            controllerAs: '$ctrl',
            windowClass: 'modelPosition',
            size: size,
            appendTo: parentElem,
            resolve: {
                items: function () {
                    return { defaultItemIndex: currentItemIndex, files: $scope.attachments }
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            $ctrl.selected = selectedItem;
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
        });
    }

    $scope.deleteAttachment = function (item) {
        if (Number(item.fileId) === 0) {
            pettyCashService.deletePettyCashVoucherAttachment(item.fileId).then(function (res) {
                if (res.statusInfo.status == "0") {
                    var i = $scope.attachments.indexOf(item);
                    if (i != -1) {
                        $scope.attachments.splice(i, 1);
                    }
                }
            });
        }
        else {
            //pettyCashService.deletePettyCashVoucherAttachment(item.fileId).then(function (res) {
            //    if (res.statusInfo.status == "0") {
            //        var i = $scope.attachments.indexOf(item);
            //        if (i != -1) {
            //            $scope.attachments.splice(i, 1);
            //        }
            //    }
            //});

            pettyCashService.deletePettyCashVoucherAttachmentByName(item.fileName).then(function (res) {
                if (res.statusInfo.status == "0") {
                    var i = $scope.attachments.indexOf(item);
                    if (i != -1) {
                        $scope.attachments.splice(i, 1);
                    }
                }
            });


        }
    }

    //File Uploader
    var uploader = $scope.uploader = new FileUploader({
        url: 'api/pettyCashVoucherAttachment/uploadPettyCashVoucherAttachment/' + $scope.id,
        autoUpload: true,
        formData: [{ id: $scope.id }]
    });

    $scope.UploadAllFiles = function (e) {
        $("#fileUploader").trigger("click");
        e.stopPropagation();
    }

    $scope.deleteFile = function (item) {
        eventService.deleteFile(item.fileId).then(function (res) {
            if (res.statusInfo.status == "0") {
                var i = $scope.eventFiles.files.indexOf(item);
                if (i != -1) {
                    $scope.eventFiles.files.splice(i, 1);
                }
            }
        });
    }

    $("#fileUploader").onchange = function (e) {
        uploader.uploadAll();
    };

    uploader.onCompleteItem = function (fileItem, response, status, headers) {
        if (fileItem) {
            if (fileItem.file) {
                if (fileItem.file.type != 'image/png' && fileItem.file.type != 'image/jpg' && fileItem.file.type != 'image/jpeg') {
                    notificationMsgService.showErrorMessage('File type should be png, jpg or jpeg');
                    return;
                }
                if (fileItem.file.size > 2097152) {
                    notificationMsgService.showErrorMessage('File size should be less than 2 MB');
                    return;
                }
            }
        }
        $scope.attachments.push({
            fileId: response.fileID,
            fileName: response.fileName
        });
        uploader.removeFromQueue(fileItem);
    };

    uploader.onCompleteAll = function () {
        console.info('onCompleteAll');
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});