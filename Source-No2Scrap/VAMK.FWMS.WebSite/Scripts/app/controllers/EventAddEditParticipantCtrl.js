angular.module('MetronicApp').controller('EventAddEditParticipantCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, organizationService, industryService, designationCategoryService, countryService, eventService, notificationMsgService, employeeService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.pageTitle = $stateParams.pageTitle;
    $scope.eventId = $stateParams.eventId;
    $scope.eventParticipant = {};
    $scope.selectedOrganization;
    $scope.selectedIndustry;
    $scope.selectedDesignationCategory;
    $scope.selectedCountry;
    $scope.selectedAccountManager;

    (function () {
        loadOrganizations().then(loadIndustries).then(loadDesignationCategories).then(loadCountries).then(loadEmployees).then(loadEventParticipant);
    }());

    function loadOrganizations() {
        var defer = $.Deferred();
        organizationService.getAll().then(function (res) {
            $scope.organizations = res;
            defer.resolve();
        });
        return defer;
    };

    function loadIndustries() {
        var defer = $.Deferred();
        industryService.getAll().then(function (res) {
            $scope.industries = res;
            defer.resolve();
        });
        return defer;
    };

    function loadDesignationCategories() {
        var defer = $.Deferred();
        designationCategoryService.getAll().then(function (res) {
            $scope.designationCategories = res;
            defer.resolve();
        });
        return defer;
    };

    function loadCountries() {
        var defer = $.Deferred();
        countryService.getAll().then(function (res) {
            $scope.countries = res;
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

    function loadEventParticipant() {
        var defer = $.Deferred();
        eventService.getEventParticipant($stateParams.participantId).then(function (res) {
            $scope.eventParticipant = res;
            $scope.selectedOrganization = $scope.organizations.find(x => x.id === res.organizationId)
            $scope.selectedIndustry = $scope.industries.find(x => x.id === res.industryId)
            $scope.selectedDesignationCategory = $scope.designationCategories.find(x => x.id === res.designationCategoryId)
            $scope.selectedCountry = $scope.countries.find(x => x.id === res.countryId)
            $scope.selectedAccountManager = $scope.employees.find(x => x.id === res.accountManagerId)
            defer.resolve();
        });
        return defer;
    };

    $scope.updateOrganizationName = function () {
        $scope.eventParticipant.organizationName = $scope.selectedOrganization.name;
    };

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            if ($scope.selectedOrganization)
                obj.organizationId = $scope.selectedOrganization.id
            else
                obj.organizationId = null

            if ($scope.selectedIndustry)
                obj.industryId = $scope.selectedIndustry.id
            else
                obj.industryId = null

            if ($scope.selectedDesignationCategory)
                obj.designationCategoryId = $scope.selectedDesignationCategory.id
            else
                obj.designationCategoryId = null

            if ($scope.selectedCountry)
                obj.countryId = $scope.selectedCountry.id
            else
                obj.countryId = null

            if ($scope.selectedAccountManager)
                obj.accountManagerId = $scope.selectedAccountManager.id
            else
                obj.accountManagerId = null

            if (obj.isParticipatingKeyNote || obj.isParticipatingTrack || obj.isParticipatingSoc) {
                obj.hasResponded = true;
                obj.IsParticipating = true;
            }
            else {
                obj.IsParticipating = false;
            }

            obj.eventId = $stateParams.eventId;

            eventService.saveEventParticipant(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('eventHome', { eventId: $stateParams.eventId });
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('eventHome', { eventId: $stateParams.eventId });
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});