var slider = $('.slider');
//ssssss
slider.bxSlider({
    controls: false,
    pager: ($(".slider>li").length > 1) ? true : false,
    mode: 'fade',
    auto: ($(".slider>li").length > 1) ? true : false,
    pause: $("#sliderspeed").val(),
    infiniteLoop: true,
    responsive: true,
    onSlideAfter: function (currentSlideNumber, totalSlideQty, currentSlideHtmlObject) {

        $('.active-slide').removeClass('active-slide');
        $('.slider>li').eq(currentSlideHtmlObject).addClass('active-slide');

        slider.stopAuto();
        slider.startAuto();
    },
    onSliderLoad: function () {
        $('.slider>li').eq(0).addClass('active-slide');
    },


});

$(function () {
    $('.main-slider .bx-wrapper').hover(function () {
        $('.bx-wrapper .bx-pager').css('opacity', '1');
    }, function () {
        // on mouseout, reset the background colour
        $('.bx-wrapper .bx-pager').css('opacity', '0');
    });
});


if (navigator.userAgent.match(/MSIE 9/) == null) {
    if ($('.single-slider li').length > 1) {
        $('.single-slider').owlCarousel({
            nav: true, // Show next and prev buttons
            autoplay: true,
            loop: true,
            items: 1,
            center: true,
            dots: false,
            rewindNav: true,
        });
    }

    if ($('.aside-slider li').length > 1) {
        $('.aside-slider').owlCarousel({
            nav: true, // Show next and prev buttons
            autoplay: true,
            loop: true,
            items: 1,
            dots: true,
            rewindNav: false
        });
    }

    /*$('.quote-slider').each(function () {

        if ($(this).find('li').length > 1) {
            var quote_slider = $(this);
            quote_slider.owlCarousel({
                nav: true, // Show next and prev buttons
                autoplay: true,
                loop: true,
                items: 1,
                dots: true,
                rewindNav: false,
                //autoHeight : true
            });
        }


    });*/


    var quote_slider = $('.cd-tabs-content > li:first-child .quote-slider');
    //console.log(quote_slider.length)
    quote_slider.owlCarousel({
        nav: true, // Show next and prev buttons
        autoplay: true,
        loop: true,
        items: 1,
        dots: true,
        rewindNav: false,
        //autoHeight : true
    });


    new WOW().init();

}




// For article image croping

$.fn.cropImage = function () {
    $(this).each(function () {

        var img_src = $(this).find('img').attr('src');
        $(this).find('img').hide();
        $(this).css({ 'background-size': 'cover', 'background-image': 'url(' + img_src + ')' });
    });
};

if ($('.article-image.resize-image')) {
    $('.article-image.resize-image').cropImage();
}

$('.search-toggler,#close-search').on('click', function () {
    $('#search-section').toggleClass('active');
});
// list 
if ($('.list-strctr')) {
    $('.list-strctr').each(function () {
        if (!$(this).hasClass('blue-carret-list')) {
            $(this).find('ul').addClass('normal-list list-unstyled orng-carret-list');
        } else {
            $(this).find('ul').addClass('normal-list list-unstyled blue-carret-list');
        }
        var caret = '<i class="fa fa-caret-right"></i>';
        $(this).find('li').prepend(caret);
    });
}

// navigation

$('.main-nav li.submenu > a').on('click', function () {
    if ($(this).parent().hasClass('open')) {
        $(this).parent().removeClass('open');
        return false;
    } else {
        $('.main-nav li.submenu').removeClass('open');
        $(this).parent().addClass('open');
        return false;
    }

});

$('.mobile-nav li.submenu > a').on('click', function () {
    if ($(this).parent().hasClass('open')) {
        $(this).parent().removeClass('open');
        return false;
    } else {
        $('.mobile-nav li.submenu').removeClass('open');
        $(this).parent().addClass('open');
        return false;
    }


});

$('.main-nav.collapse .submenu>a').click(function (e) {
    e.preventDefault();
    $(this).parent().toggleClass('active');
    //$(this).next('.submenu-list').slideToggle();
});

$(window).on("resize", function () {
    $('.analytic-list-grp.bg-light-grey-var-2 h4.title').height('auto');
    var tempHeight = 0;
    $('.analytic-list-grp.bg-light-grey-var-2 h4.title').each(function (i) {
        tempHeight = tempHeight < $(this).height() ?
			$(this).height() : tempHeight;
    });
    $('.analytic-list-grp.bg-light-grey-var-2 h4.title').height(tempHeight);

    if ($('.bnr-ext-img')) {
        $('.bnr-ext-img').each(function (i) {
            if ($(".bnr-ext-img").index($(this)) == 0) {
                if ($(window).width() <= 767) {
                    //($(this)).after("<hr/>");
                    jQuery("<hr class='rule' />").insertAfter(
$(this).not($(this).next('.rule').prev())
);
                }
            }
            else {
                $(this).find('.map-wrapper').css({ "padding-top": "0px" });
            }
        });
    }

    if ($(window).width() <= 992) {
        $('.owl-stage').children().height('auto');
        $('.owl-stage').parent().height('auto');

        //$('.expndbl-list>h3>a,.expndbl-list>.bck-to-menu').on('click', function (e) {
        //    if (!$(this).parents('.expndbl-list').hasClass('expand')) {
        //        e.preventDefault();
        //    }

        //    $(this).parents('.expndbl-list').toggleClass('expand');
        //    $(this).parents('.main-nav').toggleClass('overflow-visible');
        //});
        $('.accord-mob-expand a.link').css({ "position": "relative" });
        $('.accord-mob-expand').height('auto');

    } else {
        $('.owl-stage').each(function (i) {
            var me = this;
            var largestHeight = 0;
            $(me).children().each(function (i) {
                largestHeight = largestHeight < $(this).height() ?
                    $(this).height() : largestHeight;
            });
            $(me).parent().height(largestHeight);
            $(me).children().height(largestHeight);
            console.log(largestHeight);
        });
        var equalHeightAccord = "";
        $('.accord-mob-expand').each(function (i) {
            equalHeightAccord = equalHeightAccord < $(this).height() ?
                $(this).height() : equalHeightAccord;
        })
        //$('.link-lists > li').outerHeight(equalHeight);
        $('.accord-mob-expand').outerHeight(equalHeightAccord);

        var equaltProductSubmenu = "";
        $('.main-nav .submenu .submenu-list.solutions > li').each(function (i) {
            equaltProductSubmenu = equaltProductSubmenu < $(this).height() ?
                $(this).height() : equaltProductSubmenu;
        })
        //$('.link-lists > li').outerHeight(equalHeight);
        $('.main-nav .submenu .submenu-list.solutions > li').outerHeight(equaltProductSubmenu);
        $('.accord-mob-expand a.link, .link-lists > li a.link').css({ "position": "absolute", "bottom": "0px" });
    }

}).resize();
$('.expndbl-list>h3>a,.expndbl-list>.h3>a,.expndbl-list>.bck-to-menu').on('click', function (e) {
    if ($(window).width() <= 991) {
        if (!$(this).parents('.expndbl-list').hasClass('expand')) {
            e.preventDefault();
        }

        $(this).parents('.expndbl-list').toggleClass('expand');
        $(this).parents('.main-nav').toggleClass('overflow-visible');
    }
});
$(window).resize(function () {
    //$('.expndbl-box-list').find('.box-expand').addClass('accord-mob-expand');
    // $('.expndbl-box-list').css('height', '');
    if ($(window).width() <= 991) {
        $('.expndbl-list>h3>a,.expndbl-list>.h3>a,.expndbl-list>.bck-to-menu').on('click', function (e) {
            if (!$(this).parents('.expndbl-list').hasClass('expand')) {
                e.preventDefault();
            }

            $(this).parents('.expndbl-list').toggleClass('expand');
            $(this).parents('.main-nav').toggleClass('overflow-visible');
        });
    }
});
// Accordions
$('.expndbl-box-list>.h3>span ,.expndbl-box-list>h3>span').on('click', function (e) {

    if ($(this).parents('.expndbl-box-list').find('.box-expand').hasClass('accord-mob-expand')) {
        e.preventDefault();
    }

    $(this).parents('.expndbl-box-list').find('.box-expand').toggleClass('accord-mob-expand');

});
$('.analytic-box > h3').on('click', function () {
    $(this).toggleClass('open');
});
$(window).resize(function () {
    //$('.expndbl-box-list').find('.box-expand').addClass('accord-mob-expand');
    $('.expndbl-box-list').css('height', '');
    if ($(window).width() <= 991) {
        $('.expndbl-box-list>.h3>span').on('click', function (e) {

            if ($(this).parents('.expndbl-box-list').find('.box-expand').hasClass('accord-mob-expand')) {
                e.preventDefault();
            }

            $(this).parents('.expndbl-box-list').find('.box-expand').toggleClass('accord-mob-expand');

        });
    }
});
$('.three-column-link-listing .solutions> li > h3 > span').on('click', function (e) {

    e.preventDefault();

    $(this).parent().toggleClass('open');
});



// for news search field

$('.search-fitler').on('click', function (e) {
    e.preventDefault();
    $(this).parents('.filter,.search-filter-wrapper').addClass('wayforform');
    $(this).find('.form-control').focus(function () {
        $(this).parents('.filter,.search-filter-wrapper').addClass('wayforform');
    });

    $(this).blur(function () {
        $(this).parents('.filter,.search-filter-wrapper').removeClass('wayforform');
    });
    $(this).find('.form-control').blur(function () {
        $(this).parents('.filter,.search-filter-wrapper').removeClass('wayforform');
    });
});


// Sticky navigation
if ($('#stickynav')) {
    $('#stickynav>ul>li>a').on('click', function (e) {
        e.preventDefault();
        var target = $(this).data('target');
        $("html, body").animate({ scrollTop: $(target).offset().top });
    });

    $('body').scrollspy({ target: '#stickynav', offset: 10 });
}

$(window).scroll(function (event) {
    if ($(".subnavigator-section").length > 0) {
        if ($(window).scrollTop() >= $('.subnavigator-section').outerHeight() + $('.subnavigator-section').offset().top)
            $('.stickynav.affix').css({ top: "5%" })
        else
            $('.stickynav.affix').css({ top: "360px" })
    }
});

// jquery UI datepicker

$("#shedule_date").datepicker({ minDate: 0 }).attr('readonly', 'readonly');

// Youtube player close

if ($('#videoModal')) {
    $("#videoModal, #videoModal .close, #videoModal .btn").on("click", function () {
        $("#videoModal iframe").attr("src", $("#videoModal iframe").attr("src"));
    });
}
// Dotdotdot
if (navigator.userAgent.match(/MSIE 9/) == null) {
    $('.framework-box > p,.common-article-strctr .article-content .article-caption,.common-article-strctr .article-content > h4,.box-strctr > p,.article-structure .article-content .article-caption,.main-slider .slider .caption-content,.site-search').dotdotdot({
        watch: 'window'
    });
}
else {
    $('.framework-box > p,.common-article-strctr .article-content .article-caption,.common-article-strctr .article-content > h4,.box-strctr > p,.article-structure .article-content .article-caption,.main-slider .slider .caption-content,.site-search').trunk8({
        lines: 2
    });
}


$(window).on('resize load', function () {
    $($('.boxes .box')[0]).height() > $($('.boxes .box')[1]).height() ? $($('.boxes .box')[1]).height($($('.boxes .box')[0]).height()) : $($('.boxes .box')[0]).height($($('.boxes .box')[1]).height());
    if ($('.analytic-list-grp .col-sm-12.box .list-strctr').children('ul').length)
        switch ($('.analytic-list-grp .col-sm-12.box .list-strctr').children('ul').length) {
            case 4:
                $('.analytic-list-grp .col-sm-12.box .list-strctr').children('ul').each(function (i) {
                    $(this).addClass('col-sm-3');
                });
                break;
            case 3:
                $('.analytic-list-grp .col-sm-12.box .list-strctr').children('ul').each(function (i) {
                    $(this).addClass('col-sm-4');
                });
                break;
            case 2:
                $('.analytic-list-grp .col-sm-12.box .list-strctr').children('ul').each(function (i) {
                    $(this).addClass('col-sm-6');
                });
                break;
            default:
                break;
        }
});

jQuery(document).ready(function ($) {
    var img = img_find();
    var ogmetatag = document.createElement('meta');
    ogmetatag.setAttribute('property', 'og:image');
    ogmetatag.setAttribute('content', img[0]);
    var twitmgmetatag = document.createElement('meta');
    twitmgmetatag.setAttribute('property', 'twitter:image:src');
    twitmgmetatag.setAttribute('content', img[0]);

    document.head.appendChild(ogmetatag);
    document.head.appendChild(twitmgmetatag);
    function img_find() {
        var imgs = document.getElementsByTagName("img");
        var imgSrcs = [];
        for (var i = 0; i < imgs.length; i++) {
            imgSrcs.push(imgs[i].src);
        }
        return imgSrcs;
    }

    /*if (typeof $.cookie('cb-enabled') === 'undefined'){
     alert("no cookie");
    } else {
     alert("have cookie");
    }*/
});

/*for the accordion solution links */
$('.solutions h4').click(function () {
    if ($(window).width() >= 992) {
        if ($(this).hasClass("open")) {
            $(this).next().css('max-height', 'none');
            var offset = $(this).next().height();
            $('.main-nav .submenu .submenu-list.solutions > li').css("height", "-=" + offset + "");
            $(this).next().css('max-height', '');
            $(this).removeClass("open");
            $('.solutions h4').removeClass("open");
        } else {
            $(this).next().css('max-height', 'none');
            var plusOffset = $(this).next().height();
            var minusOffset = 0;
            $(this).next().css('max-height', '');

            $('.main-nav .submenu .submenu-list.solutions > li h4').each(function (i) {
                if ($(this).hasClass("open")) {
                    var htoffset = $(this).next().height();
                    minusOffset += htoffset;
                    $(this).removeClass("open");
                }
            });
            $('.main-nav .submenu .submenu-list.solutions > li').css("height", "+=" + plusOffset + "");
            $('.main-nav .submenu .submenu-list.solutions > li').css("height", "-=" + minusOffset + "");
            $(this).addClass("open");
        }
    }
});
$('.solutions .h4').click(function () {
    if ($(window).width() >= 992) {
        if ($(this).hasClass("open")) {
            $(this).next().css('max-height', 'none');
            var offset = $(this).next().height();
            $('.main-nav .submenu .submenu-list.solutions > li').css("height", "-=" + offset + "");
            $(this).next().css('max-height', '');
            $(this).removeClass("open");
            $('.solutions .h4').removeClass("open");
        } else {
            $(this).next().css('max-height', 'none');
            var plusOffset = $(this).next().height();
            var minusOffset = 0;
            $(this).next().css('max-height', '');
            $('.main-nav .submenu .submenu-list.solutions > li .h4').each(function (i) {
                if ($(this).hasClass("open")) {
                    var htoffset = $(this).next().height();
                    minusOffset += htoffset;
                    $(this).removeClass("open");
                }
            });
            $('.main-nav .submenu .submenu-list.solutions > li').css("height", "+=" + plusOffset + "");
            $('.main-nav .submenu .submenu-list.solutions > li').css("height", "-=" + minusOffset + "");

            $(this).addClass("open");
        }
    }
});
/*for the dropdown side navigation */
$(function () {
    $('.side-nav-dropdown').on('change', function () {
        var url = $(this).val(); // get selected value
        if (url) { // require a URL
            window.location = url; // redirect
        }
        return false;
    });
});
/*for the industry inner tab */
$(function () {
    $('.industry-inner .cd-tabs-navigation > li > a').click(function () {
        var url = $(this).attr('href');// get selected value
        if (url) { // require a URL
            window.location = url; // redirect
        }
        return false;
    });
});
jQuery(document).ready(function ($) {
    var tabs = $('.cd-tabs');
    resizeNavTab();

    tabs.each(function () {
        var tab = $(this),
			tabItems = tab.find('ul.cd-tabs-navigation'),
			tabContentWrapper = tab.children('ul.cd-tabs-content'),
			tabNavigation = tab.find('nav');

        tabItems.on('click', 'a', function (event) {
            event.preventDefault();
            var selectedItem = $(this);
            if (!selectedItem.hasClass('selected')) {
                var selectedTab = selectedItem.data('content'),
					//selectedContent = tabContentWrapper.find($(‘li[data-content="'+selectedTab+'"]')),
					selectedContent = $('.cd-tabs-content li[data-content="' + selectedTab + '"]');
                tabItems.find('a.selected').removeClass('selected').parent().removeClass('selected').addClass('normal');
                selectedItem.addClass('selected').parent().addClass('selected').removeClass('normal');
                selectedContent.addClass('selected').siblings('li').removeClass('selected');
                var slectedContentHeight = selectedContent.outerHeight();
                //console.log(slectedContentHeight);
                //animate tabContentWrapper height when content changes 
                tabContentWrapper.animate({
                    'height': slectedContentHeight
                }, 200);
            }
            var quote_slider = $('.cd-tabs-content li[data-content="' + selectedTab + '"] .quote-slider');
            quote_slider.owlCarousel({
                nav: true, // Show next and prev buttons
                autoplay: true,
                loop: true,
                items: 1,
                dots: true,
                rewindNav: false,
                //autoHeight : true
            });
        });
        //hide the .cd-tabs::after element when tabbed navigation has scrolled to the end (mobile version)
        checkScrolling(tabNavigation);
        tabNavigation.on('scroll', function () {
            checkScrolling($(this));
        });
    });

    $(window).on('resize', function () {
        tabs.each(function () {
            var tab = $(this);
            checkScrolling(tab.find('nav'));
            tab.find('.cd-tabs-content').css('height', 'auto');

        });
        resizeNavTab();
    });

    function checkScrolling(tabs) {
        var totalTabWidth = parseInt(tabs.children('.cd-tabs-navigation').width()),
		 	tabsViewport = parseInt(tabs.width());
        if (tabs.scrollLeft() >= totalTabWidth - tabsViewport) {
            tabs.parent('.cd-tabs').addClass('is-ended');
        } else {
            tabs.parent('.cd-tabs').removeClass('is-ended');
        }
    }

    function resizeNavTab() {
        $('ul.cd-tabs-navigation').width(
			$($('ul.cd-tabs-navigation li.normal')[0]).width() * ($('ul.cd-tabs-navigation li').length - 1) +
			$('ul.cd-tabs-navigation li.selected').width() + 5
		);
    }

});