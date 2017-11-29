// function for Framework
$(document).ready(function () {
    $.fn.frameWorkBox = function () {
        var link = $(this).find('.framework-link-list li>a');
        var defaultTitle = $(this).find('.framework-box > h4').html();
        var defaultContent = $(this).find('.framework-box > p').html();
        var pdctTag = $("#industryTagId").val();

        $(link).on('click', function (e) {
            if (!$(this).hasClass('active')) {
                e.preventDefault();
                $(link).removeClass('active');
                $(this).toggleClass('active');

                pdctSinglePhase = $(this).data('id');
                var data = { type: 'productlist', pdctTag: pdctTag, pdctSinglePhase: pdctSinglePhase };
                callAjaxProduct(data);


                var box = $(this).parents('.framework-box-container').find('.framework-box');
                var title = $(this).data('title');
                var content = $(this).data('content');
                var href = $(this).attr('href');
                var linkText = $(this).data('linkname');
                $(box).find('.link').fadeIn('slow');
                $(box).find('#close-desc').fadeIn('slow');
                $(box).find('h4 a').hide().html(title).fadeIn('slow');
                $(box).find('p').hide().html(content).fadeIn('slow');
                $(box).find('.link').attr({ 'href': href });
                $(box).find('.title-link').attr({ 'href': href });
                $(box).find('#framework-desc-link').text(linkText);
                $(box).find('h4 .title-link').addClass('clr-drk-blu');
                $(box).find('h4 .title-link').removeClass('dark-color');
                $(box).find("h4 .title-link").css("pointer-events", "initial");
            }
        });

        $(this).find('#close-desc').on('click', function () {
            pdctSinglePhase = null;
            var data = { type: 'productlist', pdctTag: pdctTag, pdctSinglePhase: pdctSinglePhase };
            callAjaxProduct(data);
            var box = $(this).parents('.framework-box-container').find('.framework-box');
            var dif_title = $(this).data('title');
            var dif_content = $(this).data('content');
            $(box).find('h4 a').hide().html(dif_title).fadeIn('slow');
            $(box).find('p').hide().html(dif_content).fadeIn('slow');
            $(box).find('.link').fadeOut('slow');
            $(this).fadeOut('slow');
            $('.framework-link-list li a').removeClass('active');
            $(box).find("h4 .title-link").css("pointer-events", "none");
            $(box).find('h4 .title-link').removeClass('clr-drk-blu');
            $(box).find('h4 .title-link').addClass('dark-color');
        });

    };

    $('.framework-box-container').frameWorkBox();
});
function callAjaxProduct(data) {
    //$(".loadmoregif").show();
    $.ajax({
        url: "/industryAjaxHandler/",
        type: "POST",
        data: data
    }).success(function (msg) {
        if (msg != null) {
            $(".bottom-prdct-listing .three-column-link-listing").html('');
            $(".bottom-prdct-listing .three-column-link-listing").append(msg);
            //alert(msg);

        }
    }).complete(function () {
        //$(".loadmoregif").hide();
    });

}
function callAjaxSuccessStories(data) {
    //$(".loadmoregif").show();
    $.ajax({
        url: "/industryAjaxHandler/",
        type: "POST",
        data: data
    }).success(function (msg) {
        if (msg != null) {
            $(".single-slider-wrapper").html('');
            $(".single-slider-wrapper").append(msg);
            //alert(msg);

        }
    }).complete(function () {
        //$(".loadmoregif").hide();
    });

}