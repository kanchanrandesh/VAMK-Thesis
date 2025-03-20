angular.module('MetronicApp').controller('IOURequestViewCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, iouService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    iouService.getForView($stateParams.id).then(function (res) {
        $scope.iouRequest = res;
    });

    iouService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.cancel = function () {
        $state.go('iouRequestList', {});
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});