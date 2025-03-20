angular.module('MetronicApp').controller('PettyCashDisbursementListCtrl', function ($rootScope, $stateParams, $scope, $state, pettyCashService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    var employeeId = document.getElementById('HiddenEmployeeID').value;

    $scope.toApproveCount = '0';
    $scope.toDisburseCount = '0';
    $scope.searchQuery = {};
    $scope.searchQuery.type = $stateParams.type ? $stateParams.type : 'ToPay';

    pettyCashService.searchForPettyCashDisbursement($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    pettyCashService.getDisplayCounts().then(function (res) {
        $scope.toApproveCount = res.toApproveCount;
        $scope.toDisburseCount = res.toDisburseCount;
    });

    $scope.search = function (srch) {
        pettyCashService.searchForPettyCashDisbursement(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    $scope.printPettyCash = function (id) {
        pettyCashService.printPettyCash(id).then(function (res) {
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});