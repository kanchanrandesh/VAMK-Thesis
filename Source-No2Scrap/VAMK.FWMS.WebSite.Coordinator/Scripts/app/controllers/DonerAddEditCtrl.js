angular.module('MetronicApp').controller('DonerAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, donerService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;


    donerService.getById($stateParams.id).then(function (res) {
        debugger;
        $scope.doner = res;
    });

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            donerService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('donerList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    //Contact Person
    $scope.addNewContactPerson = function () {
        //if (!$scope.doner.contactPersonList) {
        //    $scope.doner.contactPersonList = [];
        //}
        $scope.doner.contactPersonList.push({
            "doner": null,
            "donerId": null,
            "code": null,
            "name": null,
            "email": null,
            "phoneNumber": null,
            "mobile": null,
            "id": null,
            "timeStamp": null,
            "timeStamp": null,
            "dateCreated": null
        });
    }
    $scope.deleteContactPerson = function (contactPerson) {       
        var i = $scope.doner.contactPersonList.indexOf(contactPerson);
        if (i != -1) {
            $scope.doner.contactPersonList.splice(i, 1);
        }
    }



    $scope.cancel = function () {
        $state.go('donerList', {});
    }

    $scope.headerDescription = '';
    $scope.$watch('doner.name', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Donor";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Donor";
            }
            else {
                $scope.headerTitle = newValue;
            }
        }
    }, true);

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});