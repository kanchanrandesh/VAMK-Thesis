angular.module('MetronicApp').factory('requestService', function ($http, $q, notificationMsgService) {
    return {
        search: function (searchQuery) {
            var urlBase = '/api/request/search';
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
            var urlBase = '/api/request/getById/' + id;
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

        save: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/request/save',
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

        accept: function (data) {
            debugger;
            var deferred = $q.defer();
            $http({
                url: '/api/request/accept',
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

        void: function (data) {
            debugger;
            var deferred = $q.defer();
            $http({
                url: '/api/request/void',
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


        issue: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/request/issue',
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
            var urlBase = '/api/request/getAll';
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