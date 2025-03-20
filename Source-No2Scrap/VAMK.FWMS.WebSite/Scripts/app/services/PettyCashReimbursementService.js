angular.module('MetronicApp').factory('pettyCashReimbursementService', function ($http, $q, notificationMsgService) {
    return {
        search: function (searchQuery) {
            var urlBase = '/api/pettyCashReimbursement/search';
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
            var urlBase = '/api/pettyCashReimbursement/getById/' + id;
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

        getAll: function () {
            var urlBase = '/api/pettyCashReimbursement/getAll';
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

        getCompanyPettyCashReimbursementSummary: function () {
            var urlBase = 'api/pettyCashReimbursement/getCompanyPettyCashReimbursementSummary';
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

        getPettyCashReimbursement: function (id) {
            var urlBase = 'api/pettyCashReimbursement/getById/' + id;
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

        processReimbursement: function (companyid,processedById) {
            var urlBase = 'api/pettyCashReimbursement/processReimbursement/' + companyid + '/' + processedById;
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
                if (status == "401") {
                    notificationMsgService.showErrorMessage("You are not authorized to access this function");
                }
            });
            return deferred.promise;
        },

        reProcessReimbursement: function (reimbursementId, processedById) {
            var urlBase = 'api/pettyCashReimbursement/reProcessReimbursement/' + reimbursementId + '/' + processedById;
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
                if (status == "401") {
                    notificationMsgService.showErrorMessage("You are not authorized to access this function");
                }
            });
            return deferred.promise;
        },

        printReimbursementDetail: function (id) {
            var urlBase = '/api/pettyCashReimbursement/printReimbursementDetail/' + id;
            var deferred = $q.defer();
            $http({
                url: urlBase,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                responseType: 'arraybuffer'
            })
            .success(function (data, status, headers) {
                headers = headers();
                var filename = headers['x-filename'];
                var contentType = headers['content-type'];
                var linkElement = document.createElement('a');
                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = window.URL.createObjectURL(blob);
                    linkElement.setAttribute('href', url);
                    linkElement.setAttribute("download", filename);

                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });

                    linkElement.dispatchEvent(clickEvent);
                    deferred.resolve(data);

                } catch (ex) {
                    deferred.reject(data);
                }
            })
            .error(function (data) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        printReimbursementVouchers: function (id) {
            var urlBase = '/api/pettyCashReimbursement/printReimbursementVouchers/' + id;
            var deferred = $q.defer();
            $http({
                url: urlBase,
                method: 'GET',
                headers: { 'Content-Type': 'application/json' },
                responseType: 'arraybuffer'
            })
            .success(function (data, status, headers) {
                headers = headers();
                var filename = headers['x-filename'];
                var contentType = headers['content-type'];
                var linkElement = document.createElement('a');
                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = window.URL.createObjectURL(blob);
                    linkElement.setAttribute('href', url);
                    linkElement.setAttribute("download", filename);

                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });

                    linkElement.dispatchEvent(clickEvent);
                    deferred.resolve(data);

                } catch (ex) {
                    deferred.reject(data);
                }
            })
            .error(function (data) {
                deferred.reject(data);
            });
            return deferred.promise;
        },

        reimburse: function (reimbursedata) {
            var urlBase = '/api/pettyCashReimbursement/reimburse';
            var deferred = $q.defer();
            $http({
                url: urlBase,
                data: reimbursedata,
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