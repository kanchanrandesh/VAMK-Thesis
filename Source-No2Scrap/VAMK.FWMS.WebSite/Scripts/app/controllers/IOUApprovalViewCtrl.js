angular.module('MetronicApp').controller('IOUApprovalViewCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, iouService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    iouService.getForView($stateParams.id).then(function (res) {
        $scope.iouRequest = res;
        if (res.iouStatus == 'Requested')
            $scope.headerTitle = 'IOU Approval - Project Lead';
        else
            $scope.headerTitle = 'IOU Approval - HOD';
    });

    iouService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.approve = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        if (obj.iouStatus == 'Requested') {
            obj.leadApprovedById = employeeId;
        }
        else
        {
            obj.hodApprovedById = employeeId;
        }

        iouService.approveIOU(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('IOU approved successfully');
                $state.go('iouApprovalList', {});
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    $scope.reject = function (obj) {
        var confirmation = confirm("Are you sure you want to reject this request ?");

        if (confirmation == true) {
            var employeeId = document.getElementById('HiddenEmployeeID').value;
            obj.rejectedById = employeeId;

            iouService.rejectIOU(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('IOU rejected successfully');
                    $state.go('iouApprovalList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        } 
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});