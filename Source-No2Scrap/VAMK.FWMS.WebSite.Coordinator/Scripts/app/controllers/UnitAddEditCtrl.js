angular.module('MetronicApp').controller('UnitAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, unitService, departmentService, employeeService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.selectedDepartment;
    $scope.selectedAuthorizedOfficer;

    (function () {
        debugger;
        loadDepartments().then(loadEmployees).then(loadUnit);
    }());

    function loadDepartments() {
        var defer = $.Deferred();
        departmentService.getAll().then(function (res) {
            $scope.departments = res;
            defer.resolve();
        });
        return defer;
    };

    function loadEmployees() {
        var defer = $.Deferred();
        employeeService.getAll().then(function (res) {
            $scope.employees = res;
            defer.resolve();
        });
        return defer;
    };

    function loadUnit() {
        debugger;
        var defer = $.Deferred();
        unitService.getById($stateParams.id).then(function (res) {
            $scope.unit = res;
            //$scope.code = res.code;
            //$scope.name = res.name;
            defer.resolve();
        });
        return defer;
    };

    $scope.save = function (obj, frm) {
        if (frm.$valid) {

            if ($scope.selectedDepartment) {
                obj.departmentId = $scope.selectedDepartment.id
                obj.authorizedOfficer = null;
            }
            else
                obj.departmentId = null

            if ($scope.selectedAuthorizedOfficer) {
                obj.authorizedOfficerId = $scope.selectedAuthorizedOfficer.id
                obj.authorizedOfficer = null;
            }
            else
                obj.authorizedOfficerId = null

            unitService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('unitList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('unitList', {});
    }

    $scope.$watch('unit.code', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Unit";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Unit";
            }
            else {
                $scope.headerTitle = newValue;
            }
        }
    }, true);

    $scope.$watch('unit.description', function (newValue, oldValue, scope) {
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