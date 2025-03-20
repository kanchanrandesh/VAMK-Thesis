angular.module('MetronicApp').controller('PettyCashReimbursementListCtrl', function ($rootScope, $scope, $state, pettyCashReimbursementService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.searchQuery = {};
    $scope.searchQuery.type = 'Recent';

    pettyCashReimbursementService.getCompanyPettyCashReimbursementSummary().then(function (res) {
        $scope.companyPettyCashReimbursementSummary = res;
    });

    pettyCashReimbursementService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        pettyCashReimbursementService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    $scope.printReimbursementDetail = function (id) {
        pettyCashReimbursementService.printReimbursementDetail(id).then(function (res) {
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});