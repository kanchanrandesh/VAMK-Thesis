angular.module('MetronicApp').factory('projectUnitService', function ($http, $q, notificationMsgService) {
    return {
        getAll: function (employeeId) {
            var urlBase = '/api/projectUnit/getAll/' + employeeId;
            var deferred = $q.defer();
            $http({
                url: urlBase,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            }).
            error(function (data, status, headers, config) {
                deferred.reject(status);
            });
            return deferred.promise;
        },

        getAllProjectsAndUnits: function () {
            var urlBase = '/api/projectUnit/getAllProjectsAndUnits/';
            var deferred = $q.defer();
            $http({
                url: urlBase,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(status);
                });
            return deferred.promise;
        },
    }
});