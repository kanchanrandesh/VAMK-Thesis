angular.module('MetronicApp').factory('confidenceLevelCriteriaService', function ($http, $q, notificationMsgService) {
    return {
        search: function (searchQuery) {
            var urlBase = '/api/confidenceLevelCriteria/search';
            var deferred = $q.defer();
            $http({
                url: urlBase,
                data: searchQuery,
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
        },

        getById: function (id) {
            var urlBase = '/api/confidenceLevelCriteria/getById/' + id;
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
                });
            return deferred.promise;
        },

        save: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/confidenceLevelCriteria/save',
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

        getAll: function () {
            var urlBase = '/api/confidenceLevelCriteria/getAll';
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