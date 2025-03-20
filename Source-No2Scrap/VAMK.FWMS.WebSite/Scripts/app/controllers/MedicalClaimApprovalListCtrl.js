angular.module('MetronicApp').controller('MedicalClaimApprovalListCtrl', function ($rootScope, $scope, $state, medicalClaimService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    var employeeId = document.getElementById('HiddenEmployeeID').value;
    $scope.searchQuery = {};
    $scope.toApprove = 0;
    $scope.searchQuery.ApproverID = employeeId;
    $scope.canVerify = false;
    $scope.canApprove = false;

    (function () {
        loadApprovelCount().then(checkApproval);
    }());

    medicalClaimService.searchForMedicalClaimApproval($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    function loadApprovelCount() {
        var defer = $.Deferred();
        medicalClaimService.searchMedicalClaimApprovalCount($scope.searchQuery).then(function (res) {
            $scope.toApprove = res;
            defer.resolve();
        });
        return defer;
    };

    function checkApproval() {
        var defer = $.Deferred();
        medicalClaimService.checkApprovalEligibility().then(function (res) {
            if (res.mchrCheckedByID == employeeId) {
                $scope.canVerify = true;
            }
            else if (res.mchrApprovedByID == employeeId) {
                $scope.canApprove = true;
            }
            defer.resolve();
        });
        return defer;
    };

    $scope.search = function (srch) {
        medicalClaimService.search(srch).then(function (res) {
            checkApproval();
            $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});