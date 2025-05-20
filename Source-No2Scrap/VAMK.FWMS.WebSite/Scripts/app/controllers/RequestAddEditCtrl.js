angular.module('MetronicApp').controller('RequestAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, requestService, itemService, recipientService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    function loadRequest() {
        requestService.getById($stateParams.id).then(function (res) {
            $scope.request = res;
            if (res.date)
                $scope.request.date = new Date(res.date);
            else
                $scope.request.date = null;

            if ($scope.request.recipientId)
                $scope.selectedRecipient = $scope.recipients.find(r => r.id == res.recipientId);
            else
                $scope.selectedRecipient = null

            for (var i = 0; i < res.requestItemList.length; i++) {
                $scope.request.requestItemList[i].selectedItem = $scope.items.find(y => y.id === res.requestItemList[i].itemID)
            }
        });
    }

    (function () {
        loadItems().then(loadIrecipients).then(loadRequest);
    }());

    function loadItems() {
        var defer = $.Deferred();
        itemService.getAllForDropdown().then(function (res) {
            $scope.items = res;
            defer.resolve();
        });
        return defer;
    };

    function loadIrecipients() {
        var defer = $.Deferred();
        recipientService.getAllForDropdownForUser().then(function (res) {
            $scope.recipients = res;
            defer.resolve();
        });
        return defer;
    };

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            if ($scope.selectedRecipient) {
                obj.recipientId = $scope.selectedRecipient.id;
            }
            else
                obj.recipientId = null

            for (var i = 0; i < obj.requestItemList.length; i++) {
                if (obj.requestItemList[i].selectedItem != null) {
                    obj.requestItemList[i].itemId = obj.requestItemList[i].selectedItem.id;
                } else obj.requestItemList[i].itemId = null;
            }

            requestService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully. Request Number  : ' + res.transacionNumber);
                    $state.go('requestList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    //Request Items
    $scope.addNewRequestItem = function () {
        $scope.request.requestItemList.push({
            "id": null,
            "requestId": null,
            "itemId": null,
            "qty": null,
            "timeStamp": null,
            "dateCreated": null
        });

        setTimeout(function () {
            // $('.select2').select2();
            ComponentsSelect2.init(); // init todo page

        }, 500);
    }
    $scope.deleteRequestItems = function (requestItem) {
        var i = $scope.request.requestItemList.indexOf(requestItem);
        if (i != -1) {
            $scope.request.requestItemList.splice(i, 1);
        }
    }

    $scope.cancel = function () {
        $state.go('requestList', {});
    }

    $scope.headerDescription = '';
    $scope.$watch('request.transacionNumber', function (newValue, oldValue, scope) {
        debugger
        if ($stateParams.id == "0" && (newValue == undefined || newValue == "-- AUTO GENERATED --")) {
            $scope.headerTitle = "New Request";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Request";
            }
            else {
                $scope.headerTitle = "Edit Request " + newValue;
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