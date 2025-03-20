angular.module('MetronicApp').controller('CommitmentSummaryReportCtrl', function ($rootScope, $q, $scope, $state, companyService, financialYearService, financialPeriodService, opportunityService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.search = {};
    $scope.search.reportType = 'Leaders';
    $scope.selectedCompany;
    $scope.selectedYear;
    $scope.selectedPeriod;

    (function () {
        loadCompnies().then(loadFinancialYears).then(loadFinancialPeriods);
    }());

    function loadCompnies() {
        var deferred = $q.defer();
        companyService.getAll().then(function (res) {
            $scope.companies = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    function loadFinancialYears() {
        var deferred = $q.defer();
        financialYearService.getAll().then(function (res) {
            $scope.financialYears = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    function loadFinancialPeriods() {
        var deferred = $q.defer();
        financialPeriodService.getAll().then(function (res) {
            $scope.financialPeriods = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    $scope.generate = function () {
        if ($scope.selectedCompany)
            $scope.search.companyId = $scope.selectedCompany.id
        else
            $scope.search.companyId = null

        if ($scope.selectedYear)
            $scope.search.financialYearId = $scope.selectedYear.id
        else
            $scope.search.financialYearId = null

        if ($scope.selectedPeriod)
            $scope.search.financialPeriodId = $scope.selectedPeriod.id
        else
            $scope.search.financialPeriodId = null

        if (!$scope.search.companyId) {
            notificationMsgService.showErrorMessage('Company needs to be selected');
            return;
        }
        if (!$scope.search.financialYearId) {
            notificationMsgService.showErrorMessage('Year needs to be selected');
            return;
        }

        opportunityService.printCommitmentSummaryReport($scope.search).then(function (res) {
        });
    };
    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});