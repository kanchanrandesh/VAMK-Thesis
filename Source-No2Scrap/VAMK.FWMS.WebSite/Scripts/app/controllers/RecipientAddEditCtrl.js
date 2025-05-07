angular.module('MetronicApp').controller('RecipientAddEditCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, recipientService, notificationMsgService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.id = $stateParams.id;
    $scope.pageTitle = $stateParams.pageTitle;


    recipientService.getById($stateParams.id).then(function (res) {
        debugger;
        $scope.recipient = res;
    });

    $scope.save = function (obj, frm) {
        if (frm.$valid) {
            recipientService.save(obj).then(function (res) {
                if (res.status == true) {
                    notificationMsgService.showSuccessMessage('Record saved successfully');
                    $state.go('recipientList', {});
                }
                else
                    notificationMsgService.showErrorMessage(res.message);
            });
        }
    }

    //Contact Person
    $scope.addNewContactPerson = function () {
        //if (!$scope.recipient.contactPersonList) {
        //    $scope.recipient.contactPersonList = [];
        //}
        $scope.recipient.contactPersonList.push({
            "recipient": null,
            "recipientId": null,
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
        var i = $scope.recipient.contactPersonList.indexOf(contactPerson);
        if (i != -1) {
            $scope.recipient.contactPersonList.splice(i, 1);
        }
    }



    $scope.cancel = function () {
        $state.go('recipientList', {});
    }

    $scope.headerDescription = '';
    $scope.$watch('recipient.name', function (newValue, oldValue, scope) {
        if ($stateParams.id == "0" && newValue == undefined) {
            $scope.headerTitle = "New Recipient";
        }
        else {
            if (newValue == undefined) {
                $scope.headerTitle = "Edit Recipient";
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