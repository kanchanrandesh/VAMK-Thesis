angular.module('MetronicApp').controller('MedicalClaimReportFilterCtrl', function ($rootScope, $scope, $state, notificationMsgService, medicalClaimService,employeeService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.search = {};
    $scope.search.groupBy = 'Payee';
    $scope.selectedPayee;

    employeeService.getAll().then(function (res) {
        $scope.employees = res;
    });

    $scope.generate = function () {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        var filter = {}
        filter.groupBy = $scope.search.groupBy;
        filter.companyIds = '';
       
        filter.dateFrom = $scope.search.dateFrom;
        filter.dateTo = $scope.search.dateTo;
        
        if ($scope.selectedPayee)
            filter.payeeId = $scope.selectedPayee.id
        else
            filter.payeeId = null
        filter.accountCodeFrom = $scope.search.accountCodeFrom
        filter.accountCodeTo = $scope.search.accountCodeTo
        filter.loggedEmployeeId = employeeId;

        //console.log(filter);
        medicalClaimService.printMedicalClaimReport(filter).then(function (res) {
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