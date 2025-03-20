angular.module('MetronicApp').controller('MedicalClaimRequestAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal,
    $stateParams, $state, medicalClaimService, employeeService, FileUploader, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.headerTitle;
    $scope.animationsEnabled = true;
    $scope.selectedFamilyMember;
    $scope.thumbnailAllowance;
    $scope.thumbnailBalance;
    $scope.employeeId = document.getElementById('HiddenEmployeeID').value;
    $scope.eligible = document.getElementById('HiddenEligibleForMedicalClaims').value;
    $scope.amount;

    (function () {
        loadFamilyMembers().then(loadMedicalClaimRequest).then(loadDisplayCounts).then(loadAttachments).then(loadTxList);
    }());

    function loadTxList() {
        var defer = $.Deferred();
        medicalClaimService.getTxHistory($stateParams.id).then(function (res) {
            $scope.txHistoryList = res;
            defer.resolve();
        });
        return defer;
    };

    
    function loadFamilyMembers() {
        var defer = $.Deferred();
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        employeeService.getFamilyMembersForMedicalClaim(employeeId).then(function (res) {
            $scope.familyMembers = res;
            defer.resolve();
        });
        return defer;
    };

    function loadMedicalClaimRequest() {
        var defer = $.Deferred();
        if ($stateParams.id != 0)
            $scope.headerTitle = "Edit Claim Request";
        else
            $scope.headerTitle = "New Claim Request";

        medicalClaimService.getById($stateParams.id).then(function (res) {
            $scope.medicalClaimRequest = res;
            $scope.thumbnailAllowance = parseInt(res.allowance);
            $scope.amount = parseInt(res.amount);
            for (var x = 0; x < res.itemList.length; x++) {
                if (res.itemList[x].relationship) {
                    $scope.medicalClaimRequest.itemList[x].selectedRelationship = $scope.familyMembers.find(s => s.relationship === res.itemList[x].relationship)
                }
                else
                    $scope.medicalClaimRequest.itemList[x].selectedRelationship = null;
            }

            for (var i = 0; i < res.itemList.length; i++) {
                if (res.itemList[i].dateOfExpenditure) {
                    $scope.medicalClaimRequest.itemList[i].dateOfExpenditure = new Date(res.itemList[i].dateOfExpenditure);
                }
                else
                    $scope.medicalClaimRequest.itemList[i].dateOfExpenditure = null;
            }

            defer.resolve();
        });

        return defer;
    };

    function loadDisplayCounts() {
        var defer = $.Deferred();
        if (($stateParams.id != 0 && $scope.medicalClaimRequest.medicalClaimStatus != 'Rejected')) {
            medicalClaimService.getDisplayCounts($scope.employeeId).then(function (res) {
                $scope.temp = $scope.medicalClaimRequest.medicalClaimStatus;
                $scope.thumbnailUtilized = parseInt(res.thumbnailUtilized);
                $scope.thumbnailInApprove = parseInt(res.thumbnailInApprove) - $scope.amount;
                $scope.thumbnailBalance = parseInt($scope.thumbnailAllowance - ($scope.thumbnailUtilized + $scope.thumbnailInApprove));
                defer.resolve();
            });
        }
        else {
            medicalClaimService.getDisplayCounts($scope.employeeId).then(function (res) {
                $scope.thumbnailUtilized = parseInt(res.thumbnailUtilized);
                $scope.thumbnailInApprove = parseInt(res.thumbnailInApprove) 
                $scope.thumbnailBalance = parseInt($scope.thumbnailAllowance - ($scope.thumbnailUtilized + $scope.thumbnailInApprove));
                defer.resolve();
            });
        }
        
        return defer;
    };

    function loadAttachments() {
        var defer = $.Deferred();
        medicalClaimService.getMedicalClaimAttachments($scope.id).then(function (res) {
            console.log(res);
            $scope.attachments = res.files;
            defer.resolve();
        });
        return defer;
    };

    $scope.addNewListItem = function () {
        $scope.medicalClaimRequest.itemList.push({
            "medicalClaimId": null,
            "relationship": null,
            "patient": null,
            "illness": null,
            "nameOfSpecialist": null,
            "dateOfExpenditure": null,
            "amount": null,
            
        });
        $scope.memberName();
    }

    $scope.deleteListItem = function (item) {
        var i = $scope.medicalClaimRequest.itemList.indexOf(item);
        if (i != -1) {
            $scope.medicalClaimRequest.itemList.splice(i, 1);
        }
        $scope.calcAmount();
    }

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            var employeeId = document.getElementById('HiddenEmployeeID').value;
            obj.payeeId = employeeId;
            obj.requestedById = employeeId;
            obj.medicalClaimStatus = "Requested";            
            $scope.medicalClaimRequest.attachmentList = [];
            for (var i = 0; i < $scope.attachments.length; i++) {
                $scope.medicalClaimRequest.attachmentList.push({
                    "fileName": $scope.attachments[i].fileName
                });
            }
            medicalClaimService.saveRequst(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Medical Claim saved successfully');
                    $state.go('medicalClaimRequestList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('medicalClaimRequestList', {});
    }

    $scope.calcAmount = function () {       
        $scope.medicalClaimRequest.billAmount = 0;
        for (var i = 0; i < $scope.medicalClaimRequest.itemList.length; i++) {
            if ($scope.medicalClaimRequest.itemList[i].amount)
                $scope.medicalClaimRequest.billAmount += parseFloat($scope.medicalClaimRequest.itemList[i].amount);                
        }
        $scope.calcClaimAmount();
    }

    $scope.calcClaimAmount = function () {
        if ($scope.medicalClaimRequest.billAmount <= $scope.thumbnailBalance)
            $scope.medicalClaimRequest.amount = $scope.medicalClaimRequest.billAmount;
        else
            $scope.medicalClaimRequest.amount = $scope.thumbnailBalance;
    }

    $scope.fillMemberName = function (item) {
        item.patient = '';
        if (item.selectedRelationship) {
            item.patient = item.selectedRelationship.name;
            item.relationship = item.selectedRelationship.relationship;
        }
    }

    //Date Picker [Start]
    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2100, 12, 31),
        minDate: new Date(2016, 01, 01),
        startingDay: 1,
        format: 'dd-MMMM-yyyy'
    };
    $scope.format = 'dd-MMMM-yyyy';
    $scope.openDatePicker1 = function ($event,item) {
        $scope.popup1.opened = true;

        //$event.preventDefault();
        //$event.stopPropagation();
        //item.dateOfExpenditureOpened = true;
    };

    $scope.popup1 = {
        opened: false
    };

    $scope.openDatePickerDateOfExpenditure = function ($event, item) {
        $event.preventDefault();
        $event.stopPropagation();
        item.dateOfExpenditureOpened = true;
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 1);
    $scope.dateEvents = [
        {
            date: tomorrow,
            status: 'full'
        },
        {
            date: afterTomorrow,
            status: 'partially'
        }
    ];

    function getDayClass(data) {
        var date = data.date,
            mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.dateEvents[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.dateEvents[i].status;
                }
            }
        }
        return '';
    }
    //Date Picker [End]

    $scope.openModel = function (size, parentSelector, currentItemIndex) {
        var parentElem = parentSelector ?
            angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'medicalClaimAttachment.html',
            controller: 'MedicalClaimAttachmentInstanceCtrl',
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
            medicalClaimService.deleteMedicalClaimAttachment(item.fileId).then(function (res) {
                if (res.statusInfo.status == "0") {
                    var i = $scope.attachments.indexOf(item);
                    if (i != -1) {
                        $scope.attachments.splice(i, 1);
                    }
                }
            });
        }
        else {

            medicalClaimService.deleteMedicalClaimAttachmentByName(item.fileName).then(function (res) {
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
        url: 'api/medicalClaimAttachment/uploadMedicalClaimAttachment/' + $scope.id,
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

    //TODO
    //$scope.rotateAttachment = function (item) {
    //    debugger;

    //    var img = document.getElementById(item.fileId);

    //    angle = (angle + 90) % 360;
    //    img.className = "rotate" + angle;
    //    //img.setAttribute('style', 'transform:rotate(180deg)');
    //}

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