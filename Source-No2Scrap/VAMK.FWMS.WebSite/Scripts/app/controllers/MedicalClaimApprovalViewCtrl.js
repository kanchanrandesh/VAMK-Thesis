angular.module('MetronicApp').controller('MedicalClaimApprovalViewCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal,
    $stateParams, $state, medicalClaimService, notificationMsgService) {
    var $ctrl = this;
    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });
    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.thumbnailAllowance;
    $scope.employeeId;
    $scope.amount;

    (function () {
        loadAllowance().then(loadClaimAmount).then(loadDisplayCounts).then(loadMedicalClaimApproval);
    }());

    function loadMedicalClaimApproval() {
        var defer = $.Deferred();
        medicalClaimService.getForView($stateParams.id).then(function (res) {
            $scope.medicalClaimRequest = res;
            if (res.medicalClaimStatus == 'Requested')
                $scope.headerTitle = 'Claim - HR Verification';
            else
                $scope.headerTitle = 'Claim - HR Approval';

            defer.resolve();
        });

        return defer;
    };

    function loadAllowance() {
        var defer = $.Deferred();
        medicalClaimService.getById($stateParams.id).then(function (res) {
            $scope.medicalClaimRequest = res;
            $scope.employeeId = res.payeeId;
            $scope.thumbnailAllowance = parseInt(res.allowance);
            defer.resolve();
        });
        return defer;
    };

    function loadClaimAmount() {
        var defer = $.Deferred();
        medicalClaimService.getById($stateParams.id).then(function (res) {
            $scope.amount = res.amount;
            defer.resolve();
        });
    };

    function loadDisplayCounts() {
        var defer = $.Deferred();
        medicalClaimService.getDisplayCounts($scope.employeeId).then(function (res) {
            $scope.thumbnailUtilized = parseInt(res.thumbnailUtilized);
            $scope.thumbnailInApprove = parseInt(res.thumbnailInApprove) - parseInt($scope.amount);
            $scope.thumbnailBalance = parseInt($scope.thumbnailAllowance - ($scope.thumbnailUtilized + $scope.thumbnailInApprove));
            defer.resolve();
        });
    };

    medicalClaimService.getTxHistory($stateParams.id).then(function (res) {
        $scope.txHistoryList = res;
    });

    $scope.approve = function (obj) {
        var employeeId = document.getElementById('HiddenEmployeeID').value;
        if (obj.medicalClaimStatus == 'Requested') {
            obj.hrCheckedById = employeeId;
        }
        else {
            obj.hrApprovedById = employeeId;
        }

        medicalClaimService.approveMedicalClaim(obj).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Medical Claim approved successfully');
                $state.go('medicalClaimApprovalList', {});
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    };

    $scope.reject = function (obj) {
        var confirmation = confirm("Are you sure you want to reject this request ?");

        if (confirmation == true) {
            var employeeId = document.getElementById('HiddenEmployeeID').value;
            obj.rejectedById = employeeId;

            medicalClaimService.rejectMedicalClaim(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Medical Claim rejected successfully');
                    $state.go('medicalClaimApprovalList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    };

    $scope.searchHistory = function (srch) {
        srch.payeeId = $scope.employeeId;
        medicalClaimService.search(srch).then(function (res) {
            $scope.searchList = res;
        });
    };

    //Attachments
    function loadAttachments() {
        var defer = $.Deferred();
        medicalClaimService.getMedicalClaimAttachments($scope.id).then(function (res) {
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
            controller: 'MedicalClaimApprovalViewInstanceCtrl',
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
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});