angular.module('MetronicApp').controller('IOUPaymentAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, iouService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.filterType = $stateParams.type;

    iouService.getForView($stateParams.id).then(function (res) {
        $scope.iouRequest = res;
        $scope.float = res.pettyCashFloat;
        $scope.cashInHand = res.pettyCashBalance;

        if (res.iouStatus == 'HODApproved')
            $scope.headerTitle = 'IOU Payment - Finance Approval';
        else
            $scope.headerTitle = 'IOU Payment';
    });

    iouService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.approve = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        obj.financeApprovedById = employeeId;

        iouService.finApprove(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('IOU approved successfully');
                $state.go('iouPaymentList', { type: $stateParams.type });
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    $scope.approveAndPay = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        obj.financeApprovedById = employeeId;
        obj.paidById = employeeId;

        iouService.finApproveAndPay(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('IOU approved and paid successfully');
                $state.go('iouPaymentList', { type: $stateParams.type });
                iouService.printIOU(res.id).then(function (res) {
                });
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    $scope.pay = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        obj.paidById = employeeId;

        iouService.payIOU(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('IOU paid successfully');
                $state.go('iouPaymentList', { type: $stateParams.type });
                iouService.printIOU(res.id).then(function (res) {
                });
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    $scope.return = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        obj.settledById = employeeId;

        iouService.returnFullAmount(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('IOU full amount returned successfully');
                $state.go('iouPaymentList', { type: $stateParams.type });
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    $scope.reject = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        obj.rejectedById = employeeId;

        iouService.rejectIOU(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('IOU rejected successfully');
                $state.go('iouPaymentList', { type: $stateParams.type });
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});