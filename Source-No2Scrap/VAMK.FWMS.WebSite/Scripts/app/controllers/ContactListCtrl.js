angular.module('MetronicApp').controller('ContactListCtrl', function ($rootScope, $scope, $state, $stateParams, contactService, industryService,
    designationCategoryService, organizationService, employeeService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.searchQuery = {};    
    $scope.searchQuery.designationCategories = [];
    $scope.organizationId = $stateParams.organizationId;
    $scope.accountManagerId = $stateParams.accountManagerId;
    $scope.industryId = $stateParams.industryId;


    (function () {
        loadOrganizations().then(loadEmployees).then(loadDesignationCategories).then(loadIndustries).then(loadContacts);
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
    }

    function loadEmployees() {
        var defer = $.Deferred();
        employeeService.getAll().then(function (res) {
            $scope.employees = res;
            defer.resolve();
        });
        return defer;
    };

    function loadDesignationCategories() {
        var defer = $.Deferred();
        designationCategoryService.getAll().then(function (res) {
            $scope.designationCategories = res;
            $scope.srchDesignationCategories = res;
            $scope.searchQuery = {};
            $scope.searchQuery.designationCategories = new Array();
            for (var i = 0; i < res.length; i++) {
                $scope.searchQuery.designationCategories.push(
                    {
                        id: res[i].id,
                        description: res[i].description,
                        selected: false
                    }
                );
            }
            defer.resolve();
        });
        return defer;
    };

    function loadContacts() {
        $scope.searchQuery.organization = $stateParams.organizationId ? $scope.organizations.find(x => x.id === $stateParams.organizationId) : null;
        $scope.searchQuery.accountManager = $stateParams.accountManagerId ? $scope.employees.find(x => x.id === $stateParams.accountManagerId) : null;
        $scope.searchQuery.industry = $stateParams.industryrId ? $scope.industries.find(x => x.id === $stateParams.industryrId) : null;

        var defer = $.Deferred();
        contactService.search($scope.searchQuery).then(function (res) {
            $scope.searchList = res;
            defer.resolve();
        });
        return defer;
    };

    $scope.search = function (srch) {
        if (srch.organization)
            $scope.organizationId = srch.organization.id;
        else
            $scope.organizationId = null;
        if (srch.accountManager)
            $scope.accountManagerId = srch.accountManager.id;
        else
            $scope.accountManagerId = null;
        if (srch.jobRole) 
            $scope.jobRole = srch.jobRole;
        else
            $scope.jobRole = null;
        if (srch.industry)
            $scope.industryId = srch.industry.id;
        else
            $scope.industryId = null;

        contactService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    $scope.export = function () {
        contactService.exportToExcel().then(function (res) {
            if (res.status === "Success") {
                notificationMsgService.showSuccessMessage(res.message);
            }
            else {
                notificationMsgService.showErrorMessage(res.message);
            }
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});