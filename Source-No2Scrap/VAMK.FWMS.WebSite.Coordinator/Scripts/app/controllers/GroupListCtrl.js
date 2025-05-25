angular.module('MetronicApp').controller('GroupListCtrl', function ($rootScope, $scope, $state, groupService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    groupService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        srch.isActive = null;
        if (srch.status == "Active")
            srch.isActive = true;
        else if (srch.status == "Inactive")
            srch.isActive = false;

        groupService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});