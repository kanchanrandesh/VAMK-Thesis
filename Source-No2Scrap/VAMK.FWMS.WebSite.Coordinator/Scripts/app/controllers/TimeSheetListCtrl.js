angular.module('MetronicApp').controller('TimeSheetListCtrl', function ($rootScope, $scope, $state, companyService, employeeService, timeSheetService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.selectedCompany;

    (function () {
        loadCompanies().then(setDefaults);
    }());

    function loadCompanies() {
        var defer = $.Deferred();
        companyService.getAll().then(function (res) {
            $scope.companies = res;
            defer.resolve();
        });
        return defer;
    };

    function setDefaults() {
        var defer = $.Deferred();
        var employeeCompanyId = document.getElementById('HiddenEmployeeCompanyID').value;
        $scope.selectedCompany = $scope.companies.find(x => x.id === employeeCompanyId)
        $scope.loadEmployees();
        return defer;
    };

    $scope.loadEmployees = function () {
        var filter = {};
        filter.IsActive = true;
        filter.CompanyID = $scope.selectedCompany.id;

        var defer = $.Deferred();
        employeeService.search(filter).then(function (res) {
            $scope.employees = res;
            defer.resolve();
        });
        return defer;
    };

    $scope.search = function (srch) {
        if (!$scope.selectedCompany || !srch.dateFrom) {
            notificationMsgService.showErrorMessage('Required fields cannot be empty');
            return;
        }
        var search = {};
        search.CompanyID = $scope.selectedCompany.id;
        search.DateFrom = srch.dateFrom;
        search.DateTo = srch.dateTo;
        if (srch.employee)
            search.AssigneeID = srch.employee.id
        else
            search.AssigneeID = null

        timeSheetService.search(search).then(function (res) {
            $scope.searchList = res;
        });
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