angular.module('MetronicApp').controller('DashboardController', function ($rootScope, $scope, $http, $timeout, dashboardService) {
    $scope.$on('$viewContentLoaded', function() {   
        // initialize core components
        App.initAjax();
    });  

    dashboardService.get().then(function (res) {
        $scope.openIOUs = res.openIOUs;
        $scope.openPCVs = res.openPCVs;
        $scope.iousToApprove = res.iousToApprove;
        $scope.pcvsToApprove = res.pcvsToApprove;
        $scope.recentPcvs = res.recentPcvs;
        $scope.recentIOUs = res.recentIOUs;
        $scope.openMCs = res.openMCs;
        $scope.mcsToApprove = res.mcsToApprove;
        $scope.recentMCs = res.recentMCs;
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});