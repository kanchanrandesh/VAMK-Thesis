angular.module('MetronicApp').controller('IOURequestAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, iouService, companyService, projectUnitService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.selectedCompany;
    $scope.selectedProject;
    $scope.headerTitle;

    (function () {
        loadCompanies().then(loadProjectUnits).then(loadTxList).then(loadIOURequest);
    }());

    function loadCompanies() {
        var defer = $.Deferred();
        companyService.getAll().then(function (res) {
            $scope.companies = res;
            defer.resolve();
        });
        return defer;
    };

    function loadProjectUnits() {
        var defer = $.Deferred();
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        projectUnitService.getAll(employeeId).then(function (res) {
            $scope.projectUnits = res;
            defer.resolve();
        });
        return defer;
    };

    function loadTxList() {
        var defer = $.Deferred();
        iouService.getTxHistory($stateParams.id).then(function (res) {
            $scope.txHistoryList = res;
            defer.resolve();
        });
        return defer;
    };

    function loadIOURequest() {
        var defer = $.Deferred();
        if ($stateParams.id != 0)
            $scope.headerTitle = "Edit IOU Request";
        else
            $scope.headerTitle = "New IOU Request";

        iouService.getById($stateParams.id).then(function (res) {
            $scope.iouRequest = res;
            if (!res.companyId) {
                var employeeCompanyId = document.getElementById('HiddenEmployeeCompanyID').value;
                $scope.selectedCompany = $scope.companies.find(x => x.id === employeeCompanyId)
            }
            else
                $scope.selectedCompany = $scope.companies.find(x => x.id === res.companyId)
            if (res.projectId)
                $scope.selectedProjectUnit = 'P' + res.projectId;
            else
                $scope.selectedProjectUnit = 'U' + res.unitId;
            defer.resolve();
        });
        return defer;
    };

    $scope.addNewIOURequestListItem = function () {
        $scope.iouRequest.itemList.push({
            "iouId": null,
            "description": null,
            "categoryId": null,
            "amount": null,
        });
    }

    $scope.deleteIOURequestListItem = function (item) {
        var i = $scope.iouRequest.itemList.indexOf(item);
        if (i != -1) {
            $scope.iouRequest.itemList.splice(i, 1);
        }
        $scope.calcAmount();
    }

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            var employeeId = document.getElementById('HiddenEmployeeID').value;

            if ($scope.selectedProjectUnit) {
                var cha = $scope.selectedProjectUnit.substring(0, 1);
                var length = $scope.selectedProjectUnit.length;
                if (cha == 'P') {
                    obj.projectId = $scope.selectedProjectUnit.substring(1, length)
                    obj.unitId = '';
                }
                else {
                    obj.unitId = $scope.selectedProjectUnit.substring(1, length)
                    obj.projectId = '';
                }
            }
            obj.payeeId = employeeId;
            obj.requestedById = employeeId;
            obj.iouStatus = "Requested";
            if ($scope.selectedCompany)
                obj.companyId = $scope.selectedCompany.id
            else
                obj.companyId = null

            iouService.saveRequst(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('IOU saved successfully');
                    $state.go('iouRequestList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    $scope.cancel = function () {
        $state.go('iouRequestList', {});
    }

    $scope.calcAmount = function () {
        $scope.iouRequest.amount = 0;
        for (var i = 0; i < $scope.iouRequest.itemList.length; i++) {
            if ($scope.iouRequest.itemList[i].amount)
                $scope.iouRequest.amount += parseFloat($scope.iouRequest.itemList[i].amount);
        }
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});