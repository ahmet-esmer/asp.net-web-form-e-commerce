
function BankaTaksitListe() {


    if ($("#odemeSec").children().size() > 0) {
        return false;
        }

    var prm = "fiyat=" + $('#hdfUrunFiyat').val();
    $.ajax({
        type: "POST",
        url: "../../../Include/Ajax/UrunDetayTaksitler.ashx",
        data: prm,
        dataType: "json",
        success: function (result) {

            try {

                var taksitHtml = new StringBuffer();
                var count = 0;
                var classTak = "";
                var strAy = "Peşin";
                var aktivTr = "";

                $.each(result, function () {

                    taksitHtml.append("<div class='taksitDiv' >");
                    taksitHtml.append("<table id='tblTaksitler'  class='taksitTablo' >" +
                    "<tr>" +
                        "<td colspan='3' >" +
                            "<img id='imgBankaBaslik' Width='245px' src='../../../Products/Sayfa_Resim/" +
                             this.Resim + "'  />" +
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

                    $.each(this.Taksit, function () {

                        count++;

                        if (count == 1) {
                            classTak = "taksitFiyatPesin";
                            strAy = "Peşin";
                            aktivTr = "taksitaktiv";
                        }
                        else {
                            classTak = "taksitFiyat";
                            strAy = this.Ay;
                            aktivTr = "";
                        }

                        taksitHtml.append("<tr class='taksitSatirBg " + aktivTr + "' >" +
                         "<td class='taksitler'>" +
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

                    count = 0;
                    taksitHtml.append("</table>");
                    taksitHtml.append("</div>");
                });

                $("#odemeSec").html(taksitHtml.toString()).fadeIn(1000);
                tabloaTr();
            }
            catch (e) {
                alert(e.ToString())
            }
        },
        error: function () {
            alert("Banka Taksitleri Listeleme Hatası");
        },
        beforeSend: function () {
        },
        complate: function () {
        }
    });

}

function tabloaTr() {
    $(".taksitTablo tr:even").addClass("taksitSatirBgAlter");
}


function UrunYorumlari(_sayfaNo) {

   var _id = $("#hdfUrunId").val();
   var veriler = { "urunId": _id, "sayfaNo": _sayfaNo };
   $.ajax({
       type: "POST",
       url: "../../Include/Ajax/Yorumlar.aspx/Liste",
       data: $.toJSON(veriler),
       contentType: "application/json; charset=utf-8",
       dataType: "json",
       success: function (msg) {
           $("#yorumlar").html("");
           var count = 0;
           $.each(msg.d, function (i) {
               count++;
               try {
                   $("#yorumlar").append(
                     "<div class='urunYorumAd'>" +
                     "<b>" + this.AdiSoyadi + "</b><br /><span style='font-size:11px;color:#747679;'>" +
                     "<div><img src='../../images/icons/rating" + this.DegerKiriteri + ".gif' /></div>" +
                     "</span></div>" +
                     "<div class='urunYorumTarih'>" + this.UrunAdi + "</div>" +
                     "<div class='urunYorumYazi'>" + this.Yorum + "</div>").fadeIn("slow");
               } catch (e) { }
           });
           if (count == 0) {
               $("#yorumlar").html("Bu ürüne ait yorum bulunamadı...").addClass("lblUrunYorumBilgi");
           }
           Sayfalama(_id, _sayfaNo);
          
           count = 0;
       },
       error: function () {
           $("#yorumlar").html("Hata oluştu");
       }
   });
}



function Sayfalama(_id, _sayfaNo) {

    var veriler = { "urunId": _id, "sayfaNo": _sayfaNo };
    $.ajax({
        type: "POST",
        url: "../../Include/Ajax/Yorumlar.aspx/Sayfalama",
        data: $.toJSON(veriler),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $(".pagination").html("");
            $(".pagination").append(msg.d.toString());
            $('.pagination a').click(function () {
                UrunYorumlari(this.rel)
                $("#yorumBaslik").slideto({ highlight: false });
                return false;
            });
        },
        error: function () {
            $(".pagination").html("Sayfalama Hatası.");
        }
    });
}


$(document).ready(function () {


    if ($('.kamResim').val() != undefined) {

        $('#hlHediye').fancyZoom({ width: 220, height: 200 });
    }

    if ($("#trSenekInfo").val() != undefined) {
        $('#bilgi').fancyZoom({ width: 500, height: 300 });
    }



    //Ürün yorumlari listeleme
    UrunYorumlari(0);

    //Arakadaşına gönder
    $('#imgArkadasaGonder').click(function () {
        $("#imgSepeteEkle").slideto({ highlight: false });
        location.href = '#arkadas-tab';
    })

    $("#odemeSecenkleri").click(function () {

        BankaTaksitListe();
    })


    // Ürün adet seçimi ile hediye ürün islemi
    $("#ddlAdet, #ddlAdet1").change(function () {

        HediyeUrunListe();
    })

    $('#menuTab').tabify();

    //$('table .taksitTablo tr:even').addClass("taksitSatirBgAlter");

    //Sepet Görüntüleme
    $('#imgSepetGoruntu').click(function () {

        if ($("#hdfUyeId").val() > 0) {
            window.location.href = "../../Market/Sepet.aspx";
        }
        else {
            window.location.href = "../../Kullanici/Kayit.aspx?sepet=0&returnUrl=" + $('#hdfGeridonusUrl').val();
        }
    });

    // Favori Ürün Ekleme
    $('#imgFavoriEkle').click(function () {
        if ($("#hdfUyeId").val() > 0) {

            $.ajax({
                type: 'POST',
                url: "../../Include/Ajax/UrunService.asmx/FavoriEkle",
                data: '{"urunId":' + $("#hdfUrunId").val() + ' }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (result) {
                    alert(result.d);
                },
                error: function () {
                    alert('İşlem hata ile sonuçlandı. Yeniden deneyin');
                }
            });
            return false;
        }
        else {
            window.location.href = "../../Kullanici/Kayit.aspx?returnUrl=" + $('#hdfGeridonusUrl').val();
        }
    });

    //  Ürün Fiyat İzleme
    $('#imgIzlemeyeAl').click(function () {
        if ($("#hdfUyeId").val() > 0) {

            $.ajax({
                type: 'POST',
                url: "../../Include/Ajax/UrunService.asmx/UrunIzleme",
                data: '{"urunId":' + $("#hdfUrunId").val() + ' }',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (result) {
                    alert(result.d);
                },
                error: function () {
                    alert('İşlem hata ile sonuçlandı. Yeniden deneyin');
                }
            });
            return false;
        }
        else {
            window.location.href = "../../Kullanici/Kayit.aspx?returnUrl=" + $('#hdfGeridonusUrl').val();
        }

    });

    // Ürün Yorum ekleme işlemi
    $('#imgYorum').click(function () {

        if ($("#hdfUyeId").val() > 0) {
            location.href = '#yorum-tab';
            $("#yorumBaslik").slideto({ highlight: false });
        }
        else {
            window.location.href = "../../Kullanici/Kayit.aspx?returnUrl=" + $('#hdfGeridonusUrl').val();
        }
    })

    // Ürün sepete ekleme işlemi 
    $('#imgSepeteEkle').click(function () {

        var sagAdet = $('#ddlAdet').val();
        var solAdet = $('#ddlAdet1').val();

        if (solAdet == undefined)
            solAdet = 0;

        if (sagAdet == 0 && solAdet == 0) {
            alert("Lütfen ürün adeti seçiniz");
            return false;
        }

        var urunId = $('#hdfUrunId').val();
        var sagBilgi = new StringBuffer();
        var solBilgi = new StringBuffer();


        var aks = $('#ddlAks').val();
        var bc = $('#ddlBc').val();
        var dioptri = $('#ddlDioptri').val();
        var renk = $('#ddlRenk').val();
        var silindirik = $('#ddlSilindirik').val();
        var dia = $('#ddlDia').val();
        var stok = $('#lblStok').text();

        // Sag göz bilgileri
        if (aks != undefined)
            sagBilgi.append("Aks: " + aks + ", ");
        if (bc != undefined)
            sagBilgi.append("Bc: " + bc + ", ");
        if (dioptri != undefined)
            sagBilgi.append("Dioptri: " + dioptri + ", ");
        if (renk != undefined)
            sagBilgi.append("Renk: " + renk + ", ");
        if (silindirik != undefined)
            sagBilgi.append("Silindirik: " + silindirik + ", ");
        if (dia != undefined)
            sagBilgi.append("Dia: " + dia);

        // Sol Göz bilgileri 
        if ($("#ddlAks1").val() != undefined)
            solBilgi.append("Aks: " + $("#ddlAks1").val() + ", ");
        if ($("#ddlBc1").val() != undefined)
            solBilgi.append("Bc: " + $("#ddlBc1").val() + ", ");
        if ($('#ddlDioptri1').val() != undefined)
            solBilgi.append("Dioptri: " + $('#ddlDioptri1').val() + ", ");
        if ($('#ddlRenk1').val() != undefined)
            solBilgi.append("Renk: " + $('#ddlRenk1').val() + ", ");
        if ($('#ddlSilindirik1').val() != undefined)
            solBilgi.append("Silindirik: " + $('#ddlSilindirik1').val() + ", ");
        if ($('#ddlDia1').val() != undefined)
            solBilgi.append("Dia: " + $('#ddlDia1').val());


        if (stok == 'Stok Bilgisi Alınız') {
            alert(" Bu ürün stoklarımızda mevcut değil. \n Lütfen müşteri hizmelerinden stok bilgisi alınız.\n Telefon: 0212 441 32 01");
            return false;
        }

        // $('#hdfHediyeUrun').val($(obje).val());
        var hediyeId = $("#hdfHediyeUrun").val();
        var hediyeBilgi = "";

        if (hediyeId > 0) {

            if ($("#ddl_" + hediyeId).val() != undefined) {
                hediyeBilgi = $("#ddl_" + hediyeId).val();

                if (hediyeBilgi == "0") {
                    alert("Lütfen hediye ürün seçenegini seçiniz.");
                    return false;
                }

                if (hediyeBilgi == "Indirim:4 al 3 öde") {
                    hediyeId = 0;
                }
            }
            else {
           
                var dortAl = $('input[name=rdbSecenek]:checked').parent().text();

                if ($.trim(dortAl) == "4 al 3 öde") {
                    hediyeId = 0;
                }
                //alert(dortAl);

            }
        }

        var prm = {
            "urunId": urunId,
            "sagAdet": sagAdet,
            "solAdet": solAdet,
            "sagBilgi": sagBilgi,
            "solBilgi": solBilgi,
            "hediyeId": hediyeId,
            "hediyeBilgi": hediyeBilgi
        };

        if ($("#hdfUyeId").val() > 0) {
            post("../../Market/Sepet.aspx", prm);
        }
        else {
            post("../../Kullanici/Kayit.aspx?sepeteEkle=0", prm);
        }
    });
});


function HediyeUrunListe() {

    $("#hediye").slideUp("slow");

    var adetSag = $('#ddlAdet').val();
    var adetSol = $('#ddlAdet1').val();

    if (adetSol == undefined)
        adetSol = 0;


    var toplam = parseInt(adetSag) + parseInt(adetSol);


    if (toplam == 0) {
        return false;
    }

    var prm = "urunId="+ $('#hdfUrunId').val()+ "&" + "adet=" + toplam;

    $.ajax({
        type: "POST",
        url: "../../Include/Ajax/HediyeUrunContlor.ashx",
        data: prm,
        dataType: "json",
        success: function (result) {

            var hediyeHtml = new StringBuffer();
            var count = 0;
            var selectCount = 0;
            var baslik = "";
            var secAktivClass = "aktiv";
            var secAktifValue = "checked";
            var padding = "style='margin-right: 5px; margin-left:5px;'";


            $.each(result, function (i) {

                var padding = "";
                try {
                    count++;

                    if (count > 1) {
                        secAktivClass = "";
                        secAktifValue = "";

                    } else {
                        $('#hdfHediyeUrun').val(this.Id);
                        hediyeHtml.append("<div id='kampanyaBaslik' >");
                        hediyeHtml.append("<div>" + this.BaslikAdi + "</div>")
                        hediyeHtml.append("</div>");
                    }

                    var strSelect = "<select id='ddl_" + this.Id + "'>" +
                                    "<option value='0' > Seçiniz </option>";

                    $.each(this.Secenekler, function () {
                        selectCount++;
                        strSelect += "<option value='" + this.Value + "' >" + this.Name + "</option>";
                    });


                    if (selectCount > 0) {
                        strSelect += "</select>";
                    }
                    else {
                        strSelect = "";
                    }

                    if (count == 2) {
                        padding = "style='margin-right: 5px; margin-left:5px;'";
                    }

                    var strSatir =
                    "<table class='tblSecenek' >" +
                    "<tr>" +
			            "<td style='width:100px'>" +
                        "<a href='#box_" + this.Id + "'>" +
                          "<img src='/Products/Small/" + this.Resim + "' style='width:130px' />" +
                        "</a>" +
                        "<a id='box_" + this.Id + "' style='display: none;'>" +
                          "<img src='/Products/Big/" + this.Resim + "' />" +
                        "</a>" +
                        "</td>" +
                        "<td style='width:200px' >" +
                        " " + this.Marka +
                        "</br> </br>" + strSelect + "</td>" +
		            "</tr>" +
                    "</table>";

                    var strdiv =
                    "<div class='hediyeDiv' " + padding + " >" +
                        "<div class='hediyeRadio " + secAktivClass + "' >" +
                            "<label>" +
                                "<input name='rdbSecenek' onclick='HediyeSec(this);' type='radio'" + secAktifValue + " value='" + this.Id + "' />" +
                                this.UrunAdi +
                            "</label>" +
                        "</div>" +
                        "<div class='Adres' >" +
                        strSatir +
                        "</div>" +
                    "</div>";

                    hediyeHtml.append(strdiv);
                }
                catch (e) {
                    alert(e.ToString())
                }
            });


            if (count == 0) {
                $("#hediye").removeClass("hediyelerDiv");
                $("#hediye").html("");
            }
            else {

                $("#kampanya").slideto({ highlight: false });
                $("#hediye").addClass("hediyelerDiv");
                hediyeHtml.append("<div style='clear:both'></div>");
                $("#hediye").html(hediyeHtml.toString()).slideDown("slow");
                $('.tblSecenek a').fancyZoom({ scaleImg: true, closeOnClick: true });
            }

            count = 0;
        },
        error: function () {

        },
        beforeSend: function () {

        },
        complate: function () {

        }
    });

}


function HediyeSec(obje) {

   
    $('#hdfHediyeUrun').val($(obje).val());

    $('input[name="rdbSecenek"]').parent().parent().removeClass("aktiv");
    $(obje).parent().parent().addClass("aktiv");
}

function post(path, parameters) {
    var form = $('<form></form>');

    form.attr("method", "post");
    form.attr("action", path);

    $.each(parameters, function (key, value) {
        var field = $('<input></input>');

        field.attr("type", "hidden");
        field.attr("name", key);
        field.attr("value", value);

        form.append(field);
    });
    $(document.body).append(form);
    form.submit();
}



//$.extend({ URLEncode: function (c) {
//    var o = ''; var x = 0; c = c.toString(); var r = /(^[a-zA-Z0-9_.]*)/;
//    while (x < c.length) {
//        var m = r.exec(c.substr(x));
//        if (m != null && m.length > 1 && m[1] != '') {
//            o += m[1]; x += m[1].length;
//        } else {
//            if (c[x] == ' ') o += '+'; else {
//                var d = c.charCodeAt(x); var h = d.toString(16);
//                o += '%' + (h.length < 2 ? '0' : '') + h.toUpperCase();
//            } x++;
//        } 
//    } return o;
//},



//    URLDecode: function (s) {
//        var o = s; var binVal, t; var r = /(%[^%]{2})/;
//        while ((m = r.exec(o)) != null && m.length > 1 && m[1] != '') {
//            b = parseInt(m[1].substr(1), 16);
//            t = String.fromCharCode(b); o = o.replace(m[1], t);
//        } return o;
//    }
//});







function openpopup() {
    var e = document.getElementById('hlResimBuyuk');
    var popurl = e.href;
    winpops = window.open(popurl, "", "toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=700,height=800,left =200,top =90")
}

function renkPopUp(sayfa) {
    winpops = window.open(sayfa, "", "toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1, width=900, height=700,left =200,top =90")
}



//$(function () {
//    $('.bubbleInfo').each(function () {
//        var distance = 8;
//        var time = 250;
//        var hideDelay = 500;

//        var hideDelayTimer = null;

//        var beingShown = false;
//        var shown = false;
//        var trigger = $('.trigger', this);
//        var info = $('.popup', this).css('opacity', 0);



//        $([trigger.get(0), info.get(0)]).click(function () {
//            if (hideDelayTimer) clearTimeout(hideDelayTimer);
//            if (beingShown || shown) {
//                // don't trigger the animation again
//                return;
//            } else {
//                // reset position of info box
//                beingShown = true;

//                var top = info.css("top");
//                var left = info.css("left");

//              //  alert(top + ": left" + left);

//                info.css({
//                    top: top,
//                    left: left,
//                    display: 'block'
//                }).animate({
//                    top: '-=' + distance + 'px',
//                    opacity: 1
//                }, time, 'swing', function () {
//                    beingShown = false;
//                    shown = true;
//                });
//            }

//            return false;
//        }).mouseout(function () {
//            if (hideDelayTimer) clearTimeout(hideDelayTimer);
//            hideDelayTimer = setTimeout(function () {
//                hideDelayTimer = null;
//                info.animate({
//                    top: '-=' + distance + 'px',
//                    opacity: 0
//                }, time, 'swing', function () {
//                    shown = false;
//                    info.css('display', 'none');
//                });

//            }, hideDelay);

//            return false;
//        });
//    });
//});


