angular.module('MetronicApp').controller('EventAddEditsCtrl', function ($rootScope, $scope, $http, $timeout,
    $document, $uibModal, $stateParams, $state, notificationMsgService, eventService) {

    $scope.$on('$viewContentLoaded', function () {

        App.initAjax();

    });
    //Getting State Parameters
    $scope.title = $stateParams.title;
    $scope.id = $stateParams.id;
    //Set Page Titles
    $scope.$watch('event.name', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.msgEventName = "New Event";
        }
        else {
            if (newValue == undefined) {
                $scope.msgEventName = "Edit Event";
            }
            else {
                $scope.msgEventName = newValue;
            }

        }
    }, true);

    $scope.$watch('event.description', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.msgEventDescription = "";
        }
        else {
            if (newValue == undefined) {
                $scope.msgEventDescription = "";
            }
            else {
                $scope.msgEventDescription = newValue;
            }
        }
    }, true);


    //Retrieve Data
    eventService.getEventTest($scope.id).then(function (res) {
        console.log('res');
        console.log(res);
        $scope.event = res;
        $scope.event.timeFrom = new Date(res.timeFrom);
        $scope.event.timeTo = new Date(res.timeTo);
        $scope.event.date = new Date(res.date);
        //$scope.event.timeTo = new Date();
        //$scope.event.date = new Date();
    });

    //Events
    $scope.addNewCheckListItem = function () {
        $scope.event.checkList.push(
            {
                "event": null,
                "eventID": null,
                "task": null,
                "dueDate": null,
                "assignee": null,
                "assigneeID": null,
                "notes": null,
                "id": null,
                "timeStamp": null,
                "dateCreated": null
            }
            );
    }
    $scope.deleteCheckListItem = function (item) {
        var i = $scope.event.checkList.indexOf(item);
        if (i != -1) {
            $scope.event.checkList.splice(i, 1);
        }
    }
    //Sponsor
    $scope.addNewSponsor = function () {
        $scope.event.sponsorList.push({
            "event": null,
            "eventID": null,
            "name": null,
            "category": null,
            "logoURL": null,
            "contactPerson": null,
            "phone": null, "id": null,
            "timeStamp": null,
            "dateCreated": null
        });
    }
    $scope.deleteSponsor = function (spon) {
        var i = $scope.event.sponsorList.indexOf(spon);
        if (i != -1) {
            $scope.event.sponsorList.splice(i, 1);
        }
    }
    //Vendor
    $scope.addNewVendor = function () {
        $scope.event.vendorInformationList.push({
            "event": null,
            "eventID": null,
            "name": null,
            "description": null,
            "logoURL": null,
            "website": null,
            "id": null,
            "timeStamp": null,
            "dateCreated": null
        });
    }
    $scope.deleteVendor = function (ven) {
        var i = $scope.event.vendorInformationList.indexOf(ven);
        if (i != -1) {
            $scope.event.vendorInformationList.splice(i, 1);
        }
    }
    //Session
    $scope.addSession = function () {
        var noOfTrack = $scope.event.trackList.length;
        var session = {
            "event": null,
            "eventID": null,
            "timeFrom": null,
            "timeTo": null,
            "trackSessionList": [],
            "id": null,
            "timeStamp": null,
            "dateCreated": null
        }
        for (var i = 0; i < noOfTrack; i++) {
            session.trackSessionList.push({
                "session": null,
                "sessionID": null,
                "track": null,
                "trackID": null,
                "title": null,
                "description": null,
                "speaker": null,
                "speakerID": null,
                "coordinator1": null,
                "coordinator1ID": null,
                "coordinator2": null,
                "coordinator2ID": null,
                "id": null,
                "timeStamp": null,
                "dateCreated": null
            });
        }
        $scope.event.sessionList.push(session);
    }
    $scope.deleteSession = function (ses) {
        if ($scope.event.sessionList.length <= 1) {
            notificationMsgService.showErrorMessage('Event should have at leaset one Session');
        }
        else {
            var i = $scope.event.sessionList.indexOf(ses);
            if (i != -1) {
                $scope.event.sessionList.splice(i, 1);
            }
        }
    }
    //Track
    $scope.addTrack = function () {
        //Add Tracks
        $scope.event.trackList.push({
            "event": null,
            "eventID": null,
            "name": null,
            "description": null,
            "id": null,
            "timeStamp": null,
            "dateCreated": null
        });
        //Add Session Tracks To Session
        $scope.event.sessionList.forEach(function (curr, index, arr) {
            curr.trackSessionList.push({
                "session": null,
                "sessionID": null,
                "track": null,
                "trackID": null,
                "title": null,
                "description": null,
                "speaker": null,
                "speakerID": null,
                "coordinator1": null,
                "coordinator1ID": null,
                "coordinator2": null,
                "coordinator2ID": null,
                "id": null,
                "timeStamp": null,
                "dateCreated": null
            });
        });
    }
    $scope.deleteTrack = function (tra) {
        
        if ($scope.event.trackList.length <= 1) {
            notificationMsgService.showErrorMessage('Event should have at leaset one Track');
        }
        else {
            //remove track
            var i = $scope.event.trackList.indexOf(tra);
            if (i != -1) {
                $scope.event.trackList.splice(i, 1);
            }
            //remove track from session
            $scope.event.sessionList.forEach(function (curr, index, arr) {
                curr.trackSessionList.splice(i, 1);
            });
        }        
    }

    //Save Session and Tracks
    $scope.saveEventAddEdit = function (eve, form) {
        if (form.$valid) {
            eventService.saveEventAddEdit(eve).then(function (res) {
                if (res.statusInfo.status == 0) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $timeout(function () {
                        $state.go("eventList");
                    }, 10);
                    
                }
                else {
                    notificationMsgService.showErrorMessage('Record could not be saved, Please verify input fields');
                }
            });
        }
    }

    //Date Picker [Start]

    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2100, 12, 31),
        minDate: new Date(),
        startingDay: 1,
        format: 'dd-MMMM-yyyy'
    };
    $scope.format = 'dd-MMMM-yyyy';

    $scope.openDatePicker = function () {
        $scope.popup2.opened = true;
    };

    $scope.popup2 = {
        opened: false
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 1);
    $scope.dateEvents = [
      {
          date: tomorrow,
          status: 'full'
      },
      {
          date: afterTomorrow,
          status: 'partially'
      }
    ];

    function getDayClass(data) {
        var date = data.date,
          mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.dateEvents[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.dateEvents[i].status;
                }
            }
        }

        return '';
    }

    //Date Picker [End]

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});