angular.module('MetronicApp').controller('ItemListCtrl', function ($rootScope, $scope, $state, itemService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    itemService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        itemService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});