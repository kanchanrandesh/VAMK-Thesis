angular.module('MetronicApp').controller('PettyCashReportFilterCtrl', function ($rootScope, $scope, $state, notificationMsgService, pettyCashService, companyService, departmentService, projectUnitService, employeeService, ledgerAccountService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.search = {};
    $scope.search.groupBy = 'Payee';
    $scope.selectedDepartment;
    $scope.selectedProjectUnit;
    $scope.selectedPayee;

    companyService.getAll().then(function (res) {
        $scope.companies = res;
    });

    departmentService.getAll().then(function (res) {
        $scope.departments = res;
    });

    projectUnitService.getAllProjectsAndUnits().then(function (res) {
        $scope.projectUnits = res;
    });

    employeeService.getAll().then(function (res) {
        $scope.employees = res;
    });

    ledgerAccountService.getAll().then(function (res) {
        $scope.accounts = res;
    });

    $scope.generate = function () {
        var employeeId = document.getElementById('HiddenEmployeeID').value;

        var filter = {}
        filter.groupBy = $scope.search.groupBy;
        filter.companyIds = '';
        for (var i = 0; i < $scope.companies.length; i++) {
            if ($scope.companies[i].hasSelected)
                filter.companyIds += $scope.companies[i].id + ',';
        }
        if (filter.companyIds == '') {
            notificationMsgService.showErrorMessage('At least one Company needs to be selected');
            return;
        }
        filter.dateFrom = $scope.search.dateFrom;
        filter.dateTo = $scope.search.dateTo;
        if ($scope.selectedDepartment)
            filter.departmentId = $scope.selectedDepartment.id
        else
            filter.departmentId = null
        if ($scope.selectedProjectUnit) {
            var cha = $scope.selectedProjectUnit.substring(0, 1);
            var length = $scope.selectedProjectUnit.length;
            if (cha == 'P') {
                filter.projectId = $scope.selectedProjectUnit.substring(1, length)
                filter.unitId = '';
            }
            else {
                filter.unitId = $scope.selectedProjectUnit.substring(1, length)
                filter.projectId = '';
            }
        }
        if ($scope.selectedPayee)
            filter.payeeId = $scope.selectedPayee.id
        else
            filter.payeeId = null
        filter.accountCodeFrom = $scope.search.accountCodeFrom
        filter.accountCodeTo = $scope.search.accountCodeTo
        filter.loggedEmployeeId = employeeId;

        //console.log(filter);
        pettyCashService.printPettyCashReport(filter).then(function (res) {
        });
    };

    $scope.clear = function () {
        alert('clear');
    };

    //Date Picker [Start]
    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2100, 12, 31),
        minDate: new Date(2016, 01, 01),
        startingDay: 1,
        format: 'dd-MMMM-yyyy'
    };
    $scope.format = 'dd-MMMM-yyyy';
    $scope.openDatePicker1 = function () {
        $scope.popup1.opened = true;
    };
    $scope.openDatePicker2 = function () {
        $scope.popup2.opened = true;
    };
    $scope.popup1 = {
        opened: false
    };
    $scope.popup2 = {
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