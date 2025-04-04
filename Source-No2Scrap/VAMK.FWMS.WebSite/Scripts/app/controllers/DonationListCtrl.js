angular.module('MetronicApp').controller('DonationListCtrl', function ($rootScope, $scope, $state, donationService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.openDatePickerDate = function () {
        $scope.popupDate.opened = true;
    };
    $scope.popupDate = {
        opened: false
    };



    donationService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        debugger;
        donationService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});