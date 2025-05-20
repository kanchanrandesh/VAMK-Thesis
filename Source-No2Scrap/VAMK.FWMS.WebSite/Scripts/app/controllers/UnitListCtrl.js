angular.module('MetronicApp').controller('UnitListCtrl', function ($rootScope, $scope, $state, unitService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });


    unitService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });


    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});