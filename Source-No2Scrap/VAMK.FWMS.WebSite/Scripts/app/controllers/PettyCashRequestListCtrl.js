angular.module('MetronicApp').controller('PettyCashRequestListCtrl', function ($rootScope, $scope, $stateParams, $state, pettyCashService, companyService, projectUnitService, notificationMsgService) {
  
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    var employeeId = document.getElementById('HiddenEmployeeID').value;
    $scope.searchQuery = {};
    $scope.searchQuery.payeeId = employeeId;
    $scope.searchQuery.type = $stateParams.type ? $stateParams.type : 'Open';

    companyService.getAll().then(function (res) {
        $scope.companies = res;
    });

    projectUnitService.getAll(employeeId).then(function (res) {
        $scope.projectUnits = res;
    });

    pettyCashService.search($scope.searchQuery).then(function (res) {
        $scope.searchList = res;
    });

    $scope.search = function (srch) {
        if ($scope.selectedProjectUnit) {
            var cha = $scope.selectedProjectUnit.substring(0, 1);
            var length = $scope.selectedProjectUnit.length;
            if (cha == 'P') {
                srch.projectId = $scope.selectedProjectUnit.substring(1, length)
                srch.unitId = '';
            }
            else {
                srch.unitId = $scope.selectedProjectUnit.substring(1, length)
                srch.projectId = '';
            }
        }

        if ($scope.selectedCompany)
            srch.companyId = $scope.selectedCompany.id
        else
            srch.companyId = null

        pettyCashService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };
    $scope.cancel = function (id) {
        var confirmation = confirm("Are you sure you want to cancel this request ?");

        if (confirmation == true) {
            pettyCashService.cancelPettyCash(id).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Petty Cash cancelled successfully');
                    pettyCashService.search($scope.searchQuery).then(function (res) {
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