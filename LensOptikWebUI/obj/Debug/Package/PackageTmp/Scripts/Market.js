

function puan(obje) {

    if ($(obje).is(':checked')) {
        $("#pnlPuan").addClass("puanAktiv");
    }
    else {
        $("#pnlPuan").removeClass("puanAktiv");
    }
}


/* ================================================================ 
Ana Menu Aktif Sitil Seçimi
=================================================================== */

function scriptInit() {
    if (!document.getElementById) {
        return;
    }
}

function addEvent(elm, evType, fn, useCapture) {
    if (elm.addEventListener) {
        elm.addEventListener(evType, fn, useCapture);
        return true;
    } else if (elm.attachEvent) {
        var r = elm.attachEvent('on' + evType, fn);
        return r;
    } else {
        elm['on' + evType] = fn;
    }
}
function checkActive() {

    var a = $("#marketMap  a ");
    var loc = window.location.href;

    for (var i = 0; i < a.length; i++) {
        if (a[i].href == loc) {
            a[i].setAttribute("class", "marketLink on");
            a[i].setAttribute("className", "marketLink on");
        }
    }
}

addEvent(window, 'load', checkActive, false);