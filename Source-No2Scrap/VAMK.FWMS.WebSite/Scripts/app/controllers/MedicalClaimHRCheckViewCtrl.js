﻿angular.module('MetronicApp').controller('MedicalClaimHRCheckViewCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal,
    $stateParams, $state, medicalClaimService, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;

    medicalClaimService.getForView($stateParams.id).then(function (res) {
        $scope.medicalClaimRequest = res;
        if (res.medicalClaimStatus == 'Requested')
            $scope.headerTitle = 'Medical Claim - HR Conformation';
        else
            $scope.headerTitle = 'Medical Claim - HR Approval';
    });

    medicalClaimService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.approve = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        if (obj.medicalClaimStatus == 'Requested') {
            //obj.leadApprovedById = employeeId;
            obj.leadApproveledById = 35;
        }
        else {
            obj.hodApprovedById = 2;
        }

        medicalClaimService.checkMedicalClaim(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Medical Claim conformation successfully');
                $state.go('medicalClaimHRCheckList', {});
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

            medicalClaimService.rejectMedicalClaim(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Medical Claim rejected successfully');
                    $state.go('medicalClaimHRApprovalAddEdit', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    //Attachments
    function loadAttachments() {
        var defer = $.Deferred();
        medicalClaimService.getMedicalClaimVoucherAttachments($scope.id).then(function (res) {
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
            templateUrl: 'medicalClaimAttachment.html',
            controller: 'MedicalClaimHRCheckViewInstanceCtrl',
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