angular.module('MetronicApp').controller('DepartmentListCtrl', function ($rootScope, $scope, $state, departmentService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    departmentService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        departmentService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});