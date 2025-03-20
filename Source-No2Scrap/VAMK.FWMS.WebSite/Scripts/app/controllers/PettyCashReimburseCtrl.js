angular.module('MetronicApp').controller('PettyCashReimburseCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal,
    $stateParams, $state, pettyCashService, pettyCashReimbursementService, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    //debugger;
    $scope.id = 0;
    $scope.id = parseInt($stateParams.id);
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.reimbursement = $stateParams.reimbursement;
    $scope.isreimbursable = true;
    $scope.actionName = "Reimburse";
    $scope.actionLabel = "Reimburse By";
    $scope.showProcess = false;
    if (parseInt($scope.id) > 0) {
        $scope.showProcess = true;
        pettyCashReimbursementService.getPettyCashReimbursement($scope.id).then(function (res) {
            updatePettyCashReimbursementData(res);
            console.log(res);
        });
    }

    $scope.reimburse = function () {
        pettyCashReimbursementService.reimburse($scope.reimbursement).then(function (res) {
            notificationMsgService.showSuccessMessage('Reimbursed successfully');
            $scope.isreimbursable = false;
        });
    }

    function updatePettyCashReimbursementData(res) {
        var id = $scope.reimbursement.reimbursedById
        $scope.reimbursement = res;
        $scope.reimbursement.reimbursedById = id;
    }


    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});