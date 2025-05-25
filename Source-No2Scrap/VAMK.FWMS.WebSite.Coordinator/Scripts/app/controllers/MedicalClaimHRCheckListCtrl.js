angular.module('MetronicApp').controller('MedicalClaimHRCheckListCtrl', function ($rootScope, $scope, $state, medicalClaimService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    var employeeId = document.getElementById('HiddenEmployeeID').value;
    $scope.searchQuery = {};
    $scope.searchQuery.ApproverID = employeeId;

    medicalClaimService.searchForMedicalClaimHRCheck($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        medicalClaimService.searchForMedicalClaimHRCheck(srch).then(function (res) {
                $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});