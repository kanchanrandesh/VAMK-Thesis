﻿angular.module('MetronicApp').factory('timeZoneService', function ($http, $q, notificationMsgService) {
    return {
        getAll: function () {
            var urlBase = '/api/timeZone/getAll';
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