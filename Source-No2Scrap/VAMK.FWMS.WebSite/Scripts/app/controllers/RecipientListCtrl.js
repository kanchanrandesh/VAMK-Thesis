angular.module('MetronicApp').controller('RecipientListCtrl', function ($rootScope, $scope, $state, recipientService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    recipientService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        recipientService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});