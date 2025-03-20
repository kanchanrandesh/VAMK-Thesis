/***
GLobal Directives
***/

// Route State Load Spinner(used on page or content load)
MetronicApp.directive('ngSpinnerBar', ['$rootScope', '$state',
    function ($rootScope, $state) {
        return {
            link: function (scope, element, attrs) {
                // by defult hide the spinner bar
                element.addClass('hide'); // hide spinner bar by default

                // display the spinner bar whenever the route changes(the content part started loading)
                $rootScope.$on('$stateChangeStart', function () {
                    element.removeClass('hide'); // show spinner bar
                });

                // hide the spinner bar on rounte change success(after the content loaded)
                $rootScope.$on('$stateChangeSuccess', function (event) {
                    element.addClass('hide'); // hide spinner bar
                    $('body').removeClass('page-on-load'); // remove page loading indicator
                    Layout.setAngularJsSidebarMenuActiveLink('match', null, event.currentScope.$state); // activate selected link in the sidebar menu

                    // auto scorll to page top
                    setTimeout(function () {
                        App.scrollTop(); // scroll to the top on content load
                    }, $rootScope.settings.layout.pageAutoScrollOnLoad);
                });

                // handle errors
                $rootScope.$on('$stateNotFound', function () {
                    element.addClass('hide'); // hide spinner bar
                });

                // handle errors
                $rootScope.$on('$stateChangeError', function () {
                    element.addClass('hide'); // hide spinner bar
                });
            }
        };
    }
])

// Handle global LINK click
MetronicApp.directive('a', function () {
    return {
        restrict: 'E',
        link: function (scope, elem, attrs) {
            if (attrs.ngClick || attrs.href === '' || attrs.href === '#') {
                elem.on('click', function (e) {
                    e.preventDefault(); // prevent link click for above criteria
                });
            }
        }
    };
});

// Handle Dropdown Hover Plugin Integration
MetronicApp.directive('dropdownMenuHover', function () {
    return {
        link: function (scope, elem) {
            elem.dropdownHover();
        }
    };
});

MetronicApp.directive('codeMask', [
  function () {
      return {
          restrict: 'A',
          require: 'ngModel',
          link: function (scope, element, attrs, ctrl) {
              element.on('keypress', function (e) {

                  if ([0, 8].indexOf(e.charCode) !== -1) return;

                  var char = e.char || String.fromCharCode(e.charCode);
                  if ((!/^[A-Z0-9\-\s]$/i.test(char)) ) {
                      e.preventDefault();
                      return false;
                  }
              });

              function parser(value) {
                  if (ctrl.$isEmpty(value)) {
                      return value;
                  }
                  var formatedValue = value.toUpperCase();
                  if (ctrl.$viewValue !== formatedValue) {
                      ctrl.$setViewValue(formatedValue);
                      ctrl.$render();
                  }
                  return formatedValue;
              }

              function formatter(value) {
                  if (ctrl.$isEmpty(value)) {
                      return value;
                  }
                  return value.toUpperCase();
              }
              ctrl.$formatters.push(formatter);
              ctrl.$parsers.push(parser);
          }
      };
  }
]);
