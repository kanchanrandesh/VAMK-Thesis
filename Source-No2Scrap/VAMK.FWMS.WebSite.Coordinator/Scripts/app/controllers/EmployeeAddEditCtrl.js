﻿angular.module('MetronicApp').controller('EmployeeAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state,
    employeeService,
    companyService,
    recipientService,
    donerService,
    notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.selectedCompany;
    (function () {
        loadRecipiants().then(loadDoners).then(loadEmployee);
    }());

    function loadCompanies() {
        var defer = $.Deferred();
        companyService.getAll().then(function (res) {
            $scope.companies = res;
            defer.resolve();
        });
        return defer;
    };

    function loadRecipiants() {
        var defer = $.Deferred();
        recipientService.getAllForDropdown().then(function (res) {
            $scope.recipients = res;
            defer.resolve();
        });
        return defer;
    };
    function loadDoners() {
        var defer = $.Deferred();
        donerService.getAllForDropdown().then(function (res) {
            $scope.doners = res;
            defer.resolve();
        });
        return defer;
    };
    function loadEmployee() {
            debugger;
        var defer = $.Deferred();
        employeeService.getById($stateParams.id).then(function (res) {
            $scope.employee = res;

            //$scope.selectedCompany = $scope.companies.find(x => x.id === res.companyId);

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

            for (var i = 0; i < res.employeeDoners.length; i++) {
                $scope.employee.employeeDoners[i].selectedDoner = $scope.doners.find(y => y.id === res.employeeDoners[i].donerId)
            }

            for (var i = 0; i < res.employeeRecipients.length; i++) {
                $scope.employee.employeeRecipients[i].selectedRecipient = $scope.recipients.find(y => y.id === res.employeeRecipients[i].recipientId)
            }
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

            for (var i = 0; i < obj.employeeDoners.length; i++) {
                $scope.employee.employeeDoners[i].donerId = $scope.employee.employeeDoners[i].selectedDoner.id;
            }

            for (var i = 0; i < obj.employeeRecipients.length; i++) {
                $scope.employee.employeeRecipients[i].recipientId = $scope.employee.employeeRecipients[i].selectedRecipient.id;
            }

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

    $scope.addNewEmployeeDoner = function () {
        $scope.employee.employeeDoners.push({
            "id": null,
            "employeeId": null,
            "donerId": null,
        });
    }
    $scope.deleteEmployeeDoners = function (item) {

        var i = $scope.employee.employeeDoners.indexOf(item);
        if (i != -1) {
            $scope.employee.employeeDoners.splice(i, 1);
        }
    }

    $scope.addNewEmployeeRecipient = function () {
        $scope.employee.employeeRecipients.push({
            "id": null,
            "employeeId": null,
            "recipientId": null,
        });
    }
    $scope.deleteEmployeeRecipient = function (item) {

        var i = $scope.employee.employeeRecipients.indexOf(item);
        if (i != -1) {
            $scope.employee.employeeRecipients.splice(i, 1);
        }
    }

    //$scope.$watch('employee.firstName', function (newValue, oldValue, scope) {
    //    if ($stateParams.id == "0" && newValue == undefined && $scope.employee.lastName == undefined) {
    //        $scope.headerTitle = "New System User";
    //    }
    //    else {
    //        if (newValue == undefined && $scope.employee.lastName == undefined) {
    //            $scope.headerTitle = "Edit System User";
    //        }
    //        else {
    //            if (newValue != undefined && $scope.employee.lastName == undefined) {
    //                $scope.headerTitle = newValue;
    //            }
    //            else {
    //                if (newValue == undefined && $scope.employee.lastName != undefined) {
    //                    $scope.headerTitle = $scope.employee.lastName;
    //                }
    //                else {
    //                    $scope.headerTitle = newValue + ' ' + $scope.employee.lastName;
    //                }
    //            }
    //        }
    //    }
    //}, true);

    //$scope.$watch('employee.lastName', function (newValue, oldValue, scope) {
    //    if ($stateParams.id == "0" && $scope.employee.firstName == undefined && newValue == undefined) {
    //        $scope.headerTitle = "New System User";
    //    }
    //    else {
    //        if ($scope.employee.firstName == undefined && newValue == undefined) {
    //            $scope.headerTitle = "Edit System User";
    //        }
    //        else {
    //            if ($scope.employee.firstName == undefined && newValue != undefined) {
    //                $scope.headerTitle = newValue;
    //            }
    //            else {
    //                if ($scope.employee.firstName != undefined && newValue == undefined) {
    //                    $scope.headerTitle = $scope.employee.firstName;
    //                }
    //                else {
    //                    $scope.headerTitle = $scope.employee.firstName + ' ' + newValue;
    //                }
    //            }
    //        }
    //    }
    //}, true);

    //$scope.$watch('employee.code', function (newValue, oldValue, scope) {
    //    if ($stateParams.id == "0" && newValue == undefined) {
    //        $scope.headerDescription = "";
    //    }
    //    else {
    //        if (newValue == undefined) {
    //            $scope.headerDescription = "";
    //        }
    //        else {
    //            $scope.headerDescription = newValue;
    //        }
    //    }
    //}, true);

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});