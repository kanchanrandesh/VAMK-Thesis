angular.module('MetronicApp').factory('accountService', function ($http, $q, notificationMsgService) {
    return {
        changePassword: function (data) {
            var urlBase = '/api/Account/ChangePassword';
            var deferred = $q.defer();
            $http({
                url: urlBase,
                data: data,
                method: 'POST',
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
        }
    }
});