angular.module('MetronicApp').controller('SystemConfigurationAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, systemConfigurationService, notificationMsgService, timeZoneService, employeeService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.pageTitle = $stateParams.pageTitle;
    $scope.selectedTimeZone;
    $scope.selectedMCCheckBy;
    $scope.selectedMCApprovalBy;
    $scope.selectedMCStartMonth;

    (function () {
        loadEmployees().then(loadTimeZones).then(loadSystemConfiguration);
    }());

    function loadTimeZones() {
        var defer = $.Deferred();
        timeZoneService.getAll().then(function (res) {
            $scope.timeZones = res;
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

    function loadSystemConfiguration() {
        var defer = $.Deferred();
        systemConfigurationService.getLatest().then(function (res) {
            $scope.systemConfiguration = res;
            $scope.selectedTimeZone = $scope.timeZones.find(x => x.id === res.timeZoneId);
            $scope.selectedMCCheckBy = $scope.employees.find(y => y.id === res.mCHRCheckedById);
            $scope.selectedMCApprovalBy = $scope.employees.find(z => z.id === res.mCHRApprovedById);

            defer.resolve();
        });
        return defer;
    };

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            if ($scope.selectedTimeZone)
                obj.timeZoneId = $scope.selectedTimeZone.id
            else
                obj.timeZoneId = null

            if ($scope.selectedMCCheckBy) {
                obj.mCHRCheckedById = $scope.selectedMCCheckBy.id
            }
            else
                obj.mCHRCheckedById = null

            if ($scope.selectedMCApprovalBy) {
                obj.mCHRApprovedById = $scope.selectedMCApprovalBy.id
            }
            else
                obj.mCHRApprovedById = null
            systemConfigurationService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('systemConfigurationAddEdit', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('systemConfigurationAddEdit', {});
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});