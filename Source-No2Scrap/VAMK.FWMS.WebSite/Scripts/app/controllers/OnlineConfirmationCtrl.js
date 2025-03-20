var app = angular.module('OnlineConfirmationApp', [
    "ui.router",
    "ui.bootstrap",
    "oc.lazyLoad",
    "ngSanitize",
    "ui.bootstrap.tpls",
    "ui.bootstrap.modal",
    "ui.bootstrap.popover",
    "ngNotify"
]);
//debugger;
app.controller('OnlineConfirmationCtrl', function ($q, $scope, $stateParams, onlineConfirmationService) {
    $scope.referenceKey = document.getElementById('HiddenReferenceKey').value;
    $scope.eventParticipant = {};
    $scope.selectedOrganization;
    $scope.selectedIndustry;
    $scope.selectedCountry;

    (function () {
        //loadOrganizations().then(loadIndustries).then(loadCountries).then(loadEventParticipant).then(loadEvent);
        loadEventParticipant().then(loadEvent);
    }());

    function loadOrganizations() {
        var deferred = $q.defer();
        onlineConfirmationService.getAllOrganizations().then(function (res) {
            $scope.organizations = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    function loadIndustries() {
        var deferred = $q.defer();
        onlineConfirmationService.getAllIndustries().then(function (res) {
            $scope.industries = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    function loadCountries() {
        var deferred = $q.defer();
        onlineConfirmationService.getAllCountries().then(function (res) {
            $scope.countries = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    function loadEventParticipant() {
        var deferred = $q.defer();
        onlineConfirmationService.getEventParticipantByReferenceKey($scope.referenceKey).then(function (res) {
            $scope.eventParticipant = res;
            $scope.eventParticipant.isParticipatingKeyNote = true;
            $scope.eventParticipant.isParticipatingTrack = true;
            //$scope.selectedOrganization = $scope.organizations.find(x => x.id === res.organizationId);
            //$scope.selectedIndustry = $scope.industries.find(x => x.id === res.industryId);
            //$scope.selectedCountry = $scope.countries.find(x => x.id === res.countryId);

            $scope.alreadyConfirmed = res.hasResponded;

            if ($scope.eventParticipant.id == null) {
                onlineConfirmationService.showErrorMessage('Invalid Reference Key');
                return;
            }
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    function loadEvent() {
        var deferred = $q.defer();
        onlineConfirmationService.getEvent($scope.eventParticipant.eventId).then(function (res) {
            $scope.event = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    $scope.save = function (obj) {
        //if ($scope.selectedOrganization)
        //    obj.organizationId = $scope.selectedOrganization.id
        //else
        //    obj.organizationId = null

        //if ($scope.selectedIndustry)
        //    obj.industryId = $scope.selectedIndustry.id
        //else
        //    obj.industryId = null

        //if ($scope.selectedCountry)
        //    obj.countryId = $scope.selectedCountry.id
        //else
        //    obj.countryId = null
        //obj.hasResponded = true;

        obj.trackList = [];
        $scope.event.sessionList.forEach(function (session) {
            session.trackSessionList.forEach(function (trackSession) {
                if (trackSession.hasSelected) {
                    var participantTrackSession = {};
                    participantTrackSession.eventParticipantId = obj.id;
                    participantTrackSession.trackId = trackSession.id;
                    obj.trackList.push(participantTrackSession);
                }
            });
        });

        onlineConfirmationService.saveEventParticipant(obj).then(function (res) {
            if (res.status == true) {
                onlineConfirmationService.showSuccessMessage('Your confirmation successfully recorded. Thank you!');
            }
            else
                onlineConfirmationService.showErrorMessage(res.message);
        });
    }

    $scope.selectTrackSession = function (sesTrack) {
        if (sesTrack.hasSelected) {
            for (var i = 0; i < $scope.event.sessionList.find(x => x.id === sesTrack.sessionID).trackSessionList.length; i++) {
                if ($scope.event.sessionList.find(x => x.id === sesTrack.sessionID).trackSessionList[i].id != sesTrack.id)
                    $scope.event.sessionList.find(x => x.id === sesTrack.sessionID).trackSessionList[i].hasSelected = false;
            }
        }
    }
});
