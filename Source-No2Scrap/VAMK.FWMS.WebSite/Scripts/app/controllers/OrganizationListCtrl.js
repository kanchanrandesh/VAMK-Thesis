angular.module('MetronicApp').controller('OrganizationListCtrl', function ($rootScope, $scope, $state, $stateParams, organizationService, organizationCategoryService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.searchQuery = {};
    $scope.categoryId = $stateParams.categoryId;
    $scope.selectedCategory;

    (function () {
        loadCategories().then(loadOrganizations);
    }());

    organizationService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    function loadCategories() {
        var defer = $.Deferred();
        organizationCategoryService.getAll().then(function (res) {
            $scope.categories = res;
            defer.resolve();
        });
        return defer;
    };

    function loadOrganizations() {
        $scope.searchQuery.category = $stateParams.categoryId ? $scope.categories.find(x => x.id === $stateParams.categoryId) : null;

        var defer = $.Deferred();
        organizationService.search($scope.searchQuery).then(function (res) {
            $scope.searchList = res;
            defer.resolve();
        });
        return defer;
    };

    $scope.search = function (srch) {
        var search = {};
        search.Code = srch.code;
        search.Name = srch.name;
        search.Type = srch.type;
        if ($scope.selectedCategory) {
            search.CategoryID = $scope.selectedCategory.id
        }
        else
            search.CategoryID = null;

        organizationService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    $scope.export = function () {
        organizationService.exportToExcel().then(function (res) {

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







