﻿angular.module('MetronicApp').directive('showModel', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs, controller) {
            element.on('click', function (event) {
                alert('hhhhhhhhh');
                return true;
            });
        }
    }
});

MetronicApp.directive('convertToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return val != null ? parseInt(val, 10) : null;
            });
            ngModel.$formatters.push(function (val) {
                return val != null ? '' + val : null;
            });
        }
    };
});