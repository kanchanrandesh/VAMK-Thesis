angular.module('MetronicApp').factory('dashboardService', function ($http, $q) {
    return {

        get: function () {
            console.log('aa');
            var urlBase = '/api/dashboard/get';
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