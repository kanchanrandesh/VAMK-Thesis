angular.module('MetronicApp').factory('loginService', function ($http, $q) {
    return {
        resetPassword: function (password) {
            var urlBase = '/api/Account/ResetPassword';
            var deferred = $q.defer();
            $http({
                url: urlBase,
                data: password,
                method: 'POST',
                headers: { 'Content-Type': 'application/json' }
            }).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(status);
                });
            return deferred.promise;
        }
    }
});