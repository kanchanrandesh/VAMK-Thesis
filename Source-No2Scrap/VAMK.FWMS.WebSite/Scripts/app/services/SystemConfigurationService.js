angular.module('MetronicApp').factory('systemConfigurationService', function ($http, $q, notificationMsgService) {
    return {
        getLatest: function (id) {
            var urlBase = '/api/systemConfiguration/getLatest';
            var deferred = $q.defer();
            $http({
                url: urlBase,
                //data: id,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            }).
            error(function (data, status, headers, config) {
                deferred.reject(status);
                if (status == "401") {
                    notificationMsgService.showErrorMessage("You are not authorized to access this function");
                }
            });
            return deferred.promise;
        },

        save: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/systemConfiguration/save',
                method: 'POST',
                data: data,
                headers: { 'Content-Type': 'application/json' }
            }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            }).
            error(function (data, status, headers, config) {
                deferred.reject(status);
                if (status == "401") {
                    notificationMsgService.showErrorMessage("You are not authorized to access this function");
                }
            });
            return deferred.promise;
        },
    }
});