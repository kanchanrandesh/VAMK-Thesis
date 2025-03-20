angular.module('MetronicApp').controller('OrganizationCategoryAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, organizationCategoryService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    organizationCategoryService.getById($stateParams.id).then(function (res) {
        $scope.organizationCategory = res;
    });

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            organizationCategoryService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('organizationCategoryList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('organizationCategoryList', {});
    }

    $scope.headerDescription = '';
    $scope.$watch('organizationCategory.name', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Organization Category";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Organization Category";
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