angular.module('MetronicApp').controller('PettyCashApprovalListCtrl', function ($rootScope, $scope, $state, pettyCashService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    var employeeId = document.getElementById('HiddenEmployeeID').value;
    $scope.searchQuery = {};
    $scope.searchQuery.ApproverID = employeeId;

    pettyCashService.searchForPettyCashApproval($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        pettyCashService.searchForPettyCashApproval(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});