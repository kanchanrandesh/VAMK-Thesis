angular.module('MetronicApp').factory('notificationMsgService', function (ngNotify) {
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
    }
});