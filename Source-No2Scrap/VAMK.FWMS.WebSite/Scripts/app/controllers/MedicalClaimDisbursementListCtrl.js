angular.module('MetronicApp').controller('MedicalClaimDisbursementListCtrl', function ($rootScope, $stateParams, $scope, $state, medicalClaimService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    var employeeId = document.getElementById('HiddenEmployeeID').value;

    $scope.toApproveCount = '0';
    $scope.toDisburseCount = '0';
    $scope.searchQuery = {};
    $scope.searchQuery.type = $stateParams.type ? $stateParams.type : 'ToPay';

    medicalClaimService.searchForMedicalClaimDisbursement($scope.searchQuery).then(function (res) {
        $scope.searchList = res;

    });

    medicalClaimService.getDisplayCount().then(function (res) {
        $scope.toApproveCount = res.thumbnailToApprove;
        $scope.toDisburseCount = res.thumbnailToDisburse;
    });

    $scope.search = function (srch) {
        medicalClaimService.searchForMedicalClaimDisbursement(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    $scope.printMedicalClaim = function (id) {
        medicalClaimService.printMedicalClaim(id).then(function (res) {
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});