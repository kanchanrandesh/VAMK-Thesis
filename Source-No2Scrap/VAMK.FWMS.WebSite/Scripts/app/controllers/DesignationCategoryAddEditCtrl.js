angular.module('MetronicApp').controller('DesignationCategoryAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, designationCategoryService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.pageTitle = $stateParams.pageTitle;

    designationCategoryService.getById($stateParams.id).then(function (res) {
        $scope.designationCategory = res;
    });

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            designationCategoryService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('designationCategoryList', { });
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('designationCategoryList', {});
    }

    $scope.$watch('designationCategory.code', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Designation Category";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Designation Category";
            }
            else {
                $scope.headerTitle = newValue;
            }
        }
    }, true);

    $scope.$watch('designationCategory.description', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerDescription = "";
        }
        else {
            if (newValue == undefined) {
                $scope.headerDescription = "";
            }
            else {
                $scope.headerDescription = newValue;
            }
        }
    }, true);

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});