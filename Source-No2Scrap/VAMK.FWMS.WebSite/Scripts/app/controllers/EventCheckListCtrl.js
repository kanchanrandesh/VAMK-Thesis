angular.module('MetronicApp').controller('EventCheckListCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, notificationMsgService, eventService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.pageTitle = $stateParams.pageTitle;
    $scope.eventId = $stateParams.eventId;
    $scope.eventName = $stateParams.eventName;

    eventService.getEventCheckList($scope.eventId).then(function (res) {
        $scope.checkList = res;
    });

    $scope.updateCheckList = function () {
        eventService.updateEventCheckList($scope.checkList).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Record(s) updated successfully');
                $state.go('eventHome', { eventId: $stateParams.eventId });
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    },

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