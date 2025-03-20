angular.module('MetronicApp').controller('StrategicBusinessUnitAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, strategicBusinessUnitService, employeeService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.selectedAuthorizedOfficer;

    (function () {
        loadEmployees().then(loadStrategicBusinessUnit);
    }());

    function loadEmployees() {
        var defer = $.Deferred();
        employeeService.getAll().then(function (res) {
            $scope.employees = res;
            defer.resolve();
        });
        return defer;
    };

    function loadStrategicBusinessUnit() {
        var defer = $.Deferred();
        strategicBusinessUnitService.getById($stateParams.id).then(function (res) {
            $scope.strategicBusinessUnit = res;
            $scope.selectedAuthorizedOfficer = $scope.employees.find(x => x.id === res.authorizedOfficerId)
        });
        return defer;
    };

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            if ($scope.selectedAuthorizedOfficer) {
                obj.authorizedOfficerId = $scope.selectedAuthorizedOfficer.id
                obj.authorizedOfficer = null;
            }
            else
                obj.authorizedOfficerId = null

            strategicBusinessUnitService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('strategicBusinessUnitList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('strategicBusinessUnitList', {});
    }

    $scope.$watch('strategicBusinessUnit.code', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Strategic Business Unit";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Strategic Business Unit";
            }
            else {
                $scope.headerTitle = newValue;
            }
        }
    }, true);

    $scope.$watch('strategicBusinessUnit.name', function (newValue, oldValue, scope) {
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