angular.module('OnlineConfirmationApp').factory('onlineConfirmationService', function ($http, $q, ngNotify) {
    return {
        showSuccessMessage: function (message) {
            ngNotify.set(message, {
                theme: 'pure',
                position: 'top',
                duration: 3000,
                type: 'success',
                sticky: false,
                button: true,
                html: false,
            });
        },

        showErrorMessage: function (message) {
            ngNotify.set(message, {
                theme: 'pure',
                position: 'top',
                duration: 3000,
                type: 'error',
                sticky: false,
                button: true,
                html: false,
            });
        },

        getEventParticipantByReferenceKey: function (referenceKey) {
            var urlBase = '/api/event/getEventParticipantByReferenceKey/' + referenceKey;
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

        getEvent: function (id) {
            var urlBase = '/api/Event/EventDetail/' + id;
            var deferred = $q.defer();
            $http({
                url: urlBase,
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

        getAllOrganizations: function () {
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

        getAllIndustries: function () {
            var urlBase = '/api/industry/getAll';
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

        getAllCountries: function () {
            var urlBase = '/api/country/getAll';
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

        saveEventParticipant: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/event/saveForOnlineConfirmation',
                method: 'POST',
                data: data,
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
