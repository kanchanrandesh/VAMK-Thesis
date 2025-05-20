angular.module('MetronicApp').factory('employeeService', function ($http, $q, notificationMsgService) {
    return {
        search: function (searchQuery) {
            var urlBase = '/api/employee/search';
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
            var urlBase = '/api/employee/getById/' + id;
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
                url: '/api/employee/save',
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

        saveEmployeeFamilyMembers: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/employee/saveEmployeeFamilyMembers',
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
            var urlBase = '/api/employee/getAll';
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

        getFamilyMembers: function (employeeId) {
            var urlBase = '/api/employee/getFamilyMembers/' + employeeId;
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

        getFamilyMembersForMedicalClaim: function (employeeId) {
            var urlBase = '/api/employee/getFamilyMembersForMedicalClaim/' + employeeId;
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

        saveLockedEmployees: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/employee/saveLockedEmployees',
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