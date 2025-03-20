angular.module('MetronicApp').directive('select', [function () {
    return {
        restrict: 'E',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {
            if (
                   'undefined' !== typeof attrs.type
                && 'string' === attrs.type
                && ngModel
            ) {
                ngModel.$formatters.push(function (modelValue) {
                    return modelValue.toString();
                });

                ngModel.$parsers.push(function (viewValue) {
                    return viewValue.toString();
                });
            }
        }
    }
}]);