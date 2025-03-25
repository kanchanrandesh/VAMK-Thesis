angular.module('MetronicApp').controller('ItemAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, itemService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    itemService.getById($stateParams.id).then(function (res) {
        $scope.item = res;
    });

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            itemService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('itemList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('itemList', {});
    }

    $scope.headerDescription = '';
    $scope.$watch('item.name', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Item";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Item";
            }
            else {
                $scope.headerTitle = newValue;
            }
        }
    }, true);

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});