angular.module('MetronicApp').controller('OpportunityListCtrl', function ($rootScope, $q, $scope, $state,
    companyService, strategicBusinessUnitService, organizationService, financialYearService, financialPeriodService,
    opportunityService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.searchQuery = {};
    $scope.searchQuery.status = "Pending";

    $scope.loggedEmployeeId = document.getElementById('HiddenEmployeeID').value;
    $scope.loggedEmployeeCompanyId = document.getElementById('HiddenEmployeeCompanyID').value;
    $scope.totalProductAmount = 0;
    $scope.totalProfitAmount = 0;
    $scope.totalConfidenceLevel = 0;
    $scope.selectedCompany;
    $scope.selectedSBU;
    $scope.selectedCustomer;
    $scope.selectedYear;
    $scope.selectedPeriod;

    $scope.selectedSearchCompany;
    $scope.selectedSearchSBU;
    $scope.selectedSearchCustomer;
    $scope.selectedSearchTargetYear;
    $scope.selectedSearchTargetPeriod;
    $scope.selectedSearchInitialYear;
    $scope.selectedSearchInitialPeriod;

    (function () {
        loadOpportunities().then(loadCompnies).then(loadSBUs).then(loadCustomers).then(loadFinancialYears).then(loadFinancialPeriods);
    }());

    function loadOpportunities() {
        $scope.searchQuery.LoggedEmployeeID = $scope.loggedEmployeeId;
        var deferred = $q.defer();
        opportunityService.search($scope.searchQuery).then(function (res) {
            $scope.searchList = res;
            deferred.resolve(res);
        });
        return deferred.promise;
    };

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

    function thousands_separators(num) {
        var num_parts = num.toString().split(".");
        num_parts[0] = num_parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return num_parts.join(".");
    }

    $scope.search = function (srch) {
        if ($scope.selectedSearchCompany)
            srch.CompanyID = $scope.selectedSearchCompany.id;
        else
            srch.CompanyID = null;
        if ($scope.selectedSearchSBU)
            srch.StrategicBusinessUnitID = $scope.selectedSearchSBU.id;
        else
            srch.StrategicBusinessUnitID = null;
        if ($scope.selectedSearchCustomer)
            srch.CustomerID = $scope.selectedSearchCustomer.id;
        else
            srch.CustomerID = null;
        if ($scope.selectedSearchTargetYear)
            srch.TargetYearID = $scope.selectedSearchTargetYear.id;
        else
            srch.TargetYearID = null;
        if ($scope.selectedSearchTargetPeriod)
            srch.TargetPeriodID = $scope.selectedSearchTargetPeriod.id;
        else
            srch.TargetPeriodID = null;
        if ($scope.selectedSearchInitialYear)
            srch.InitialYearID = $scope.selectedSearchInitialYear.id;
        else
            srch.InitialYearID = null;
        if ($scope.selectedSearchInitialPeriod)
            srch.InitialPeriodID = $scope.selectedSearchInitialPeriod.id;
        else
            srch.InitialPeriodID = null;
        srch.LoggedEmployeeID = $scope.loggedEmployeeId;

        opportunityService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    $scope.showDiv = function (number) {
        $("#div-oppportunity-addnew-detail-step1").hide();
        $("#div-oppportunity-addnew-footer-step1").hide();
        $("#div-oppportunity-addnew-detail-step2").hide();
        $("#div-oppportunity-addnew-footer-step2").hide();
        $("#div-oppportunity-addnew-detail-step3").hide();
        $("#div-oppportunity-addnew-footer-step3").hide();
        $("#div-oppportunity-addnew-detail-step4").hide();
        $("#div-oppportunity-addnew-footer-step4").hide();
        $("#div-oppportunity-addnew-detail-step5").hide();
        $("#div-oppportunity-addnew-footer-step5").hide();
        switch (number) {
            case 1:
                $("#div-oppportunity-addnew-detail-step1").show();
                $("#div-oppportunity-addnew-footer-step1").show();
                break;
            case 2:
                $("#div-oppportunity-addnew-detail-step2").show();
                $("#div-oppportunity-addnew-footer-step2").show();
                break;
            case 3:
                $("#div-oppportunity-addnew-detail-step3").show();
                $("#div-oppportunity-addnew-footer-step3").show();
                break;
            case 4:
                $("#div-oppportunity-addnew-detail-step4").show();
                $("#div-oppportunity-addnew-footer-step4").show();
                break;
            case 5:
                $("#div-oppportunity-addnew-detail-step5").show();
                $("#div-oppportunity-addnew-footer-step5").show();
                break;
            default:
                $("#div-oppportunity-addnew-detail-step1").show();
                $("#div-oppportunity-addnew-footer-step1").show();
        }
    };

    $scope.showAddNew = function () {
        $scope.showDiv(1);
        $scope.selectedCompany = $scope.companies.find(x => x.id === $scope.loggedEmployeeCompanyId);
        opportunityService.getById(0).then(function (res) {
            $scope.opportunity = res;
            $scope.opportunity.leaderId = $scope.loggedEmployeeId;
            $scope.opportunity.accountManagerList = [];
            $scope.addNewAccountManager();
            $scope.opportunity.salesManagerList = [];
            $scope.addNewSalesManager();
            $scope.opportunity.bizDevList = [];
            $scope.addNewBizDev();
            $scope.opportunity.saleEngineerList = [];
            $scope.addNewSalesEngineer();
            $scope.opportunity.preSaleEngineerList = [];
            $scope.addNewPreSalesEngineer();
            $scope.opportunity.projectManagerList = [];
            $scope.addNewProjectManager();
            $scope.opportunity.technicalPersonList = [];
            $scope.addNewTechnicalPerson();
            $scope.opportunity.legalOfficerList = [];
            $scope.addNewLegalOfficer();
            $scope.opportunity.productList = [];
            $scope.addNewProduct();
        });
    };

    $scope.closeAddNew = function () {
        $('#responsive').modal('hide');
    };

    $scope.step1Next = function () {
        if ($scope.selectedCompany && $scope.selectedSBU && $scope.selectedCustomer
            && $scope.opportunity.name && $scope.opportunity.code && $scope.opportunity.code && $scope.opportunity.description)
            $scope.showDiv(2);
        else
            toastr.error('Please fill the mandatory fields');
    };

    $scope.step2Next = function () {
        if ($scope.opportunity.revenue && $scope.opportunity.profit && $scope.selectedYear
            && $scope.selectedPeriod && $scope.opportunity.targetMonth)
            $scope.showDiv(3);
        else
            toastr.error('Please fill the mandatory fields');
    };

    $scope.step2Previous = function () {
        $scope.showDiv(1);
    };

    $scope.step3Next = function () {
        if (parseFloat($scope.productBalanceAmount) != 0) {
            notificationMsgService.showErrorMessage("Product divident needs to be equal to the Revenue");
            return;
        }
        if (parseFloat($scope.profitBalanceAmount) != 0) {
            notificationMsgService.showErrorMessage("Profit divident needs to be equal to the Profit");
            return;
        }
        $scope.showDiv(4);
    };

    $scope.step3Previous = function () {
        $scope.showDiv(2);
    };

    $scope.step4Next = function () {        
        $scope.showDiv(5);
    };

    $scope.step4Previous = function () {
        $scope.showDiv(3);
    };

    $scope.finish = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;

        if ($scope.selectedCompany)
            obj.companyId = $scope.selectedCompany.id;
        else
            obj.companyId = null;
        if ($scope.selectedCustomer)
            obj.customerId = $scope.selectedCustomer.id;
        else
            obj.customerId = null;
        if ($scope.selectedSBU)
            obj.sbuId = $scope.selectedSBU.id;
        else
            obj.sbuId = null;
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
                $scope.search($scope.searchQuery);
                $scope.closeAddNew();
                notificationMsgService.showSuccessMessage('Opportunity saved successfully');
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    };

    $scope.step5Previous = function () {
        $scope.showDiv(4);
    };

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
            if ($scope.opportunity.confidenceLevelList[i].hasCompleted && $scope.opportunity.confidenceLevelList[i].percentage)
                $scope.totalConfidenceLevel += parseFloat($scope.opportunity.confidenceLevelList[i].percentage);
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

    $.fn.center = function () {
        this.css("position", "absolute");
        //this.css("top", Math.max(0, (($(window).height() - $(this).outerHeight()) / 2) +
        //    $(window).scrollTop()) + "px");
        //this.css("left", Math.max(0, (($(window).width() - $(this).outerWidth()) / 2) +
        //    $(window).scrollLeft()) + "px");
        this.css("top", 8 + "%");
        this.css("left", 18 + "%");
        return this;
    }
    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});