angular.module('MetronicApp').factory('medicalClaimService', function ($http, $q, notificationMsgService) {
    return {
        search: function (searchQuery) {
            var urlBase = '/api/medicalClaim/search';
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

        searchForMedicalClaimApproval: function (searchQuery) {
            var urlBase = '/api/medicalClaim/searchForMedicalClaimApproval';
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

        searchMedicalClaimApprovalCount: function (searchQuery) {
            var urlBase = '/api/medicalClaim/searchMedicalClaimApprovalCount';
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

        checkApprovalEligibility: function (searchQuery) {
            var urlBase = '/api/medicalClaim/checkApprovalEligibility/';
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
        
        searchForMedicalClaimDisbursement: function (searchQuery) {
            var urlBase = '/api/medicalClaim/searchForMedicalClaimDisbursement';
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
            var urlBase = '/api/medicalClaim/getById/' + id;
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

        saveRequst: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/medicalClaim/saveRequst',
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

        approveMedicalClaim: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/medicalClaim/approveMedicalClaim',
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

        checkMedicalClaim: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/medicalClaim/checkMedicalClaim',
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

        rejectMedicalClaim: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/medicalClaim/rejectMedicalClaim',
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

        medicalClaimHRApproval: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/medicalClaim/medicalClaimHRApproval',
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

        disburse: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/medicalClaim/disburse',
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
                url: '/api/medicalClaim/deleteData',
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

        getForView: function (id) {
            var urlBase = '/api/medicalClaim/getForView/' + id;
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

        finApproval: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/medicalClaim/finApproval',
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

        getForDisbursement: function (id) {
            var urlBase = '/api/medicalClaim/getForDisbursement/' + id;
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

        getForDisbursementView: function (id) {
            var urlBase = '/api/medicalClaim/getForDisbursementView/' + id;
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

        printMedicalClaim: function (id) {
            var urlBase = '/api/medicalClaim/printMedicalClaim/' + id;
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
                    if (status == "401") {
                        notificationMsgService.showErrorMessage("You are not authorized to access this function");
                    }
                });
            return deferred.promise;
        },

        printMedicalClaimReport: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/medicalClaim/printMedicalClaimReport',
                method: 'POST',
                data: data,
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
                    if (status == "401") {
                        notificationMsgService.showErrorMessage("You are not authorized to access this function");
                    }
                });
            return deferred.promise;
        },

        printMedicalClaimFinanceReport: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/medicalClaim/printMedicalClaimFinanceReport',
                method: 'POST',
                data: data,
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
                    if (status == "401") {
                        notificationMsgService.showErrorMessage("You are not authorized to access this function");
                    }
                });
            return deferred.promise;
        },

        getTxHistory: function (medicalClaimId) {
            var urlBase = '/api/medicalClaim/getTxHistory/' + medicalClaimId;
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

        getMedicalClaimAttachments: function (id) {
            var urlBase = '/api/medicalClaimAttachment/getMedicalClaimAttachments/' + id;
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
        
        deleteMedicalClaimAttachment: function (id) {
            var urlBase = '/api/medicalClaimAttachment/deleteMedicalClaimAttachment/' + id;
            var deferred = $q.defer();
            $http({
                url: urlBase,
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json' }
            }).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                    if (status == "401") {
                        notificationMsgService.showErrorMessage("You are not authorized to access this function");
                    }
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(status);
                });
            return deferred.promise;
        },

        deleteMedicalClaimAttachmentByName: function (name) {
            var urlBase = '/api/medicalClaimAttachment/deleteMedicalClaimAttachmentByName/' + name;
            var deferred = $q.defer();
            $http({
                url: urlBase,
                method: 'DELETE',
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

        getDisplayCount: function () {
            var urlBase = '/api/medicalClaim/getDisplayCount';
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

        getDisplayCounts: function (employeeId) {
            var urlBase = '/api/medicalClaim/getDisplayCounts/' + employeeId;
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

        cancelMedicalClaim: function (id) {
            var urlBase = '/api/medicalClaim/cancelRequest/' + id;
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

        getAllReimbursementNumbers: function () {
            var urlBase = '/api/medicalClaim/getAllReimbursementNumbers';
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
    }
});