angular.module('MetronicApp').controller('RequestIssueCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, requestService, itemService, recipientService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    function loadRequest() {
        debugger;
        requestService.getById($stateParams.id).then(function (res) {
            $scope.request = res;

            $scope.request.date = res.date;


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
        debugger;
        var defer = $.Deferred();
        itemService.getAllForDropdown().then(function (res) {
            $scope.items = res;
            defer.resolve();
        });
        return defer;
    };

    function loadIrecipients() {
        debugger;
        var defer = $.Deferred();
        recipientService.getAllForDropdownForUser().then(function (res) {
            $scope.recipients = res;
            defer.resolve();
        });
        return defer;
    };

    $scope.issue = function (obj, frm) {
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

            requestService.issue(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Foods issue was completed and saved successfully. Request Number  : ' + res.transacionNumber);
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
    }
    $scope.deleteRequestItems = function (requestItem) {
        debugger;
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
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "Issue Request";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Issue Request";
            }
            else {
                $scope.headerTitle = "Issue Request " + newValue;
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

    //Date Picker [End]


    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});