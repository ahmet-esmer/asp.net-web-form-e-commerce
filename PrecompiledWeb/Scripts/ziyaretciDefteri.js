

$('#ortaSag').corner("round 6px");
$('#ortaSagIc').corner("round 6px");


$(document).ready(function () {


    $('#formLink').click(function () {

        $("#zFormm").slideto({ highlight: false });
    })

    var go = getQuerystring('goto');

    if (go > 0) {

        $("#" + go).slideto({ highlight_color: '#0f96e7', highlight_duration: 3000 });
    }


    $('input, select, textarea').blur(function () {

        if ($(this).val() == "") {
            $(this).next("span").css("display", "block");
        }
        else {
            $(this).next("span").css("display", "none");
        }
    });


    $("#btnZiyaretciDefteri").click(function () {

        YorumKaydet();
    })

});


  /* ================================================================ 
            Yorum Kaydetme İşlemi
  =================================================================== */

function YorumKaydet() {


    var adSoyad = $('#txtAdSoyad').val();
    var eposta = $('#txtEposta').val();
    var sehirler = $('#ddlSehirler').val();
    var ilceler = $('#ddlilceler').val();
    var mesaj = $('#txtMesaj').val();
    var guvenlik = $('#txtGuvenlik').val();

    var count = 0;

    if (adSoyad == "") {
        $("#msjAdsoyad").css("display", "block");
        count++;
    }
    if (eposta == "") {
        $("#msjEposta").css("display", "block");
        count++;
    }
    if (sehirler == "") {
        $("#msjSehirler").css("display", "block");
        count++;
    }
    if (mesaj == "") {
        $("#msjMesaj").css("display", "block");
        count++;
    }
    if (guvenlik == "") {
        $("#msjGuvenlik").css("display", "block");
        count++;
    }
    if (count > 0) {
     
        return false;
    }

    var jsonData = {
        "adiSoyad": adSoyad,
        "ePosta": eposta,
        "sehirler": sehirler,
        "ilceler": ilceler,
        "mesaj": mesaj,
        "guvenlik": guvenlik
    }

    $.ajax({
        type: 'POST',
        url: "../Include/Ajax/ZiyaretciMesajKaydet.aspx/Kayit",
        data: $.toJSON(jsonData),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {

            $("#mesajlar").html("");
            $("#mesajlar").html(result.d);

        },
        error: function () {

            $("#mesajlar").html("");
            $("#mesajlar").html(result.d);
        },
        beforeSend: function () {

            $("#progress").removeClass("endRequest").addClass("beginRequest");
        },
        complete: function () {

            
            $("#progress").removeClass("beginRequest").addClass("endRequest");
        }
    });
}



function getQuerystring(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}
