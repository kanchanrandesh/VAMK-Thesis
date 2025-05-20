var MetronicApp = angular.module("MetronicApp", [
    "ui.router",
    "ui.bootstrap",
    "oc.lazyLoad",
    "ui.bootstrap.tpls",
    "ui.bootstrap.modal",
    "ui.bootstrap.popover",
    "ngNotify"
]);

angular.module('MetronicApp').controller('LoginCtrl', function ($rootScope, $scope, $http, $timeout, loginService, notificationMsgService) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        App.initAjax();
    });

    $scope.resetPassword = function (reset, form) {
        if (form.$valid) {
            loginService.resetPassword(reset).then(function (res) {
                if (res.status == 0) {
                    $("#resetPasswordMessage").css("color", "blue");
                    notificationMsgService.showSuccessMessage(res.message);
                    $scope.reset.message = res.message;
                    //$("#back-btn").trigger('click');

                }
                else {
                    $("#resetPasswordMessage").css("color", "red");
                    notificationMsgService.showErrorMessage(res.message);
                    $scope.reset.message = res.message;
                }
            });
        }
        return false;
    };

    // set sidebar closed and body solid layout mode
    //$rootScope.settings.layout.pageContentWhite = true;
    //$rootScope.settings.layout.pageBodySolid = false;
    //$rootScope.settings.layout.pageSidebarClosed = false;
});