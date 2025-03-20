angular.module('MetronicApp').controller('ExpenseCategoryAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, expenseCategoryService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    expenseCategoryService.getById($stateParams.id).then(function (res) {
        $scope.expenseCategory = res;
    });

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            expenseCategoryService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('expenseCategoryList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('expenseCategoryList', {});
    }

    $scope.$watch('expenseCategory.code', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Expense Category";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Expense Category";
            }
            else {
                $scope.headerTitle = newValue;
            }
        }
    }, true);

    $scope.$watch('expenseCategory.description', function (newValue, oldValue, scope) {
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