angular.module('MetronicApp').controller('PettyCashReimbursementProcessCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal,
    $stateParams, $state, pettyCashService, pettyCashReimbursementService, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = 0;
    $scope.id = parseInt($stateParams.id);
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.reimbursement = $stateParams.reimbursement;
    $scope.processById = $stateParams.reimbursement.processById;
    $scope.actionName = "Process";
    $scope.actionLabel = "Process By";
    $scope.showProcess = false;
    if (parseInt($scope.id) > 0) {
        $scope.actionName = "Reprocess";
        $scope.actionLabel = "Processed By";
        $scope.showProcess = true;
        pettyCashReimbursementService.getPettyCashReimbursement($scope.id).then(function (res) {
            updatePettyCashReimbursementData(res);
            console.log(res);
        });
    }

    $scope.processReimbursement = function () {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        if (parseInt($scope.id) > 0) {
            if (confirm('Are you sure you want to Reprocess ?')) {
                pettyCashReimbursementService.reProcessReimbursement($scope.id, employeeId).then(function (res) {
                    updatePettyCashReimbursementData(res);
                    $scope.pettyCashReimbursementItems = res.itemList;
                    notificationMsgService.showSuccessMessage('Petty Cash Reimbursement details reprocessed successfully');
                });
            }

        }
        else {
            pettyCashReimbursementService.processReimbursement($scope.reimbursement.companyId, employeeId).then(function (res) {
                updatePettyCashReimbursementData(res);
                $scope.showProcess = true;
                $scope.actionName = "Reprocess";
                $scope.actionLabel = "Process By";
                $scope.pettyCashReimbursementItems = res.itemList;
                notificationMsgService.showSuccessMessage('Petty Cash Reimbursement details processed successfully');
            });
        }
    }

    $scope.printDetails = function () {
        pettyCashReimbursementService.printReimbursementDetail($scope.id).then(function (res) {

        });
    }

    $scope.printVouchers = function () {
        pettyCashReimbursementService.printReimbursementVouchers($scope.id).then(function (res) {

        });
    }

    function updatePettyCashReimbursementData(res) {
        $scope.id = res.id;
        $scope.reimbursement.companyId = res.companyId;
        $scope.reimbursement.companyName = res.companyName;
        $scope.reimbursement.date = res.preparedDate;
        $scope.reimbursement.processBy = res.preparedByName;
        $scope.reimbursement.floatAmount = res.companyFloat;
        $scope.reimbursement.currentTotal = res.amount;
        $scope.reimbursement.hasReimbursed = res.hasReimbursed;
        $scope.pettyCashReimbursementItems = res.itemList;
        $scope.reimbursement.reimbursementTxHistory = res.reimbursementTxHistory;
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});