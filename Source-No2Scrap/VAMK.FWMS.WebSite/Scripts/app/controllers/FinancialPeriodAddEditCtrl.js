angular.module('MetronicApp').controller('FinancialPeriodAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, financialPeriodService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    financialPeriodService.getById($stateParams.id).then(function (res) {
        $scope.financialPeriod = res;
    });

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            financialPeriodService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('financialPeriodList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('financialPeriodList', {});
    }

    $scope.headerDescription = '';
    $scope.$watch('financialPeriod.name', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Financial Period";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Financial Period";
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