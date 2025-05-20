angular.module('MetronicApp').controller('GroupUsersCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, groupService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    (function () {
        loadGroupUsers().then(fillMultiSelect);
    }());

    function loadGroupUsers() {
        var defer = $.Deferred();
        groupService.getForGroupUsers($stateParams.id).then(function (res) {
            $scope.group = res;
            $scope.headerTitle = res.description;
            $scope.headerDescription = res.isActive ? 'Active' : 'Inactive';
            defer.resolve();
        });
        return defer;
    };

    function fillMultiSelect() {
        var defer = $.Deferred();
        $scope.multiOptions = {
            title: '',
            filterPlaceHolder: 'Search Users',
            labelAll: 'Not Assigned',
            labelSelected: 'Assigned',
            helpMessage: '',
            orderProperty: 'name',
            items: $scope.group.notAssignedList,
            selectedItems: $scope.group.assignedList
        };
        defer.resolve();
        return defer;
    };

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            groupService.saveUsers(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('groupList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('groupList', {});
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});