angular.module('MetronicApp').controller('PettyCashDisbursementAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document,
    $uibModal, $stateParams, $state, pettyCashService, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.filterType = $stateParams.type;
    $scope.headerTitle = 'Petty Cash Disbursement';
    $scope.disburseAmountLabel = 'Disburse Amount';

    pettyCashService.getForDisbursement($stateParams.id).then(function (res) {
        $scope.float = res.pettyCashFloat;
        $scope.cashInHand = res.pettyCashBalance;
        $scope.pettyCashRequest = res;
    });

    pettyCashService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.disburse = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        obj.disbursedById = employeeId;

        if (obj.settledIOUList.length > 0 && !parseFloat(obj.totalIouExpences) > 0) {
            var confirmation = confirm("Pending IOUs available for this employee, but you have not selected any. Do you want to continue without settling them ?");
            if (confirmation == true) {
                pettyCashService.disburse(obj).then(function (res) {
                    if (res.status == true) {
                        notificationMsgService.showSuccessMessage('Petty Cash disbursed successfully');
                        $state.go('pettyCashDisbursementList', { type: $stateParams.type });
                        pettyCashService.printPettyCash(res.id).then(function (res) {
                        });
                    }
                    else
                        notificationMsgService.showErrorMessage(res.message);
                });
            }
        }
        else {
            pettyCashService.disburse(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Petty Cash disbursed successfully');
                    $state.go('pettyCashDisbursementList', { type: $stateParams.type });
                    pettyCashService.printPettyCash(res.id).then(function (res) {
                    });
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.reject = function (obj) {
        var confirmation = confirm("Are you sure you want to reject this request ?");

        if (confirmation == true) {
            var employeeId = document.getElementById('HiddenEmployeeID').value;
            obj.rejectedById = employeeId;

            pettyCashService.rejectPettyCash(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Petty Cash rejected successfully');
                    $state.go('pettyCashDisbursementList', { type: $stateParams.type });
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.fillAmount = function (id) {
        var obj = $scope.pettyCashRequest.settledIOUList.find(x => x.id === id);
        if (obj.selected) {
            if (obj.settledAmount) {
                var bal = parseFloat(obj.amount) - parseFloat(obj.settledAmount);
                obj.pettyCashUtilizedAmount = bal.toFixed(2);
            }
            else
                obj.pettyCashUtilizedAmount = obj.amount;
        }
        else
            obj.pettyCashUtilizedAmount = '';
        calcBalance();
    }

    $scope.calculateBalance = function (id) {
        var obj = $scope.pettyCashRequest.settledIOUList.find(x => x.id === id);
        if (obj.selected) {
            var bal = parseFloat(obj.amount) - parseFloat(obj.settledAmount);
            if (!obj.pettyCashUtilizedAmount) {
                obj.pettyCashUtilizedAmount = bal;
            }
            else if (parseFloat(obj.pettyCashUtilizedAmount) <= 0) {
                obj.pettyCashUtilizedAmount = bal;
            }
            else if (obj.pettyCashUtilizedAmount > bal) {
                obj.pettyCashUtilizedAmount = bal;
            }
        }
        else
            obj.pettyCashUtilizedAmount = '';

        calcBalance();
    }

    function calcBalance() {
        $scope.pettyCashRequest.totalIouExpences = 0;
        for (var i = 0; i < $scope.pettyCashRequest.settledIOUList.length; i++) {
            if ($scope.pettyCashRequest.settledIOUList[i].selected) {
                if ($scope.pettyCashRequest.settledIOUList[i].pettyCashUtilizedAmount)
                    $scope.pettyCashRequest.totalIouExpences += parseFloat($scope.pettyCashRequest.settledIOUList[i].pettyCashUtilizedAmount);
            }
        }

        if ($scope.pettyCashRequest.totalIouExpences != 0)
            $scope.disburseAmountLabel = 'Balance Amount';
        else
            $scope.disburseAmountLabel = 'Disburse Amount';

        var settle = parseFloat($scope.pettyCashRequest.amount) - parseFloat($scope.pettyCashRequest.totalIouExpences);

        $scope.pettyCashRequest.settledAmount = settle.toFixed(2);
        $scope.pettyCashRequest.totalIouExpences = $scope.pettyCashRequest.totalIouExpences.toFixed(2);
    }

    //Attachments
    function loadAttachments() {
        var defer = $.Deferred();
        pettyCashService.getPettyCashVoucherAttachments($scope.id).then(function (res) {
            console.log(res);
            $scope.attachments = res.files;
            defer.resolve();
        });
        return defer;
    };
    loadAttachments();

    $scope.openModel = function (size, parentSelector, currentItemIndex) {
        var parentElem = parentSelector ?
            angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'pettyCashAttachment.html',
            controller: 'PettyCashFinanceApprovalInstanceCtrl',
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

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});