angular.module('MetronicApp').controller('EventExistingContactCtrl', function ($rootScope, $scope, $http, $timeout, $document, $uibModal, $stateParams, $state, notificationMsgService, eventService, organizationService, designationCategoryService, employeeService) {

    $scope.$on('$viewContentLoaded', function () {
        App.initAjax();
    });

    $scope.pageTitle = $stateParams.pageTitle;
    $scope.eventId = $stateParams.eventId;
    $scope.searchQuery = {};
    $scope.searchQuery.eventID = $stateParams.eventId;

    eventService.searchNotInvitedContacts($scope.searchQuery).then(function (res) {
        $scope.contactList = [];
        for (i = 0; i < res.length; i++) {
            res[i].hasSelected = false;
            $scope.contactList.push(res[i]);
        }
    });

    organizationService.getAll().then(function (res) {
        $scope.organizations = res;
    });

    designationCategoryService.getAll().then(function (res) {
        $scope.designationCategories = res;
        $scope.srchDesignationCategories = res;
    });

    employeeService.getAll().then(function (res) {
        $scope.employees = res;
    });

    $scope.search = function (srch) {
        if (srch.organization)
            srch.organizationID = srch.organization.id;
        if (srch.designationCategory)
            srch.designationCategoryID = srch.designationCategory.id;
        if (srch.accountManager)
            srch.accountManagerID = srch.accountManager.id;
        srch.designationCategoryIDList = '';
        var list = $scope.srchDesignationCategories;
        for (var i = 0; i < list.length; i++) {
            if(list[i].hasSelected)
                srch.designationCategoryIDList += list[i].id + ',';
        }
        eventService.searchNotInvitedContacts(srch).then(function (res) {
            $scope.contactList = [];
            for (i = 0; i < res.length; i++) {
                res[i].hasSelected = false;
                $scope.contactList.push(res[i]);
            }
        });
    };

    $scope.addToList = function () {
        var saveList = [];
        for (i = 0; i < $scope.contactList.length; i++) {
            if ($scope.contactList[i].hasSelected) {
                var contact = $scope.contactList[i];
                var eventParticipant = {}
                eventParticipant.eventId = $stateParams.eventId;
                eventParticipant.participantType = 'Contact';
                eventParticipant.title = contact.title;
                eventParticipant.firstName = contact.firstName;
                eventParticipant.lastName = contact.lastName;
                eventParticipant.phone = contact.phone;
                eventParticipant.extension = contact.extension;
                eventParticipant.mobile = contact.mobile;
                eventParticipant.email = contact.email;
                eventParticipant.secretaryName = contact.secretary;
                eventParticipant.organizationId = contact.organizationId ? parseInt(contact.organizationId) : null;
                eventParticipant.organizationName = contact.organizationName;
                eventParticipant.industryId = contact.industryId ? parseInt(contact.industryId) : null;
                eventParticipant.jobRole = contact.jobRole;
                eventParticipant.department = contact.department;
                eventParticipant.designationCategoryID = contact.designationCategoryId ? parseInt(contact.designationCategoryId) : null;
                eventParticipant.addressLine1 = contact.addressLine1;
                eventParticipant.addressLine2 = contact.addressLine2;
                eventParticipant.addressLine3 = contact.addressLine3;
                eventParticipant.countryId = contact.countryId ? parseInt(contact.countryId) : null;
                eventParticipant.accountManagerId = contact.accountManagerId ? parseInt(contact.accountManagerId) : null;
                eventParticipant.hasResponded = false;
                eventParticipant.isParticipating = false;
                eventParticipant.hasParticipated = false;
                eventParticipant.contactId = contact.id;
                saveList.push(eventParticipant);
            }
        }
        eventService.addToEventParticipant(saveList).then(function (res) {
            if (res.status == true) {
                notificationMsgService.showSuccessMessage('Record saved successfully');
                $state.go('eventHome', { eventId: $stateParams.eventId });
            }
            else
                notificationMsgService.showErrorMessage(res.message);
        });
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});