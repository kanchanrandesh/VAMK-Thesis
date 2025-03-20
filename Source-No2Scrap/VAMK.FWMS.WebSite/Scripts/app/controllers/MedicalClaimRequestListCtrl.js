angular.module('MetronicApp').controller('MedicalCalimRequestListCtrl', function ($rootScope, $scope, $stateParams, $state, medicalClaimService, notificationMsgService) {
  
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    var employeeId = document.getElementById('HiddenEmployeeID').value;
    $scope.searchQuery = {};
    $scope.searchQuery.payeeId = employeeId;
    $scope.searchQuery.type = $stateParams.type ? $stateParams.type : 'Open';
    medicalClaimService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });
    
    $scope.search = function (srch) {
        medicalClaimService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    $scope.cancel = function (id) {
        var confirmation = confirm("Are you sure you want to cancel this request ?");
        if (confirmation == true) {
            medicalClaimService.cancelMedicalClaim(id).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Medical Claim cancelled successfully');
                    medicalClaimService.search($scope.searchQuery).then(function (res) {
                        $scope.searchList = res;
                    });
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});