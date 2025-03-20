angular.module('MetronicApp').controller('PettyCashRequestViewCtrl', function ($rootScope, $scope, $http, $timeout, $document,
    $uibModal, $stateParams, $state, pettyCashService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    pettyCashService.getForView($stateParams.id).then(function (res) {
        $scope.pettyCashRequest = res;
    });

    pettyCashService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.cancel = function () {
        $state.go('pettyCashRequestList', {});
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
            controller: 'PettyCashAttachmentViewInstanceCtrl',
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


angular.module('MetronicApp').directive("sliderElement", function () {
    return {
        template: "<img src='{{slide.fileName}}' style='margin:auto;'>"
    };
});
