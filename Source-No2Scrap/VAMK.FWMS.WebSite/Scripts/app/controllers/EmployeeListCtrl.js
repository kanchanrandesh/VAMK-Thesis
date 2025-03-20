angular.module('MetronicApp').controller('EmployeeListCtrl', function ($rootScope, $scope, $state, employeeService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    employeeService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        employeeService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    $scope.updateLockedEmployee = function (id) {
        var obj = $scope.searchList.find(x => x.id === id);
        employeeService.saveLockedEmployees(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Record updated successfully');
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});