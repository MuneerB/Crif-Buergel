var takeCount = 8;
var docFilter = "all";
var yearFilter = "-1";
var newsId = $(".filter-list").data('id');
var page = 0;
var searchText = "";
var filtertag = "";

$(document).ready(function () {
    $(".no-items").hide();
    $('#newsfilter').val('null');
    var data = null;
    if ($(".loadmore .loadmore-link").length > 0) {
        $(".loadmore .loadmore-link").click(function (e) {
            e.preventDefault();
            newsId = $(this).data('id');
            page = $(this).data('page');
            searchText = "";
            var data = { newsId: newsId, docFilter: docFilter, takeCount: takeCount, searchText: searchText, yearFilter: yearFilter, page: page };
            callAjax(data, 'btn');
            $('.search-filter-wrapper').removeClass("wayforform");
            $('#filtertextbox').val("");
        });
    }

    $(".filtertag a").click(function (e) {
        e.preventDefault();
        takeCount = 4;
		page=1;
        docFilter = $(this).attr('id');
        searchText = "";
        var data = { newsId: newsId, docFilter: docFilter, takeCount: takeCount, searchText: searchText, yearFilter: yearFilter, page: page };
        callAjax(data, 'filter');
        $(".filtertag").removeClass("active");
        $(this).parent().addClass("active");
        if ($('.search-filter-wrapper').hasClass) {
            $('.search-filter-wrapper').removeClass("wayforform");
            $('#filtertextbox').val("");
        }
    });
    $("#filtersearch").click(function (e) {
		e.preventDefault();
        searchText = $("input[name='txtfiltersearch']").val();
		console.log($("input[name='txtfiltersearch']").val());
		page=1;
        takeCount = 4;
        var data = { newsId: newsId, docFilter: docFilter, takeCount: takeCount, searchText: searchText, yearFilter: yearFilter, page: page };
        callAjax(data, 'filter');
        $('.search-filter-wrapper').addClass("wayforform");
    });
	$("#filtersearchmob").click(function (e) {
		e.preventDefault();
        searchText = $("input[name='txtfiltersearchmob']").val();
		console.log($("input[name='txtfiltersearchmob']").val());
		page=1;
        takeCount = 4;
        var data = { newsId: newsId, docFilter: docFilter, takeCount: takeCount, searchText: searchText, yearFilter: yearFilter, page: page };
        callAjax(data, 'filter');
        $('.search-filter-wrapper').addClass("wayforform");
    });
    $("#filtertagmob").change(function () {
        if ($(this).find('option:selected').hasClass("sing-page")) {
            var url = $(this).val(); // get selected value
            if (url) { // require a URL
                window.location = url; // redirect
            }
            return false;
        }
        else {
            docFilter = $(this).val();
            takeCount = 4;
			page=1;
            searchText = "";
            var data = { newsId: newsId, docFilter: docFilter, takeCount: takeCount, searchText: searchText, yearFilter: yearFilter, page: page };
            callAjax(data, 'filter');
            if ($('.search-filter-wrapper').hasClass) {
                $('.search-filter-wrapper').removeClass("wayforform");
                $('#filtertextbox').val("");
            }
        }
    });
    $("#newsfilter").change(function () {
        yearFilter = $(this).val();
        takeCount = 4;
		page=1;
        searchText = "";
        var data = { newsId: newsId, docFilter: docFilter, takeCount: takeCount, searchText: searchText, yearFilter: yearFilter, page: page };
        callAjax(data, 'filter');
        if ($('.search-filter-wrapper').hasClass) {
            $('.search-filter-wrapper').removeClass("wayforform");
            $('#filtertextbox').val("");
        }
    });

    var pageId = $(".filter-section .form").data('id');
});


function callAjax(data, calltype) {
    $('.post-content-wrapper').addClass('onloading');
    $.ajax({
        url: "/ajaxhandler/",
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
                newsId = $(this).data('id');
                page = $(this).data('page');
                var data = { newsId: newsId, docFilter: docFilter, yearFilter: yearFilter, page: page };
                callAjax(data, 'btn');
            });
            takeCount = takeCount + 4;
        }
    }).complete(function () {
        $('.post-content-wrapper').removeClass('onloading');
    });

}