angular.module('MetronicApp').controller('DepartmentAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, departmentService, employeeService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.selectedAuthorizedOfficer;

    (function () {
        loadEmployees().then(loadDepartment);
    }());

    function loadEmployees() {
        var defer = $.Deferred();
        employeeService.getAll().then(function (res) {
            $scope.employees = res;
            defer.resolve();
        });
        return defer;
    };

    function loadDepartment() {
        var defer = $.Deferred();
        departmentService.getById($stateParams.id).then(function (res) {
            $scope.department = res;
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

            departmentService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('departmentList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('departmentList', {});
    }

    $scope.$watch('department.code', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Department";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Department";
            }
            else {
                $scope.headerTitle = newValue;
            }
        }
    }, true);

    $scope.$watch('department.description', function (newValue, oldValue, scope) {
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