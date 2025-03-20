angular.module('MetronicApp').controller('ConfidenceLevelCriteriaAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, confidenceLevelCriteriaService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;
    $scope.totalPercentage = 0;
    $scope.totalWeightage = 0;

    confidenceLevelCriteriaService.getAll().then(function (res) {
        $scope.confidenceLevelCriteriaList = res;
        $scope.calcTotals();
    });

    $scope.calcTotals = function () {
        $scope.totalPercentage = 0;
        $scope.totalWeightage = 0;
        for (var i = 0; i < $scope.confidenceLevelCriteriaList.length; i++) {
            if ($scope.confidenceLevelCriteriaList[i].percentage)
                $scope.totalPercentage += parseFloat($scope.confidenceLevelCriteriaList[i].percentage);
            if ($scope.confidenceLevelCriteriaList[i].weightage)
                $scope.totalWeightage += parseFloat($scope.confidenceLevelCriteriaList[i].weightage);
        }
    }

    $scope.addNewListItem = function () {
        $scope.confidenceLevelCriteriaList.push({
            "id": null,
            "name": null,
            "percentage": null,
            "weightage": null,
        });
    }

    $scope.deleteListItem = function (item) {
        var i = $scope.confidenceLevelCriteriaList.indexOf(item);
        if (i != -1) {
            $scope.confidenceLevelCriteriaList.splice(i, 1);
        }
        $scope.calcTotals();
    }

    $scope.save = function () {
        if ($scope.confidenceLevelCriteriaList.length == 0) {
            notificationMsgService.showErrorMessage('At lease one item needs to be entered');
            return;
        }
        //if ($scope.totalPercentage != 100) {
        //    notificationMsgService.showErrorMessage('Percentage total should be equal to 100');
        //    return;
        //}

        confidenceLevelCriteriaService.save($scope.confidenceLevelCriteriaList).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Record saved successfully');
                $state.go('confidenceLevelCriteriaList', {});
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    }

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});