angular.module('MetronicApp').controller('MedicalClaimRequestViewCtrl', function ($rootScope, $scope, $http, $timeout, $document,
    $uibModal, $stateParams, $state, medicalClaimService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    loadAttachments();

    medicalClaimService.getForView($stateParams.id).then(function (res) {
        $scope.medicalClaimRequest = res;
    });

    medicalClaimService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.cancel = function () {
        $state.go('medicalClaimRequestList', {});
    }

    //Attachments
    function loadAttachments() {
        var defer = $.Deferred();
        medicalClaimService.getMedicalClaimAttachments($scope.id).then(function (res) {
            console.log(res);
            $scope.attachments = res.files;
            defer.resolve();
        });
        return defer;
    };

    $scope.openModel = function (size, parentSelector, currentItemIndex) {
        var parentElem = parentSelector ?
            angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
        var modalInstance = $uibModal.open({
            animation: $ctrl.animationsEnabled,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'medicalClaimAttachment.html',
            controller: 'MedicalClaimAttachmentViewInstanceCtrl',
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
