angular.module('MetronicApp').controller('ProjectAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, projectService, departmentService, employeeService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.selectedDepartment;
    $scope.selectedAuthorizedOfficer;

    (function () {
        loadDepartments().then(loadEmployees).then(loadProject);
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

    function loadProject() {
        var defer = $.Deferred();
        projectService.getById($stateParams.id).then(function (res) {
            $scope.project = res;
            $scope.selectedDepartment = $scope.departments.find(x => x.id === res.departmentId)
            $scope.selectedAuthorizedOfficer = $scope.employees.find(x => x.id === res.authorizedOfficerId)
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

            projectService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('projectList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('projectList', {});
    }

    $scope.$watch('project.code', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Project";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Project";
            }
            else {
                $scope.headerTitle = newValue;
            }
        }
    }, true);

    $scope.$watch('project.description', function (newValue, oldValue, scope) {
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