angular.module('MetronicApp').controller('UserProfileController', function ($rootScope, $scope, $http, $timeout, $state, accountService,notificationMsgService) {
    $scope.$on('$viewContentLoaded', function() {   
        App.initAjax(); // initialize core components
        Layout.setAngularJsSidebarMenuActiveLink('set', $('#sidebar_menu_link_profile'), $state); // set profile link active in sidebar menu 
    });


    //change Password
    $scope.changePassword = function (pass, form) {
        if (form.$valid) {
            accountService.changePassword(pass).then(function (res) {
                if (res.status == 0) {
                    notificationMsgService.showSuccessMessage(res.message);
                    $timeout(function () {
                        $state.go("profile");
                    }, 10);

                }
                else {
                    notificationMsgService.showErrorMessage(res.message);
                }
            });
        }
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = true;
    $rootScope.settings.layout.pageSidebarClosed = true;
}); 
