angular.module('MetronicApp').controller('EventDetailsCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, eventService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.pageTitle = $stateParams.pageTitle;
    $scope.eventId = $stateParams.eventId;

    eventService.getEventTest($stateParams.eventId).then(function (res) {
        $scope.event = res;
    });

    $scope.cancel = function () {
        $state.go('eventHome', { eventId: $stateParams.eventId });
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});