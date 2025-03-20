/*
Created by Alex Klibisz, aklibisz@utk.edu
February 2015
*/

var a;
a = angular.module("dualmultiselect", []), a.directive("dualmultiselect", [function () {
    return {
        restrict: 'E',
        scope: {
            options: '='
        },
        controller: function ($scope) {
            $scope.transfer = function (from, to, index) {
                if (index >= 0) {
                    to.push(from[index]);
                    from.splice(index, 1);
                } else {
                    for (var i = 0; i < from.length; i++) {
                        to.push(from[i]);
                    }
                    from.length = 0;
                }
            };
        },
        template: '<div class="dualmultiselect"> <div class="row"> <div class="col-lg-2 col-md-2 col-sm-2 caption font-green-sharp"><label class="caption-subject bold uppercase">{{options.labelSelected}}</label> </div> <div class="col-lg-4 col-md-4 col-sm-4"> <button type="button" class="btn btn-default btn-xs" ng-click="transfer(options.selectedItems, options.items, -1)"> Deselect All </button> </div> <div class="col-lg-2 col-md-2 col-sm-2 caption font-green-sharp"> <label class="caption-subject bold uppercase">{{options.labelAll}}</label> </div> <div class="col-lg-4 col-md-4 col-sm-4"> <button type="button" class="btn btn-default btn-xs" ng-click="transfer(options.items, options.selectedItems, -1)"> Select All </button> </div></div> <div class="row"> <div class="col-lg-1 col-md-1 col-sm-1" style="padding-left:20px;"><div><div class="dashboard-stat blue"> <div class="visual number" style="height:20px; font-size:20px; padding-top:0; color:white; text-align:right; padding-right:32px;">{{options.selectedItems.length}}</div></div></div></div> <div class="col-lg-5 col-md-5 col-sm-5"> </div> <div class="col-lg-6 col-md-6 col-sm-6" style="padding-left:20px; padding-right:20px;"> <input class="form-control" placeholder="{{options.filterPlaceHolder}}" ng-model="searchTerm"> </div></div><div class="row"> <div class="col-lg-6 col-md-6 col-sm-6">  <div class="pool"> <ul> <li ng-repeat="item in options.selectedItems | orderBy: options.orderProperty"> <a href="" ng-click="transfer(options.selectedItems, options.items, options.selectedItems.indexOf(item))"> {{item.name}}</a> </li></ul> </div></div> <div class="col-lg-6 col-md-6 col-sm-6"> <div class="pool"> <ul> <li ng-repeat="item in options.items | filter: searchTerm | orderBy: options.orderProperty"> <a href="" ng-click="transfer(options.items, options.selectedItems, options.items.indexOf(item))">{{item.name}} </a> </li></ul> </div></div></div></div>'
    };
}]);