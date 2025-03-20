angular.module('MetronicApp').controller('PettyCashFinanceApprovalInstanceCtrl', function ($uibModalInstance, items) {
    var $ctrl = this;
    $ctrl.items = items.files;
    $ctrl.selected = items.files[items.defaultItemIndex];
    var slides = $ctrl.slides = [];


    $ctrl.slides.push({
        fileId: $ctrl.selected.fileId,
        fileName: "api/pettyCashVoucherAttachment/downloadPettyCashVoucherAttachmentByName/" + $ctrl.selected.fileName,
        text: 'Attachment -' + 1,
        id: 0
    });

    var inIndex = 1;
    $ctrl.items.forEach(function (item, index) {
        if (index != items.defaultItemIndex) {
            $ctrl.slides.push({
                fileId: item.fileId,
                fileName: "api/pettyCashVoucherAttachment/downloadPettyCashVoucherAttachmentByName/" + item.fileName,
                text: 'Attachment -' + index,
                id: inIndex
            });
            inIndex++;
        }
    });

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
    var currIndex = 0;
});


