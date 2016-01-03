/* ================================================================ 
Kenarlık
=================================================================== */

$('.altLink').corner("10px");
$('#gunDis').corner("round 6px");
$('#gunIc').corner("round 6px");
$('#ortaDis').corner("round 6px");
$('#ortaIc').corner("round 6px");
$('#mesaj-sgk').corner("round 6px");

$('.menu').corner("top");
$('.menuKayit').corner("top");


$(window).load(function () {

    if ($.cookie('lnsKampanya') == null) {
       
        if ($('#pnlKampanya') != undefined) {
            $('#pnlKampanya').fadeIn(1000);
        }
        
    }
});


$(document).ready(function () {

    // Kulanici menu css 
    $("ul.bilgiIcerik li:even").addClass("alt");
    $('#bilgiBaslik').click(function () {
        $('ul.bilgiIcerik').slideToggle('medium');
    });


    $('#close').click(function () {

        //$("#pnlKampanya").animate({ opacity: 0.25, left: '-=50', height: 'toggle' }, 1000);

        $("#pnlKampanya").hide(1000);

        $.cookie('lnsKampanya', 'true');

        return false;
    });


    $obj = $('#lofslidecontent45').lofJSidernews({ interval: 4000,
        direction: 'opacitys',
        easing: 'easeInOutExpo',
        duration: 1200,
        auto: true,
        maxItemDisplay: 5,
        navPosition: 'horizontal',
        navigatorHeight: 17,
        navigatorWidth: 22,
        mainWidth: 613
    });

    $('.menu:even').css("background-color", "#118eea");

    if (!$.browser.msie) {
        $(".msnAdd").live('click', function (e) {
            alert("<info@lensoptik.com.tr> kişisini kişi listenize eklemek içi internet explorer kulanmalısınız.");
            return false;
        });
    }



    /* ================================================================ 
    Ana Menu İşlemleri
    =================================================================== */

    $("ul.subnav").parent().append("<span></span>");
    $(".dropdown a ").hover(function () {
        $(this).parent().find("ul.kategoriler").slideDown('fast').show();
        $(this).parent().hover(function () {
        }, function () {
            $(this).parent().find("ul.kategoriler").slideUp('slow');
        });
    }).hover(function () {
        $(this).addClass("subhover");
    }, function () {
        $(this).removeClass("subhover");
    });
});

    /*----------------------------------------------------------------------------
    Resim küçültme
    ------------------------------------------------------------------------------*/

function ResizeImage(image, maxwidth, maxheight) {

    w = image.width;
    h = image.height;

    if (w == 0 || h == 0) {
        image.width = maxwidth;
        image.height = maxheight;
    }
    else if (w == h) {
        image.width = maxwidth;
    }
    else if (w > h) {
        if (w > maxwidth)
            image.width = maxwidth;
    }
    else {
        if (h > maxheight)
            image.width = maxheight;
    }
}

    /* ================================================================ 
                Anket Oylama
    =================================================================== */
function AnketOyla() {

    var soruId = $('input[name="anketAd"]:checked').val()
    if (soruId == undefined) {
        $('#lblAnketBilgi').addClass('no');
        $('#lblAnketBilgi').text("Lütfen Seçim Yapınız");
        return false;
    }

    $.ajax({
        type: 'POST',
        url: $("#hdfLink").val(),
        data: '{ "id":' + soruId + ' }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            AnketSonuc(result.d);
        },
        error: function () {
            $('#lblAnketBilgi').addClass('no').text('İşlem hata ile sonuçlandı. Yeniden deneyin');
        }
    });
}

function AnketSonuc(retVal) {

    if (retVal == 1) {
        $('#lblAnketBilgi').removeClass('no').addClass('ok');
        $('#lblAnketBilgi').text("Oyunuzu kullandığınız için teşekkür ederiz.");
        $('#hlAnketOyla').css("display", "none");
    }
    else {
        $('#lblAnketBilgi').text("Hata Oluştu");
    }
}

    /* ================================================================ 
            Mail kaydet
    =================================================================== */

function MailKaydet(adres) {
    var mail = $('#txtMail').val();

    if (mail == undefined || mail == 'E-Posta Adresiniz.') {
        alert('Lütfen E-Posta adresi yazınız.');
        return false;
    }
    else if (!isValidEmailAddress(mail)) {
        alert('Lütfen Geçerli E-Posta adresi yazınız.');
        return false;
    }

    $.ajax({
        type: 'POST',
        url:  adres.toString(),
        data: '{ "mail":"' + mail + '" }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            alert(result.d);
        },
        error: function () {
            alert('İşlem hata ile sonuçlandı. Yeniden deneyin');
        }
    });
}

function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return pattern.test(emailAddress);
}

function MesengerOpen() {

    var popurl = "Mesanger.html";
    winpops = window.open(popurl, "", "toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=320,height=420,left =200,top =90")
}

$().ready(function () {
    swapValues = [];
    $("#txtMail").each(function (i) {
        swapValues[i] = $(this).val();
        $(this).focus(function () {
            if ($(this).val() == swapValues[i]) {
                $(this).val("").removeClass("watermark")
            }
        }).blur(function () {
            if ($.trim($(this).val()) == "") { $(this).val(swapValues[i]).addClass("watermark") } 
        })
    })
});

    /* ================================================================ 
                     Slideto
    =================================================================== */
(function ($) {
    $.fn.slideto = function (options) {
        var defaults = {
            slide_duration: "slow",
            highlight_duration: 3000,
            highlight: true,
            highlight_color: "#FFFF99"
        };
        var options = $.extend(defaults, options);
        return this.each(function () {
            var callback = false;
            obj = $(this);
            $('html,body').animate({ scrollTop: obj.offset().top }, options.slide_duration, function () {
                if (callback == false) {
                    if (options.highlight && $.ui.version) {
                        obj.effect("highlight", { 'color': options.highlight_color }, options.highlight_duration);
                    }
                    callback = true;
                }
            });
        });
    };
})(jQuery);


// Sitrin birleştirme
function StringBuffer() {
    this.buffer = [];
}

StringBuffer.prototype.append = function append(string) {
    this.buffer.push(string);
    return this;
};

StringBuffer.prototype.toString = function toString() {
    return this.buffer.join("");
};


String.format = function () {
    if (arguments.length == 0)
        return null;
    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}

