angular.module('MetronicApp').controller('EventHomeCtrl', function ($rootScope, $scope, $state, $stateParams, eventService, organizationService, notificationMsgService, $uibModal, employeeService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    eventService.getEventProxy($stateParams.eventId).then(function (res) {
        $scope.event = res;
    });

    eventService.getEventThumbnail($stateParams.eventId).then(function (res) {
        $scope.eventThumbnail = res;
    });

    //set defaults for advance search
    //$scope.inviteeList = [];
    $scope.searchQuery = {};
    $scope.searchQuery.eventID = $stateParams.eventId;
    $scope.searchQuery.confirmationType = 'All';
    $scope.searchQuery.participationType = 'All';
    $scope.selectedTrack;
    $scope.selectedSession;

    eventService.searchInvitees($scope.searchQuery).then(function (res) {
        $scope.inviteeList = res;
    });

    organizationService.getAll().then(function (res) {
        $scope.organizations = res;
    });

    employeeService.getAll().then(function (res) {
        $scope.employees = res;
    });

    eventService.getEventTracks($stateParams.eventId).then(function (res) {
        $scope.tracks = res;
    });

    $scope.searchInvitees = function (srch) {
        srch.eventID = $stateParams.eventId;
        srch.participatingOnly = null;
        if (srch.confirmationType) {
            if (srch.confirmationType == 'Yes')
                srch.participatingOnly = true;
            if (srch.confirmationType == 'No')
                srch.participatingOnly = false;
        }
        srch.presentOnly = null;
        if (srch.participationType) {
            if (srch.participationType == 'Yes')
                srch.presentOnly = true;
            if (srch.participationType == 'No')
                srch.presentOnly = false;
        }
        if (srch.organization)
            srch.organizationID = srch.organization.id;
        if (srch.accountManager)
            srch.accountManagerID = srch.accountManager.id;
        eventService.searchInvitees(srch).then(function (res) {
            $scope.inviteeList = res;
        });
    };

    $scope.addEditParticipant = function (title, id) {
        $state.go('eventParticipant', { pageTitle: title, participantId: id, eventId: $stateParams.eventId });
    };

    $scope.inviteExistingContact = function (title) {
        $state.go('eventExistingContact', { pageTitle: title, eventId: $stateParams.eventId });
    };

    $scope.eventCheckList = function (title) {
        $state.go('eventCheckList', { pageTitle: title, eventId: $stateParams.eventId, eventName: $scope.event.name });
    };

    $scope.eventDetails = function (title) {
        $state.go('eventDetails', { pageTitle: title, eventId: $stateParams.eventId });
    };

    $scope.sessionParticipation = function () {
        if (!$scope.selectedTrack) {
            notificationMsgService.showErrorMessage('Track needs to be selected');
            return;
        }
        if (!$scope.selectedSession) {
            notificationMsgService.showErrorMessage('Session needs to be selected');
            return;
        }

        $state.go('eventSessionParticipation', { eventId: $stateParams.eventId, trackSessionId: $scope.selectedSession.id, sessionName: $scope.selectedSession.name, trackName: $scope.selectedTrack.name });
    };

    $scope.export = function () {
        //$("#exportlocation").trigger('click');
        eventService.exportToExcel($stateParams.eventId).then(function (res) {
            if (res.status === "Success") {
                notificationMsgService.showSuccessMessage(res.message);
            }
            else {
                notificationMsgService.showErrorMessage(res.message);
            }
        });
    };

    $scope.printAllInvitees = function () {
        eventService.printAllInvitees($stateParams.eventId).then(function (res) {
        });
    };

    $scope.printConfirmedInvitees = function () {
        eventService.printConfirmedInvitees($stateParams.eventId).then(function (res) {
        });
    };

    $scope.printParticipatedInvitees = function () {
        eventService.printParticipatedInvitees($stateParams.eventId).then(function (res) {
        });
    };

    $scope.printEventSummary = function () {
        eventService.printEventSummary($stateParams.eventId).then(function (res) {
        });
    };

    $scope.eventFiles = function (title) {
        $state.go('eventFiles', { pageTitle: title, id: $stateParams.eventId });
    };

    $scope.updateParticipation = function (id) {
        debugger;
        var obj = $scope.inviteeList.find(x => x.id === id);
        eventService.saveEventParticipant(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Record updated successfully');
                eventService.getEventThumbnail($stateParams.eventId).then(function (res) {
                    $scope.eventThumbnail = res;
                });
                var srch = {};
                if ($scope.searchQuery.organization)
                    srch.organization = $scope.searchQuery.organization;
                srch.confirmationType = $scope.searchQuery.confirmationType;
                srch.participationType = $scope.searchQuery.participationType;
                srch.searchText = $scope.searchQuery.searchText;
                $scope.searchInvitees(srch);
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    };

    $scope.fillForTracks = function () {
        if (!$scope.selectedTrack) {
            notificationMsgService.showErrorMessage('Track needs to be selected');
            return;
        }
        eventService.getEventTrackSessions($scope.selectedTrack.id).then(function (res) {
            $scope.trackSessions = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});
