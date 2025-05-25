angular.module('MetronicApp').controller('DashboardController', function ($rootScope, $scope, $http, $timeout, dashboardService) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        App.initAjax();
    });

    dashboardService.get().then(function (res) {
        $scope.openDonations = res.openDonations;
        $scope.totalDonationsPostedToday = res.totalDonationsPostedToday;
        $scope.donationsCollectedToday = res.donationsCollectedToday;
        $scope.totalRequestPostedToday = res.totalRequestPostedToday;
        $scope.openRequests = res.openRequests;
        $scope.requestsTobeIssued = res.requestsTobeIssued;
        $scope.recentDonations = res.recentDonations;
        $scope.recentRequests = res.recentRequests;
        $scope.requestsCompletedToday = res.requestsCompletedToday;        
        
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});