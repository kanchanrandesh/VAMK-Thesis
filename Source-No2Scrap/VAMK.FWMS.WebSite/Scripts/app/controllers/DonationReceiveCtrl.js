angular.module('MetronicApp').controller('DonationReceiveCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, donationService, itemService, donerService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    $scope.openDatePickerDate = function () {
        $scope.popupDate.opened = true;
    };
    $scope.popupDate = {
        opened: false
    };
    function loadDonation() {
        donationService.getById($stateParams.id).then(function (res) {
            debugger;
            $scope.donation = res;
            //if (res.date)
            //    $scope.donation.date = new Date(res.date);
            //else
            //    $scope.donation.date = null;
            $scope.selectedDoner = $scope.doners.find(x => x.id == res.donerID);
            for (var i = 0; i < res.donationItemList.length; i++) {
                $scope.donation.donationItemList[i].selectedItem = $scope.items.find(y => y.id === res.donationItemList[i].itemId)
            }
        });
    }

    (function () {

        loadDoners().then(loadItems).then(loadDonation);
    }());

    function loadDoners() {

        var defer = $.Deferred();
        donerService.getAllForDropdownForUser().then(function (res) {
            $scope.doners = res;
            defer.resolve();
        });
        return defer;
    };

    function loadItems() {
        var defer = $.Deferred();
        itemService.getAllForDropdown().then(function (res) {
            $scope.items = res;
            defer.resolve();
        });
        return defer;
    };

    $scope.receive = function (obj, frm) {
        if (frm.$valid) {
            debugger;
            if ($scope.doners) {
                obj.donerID = $scope.selectedDoner.id;
                obj.doner = null;
            }
            else
                obj.donerID = null

            for (var i = 0; i < obj.donationItemList.length; i++) {
                if (obj.donationItemList[i].selectedItem != null) {
                    obj.donationItemList[i].itemId = obj.donationItemList[i].selectedItem.id;
                } else obj.donationItemList[i].itemId = null;
            }

            donationService.receive(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Donation receive successfully');
                    $state.go('donationList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('donationList', {});
    }

    $scope.headerDescription = '';
    $scope.$watch('donation.transacionNumber', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "Confirm Collection : ";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Confirm Collection : ";
            }
            else {
                $scope.headerTitle = "Confirm Collection : " + newValue;
            }
        }
    }, true);


    //Date Picker [Start]
    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2100, 12, 31),
        minDate: new Date(2016, 01, 01),
        startingDay: 1,
        format: 'dd-MMMM-yyyy'
    };
    $scope.format = 'dd-MMMM-yyyy';

    $scope.openDatePickerDate = function () {
        $scope.popupDate.opened = true;
    };
    $scope.popupDate = {
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