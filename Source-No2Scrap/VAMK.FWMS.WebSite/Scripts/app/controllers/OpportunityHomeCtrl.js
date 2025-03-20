angular.module('MetronicApp').controller('OpportunityHomeCtrl', function ($rootScope, $q, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state,
    companyService, strategicBusinessUnitService, organizationService, financialYearService, financialPeriodService,
    opportunityService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    $scope.loggedEmployeeId = document.getElementById('HiddenEmployeeID').value;
    $scope.loggedEmployeeCompanyId = document.getElementById('HiddenEmployeeCompanyID').value;
    $scope.isSuperUser = document.getElementById('HiddenIsSuperUser').value;
    $scope.selectedCompany;
    $scope.selectedSBU;
    $scope.selectedCustomer;
    $scope.selectedYear;
    $scope.selectedPeriod;
    $scope.opportunityActivity;
    $scope.accessLevel;

    (function () {
        loadCompnies().then(loadSBUs).then(loadCustomers).then(loadFinancialYears).then(loadFinancialPeriods).then(loadOpportunity);
    }());

    function loadCompnies() {
        var deferred = $q.defer();
        companyService.getAll().then(function (res) {
            $scope.companies = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    function loadSBUs() {
        var deferred = $q.defer();
        strategicBusinessUnitService.getAll().then(function (res) {
            $scope.sbus = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

    function loadCustomers() {
        var deferred = $q.defer();
        organizationService.getAllCustomers().then(function (res) {
            $scope.customers = res;
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

    function loadOpportunity() {
        var defer = $.Deferred();
        opportunityService.getById($stateParams.id).then(function (res) {
            $scope.opportunity = res;
            $scope.opportunity.accountManagerList = res.teamList.filter(x => x.role == 'AccountManger');
            $scope.opportunity.salesManagerList = res.teamList.filter(x => x.role == 'SalesManager');
            $scope.opportunity.bizDevList = res.teamList.filter(x => x.role == 'BizDevelopmentPerson');
            $scope.opportunity.saleEngineerList = res.teamList.filter(x => x.role == 'SalesEngineer');
            $scope.opportunity.preSaleEngineerList = res.teamList.filter(x => x.role == 'PresalesEngineer');
            $scope.opportunity.projectManagerList = res.teamList.filter(x => x.role == 'ProductManager');
            $scope.opportunity.technicalPersonList = res.teamList.filter(x => x.role == 'TechnicalPerson');
            $scope.opportunity.legalOfficerList = res.teamList.filter(x => x.role == 'LegalOfficer');

            if (res.companyId)
                $scope.selectedCompany = $scope.companies.find(x => x.id == res.companyId);
            if (res.sbuId)
                $scope.selectedSBU = $scope.sbus.find(x => x.id == res.sbuId);
            if (res.customerId)
                $scope.selectedCustomer = $scope.customers.find(x => x.id == res.customerId);
            if (res.targetYearId)
                $scope.selectedYear = $scope.financialYears.find(x => x.id == res.targetYearId);
            if (res.targetPeriodId)
                $scope.selectedPeriod = $scope.financialPeriods.find(x => x.id == res.targetPeriodId);

            $scope.headerTitle = $scope.opportunity.code;
            $scope.headerDescription = $scope.opportunity.name;
            $scope.calcProductTotal();
            $scope.calcProfitTotal();
            $scope.updatePercentage();
            fillEmptyList();
            setAccessLevel(res.teamList);
            setProfitRatio();
            defer.resolve();
        });
        return defer;
    };

    function fillEmptyList() {
        if ($scope.opportunity.accountManagerList.length == 0) {
            $scope.opportunity.accountManagerList = [];
            $scope.addNewAccountManager();
        }
        if ($scope.opportunity.salesManagerList.length == 0) {
            $scope.opportunity.salesManagerList = [];
            $scope.addNewSalesManager();
        }
        if ($scope.opportunity.bizDevList.length == 0) {
            $scope.opportunity.bizDevList = [];
            $scope.addNewBizDev();
        }
        if ($scope.opportunity.saleEngineerList.length == 0) {
            $scope.opportunity.saleEngineerList = [];
            $scope.addNewSalesEngineer();
        }
        if ($scope.opportunity.preSaleEngineerList.length == 0) {
            $scope.opportunity.preSaleEngineerList = [];
            $scope.addNewPreSalesEngineer();
        }
        if ($scope.opportunity.projectManagerList.length == 0) {
            $scope.opportunity.projectManagerList = [];
            $scope.addNewProjectManager();
        }
        if ($scope.opportunity.technicalPersonList.length == 0) {
            $scope.opportunity.technicalPersonList = [];
            $scope.addNewTechnicalPerson();
        }
        if ($scope.opportunity.legalOfficerList.length == 0) {
            $scope.opportunity.legalOfficerList = [];
            $scope.addNewLegalOfficer();
        }
        if ($scope.opportunity.productList.length == 0) {
            $scope.opportunity.productList = [];
            $scope.addNewProduct();
        }
    }

    function setProfitRatio() {
        $scope.opportunity.profitRatio = 0;
        if ($scope.opportunity.revenue && $scope.opportunity.profit) {
            var rev = $scope.opportunity.revenue.replace(/\,/g, '');
            var prof = $scope.opportunity.profit.replace(/\,/g, '');
            $scope.opportunity.profitRatio = parseFloat((prof / rev) * 100);
            $scope.opportunity.profitRatio = Math.round($scope.opportunity.profitRatio * 100) / 100
        }
    }

    function setAccessLevel(teamList) {
        if ($scope.isSuperUser) {
            $scope.accessLevel = "Level1";
        }
        else {
            if (teamList.filter(x => x.accessLevel == 'Level1' && x.employeeId == $scope.loggedEmployeeId).length > 0)
                $scope.accessLevel = "Level1";
            else if (teamList.filter(x => x.accessLevel == 'Level2' && x.employeeId == $scope.loggedEmployeeId).length > 0)
                $scope.accessLevel = "Level2";
            else
                $scope.accessLevel = "Level3";
        }
    }

    function thousands_separators(num) {
        var num_parts = num.toString().split(".");
        num_parts[0] = num_parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return num_parts.join(".");
    }

    $scope.close = function () {
        $('#opportunity').modal('hide');
        $('#financial').modal('hide');
        $('#team').modal('hide');
        $('#product').modal('hide');
        $('#confidenceLevel').modal('hide');
        $('#activity').modal('hide');
        $('#otherOpportunities').modal('hide');
        $('#historyLog').modal('hide');
    };

    $scope.save = function (obj, changedArea) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;

        if (changedArea == 'ProductDetails') {
            if (parseFloat($scope.productBalanceAmount) > 0) {
                notificationMsgService.showErrorMessage("Product Total cannot be greater than the Revenue");
                return;
            }
        }

        obj.changedArea = changedArea;
        if ($scope.selectedCompany)
            obj.companyId = $scope.selectedCompany.id;
        else
            obj.companyId = null;
        if ($scope.selectedSBU)
            obj.sbuId = $scope.selectedSBU.id;
        else
            obj.sbuId = null;
        if ($scope.selectedCustomer)
            obj.customerId = $scope.selectedCustomer.id;
        else
            obj.customerId = null;
        if ($scope.selectedYear)
            obj.targetYearId = $scope.selectedYear.id;
        else
            obj.targetYearId = null;
        if ($scope.selectedPeriod)
            obj.targetPeriodId = $scope.selectedPeriod.id;
        else
            obj.targetPeriodId = null;

        obj.teamList = [];
        for (var i = 0; i < $scope.opportunity.accountManagerList.length; i++) {
            if ($scope.opportunity.accountManagerList[i].employeeId && $scope.opportunity.accountManagerList[i].employeeId != '-') {
                var emp = $scope.opportunity.accountManagerList[i];
                emp.role = "AccountManger";
                obj.teamList.push(emp);
            }
        }
        for (var i = 0; i < $scope.opportunity.salesManagerList.length; i++) {
            if ($scope.opportunity.salesManagerList[i].employeeId && $scope.opportunity.salesManagerList[i].employeeId != '-') {
                var emp = $scope.opportunity.salesManagerList[i];
                emp.role = "SalesManager";
                obj.teamList.push(emp);
            }
        }
        for (var i = 0; i < $scope.opportunity.bizDevList.length; i++) {
            if ($scope.opportunity.bizDevList[i].employeeId && $scope.opportunity.bizDevList[i].employeeId != '-') {
                var emp = $scope.opportunity.bizDevList[i];
                emp.role = "BizDevelopmentPerson";
                obj.teamList.push(emp);
            }
        }
        for (var i = 0; i < $scope.opportunity.saleEngineerList.length; i++) {
            if ($scope.opportunity.saleEngineerList[i].employeeId && $scope.opportunity.saleEngineerList[i].employeeId != '-') {
                var emp = $scope.opportunity.saleEngineerList[i];
                emp.role = "SalesEngineer";
                obj.teamList.push(emp);
            }
        }
        for (var i = 0; i < $scope.opportunity.preSaleEngineerList.length; i++) {
            if ($scope.opportunity.preSaleEngineerList[i].employeeId && $scope.opportunity.preSaleEngineerList[i].employeeId != '-') {
                var emp = $scope.opportunity.preSaleEngineerList[i];
                emp.role = "PresalesEngineer";
                obj.teamList.push(emp);
            }
        }
        for (var i = 0; i < $scope.opportunity.projectManagerList.length; i++) {
            if ($scope.opportunity.projectManagerList[i].employeeId && $scope.opportunity.projectManagerList[i].employeeId != '-') {
                var emp = $scope.opportunity.projectManagerList[i];
                emp.role = "ProductManager";
                obj.teamList.push(emp);
            }
        }
        for (var i = 0; i < $scope.opportunity.technicalPersonList.length; i++) {
            if ($scope.opportunity.technicalPersonList[i].employeeId && $scope.opportunity.technicalPersonList[i].employeeId != '-') {
                var emp = $scope.opportunity.technicalPersonList[i];
                emp.role = "TechnicalPerson";
                obj.teamList.push(emp);
            }
        }
        for (var i = 0; i < $scope.opportunity.legalOfficerList.length; i++) {
            if ($scope.opportunity.legalOfficerList[i].employeeId && $scope.opportunity.legalOfficerList[i].employeeId != '-') {
                var emp = $scope.opportunity.legalOfficerList[i];
                emp.role = "LegalOfficer";
                obj.teamList.push(emp);
            }
        }

        opportunityService.save(obj).then(function (res) {
            if (res.status == true) {
                $scope.close();
                loadOpportunity();
                notificationMsgService.showSuccessMessage('Opportunity saved successfully');
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    $scope.addNewAccountManager = function () {
        $scope.opportunity.accountManagerList.push({
            "id": null,
            "opportunityId": null,
            "role": null,
            "employeeId": null,
            "employeeCode": null,
        });
    }

    $scope.deleteAccountManager = function (item) {
        var i = $scope.opportunity.accountManagerList.indexOf(item);
        if (i != -1) {
            $scope.opportunity.accountManagerList.splice(i, 1);
        }
    }

    $scope.addNewSalesManager = function () {
        $scope.opportunity.salesManagerList.push({
            "id": null,
            "opportunityId": null,
            "role": null,
            "employeeId": null,
            "employeeCode": null,
        });
    }

    $scope.deleteSalesManager = function (item) {
        var i = $scope.opportunity.salesManagerList.indexOf(item);
        if (i != -1) {
            $scope.opportunity.salesManagerList.splice(i, 1);
        }
    }

    $scope.addNewBizDev = function () {
        $scope.opportunity.bizDevList.push({
            "id": null,
            "opportunityId": null,
            "role": null,
            "employeeId": null,
            "employeeCode": null,
        });
    }

    $scope.deleteBizDev = function (item) {
        var i = $scope.opportunity.bizDevList.indexOf(item);
        if (i != -1) {
            $scope.opportunity.bizDevList.splice(i, 1);
        }
    }

    $scope.addNewSalesEngineer = function () {
        $scope.opportunity.saleEngineerList.push({
            "id": null,
            "opportunityId": null,
            "role": null,
            "employeeId": null,
            "employeeCode": null,
        });
    }

    $scope.deleteSalesEngineer = function (item) {
        var i = $scope.opportunity.saleEngineerList.indexOf(item);
        if (i != -1) {
            $scope.opportunity.saleEngineerList.splice(i, 1);
        }
    }

    $scope.addNewPreSalesEngineer = function () {
        $scope.opportunity.preSaleEngineerList.push({
            "id": null,
            "opportunityId": null,
            "role": null,
            "employeeId": null,
            "employeeCode": null,
        });
    }

    $scope.deletePreSalesEngineer = function (item) {
        var i = $scope.opportunity.preSaleEngineerList.indexOf(item);
        if (i != -1) {
            $scope.opportunity.preSaleEngineerList.splice(i, 1);
        }
    }

    $scope.addNewProjectManager = function () {
        $scope.opportunity.projectManagerList.push({
            "id": null,
            "opportunityId": null,
            "role": null,
            "employeeId": null,
            "employeeCode": null,
        });
    }

    $scope.deleteProjectManager = function (item) {
        var i = $scope.opportunity.projectManagerList.indexOf(item);
        if (i != -1) {
            $scope.opportunity.projectManagerList.splice(i, 1);
        }
    }

    $scope.addNewTechnicalPerson = function () {
        $scope.opportunity.technicalPersonList.push({
            "id": null,
            "opportunityId": null,
            "role": null,
            "employeeId": null,
            "employeeCode": null,
        });
    }

    $scope.deleteTechnicalPerson = function (item) {
        var i = $scope.opportunity.technicalPersonList.indexOf(item);
        if (i != -1) {
            $scope.opportunity.technicalPersonList.splice(i, 1);
        }
    }

    $scope.addNewLegalOfficer = function () {
        $scope.opportunity.legalOfficerList.push({
            "id": null,
            "opportunityId": null,
            "role": null,
            "employeeId": null,
            "employeeCode": null,
        });
    }

    $scope.deleteLegalOfficer = function (item) {
        var i = $scope.opportunity.legalOfficerList.indexOf(item);
        if (i != -1) {
            $scope.opportunity.legalOfficerList.splice(i, 1);
        }
    }

    $scope.addNewProduct = function () {
        $scope.opportunity.productList.push({
            "id": null,
            "opportunityId": null,
            "productName": null,
            "supplierId": null,
            "amount": null,
            "remarks": null,
        });
    }

    $scope.deleteProduct = function (item) {
        var i = $scope.opportunity.productList.indexOf(item);
        if (i != -1) {
            $scope.opportunity.productList.splice(i, 1);
        }
        $scope.calcProductTotal();
        $scope.calcProfitTotal();
    }

    $scope.calcProductTotal = function () {
        $scope.totalProductAmount = 0;
        $scope.productBalanceAmount = 0;
        for (var i = 0; i < $scope.opportunity.productList.length; i++) {
            if ($scope.opportunity.productList[i].amount) {
                var amt = $scope.opportunity.productList[i].amount.replace(/\,/g, '');
                $scope.totalProductAmount += parseFloat(amt);
            }
        }
        $scope.productBalanceAmount = parseFloat($scope.totalProductAmount) - parseFloat($scope.opportunity.revenue.replace(/\,/g, ''));
        $scope.totalProductAmount = thousands_separators($scope.totalProductAmount);
        $scope.productBalanceAmount = thousands_separators($scope.productBalanceAmount);

        // set the input with thousand seperators
        for (var i = 0; i < $scope.opportunity.productList.length; i++) {
            $scope.opportunity.productList[i].amount = $scope.opportunity.productList[i].amount.replace(/\,/g, '');
            $scope.opportunity.productList[i].amount = thousands_separators($scope.opportunity.productList[i].amount);
        }
    }

    $scope.calcProfitTotal = function () {
        $scope.totalProfitAmount = 0;
        $scope.profitBalanceAmount = 0;
        for (var i = 0; i < $scope.opportunity.productList.length; i++) {
            if ($scope.opportunity.productList[i].profit) {
                var prof = $scope.opportunity.productList[i].profit.replace(/\,/g, '');
                $scope.totalProfitAmount += parseFloat(prof);
            }
        }
        $scope.profitBalanceAmount = parseFloat($scope.totalProfitAmount) - parseFloat($scope.opportunity.profit.replace(/\,/g, ''));
        $scope.totalProfitAmount = thousands_separators($scope.totalProfitAmount);
        $scope.profitBalanceAmount = thousands_separators($scope.profitBalanceAmount);
    }

    $scope.updatePercentage = function () {
        $scope.totalConfidenceLevel = 0;
        for (var i = 0; i < $scope.opportunity.confidenceLevelList.length; i++) {
            if ($scope.opportunity.confidenceLevelList[i].hasCompleted)
                $scope.totalConfidenceLevel += parseFloat($scope.opportunity.confidenceLevelList[i].percentage) * parseFloat($scope.opportunity.confidenceLevelList[i].weightage);
        }
    }

    $scope.calcProfit = function () {
        $scope.opportunity.profit = 0;
        if ($scope.opportunity.profitRatio) {
            if ($scope.opportunity.revenue) {
                var rev = $scope.opportunity.revenue.replace(/\,/g, '');
                $scope.opportunity.profit = parseFloat(rev * $scope.opportunity.profitRatio / 100);
                $scope.opportunity.profit = thousands_separators($scope.opportunity.profit);
            }
        }
    }

    $scope.addNewActivity = function () {
        $scope.opportunityActivity = {
            "id": null,
            "opportunityId": null,
            "assigneeId": null,
            "activityType": null,
            "date": null,
            "timeFrom": null,
            "timeTo": null,
            "notes": null,
            "isDone": false,
            "timeStamp": null,
        };
    }

    $scope.editActivity = function (activity) {
        $scope.opportunityActivity = activity;
        $scope.opportunityActivity.date = new Date(activity.date);
    }

    $scope.saveActivity = function (opportunityActivity) {
        opportunityActivity.opportunityId = $scope.id;

        opportunityService.saveActivity(opportunityActivity).then(function (res) {
            if (res.status == true) {
                $scope.close();
                loadOpportunity();
                notificationMsgService.showSuccessMessage('Activity saved successfully');
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    $scope.loadOtherOpportunities = function () {
        var employeeId = document.getElementById('HiddenEmployeeID').value;

        var deferred = $q.defer();
        opportunityService.getAllOtherOpportunities($scope.id).then(function (res) {
            $scope.otherOpportunities = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    }

    $scope.loadHistoryLog = function () {
        var deferred = $q.defer();
        opportunityService.getHistoryLogs($scope.id).then(function (res) {
            $scope.historyLogs = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    }

    $scope.loadOtherOpportunity = function (id) {
        $scope.close();
        $state.go('opportunityHome', { pageTitle: 'Opportunity Home', id: id });
    }

    //Date Picker [Start]
    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2100, 12, 31),
        minDate: new Date(2016, 01, 01),
        startingDay: 1,
        format: 'dd-MMMM-yyyy'
    };
    $scope.format = 'dd-MMMM-yyyy';

    $scope.openDatePickerActivityDate = function () {
        $scope.popupActivityDate.opened = true;
    };
    $scope.popupActivityDate = {
        opened: false
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 1);
    $scope.dateEvents = [
        {
            date: tomorrow,
            status: 'full'
        },
        {
            date: afterTomorrow,
            status: 'partially'
        }
    ];
    function getDayClass(data) {
        var date = data.date,
            mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.dateEvents[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.dateEvents[i].status;
                }
            }
        }

        return '';
    }
    //Date Picker [End]

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});