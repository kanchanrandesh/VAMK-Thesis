angular.module('MetronicApp').controller('IOURequestListCtrl', function ($rootScope, $scope, $state, iouService, companyService, projectUnitService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    var employeeId = document.getElementById('HiddenEmployeeID').value;
    $scope.searchQuery = {};
    $scope.searchQuery.payeeId = employeeId;

    companyService.getAll().then(function (res) {
        $scope.companies = res;
    });

    projectUnitService.getAll(employeeId).then(function (res) {
        $scope.projectUnits = res;
    });

    iouService.search($scope.searchQuery).then(function (res) {
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

        iouService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});