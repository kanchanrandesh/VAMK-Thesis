angular.module('MetronicApp').controller('OrganizationAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, organizationService, notificationMsgService, organizationCategoryService, countryService, industryService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.pageTitle = $stateParams.pageTitle;
    $scope.selectedCategory;
    $scope.selectedCountry;
    $scope.selectedParent;
    $scope.selectedIndustry;

    (function () {
        loadCategories().then(loadOrganizations).then(loadCountries).then(loadIndustries).then(loadOrganization);
    }());

    function loadCategories() {
        var defer = $.Deferred();
        organizationCategoryService.getAll().then(function (res) {
            $scope.categories = res;
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
        industryService. getAllForAutocompete().then(function (res) {
            $scope.industries = res;
            defer.resolve();
        });
        return defer;
    };

    function loadOrganization() {
        var defer = $.Deferred();
        organizationService.getById($stateParams.id).then(function (res) {
            $scope.organization = res;
            $scope.selectedCountry = $scope.countries.find(x => x.id === res.countryId)
            $scope.selectedCategory = $scope.categories.find(x => x.id === res.categoryId)
            $scope.selectedParent = $scope.organizations.find(x => x.id === res.parentId)
            //for (var i = 0; i < $scope.organization.industryModelList.length; i++) {
            //    $scope.organization.industryModelList[i] = $scope.industries.find(x => x.id === res.industryModelList[i].id)
            //}
            $scope.selectedIndustry.push({ id: value.IndustryID, label: value.name });
            defer.resolve();
        });
        return defer;
    };

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            if ($scope.selectedCountry)
                obj.countryId = $scope.selectedCountry.id
            else
                obj.countryId = null

            if ($scope.selectedCategory)
                obj.categoryId = $scope.selectedCategory.id
            else
                obj.categoryId = null

            if ($scope.selectedParent)
                obj.parentId = $scope.selectedParent.id
            else
                obj.parentId = null

            // remove empty items in the list
            var i = (obj.associatedCompanyList.length - 1),
                    item;
            for (i; i >= 0; i -= 1) {
                item = obj.associatedCompanyList[i];
                if (item.companyId == null &&
                    item.primaryRepresentativeId == null && item.secondaryRepresentativeId == null) {
                    obj.associatedCompanyList.splice(i, 1);
                }
            }
            organizationService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('organizationList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.delete = function (obj) {
        if (!obj.id)
        {
            notificationMsgService.showErrorMessage("Cannot delete an unsaved record");
            return;
        }

        organizationService.deleteData(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Record deleted successfully');
                $state.go('organizationList', {});
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    $scope.cancel = function () {
        $state.go('organizationList', {});
    }

    $scope.addNewAssociatedCompanyListItem = function () {
        $scope.organization.associatedCompanyList.push({
            "organizationId": null,
            "companyId": null,
            "primaryRepresentativeId": null,
            "secondaryRepresentativeId": null,
        });
    }

    $scope.deleteAssociatedCompanyListItem = function (item) {
        var i = $scope.organization.associatedCompanyList.indexOf(item);
        if (i != -1) {
            $scope.organization.associatedCompanyList.splice(i, 1);
        }
    }

    $scope.$watch('organization.name', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Organization";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Organization";
            }
            else {
                $scope.headerTitle = newValue;
            }
        }
    }, true);

    $scope.$watch('organization.type', function (newValue, oldValue, scope) {
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