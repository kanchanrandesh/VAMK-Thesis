angular.module('MetronicApp').controller('UnitListCtrl', function ($rootScope, $scope, $state, unitService, departmentService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    departmentService.getAll().then(function (res) {
        $scope.departments = res;
    });

    unitService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        if (srch.department)
            srch.departmentID = srch.department.id;
        unitService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});