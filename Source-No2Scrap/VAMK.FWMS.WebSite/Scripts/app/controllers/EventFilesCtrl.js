'use strict';
angular.module('MetronicApp').controller('EventFilesCtrl', function ($rootScope, $scope, $http, $timeout,
    $document, $uibModal, $stateParams, $state, notificationMsgService, eventService, FileUploader) {

    $scope.$on('$viewContentLoaded', function () {

        App.initAjax();

    });

    $scope.title = $stateParams.title;
    $scope.id = $stateParams.id;

    $scope.currentDate = function () {
        var d = new Date();

        var curr_date = d.getDate();

        var curr_month = d.getMonth();

        var curr_year = d.getFullYear();

        console.log(curr_date + "/" + curr_month + "/" + curr_year);
        return curr_date + "/" + curr_month + "/" + curr_year;
    }();

    eventService.getEventFiles($scope.id).then(function (res) {
        console.log(res);
        $scope.eventFiles = res;
    });

    var uploader = $scope.uploader = new FileUploader({
        url: '/api/Event/uploadEventFiles/' + $scope.id,
        autoUpload: true,
        formData: [{ id: $scope.id }]
    });

    $scope.UploadAllFiles = function () {
        $("#fileUploader").trigger("click");
    }

    $scope.deleteFile = function (item) {
        eventService.deleteFile(item.fileId).then(function (res) {
            if (res.statusInfo.status == "0") {
                var i = $scope.eventFiles.files.indexOf(item);
                if (i != -1) {
                    $scope.eventFiles.files.splice(i, 1);
                }
            }
        });
    }

    $("#fileUploader").onchange = function(e) { 
        uploader.uploadAll();
    };

    uploader.onCompleteItem = function (fileItem, response, status, headers) {
        console.info('onCompleteItem', fileItem, response, status, headers);
        $scope.eventFiles.files.push({
            fileId: response.fileID,
            fileName: response.fileName,
            displayName: response.displayName,
            uploadedDate: response.uploadedDate
        });
        uploader.removeFromQueue(fileItem);
    };

    uploader.onCompleteAll = function () {
        console.info('onCompleteAll');
    };

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
});