MetronicApp.directive('integersOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            element.bind('keypress', function () {
            });

            modelCtrl.$parsers.push(function (inputValue) {
                if (inputValue == undefined)
                    return '';

                var transformedInput = inputValue.replace(/[^0-9]/g, '');
                if (transformedInput != inputValue
                    || (parseInt(transformedInput, 10) < parseInt(attrs.ngMin, 10))
                    || parseInt(transformedInput, 10) > parseInt(attrs.ngMax, 10)) {

                    if (transformedInput != inputValue) {
                        modelCtrl.$setValidity('nonnumeric', false);
                        transformedInput = '';
                    } else {
                        modelCtrl.$setValidity('nonnumeric', true);
                    }
                    if (parseInt(transformedInput, 10) < parseInt(attrs.ngMin, 10)) {
                        modelCtrl.$setValidity('belowminimum', false);
                        transformedInput = attrs.ngMin.toString();
                    } else {
                        modelCtrl.$setValidity('belowminimum', true);
                    }
                    if (parseInt(transformedInput, 10) > parseInt(attrs.ngMax, 10)) {
                        modelCtrl.$setValidity('abovemaximum', false);
                        transformedInput = attrs.ngMax.toString();
                    } else {
                        modelCtrl.$setValidity('abovemaximum', true);
                    }

                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                    return transformedInput;
                }
                modelCtrl.$setValidity('nonnumeric', true);
                modelCtrl.$setValidity('belowminimum', true);
                modelCtrl.$setValidity('abovemaximum', true);
                return transformedInput;
            });
        }
    };
});

MetronicApp.directive('decimalsOnly', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            if (!modelCtrl) {
                return;
            }

            modelCtrl.$parsers.push(function (inputValue) {
                if (angular.isUndefined(inputValue)) {
                    var inputValue = '';
                }

                var transformedInput = inputValue.replace(/[^-0-9\.]/g, '');
                var negativeCheck = transformedInput.split('-');
                var decimalCheck = transformedInput.split('.');
                if (!angular.isUndefined(negativeCheck[1])) {
                    negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                    transformedInput = negativeCheck[0] + '-' + negativeCheck[1];
                    if (negativeCheck[0].length > 0) {
                        transformedInput = negativeCheck[0];
                    }
                }

                if (!angular.isUndefined(decimalCheck[1])) {
                    decimalCheck[1] = decimalCheck[1].slice(0, 2);
                    transformedInput = decimalCheck[0] + '.' + decimalCheck[1];
                }

                if (transformedInput != inputValue) {
                    modelCtrl.$setValidity('nonnumeric', false);
                    //transformedInput = '';
                } else {
                    modelCtrl.$setValidity('nonnumeric', true);
                }
                if (parseFloat(transformedInput, 10) < parseFloat(attrs.ngMin, 10)) {
                    modelCtrl.$setValidity('belowminimum', false);
                    transformedInput = attrs.ngMin.toString();
                } else {
                    modelCtrl.$setValidity('belowminimum', true);
                }
                if (parseFloat(transformedInput, 10) > parseFloat(attrs.ngMax, 10)) {
                    modelCtrl.$setValidity('abovemaximum', false);
                    transformedInput = attrs.ngMax.toString();
                } else {
                    modelCtrl.$setValidity('abovemaximum', true);
                }

                modelCtrl.$setViewValue(transformedInput);
                modelCtrl.$render();
                return transformedInput;
            });

            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});