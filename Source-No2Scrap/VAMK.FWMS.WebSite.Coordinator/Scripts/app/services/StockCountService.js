angular.module('MetronicApp').factory('StockCountService', function ($http, $q, notificationMsgService) {
    return {
        
        printStockCountReport: function (data) {
            var deferred = $q.defer();
            $http({
                url: '/api/stockCount/printStockCountReport',
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
    }
});