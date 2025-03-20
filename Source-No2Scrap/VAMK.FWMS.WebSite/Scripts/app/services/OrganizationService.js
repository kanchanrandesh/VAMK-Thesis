angular.module('MetronicApp').factory('organizationService', function ($http, $q, notificationMsgService) {
    return {
        search: function (searchQuery) {
            var urlBase = '/api/organization/search';
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
            var urlBase = '/api/organization/getById/' + id;
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
                url: '/api/organization/save',
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
       
        deleteData: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/organization/deleteData',
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
            var urlBase = '/api/organization/getAll';
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

        getAllCustomers: function () {
            var urlBase = '/api/organization/getAllCustomers';
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

        exportToExcel: function () {
            var urlBase = '/api/organization/export/';
            var deferred = $q.defer();
            $http({
                url: urlBase,
                method: 'GET',
                //data: 'eventId='+ eventId,
                headers: { 'Content-Type': 'application/json' }
            }).
                success(function (data) {
                    deferred.resolve(data);
                }).
                error(function (data) {
                    deferred.reject(data);
                    if (status == "401") {
                        notificationMsgService.showErrorMessage("You are not authorized to access this function");
                    }
                });
            return deferred.promise;
        },
    }
});