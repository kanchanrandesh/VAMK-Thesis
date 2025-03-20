angular.module('MetronicApp').controller('IOUPaymentListCtrl', function ($rootScope, $scope, $state, $stateParams, iouService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.toApproveCount = '0';
    $scope.toPayCount = '0';
    $scope.toSettleCount = '0';

    $scope.searchQuery = {};
    $scope.searchQuery.type = $stateParams.type ? $stateParams.type : 'ToPay';

    $scope.$on('$viewContentLoaded', function (event) {
        if ($scope.authorized != false) {

            iouService.searchForIOUPayment($scope.searchQuery).then(function (res) {
                $scope.searchList = res;
            });

            iouService.getDisplayCounts().then(function (res) {
                $scope.toApproveCount = res.toApproveCount;
                $scope.toPayCount = res.toPayCount;
                $scope.toSettleCount = res.toSettleCount;
            });

            $scope.search = function (srch) {
                iouService.searchForIOUPayment(srch).then(function (res) {
                    $scope.searchList = res;
                });
            };

            $scope.printIOU = function (id) {
                iouService.printIOU(id).then(function (res) {
                });
            };
        }
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});