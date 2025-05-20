angular.module('MetronicApp').controller('ContactAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, contactService, notificationMsgService, organizationService, designationCategoryService, countryService, employeeService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.organizationId = $stateParams.organizationId;
    $scope.accountManagerId = $stateParams.accountManagerId;
    $scope.selectedOrganization;
    $scope.selectedDesignationCategory;
    $scope.selectedCountry;
    $scope.selectedAccountManager;

    (function () {
        loadOrganizations().then(loadDesignationCategories).then(loadCountries).then(loadEmployees).then(loadContact);
    }());

    function loadOrganizations() {
        var defer = $.Deferred();
        organizationService.getAll().then(function (res) {
            $scope.organizations = res;
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

    function loadContact() {
        var defer = $.Deferred();
        contactService.getById($stateParams.id).then(function (res) {
            $scope.contact = res;

            $scope.selectedOrganization = $scope.organizations.find(x => x.id === res.organizationId)
            $scope.selectedDesignationCategory = $scope.designationCategories.find(x => x.id === res.designationCategoryId)
            $scope.selectedCountry = $scope.countries.find(x => x.id === res.countryId)
            $scope.selectedAccountManager = $scope.employees.find(x => x.id === res.accountManagerId)
        });
        return defer;
    };

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            if ($scope.selectedOrganization) {
                obj.organizationId = $scope.selectedOrganization.id
                obj.organization = null;
            }
            else
                obj.organizationId = null

            if ($scope.selectedDesignationCategory) {
                obj.designationCategoryId = $scope.selectedDesignationCategory.id
                obj.designationCategory = null;
            }
            else
                obj.designationCategoryId = null

            if ($scope.selectedCountry) {
                obj.countryId = $scope.selectedCountry.id
                obj.country = null;
            }
            else
                obj.countryId = null

            if ($scope.selectedAccountManager) {
                obj.accountManagerId = $scope.selectedAccountManager.id
                obj.accountManager = null;
            }
            else
                obj.accountManagerId = null

            contactService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('contactList', { accountManagerId: $stateParams.accountManagerId, organizationId: $stateParams.organizationId });
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('contactList', { accountManagerId: $stateParams.accountManagerId, organizationId: $stateParams.organizationId });
    }

    $scope.$watch('contact.firstName', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined && $scope.contact.lastName == undefined) {
            $scope.headerTitle = "New Contact";
        }
        else {
            if (newValue == undefined && $scope.contact.lastName == undefined) {
                $scope.headerTitle = "Edit Contact";
            }
            else {
                if (newValue != undefined && $scope.contact.lastName == undefined) {
                    $scope.headerTitle = newValue;
                }
                else {
                    if (newValue == undefined && $scope.contact.lastName != undefined) {
                        $scope.headerTitle = $scope.contact.lastName;
                    }
                    else {
                        $scope.headerTitle = newValue + ' ' + $scope.contact.lastName;
                    }
                }
            }
        }
    }, true);

    $scope.$watch('contact.lastName', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && $scope.contact.firstName == undefined && newValue == undefined) {
            $scope.headerTitle = "New Contact";
        }
        else {
            if ($scope.contact.firstName == undefined && newValue == undefined) {
                $scope.headerTitle = "Edit Contact";
            }
            else {
                if ($scope.contact.firstName == undefined && newValue != undefined) {
                    $scope.headerTitle = newValue;
                }
                else {
                    if ($scope.contact.firstName != undefined && newValue == undefined) {
                        $scope.headerTitle = $scope.contact.firstName;
                    }
                    else {
                        $scope.headerTitle = $scope.contact.firstName + ' ' + newValue;
                    }
                }
            }
        }
    }, true);

    $scope.$watch('contact.department', function (newValue, oldValue, scope) {
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