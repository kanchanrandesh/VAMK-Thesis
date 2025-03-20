angular.module('MetronicApp').controller('EmployeeAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, employeeService, companyService, jobCategoryService,notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.selectedCompany;
    $scope.selectedJobCategory;
    (function () {
        loadCompanies().then(loadJobCategories).then(loadEmployee);
    }());
    

    function loadCompanies() {
        var defer = $.Deferred();
        companyService.getAll().then(function (res) {
            $scope.companies = res;
            defer.resolve();
        });
        return defer;
    };

    function loadJobCategories() {
        var defer = $.Deferred();
        jobCategoryService.getAll().then(function (res) {
            $scope.jobCategories = res;

            defer.resolve();
        });
        return defer;
    };

    function loadEmployee() {
        var defer = $.Deferred();
        employeeService.getById($stateParams.id).then(function (res) {
            $scope.employee = res;
            $scope.selectedCompany = $scope.companies.find(x => x.id === res.companyId);
            $scope.selectedJobCategory = $scope.jobCategories.find(x => x.id === res.jobCategoryId);

            $scope.multiPrjectOptions = {
                title: '',
                filterPlaceHolder: 'Search Projects',
                labelAll: 'Not Assigned',
                labelSelected: 'Assigned',
                helpMessage: '',
                orderProperty: 'name',
                items: $scope.employee.notAssignedProjectList,
                selectedItems: $scope.employee.assignedProjectList
            };

            $scope.multiUnitOptions = {
                title: '',
                filterPlaceHolder: 'Search Units',
                labelAll: 'Not Assigned',
                labelSelected: 'Assigned',
                helpMessage: '',
                orderProperty: 'name',
                items: $scope.employee.notAssignedUnitList,
                selectedItems: $scope.employee.assignedUnitList
            };

            defer.resolve();
        });
        return defer;
    };


    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            if ($scope.selectedCompany)
                obj.companyId = $scope.selectedCompany.id
            else
                obj.companyId = null
            if ($scope.selectedJobCategory)
                obj.jobCategoryId = $scope.selectedJobCategory.id
            else
                obj.jobCategoryId = null

            employeeService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('employeeList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('employeeList', {});
    }

    $scope.$watch('employee.firstName', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined && $scope.employee.lastName == undefined) {
            $scope.headerTitle = "New Employee";
        }
        else {
            if (newValue == undefined && $scope.employee.lastName == undefined) {
                $scope.headerTitle = "Edit Employee";
            }
            else {
                if (newValue != undefined && $scope.employee.lastName == undefined) {
                    $scope.headerTitle = newValue;
                }
                else {
                    if (newValue == undefined && $scope.employee.lastName != undefined) {
                        $scope.headerTitle = $scope.employee.lastName;
                    }
                    else {
                        $scope.headerTitle = newValue + ' ' + $scope.employee.lastName;
                    }
                }
            }
        }
    }, true);

    $scope.$watch('employee.lastName', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && $scope.employee.firstName == undefined && newValue == undefined) {
            $scope.headerTitle = "New Employee";
        }
        else {
            if ($scope.employee.firstName == undefined && newValue == undefined) {
                $scope.headerTitle = "Edit Employee";
            }
            else {
                if ($scope.employee.firstName == undefined && newValue != undefined) {
                    $scope.headerTitle = newValue;
                }
                else {
                    if ($scope.employee.firstName != undefined && newValue == undefined) {
                        $scope.headerTitle = $scope.employee.firstName;
                    }
                    else {
                        $scope.headerTitle = $scope.employee.firstName + ' ' + newValue;
                    }
                }
            }
        }
    }, true);

    $scope.$watch('employee.code', function (newValue, oldValue, scope) {
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