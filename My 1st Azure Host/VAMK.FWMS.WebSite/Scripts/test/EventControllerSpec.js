describe('EventListController', function () {

    var scope, $controllerConstructor, mockEventData;

    beforeEach(module('MetronicApp'));

    
     beforeEach(inject(function ($controller, $rootScope) {
         scope = $rootScope.$new();
         mockEventData = new sinon.stub({ getEventList: function () { } })
         $controllerConstructor = $controller;
     }));

    it('Should return Event List', function () {
        var ctrl = $controllerConstructor('EventListController', {
            $rootScope: {},
            $scope: scope,
            eventService: mockEventData
        });

        var mockeventList = {};
        mockEventData.getEventList.returns($q.when(mockeventList));

        expect(1).toBe(4);
	});
});