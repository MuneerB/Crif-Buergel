var pdctTag = "";
var pdctSinglePhase = "";
var page = 0;

$(document).ready(function () {
    $('#productindustry').val('null');
    $('#productphase').val('null');
    var data = null;
    var pageId = $(".filter-section .form").data('id');
    $('#productindustry').change(function () {
        pdctTag = $(this).val();
        var data = { type: 'productlist', pageId: pageId, pdctTag: pdctTag, pdctSinglePhase: pdctSinglePhase, page: page };
        callAjaxProduct(data);
    });
    $('#productphase').change(function () {
        pdctSinglePhase = $(this).val();
        var data = { type: 'productlist', pageId: pageId, pdctTag: pdctTag, pdctSinglePhase: pdctSinglePhase, page: page };
        callAjaxProduct(data);
    });

});

function callAjaxProduct(data) {
    //$(".loadmoregif").show();
    $.ajax({
        url: "/productsAjaxHandler/",
        type: "POST",
        data: data
    }).success(function (msg) {
        if (msg != null) {
            $(".prdct-list-area").html('');
            $(".prdct-list-area").append(msg);
            //alert(msg);

        }
    }).complete(function () {
        //$(".loadmoregif").hide();
    });

}