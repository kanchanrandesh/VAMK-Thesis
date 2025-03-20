angular.module('MetronicApp').controller('EventListController', function ($rootScope, $scope, eventService) {

    $scope.$on('$viewContentLoaded', function () {

        App.initAjax();
        
    });

    eventService.searchEvent($scope.eventSearch).then(function (res) {
        console.log($scope.eventSearch)
        $scope.eventList = res;
        console.log(res);
    });

    $scope.searchEvent = function (eveSear,$event) {
        //Using JQuery inside Angular is Not Recommend
        var keyCode = $event.which || $event.keyCode;
        if (keyCode === 13 || $event.type =='click') {
            if ($("#advanceSearchContainer").is(":visible")) {
                eveSear.advanceSearchedEnabled = true;
            }
            else {
                eveSear.advanceSearchedEnabled = false;
            }
            eventService.searchEvent(eveSear,$event).then(function (res) {
                $scope.eventList = res;
                console.log(res);
            });
            console.log(eveSear);
        }   
    };

    //Date Picker [Start]

    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2100, 12, 31),
        minDate: new Date(),
        startingDay: 1
    };

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







