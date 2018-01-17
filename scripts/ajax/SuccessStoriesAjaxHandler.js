var takeCount = 8;
var ccfphaseFilter = "-1";
var newsId = $(".cd-tabs-content").data('id');
var industryId = "";
var page = 0;

$(document).ready(function () {
    $(".no-items").hide();
    $('#ccfPhasefilter').val('null');
    var data = null;
    if ($(".loadmore .loadmore-link").length > 0) {
        $(".loadmore .loadmore-link").click(function (e) {
            e.preventDefault();
            newsId = $(this).data('id');
			console.log(newsId);
            page = $(this).data('page');
			ccfphaseFilter = $(this).parents('.article-list-wrapper').first().find('.ccfPhasefilter').val();
			industryId = $(this).closest('.article-list-wrapper').data('id');
            var data = { newsId: newsId, industryId: industryId, ccfphaseFilter: ccfphaseFilter, takeCount: takeCount, page: page };
            callAjax(data, 'btn')
        });
    }
    $(".cd-tabs-navigation li").click(function (e) {
        var datacontent = $(this).find('a').data('content');
        ccfphaseFilter = $(this).closest('.aside-tab').find('.cd-tabs-content').find("[data-content='" + datacontent + "']").find('.ccfPhasefilter').val();
        page = 0;
        //$(this).closest('.aside-tab').find('.cd-tabs-content').find("[data-content='" + datacontent + "']").find('.article-list-wrapper').find(".loadmore .loadmore-link").data('page');
        takeCount = 4;
        industryId = $(this).closest('.aside-tab').find('.cd-tabs-content').find("[data-content='" + datacontent + "']").find('.article-list-wrapper').data('id');
        var data = { newsId: newsId, industryId: industryId, ccfphaseFilter: ccfphaseFilter, takeCount: takeCount, page: page };
        callAjax(data, 'filter');
    });
    $(".ccfPhasefilter").change(function () {
        ccfphaseFilter = $(this).val();
        takeCount = 4;
		page=0;
        industryId = $(this).closest('.article-list-wrapper').data('id');
        var data = { newsId: newsId, industryId: industryId, ccfphaseFilter: ccfphaseFilter, takeCount: takeCount, page: page };
        callAjax(data, 'filter');
    });

    var pageId = $(".filter-section .form").data('id');
});


function callAjax(data, calltype) {
    $('.post-content-wrapper').addClass('onloading');
    $.ajax({
        url: "/successajaxhandler/",
        type: "POST",
        data: data
    }).success(function (msg) {
        if (msg != null) {
            $(".event-listing ul").remove();
            $(".event-listing div").remove();
            $(".event-listing p").remove();
            $(".event-listing").prepend(msg);
            $(".loadmore .loadmore-link").click(function (e) {
                e.preventDefault();
                industryId = $(this).closest('.article-list-wrapper').data('id');
                newsId = $(this).data('id');
                page = $(this).data('page');
                var data = { newsId: newsId, industryId: industryId, ccfphaseFilter: ccfphaseFilter, takeCount: takeCount, page: page };
                callAjax(data, 'btn');
            });
            takeCount = takeCount + 4;
        }
    }).complete(function () {
        $('.post-content-wrapper').removeClass('onloading');
    });

}