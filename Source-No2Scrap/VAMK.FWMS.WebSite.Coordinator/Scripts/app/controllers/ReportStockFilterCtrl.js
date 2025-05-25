angular.module('MetronicApp').controller('ReportStockFilterCtrl', function ($rootScope, $scope, $state, notificationMsgService, itemService, StockCountService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.search = {};
    $scope.search.groupBy = 'Payee';
    $scope.selectedDepartment;
    $scope.selectedProjectUnit;
    $scope.selectedPayee;

    itemService.getAll().then(function (res) {
        $scope.items = res;
    });
              
    $scope.generate = function () {
        

        var filter = {}
        filter.groupBy = $scope.search.groupBy;
        filter.itemIds = '';
        for (var i = 0; i < $scope.items.length; i++) {
            if ($scope.items[i].hasSelected)
                filter.itemIds += $scope.items[i].id + ',';
        }
        if (filter.itemIds == '') {
            notificationMsgService.showErrorMessage('At least one Item needs to be selected');
            return;
        }      
       
       

        //console.log(filter);
        StockCountService.printStockCountReport(filter).then(function (res) {
        });
    };

    $scope.clear = function () {
        alert('clear');
    };
       
  

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});