angular.module('MetronicApp').controller('EventSessionParticipationCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, organizationService, employeeService, eventService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.eventId = $stateParams.eventId;
    $scope.trackSessionId = $stateParams.trackSessionId;
    $scope.trackName = $stateParams.trackName;
    $scope.sessionName = $stateParams.sessionName;

    $scope.searchQuery = {};
    $scope.searchQuery.EventID = $scope.eventId;
    $scope.searchQuery.TrackSessionID = $scope.trackSessionId;
    $scope.selectedOrganization;
    $scope.selectedAccountManager;
    $scope.participantCount = 0;

    (function () {
        loadOrganizations().then(loadEmployees).then(loadParticipants).then(getCount);
    }());

    function loadOrganizations() {
        var defer = $.Deferred();
        organizationService.getAll().then(function (res) {
            $scope.organizations = res;
            defer.resolve();
        });
        return defer;
    };

    function loadEmployees() {
        var defer = $.Deferred();
        employeeService.getAll().then(function (res) {
            $scope.employees = res;
            defer.resolve();
        });
        return defer;
    };

    function loadParticipants() {
        var defer = $.Deferred();
        eventService.searchTrackParticipants($scope.searchQuery).then(function (res) {
            $scope.participationList = res;
            defer.resolve();
        });
        return defer;
    };

    function getCount() {
        var defer = $.Deferred();
        eventService.getTrackSessionParticipatedCount($stateParams.trackSessionId).then(function (res) {
            $scope.participantCount = res;
            defer.resolve();
        });
        return defer;
    };

    $scope.searchParticipants = function (srch) {
        srch.EventID = $stateParams.eventId;
        srch.TrackSessionID = $stateParams.trackSessionId;
        if ($scope.selectedOrganization)
            srch.OrganizationID = $scope.selectedOrganization.id;
        if ($scope.selectedAccountManager)
            srch.AccountManagerID = $scope.selectedAccountManager.id;
        eventService.searchTrackParticipants(srch).then(function (res) {
            $scope.participationList = res;
        });
    };

    $scope.saveParticipation = function (id) {
        var obj = $scope.participationList.find(x => x.eventParticipantId === id);

        var defer = $.Deferred();
        eventService.saveTrackSessionParticipant(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Record updated successfully');
                eventService.getTrackSessionParticipatedCount($stateParams.trackSessionId).then(function (res) {
                    $scope.participantCount = res;
                });
                var srch = {};
                srch.searchText = $scope.searchQuery.searchText;
                $scope.searchParticipants(srch);
                getCount();
                defer.resolve();
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });

        return defer;
    };

    $scope.fillFromPrevious = function () {
        var confirmation = confirm("Are you sure you want to proceed ?");

        if (confirmation == true) {
            var defer = $.Deferred();
            eventService.fillFromPreviousSession($stateParams.trackSessionId).then(function (res) {
                if (res.status == true) {
                    var srch = {};
                    $scope.searchParticipants(srch);
                    getCount();
                    notificationMsgService.showSuccessMessage('Successfully Processed');
                    defer.resolve();
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
            return defer;
        }
    };

    $scope.cancel = function () {
        $state.go('eventHome', { eventId: $stateParams.eventId });
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});