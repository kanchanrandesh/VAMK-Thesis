angular.module('MetronicApp').controller('DesignationCategoryListCtrl', function ($rootScope, $scope, $state, designationCategoryService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    designationCategoryService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        designationCategoryService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    $scope.export = function () {
        designationCategoryService.exportToExcel().then(function (res) {
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







