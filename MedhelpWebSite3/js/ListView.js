document.addEventListener('contextmenu', function (e) {
    e.preventDefault();
}); 
$(document).ready(function () {
    $(window).resize(function () {
        //SetSearchBlockStartPosition()
        ResetHiddenRecordsVisibility();
    });
  

    function ResetHiddenRecordsVisibility() {
        if ($(window).width() >= 320 || $(window).height() >= 320) {
            $('.price-list-view').children().find('#SecondHiddenRecord').each(function (index, elem) {
                if ($(elem).hasClass('show-record'))
                    $(elem).removeClass('show-record');  
            });
            if ($(window).width() >= 320 && $(window).height() >= 320) return true;
        }
        $('.price-list-view').children().find('#FirstHiddenRecord').each(function (index, elem) {
            $(elem).removeClass('show-record');
            //console.log("видимость Record1 " + "индекс " + index + " = " + $(elem).css('visibility'));
            //if ($(elem).css('visibility') != 'collapse') {
            //    if ($(elem).children().find('.comment').prop('innerText') == "")
            //        $(elem).removeClass('show-record');
            //    else
            //        $(elem).addClass('show-record');
            //}
            //else
            //    $(elem).removeClass('show-record');
        });

        $('.price-list-view').children().find('#SecondHiddenRecord').each(function (index, elem) {
            $(elem).removeClass('show-record');
            $(elem).children().find('.additional-appointment-button-container').each(function (index2, elem2) {
                console.log($(elem2));
                $(elem2).css('visibility', 'collapse');
            });
            //console.log("видимость Record2 " + "индекс " + index + " = " + $(elem).css('visibility'));
            //console.log($(elem));
            //if ($(elem).css('visibility') != 'collapse') {
            //    if ($(elem).find('.additional-appointment-button-container').css('visibility') == 'collapse')
            //        $(elem).removeClass('show-record');
            //    else {
            //        $(elem).addClass('show-record');
            //        $(elem).find('.additional-appointment-button-container').css('height', '50px');
            //    }
            //}
            //else
            //    if ($(window).width() < 320) {
            //        $(elem).addClass('show-record');
            //    }
            //else
            //    $(elem).removeClass('show-record');
        });
    }
    //SetSearchBlockStartPosition();

    //function SetSearchBlockStartPosition() {
    //    var block = $('.block-start-position');
    //    block.css('width', $('#MainContentPlaceHolder_backgroundDiv').width() - 30);
    //    block.css('left', 'Calc(50% - ' + block.width() / 2 + 'px)');
    //}
});
$(function () {
    $('.price-list-view').bind("click", function (e) {
        function handleClick(element) {
            var hiddenRecord = $(element).find('.hiddenRecord');

            if (hiddenRecord.length > 0) {
                // Если комментарий есть, то разворачиваем блок
                hiddenRecord.toggleClass('hidden-record');
            } else { 
                var serviceId = $(element).find('.service-price-td').attr('data-service-id'); 
                console.log('Переход без комментария');
            }
        }

        handleClick(this);

    }); 
});
$(function () {
    $('.price-list-view').bind("click", handleMouseClick); 
    //function handleMouseClick(e) {
    //    var hiddenRecord = $(this).children().find('.hiddenRecord');
    //    console.log(hiddenRecord);
    //    if ($(window).width() < 320) {  
    //        if (hiddenRecord.children().find('.comment').prop('innerText') == "")
    //            hiddenRecord[0].removeClass('show-record');
    //    }
    //    else
    //        hiddenRecord[0].toggleClass('show-record');
    //    hiddenRecord[1].toggleClass('show-record');   
    //    //hiddenRecord.toggleClass("show-record");
    //}
    function handleMouseClick(e) {
        var hiddenRecord1 = $(this).children().find('#FirstHiddenRecord');
        var hiddenRecord2 = $(this).children().find('#SecondHiddenRecord');
        hiddenRecord1.toggleClass('show-record');
        if ($(window).width() < 320) {
            if (hiddenRecord1.children().find('.comment').prop('innerText') == "")
                hiddenRecord1.removeClass('show-record');
        }
        hiddenRecord2.toggleClass('show-record');
        if (hiddenRecord2.find('.additional-appointment-button-container').css('visibility') == 'collapse' || hiddenRecord2.children().find('.simple-button').length == 0)
            hiddenRecord2.removeClass('show-record');
        //hiddenRecord.toggleClass("show-record");
    }
});