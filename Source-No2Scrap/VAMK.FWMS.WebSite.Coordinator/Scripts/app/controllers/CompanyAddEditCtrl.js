angular.module('MetronicApp').controller('CompanyAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, companyService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    companyService.getById($stateParams.id).then(function (res) {
        $scope.company = res;
    });

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            companyService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('companyList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('companyList', {});
    }

    $scope.headerDescription = '';
    $scope.$watch('company.name', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Company";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Company";
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