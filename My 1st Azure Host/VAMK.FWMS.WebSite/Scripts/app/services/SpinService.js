angular.module('MetronicApp').factory('spinService', function ($http, $q) {

    return {
        spinStart: function () {
            console.log("spinStart");
        },
        spinEnd: function () {
            console.log("spinEnd");
        }
    }
});