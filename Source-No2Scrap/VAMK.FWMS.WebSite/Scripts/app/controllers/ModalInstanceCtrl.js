angular.module('MetronicApp').controller('ModalInstanceCtrl', function ($uibModalInstance, items) {
    var $ctrl = this;
    $ctrl.items = items;
    $ctrl.selected = {
        item: $ctrl.items[0]
    };

    $ctrl.ok = function () {
        $uibModalInstance.close();
    };

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    //Carousel
    $ctrl.myInterval = 5000;
    $ctrl.noWrapSlides = false;
    $ctrl.active = 0;
    var slides = $ctrl.slides = [];
    var currIndex = 0;

    $ctrl.slides.push({
        image: 'api/Event/downloadFile/105',
        text: ['Nice image', 'Awesome photograph', 'That is so cool', 'I love that'],
        id: currIndex++
    });
    $ctrl.slides.push({
        image: 'api/Event/downloadFile/105',
        text: ['Nice image', 'Awesome photograph', 'That is so cool', 'I love that'],
        id: currIndex++
    });

});
