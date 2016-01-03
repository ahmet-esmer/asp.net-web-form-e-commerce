


$(document).ready(function () {

    $("#btnOdemeYap").click(function () {

        var isPageValid = Page_ClientValidate('odeme');

        if (isPageValid) {
            $("#progress").removeClass("endRequest").addClass("beginRequest");
        }
    })

    $("#ddlBankalar").change(function () {
        BankaTaksitListe(1);
    })
});


/* ================================================================ 
 Kredi Kart bilgilerini getirme
=================================================================== */

function BankaTaksitListe(secilenTaksit) {


    $("#pnlTaksitler").slideUp("slow");

    var prm = "bankaId=" + $('#ddlBankalar').val();

    $("#progress").removeClass("endRequest").addClass("beginRequest");

    $.ajax({
        type: "POST",
        url: "../../Include/Ajax/BankaTaksitleri.ashx",
        data: prm,
        dataType: "json",
        success: function (result) {

            try {

                var taksitHtml = new StringBuffer();
                var count = 0;
                var classTak = "";
                var strAy = "Tek Çekim";
                var ckbTaksit = "";
                var aktivTr = "";

                taksitHtml.append("<table id='tblTaksitler'  class='taksitTablo' >" +
                    "<tr>" +
                        "<td colspan='3' >" +
                            "<img id='imgBankaBaslik' Width='298px' src='../Products/Sayfa_Resim/" + result.Resim + "'  />" +
                        "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td class='taksitListeBaslik' style='padding-left:10px'>" +
                            "Taksit" +
                        "</td>" +
                        "<td class='taksitListeBaslik'>" +
                            "Tutarı" +
                        "</td>" +
                        "<td class='taksitListeBaslik'>" +
                            "Toplam" +
                       "</td>" +
                    "</tr>");


                $.each(result.Taksit, function () {

                    count++;

                    if (count == secilenTaksit) {
                        ckbTaksit = "checked='true'";
                        aktivTr = "taksitaktiv";
                    }
                    else {
                        ckbTaksit = "";
                        aktivTr = "";
                    }

                    if (count == 1) {
                        classTak = "taksitFiyatPesin";
                        strAy = "Tek Çekim";

                    }
                    else {
                        classTak = "taksitFiyat";
                        strAy = this.Ay;

                    }

                    taksitHtml.append("<tr class='taksitSatirBg " + aktivTr + "' >" +
                         "<td class='taksitler'>" +
                         "<input name='taksit' onclick='Taksit(this);' " + ckbTaksit.toString() + "'  type='radio' value='" + this.Ay + "' />" +
                           "<label>" + strAy.toString() + "</label>" +
                         "</td>" +
                         "<td>" +
                            "<span class='" + classTak + "' >" +
                              this.AylikFiyat +
                            "</span>" +
                         "</td>" +
                         "<td>" +
                            "<span class='" + classTak + "' >" +
                              this.ToplamFiyat +
                            "</span>" +
                         "</td>" +
                         "</tr>");
                });

                taksitHtml.append("</table>");

                $("#progress").removeClass("beginRequest").addClass("endRequest");

                if (count == 0) {
                    $("#pnlTaksitler").html("");
                }
                else {
                    $("#pnlTaksitler").html(taksitHtml.toString()).slideDown("slow");
                    tabloaTr();
                }

                count = 0;
            }
            catch (e) {
                alert(e.ToString())
            }
        },
        error: function () {
            alert("Banka Taksitleri Listeleme Hatası");
            window.location.href = "IslemOnay.aspx";
        },
        beforeSend: function () {
            // $("#progress").removeClass("endRequest").addClass("beginRequest");
        },
        complate: function () {
            //$("#progress").removeClass("beginRequest").addClass("endRequest");
        }
    });

}

function tabloaTr() {
    $(".taksitTablo tr:even").addClass("taksitSatirBgAlter");
}

function Taksit(obje) {
    $('input[name="taksit"]').parent().parent().removeClass("taksitaktiv");
    $(obje).parent().parent().addClass("taksitaktiv");
}

function karakter(elmnt, content) {
    if (content.length == elmnt.maxLength) {
        $(elmnt).next("input[type=text]").focus();
    }
}
