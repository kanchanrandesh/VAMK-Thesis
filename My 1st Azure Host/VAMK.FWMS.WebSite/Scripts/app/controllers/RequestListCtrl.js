angular.module('MetronicApp').controller('RequestListCtrl', function ($rootScope, $scope, $state, requestService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        debugger;
        App.initAjax();
    });

    $scope.openDatePickerDate = function () {
        debugger;
        $scope.popupDate.opened = true;
    };
    $scope.popupDate = {
        opened: false
    };

    requestService.search($scope.searchQuery).then(function (res) {
        debugger;
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        debugger;
        requestService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };
    debugger;

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});