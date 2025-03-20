angular.module('MetronicApp').factory('eventService', function ($http, $q, notificationMsgService) {
    return {
        searchEvent: function (eveSearch) {
            var urlBase = '/api/event/eventSearch';
            var deferred = $q.defer();

            $http({
                url: urlBase,
                data: eveSearch,
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

        getEvent: function (id) {
            var urlBase = '/api/Event/' + id;
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

        addEvent: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/Event',
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

        updateEvent: function (data, id) {
            var deferred = $q.defer();
            $http({
                url: '/api/Event/' + id,
                method: 'PUT',
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

        getEventTest: function (id) {
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

        getEventProxy: function (id) {
            var urlBase = '/api/Event/EventProxy/' + id;
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

        getEventTracks: function (eventId) {
            var urlBase = '/api/Event/EventTracks/' + eventId;
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

        getEventTrackSessions: function (eventTrackId) {
            var urlBase = '/api/Event/TrackSessions/' + eventTrackId;
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

        fillFromPreviousSession: function (eventTrackId) {
            var urlBase = '/api/Event/fillFromPreviousSession/' + eventTrackId;
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

        searchInvitees: function (searchQuery) {
            var urlBase = '/api/event/searchInvitees';
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

        searchTrackParticipants: function (searchQuery) {
            var urlBase = '/api/event/searchTrackParticipants';
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

        getEventParticipant: function (id) {
            var urlBase = '/api/event/getEventParticipant/' + id;
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

        getTrackSessionParticipatedCount: function (eventTrackId) {
            var urlBase = '/api/event/getTrackSessionParticipatedCount/' + eventTrackId;
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
                url: '/api/event/saveEventParticipant',
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

        saveTrackSessionParticipant: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/event/saveTrackSessionParticipant',
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

        saveEventAddEdit: function (event) {
            var urlBase = '/api/event/SaveEventAddEdit';
            var deferred = $q.defer();
            $http({
                url: urlBase,
                data: event,
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
        },

        exportToExcel: function (eventId) {
            var urlBase = '/api/Event/export/' + eventId;
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

        exportToExcel: function (eventId) {
            var urlBase = '/api/Event/export/' + eventId;
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

        searchNotInvitedContacts: function (searchQuery) {
            var urlBase = '/api/event/searchNotInvitedContacts';
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

        addToEventParticipant: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/event/addToEventParticipant',
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

        getEventCheckList: function (eventId) {
            var urlBase = '/api/event/getEventCheckList/' + eventId;
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

        updateEventCheckList: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/event/updateEventCheckList',
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

        printAllInvitees: function (eventId) {
            var urlBase = '/api/Event/printAllInvitees/' + eventId;
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

        printConfirmedInvitees: function (eventId) {
            var urlBase = '/api/Event/printConfirmedInvitees/' + eventId;
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

        printParticipatedInvitees: function (eventId) {
            var urlBase = '/api/Event/printParticipatedInvitees/' + eventId;
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

        printEventSummary: function (eventId) {
            var urlBase = '/api/Event/printEventSummary/' + eventId;
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

        getEventThumbnail: function (eventId) {
            var urlBase = '/api/event/getEventThumbnail/' + eventId;
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

        getEventFiles: function (id) {
            var urlBase = '/api/Event/getEventFiles/' + id;
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

        deleteFile: function (id) {
            var urlBase = '/api/Event/deleteFile/' + id;
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
        }
    }
});
