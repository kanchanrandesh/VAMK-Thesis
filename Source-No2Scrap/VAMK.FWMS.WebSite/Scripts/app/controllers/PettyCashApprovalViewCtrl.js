angular.module('MetronicApp').controller('PettyCashApprovalViewCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal,
    $stateParams, $state, pettyCashService, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    pettyCashService.getForView($stateParams.id).then(function (res) {
        $scope.pettyCashRequest = res;
        if (res.pettyCashStatus == 'Requested')
            $scope.headerTitle = 'Petty Cash Approval - Project Lead';
        else
            $scope.headerTitle = 'Petty Cash Approval - HOD';
    });

    pettyCashService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.approve = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        if (obj.pettyCashStatus == 'Requested') {
            obj.leadApprovedById = employeeId;
        }
        else {
            obj.hodApprovedById = employeeId;
        }

        pettyCashService.approvePettyCash(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Petty Cash approved successfully');
                $state.go('pettyCashApprovalList', {});
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

            pettyCashService.rejectPettyCash(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Petty Cash rejected successfully');
                    $state.go('pettyCashApprovalList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    //Attachments
    function loadAttachments() {
        var defer = $.Deferred();
        pettyCashService.getPettyCashVoucherAttachments($scope.id).then(function (res) {
            console.log(res);
            $scope.attachments = res.files;
            defer.resolve();
        });
        return defer;
    };
    loadAttachments();

    $scope.openModel = function (size, parentSelector, currentItemIndex) {
        var parentElem = parentSelector ?
                         angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'pettyCashAttachment.html',
            controller: 'PettyCashApprovalViewInstanceCtrl',
            controllerAs: '$ctrl',
            windowClass: 'modelPosition',
            size: size,
            appendTo: parentElem,
            resolve: {
                items: function () {
                    return { defaultItemIndex: currentItemIndex, files: $scope.attachments }
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            $ctrl.selected = selectedItem;
        }, function () {
            //$log.info('Modal dismissed at: ' + new Date());
        });
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});