angular.module('MetronicApp').controller('MedicalClaimDisbursementAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document,
    $uibModal, $stateParams, $state, medicalClaimService, employeeService, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.filterType = $stateParams.type;
    $scope.headerTitle = 'Claim Disbursement';
    $scope.disburseAmountLabel = 'Disburse Amount';
    $scope.employeeId;

    (function () {
        loadAllowance().then(loadMedicalClaim).then(loadClaimAmount).then(loadDisplayCounts);
    }());

    function loadMedicalClaim() {
        var defer = $.Deferred();
        medicalClaimService.getForView($stateParams.id).then(function (res) {
            $scope.medicalClaimRequest = res;
            defer.resolve();
        });
        return defer;
    };

    function loadAllowance() {
        var defer = $.Deferred();
        medicalClaimService.getById($stateParams.id).then(function (res) {
            $scope.medicalClaimRequest = res;
            $scope.employeeId = res.payeeId;
            $scope.thumbnailAllowance = parseInt(res.allowance);
            defer.resolve();
        })
        return defer;
    };

    function loadClaimAmount() {
        var defer = $.Deferred();
        medicalClaimService.getById($stateParams.id).then(function (res) {
            $scope.amount = res.amount;
            defer.resolve();
        });
    };

    function loadDisplayCounts() {
        var defer = $.Deferred();
        medicalClaimService.getDisplayCounts($scope.employeeId).then(function (res) {
            $scope.thumbnailUtilized = parseInt(res.thumbnailUtilized);
            $scope.thumbnailInApprove = parseInt(res.thumbnailInApprove) - parseInt($scope.amount);
            $scope.thumbnailBalance = parseInt($scope.thumbnailAllowance - ($scope.thumbnailUtilized + $scope.thumbnailInApprove));
            defer.resolve();
        })
        return defer;
    };

    medicalClaimService.getForDisbursement($stateParams.id).then(function (res) {
        $scope.float = res.medicalClaimFloat;
        $scope.cashInHand = res.medicalClaimBalance;
        $scope.medicalClaimRequest = res;
    });

    medicalClaimService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.disburse = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        obj.paidByID = employeeId;
            medicalClaimService.disburse(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Medical Claim disbursed successfully');
                    $state.go('medicalClaimDisbursementList', { type: $stateParams.type });
                    medicalClaimService.printMedicalClaim(res.id).then(function (res) {
                    });
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
    }

    $scope.reject = function (obj) {
        var confirmation = confirm("Are you sure you want to reject this request ?");

        if (confirmation == true) {
            var employeeId = document.getElementById('HiddenEmployeeID').value;
            obj.rejectedById = employeeId;

            medicalClaimService.rejectMedicalClaim(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Medical Claim rejected successfully');
                    $state.go('medicalClaimDisbursementList', { type: $stateParams.type });
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.fillAmount = function (id) {
        var obj = $scope.medicalClaimRequest.settledIOUList.find(x => x.id === id);
        if (obj.selected) {
            if (obj.settledAmount) {
                var bal = parseFloat(obj.amount) - parseFloat(obj.settledAmount);
                obj.medicalClaimUtilizedAmount = bal.toFixed(2);
            }
            else
                obj.medicalClaimUtilizedAmount = obj.amount;
        }
        else
            obj.medicalClaimUtilizedAmount = '';
        calcBalance();
    }

    $scope.calculateBalance = function (id) {
        var obj = $scope.medicalClaimRequest.settledIOUList.find(x => x.id === id);
        if (obj.selected) {
            var bal = parseFloat(obj.amount) - parseFloat(obj.settledAmount);
            if (!obj.medicalClaimUtilizedAmount) {
                obj.medicalClaimUtilizedAmount = bal;
            }
            else if (parseFloat(obj.medicalClaimUtilizedAmount) <= 0) {
                obj.medicalClaimUtilizedAmount = bal;
            }
            else if (obj.medicalClaimUtilizedAmount > bal) {
                obj.medicalClaimUtilizedAmount = bal;
            }
        }
        else
            obj.medicalClaimUtilizedAmount = '';

        calcBalance();
    }

    function calcBalance() {
        $scope.medicalClaimRequest.totalIouExpences = 0;
        for (var i = 0; i < $scope.medicalClaimRequest.settledIOUList.length; i++) {
            if ($scope.medicalClaimRequest.settledIOUList[i].selected) {
                if ($scope.medicalClaimRequest.settledIOUList[i].medicalClaimUtilizedAmount)
                    $scope.medicalClaimRequest.totalIouExpences += parseFloat($scope.medicalClaimRequest.settledIOUList[i].medicalClaimUtilizedAmount);
            }
        }

        if ($scope.medicalClaimRequest.totalIouExpences != 0)
            $scope.disburseAmountLabel = 'Balance Amount';
        else
            $scope.disburseAmountLabel = 'Disburse Amount';

        var settle = parseFloat($scope.medicalClaimRequest.amount) - parseFloat($scope.medicalClaimRequest.totalIouExpences);

        $scope.medicalClaimRequest.settledAmount = settle.toFixed(2);
        $scope.medicalClaimRequest.totalIouExpences = $scope.medicalClaimRequest.totalIouExpences.toFixed(2);
    }

    //Attachments
    function loadAttachments() {
        var defer = $.Deferred();
        medicalClaimService.getMedicalClaimAttachments($scope.id).then(function (res) {
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
            templateUrl: 'medicalClaimAttachment.html',
            controller: 'MedicalClaimFinanceApprovalInstanceCtrl',
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