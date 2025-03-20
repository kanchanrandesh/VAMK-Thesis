angular.module('MetronicApp').controller('PettyCashDisbursementViewCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal,
    $stateParams, $state, pettyCashService, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.filterType = $stateParams.type;
    $scope.headerTitle = 'Petty Cash Disbursement';

    pettyCashService.getForDisbursementView($stateParams.id).then(function (res) {
        $scope.pettyCashRequest = res;
        if ($scope.pettyCashRequest.settledIOUList.length > 0)
            $scope.disburseAmountLabel = 'Balance Amount';
        else
            $scope.disburseAmountLabel = 'Disburse Amount';
    });

    pettyCashService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

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