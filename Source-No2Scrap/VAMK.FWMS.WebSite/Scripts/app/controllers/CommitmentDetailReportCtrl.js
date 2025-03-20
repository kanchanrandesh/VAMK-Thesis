angular.module('MetronicApp').controller('CommitmentDetailReportCtrl', function ($rootScope, $q, $scope, $state, companyService, financialYearService, financialPeriodService, opportunityService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.search = {};
    $scope.search.reportType = 'Leaders';
    $scope.selectedCompany;
    $scope.selectedYear;
    $scope.selectedPeriod;
    $scope.selectedMember;

    (function () {
        loadCompnies().then(loadFinancialYears).then(loadFinancialPeriods).then(loadMembers);
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

    function loadMembers() {
        var deferred = $q.defer();
        opportunityService.getMembers('Leaders').then(function (res) {
            $scope.members = res;
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

        if ($scope.selectedMember)
            $scope.search.memberId = $scope.selectedMember.id
        else
            $scope.search.memberId = null

        if (!$scope.search.companyId) {
            notificationMsgService.showErrorMessage('Company needs to be selected');
            return;
        }
        if (!$scope.search.financialYearId) {
            notificationMsgService.showErrorMessage('Year needs to be selected');
            return;
        }
        if (!$scope.search.financialPeriodId) {
            notificationMsgService.showErrorMessage('Period needs to be selected');
            return;
        }

        opportunityService.printCommitmentDetailReport($scope.search).then(function (res) {
        });
    };

    $scope.loadMembers = function (role) {
        opportunityService.getMembers(role).then(function (res) {
            $scope.members = res;
            deferred.resolve(res);
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});