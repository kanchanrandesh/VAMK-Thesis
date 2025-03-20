/// <reference path="jobcategoryaddeditctrl.js" />
angular.module('MetronicApp').controller('EmployeeFamilyMemberAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, employeeService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.employeeId = $stateParams.employeeId;
    $scope.pageTitle = $stateParams.pageTitle;

    (function () {
        loadEmployee().then(loadEmployeeFamilyMembers);
    }());

    function loadEmployee() {
        var defer = $.Deferred();
        employeeService.getById($stateParams.employeeId).then(function (res) {
            $scope.employee = res;
            defer.resolve();
        });
        return defer;
    };

    function loadEmployeeFamilyMembers() {
        var defer = $.Deferred();
        employeeService.getFamilyMembers($stateParams.employeeId).then(function (res) {
            $scope.employeesFamilyMembers = res;
            defer.resolve();
        });
        return defer;
    };

    $scope.addNewListItem = function () {
        $scope.employeesFamilyMembers.push({
            "medicalClaimId": null,
            "name": null,
            "relationship": null,
            "eligibleForMedicalClaims": null,
        });
    }

    $scope.deleteListItem = function (item) {
        var i = $scope.employeesFamilyMembers.indexOf(item);
        if (i != -1) {
            $scope.employeesFamilyMembers.splice(i, 1);
        }
    }

    $scope.save = function (familyMembers, frm) {
        if (frm.$valid) {
            for (var i = 0; i < familyMembers.length; i++) {
                familyMembers[i].employeeId = $scope.employeeId;
            }
            employeeService.saveEmployeeFamilyMembers(familyMembers).then(function (res) {
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

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});