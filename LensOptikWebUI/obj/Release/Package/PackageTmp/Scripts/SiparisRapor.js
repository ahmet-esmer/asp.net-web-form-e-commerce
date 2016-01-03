$(document).ready(function () {

    $('#btnSipTarihAra').click(function () {

        var baslangicTarih = $("#txtTarih_1").val();
        var bitisTarih = $("#txtTarih_2").val();
        var siparisDurumu = $("#ddlSiparisDurum").val();

        if (baslangicTarih == "Başlanğıç Tarihi Seçiniz..") {
            alert("Başlanğıç Tarihi Seçiniz..");
            return false;
        }

        if (bitisTarih == "Bitiş Tarihi Seçiniz..") {
            alert("Bitiş Tarihi Seçiniz..");
            return false;
        }


        var veriler = { "siparisDurumu": siparisDurumu, "baslangicTarih": baslangicTarih, "bitisTarih": bitisTarih };
        
        $.ajax({
            type: "POST",
            url: "../Include/Ajax/AjaxSiparsRapor.aspx/Liste",
            data: $.toJSON(veriler),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {

                $("#divSiparis").html("<table id='rpTablo'  ><tbody><tr class='HeaderStyle' >" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:100px;'>Sipariş No </th>" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:100px;'>Kullanici ID</th>" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:250px;'>Üye İsmi</th>" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:80px;'>Giriş</th>" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:80px;'>Alışveriş</th>" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:200px;'>Sipariş Şekli</th>" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:200px;'>Şipariş Durumu</th>" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:200px;'>Tutar</th>" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:150px;'>Sipariş Tarih</th>" +
                "<th style='border-bottom:1px solid #DDD;font-weight:bold;width:20px;'>Detay</th></tr>");

                $(".pagination").html("");

                var count = 0;
                var genelToplam = 0;
                var atrRow = "RowStyle";
                $.each(msg.d, function (i) {
                    count++;
                    
                    try {

                        genelToplam += this.TaksitliGenelToplami;

                        if (count % 2 == 1) {
                            atrRow = "RowStyle";
                        }
                        else {
                            atrRow = "AlternatingRowStyle";
                        }


                        var strSatir =
                               "<tr class='" + atrRow + "' >" +
			                       "<td style='width:100px;height:19px; border-bottom:1px solid #e8e7e7;'><b>" + this.SiparisNo + "</b></td>" +
                                   "<td style='width:100px; border-bottom:1px solid #e8e7e7;'>" + this.KullaniciId + "</td>" +
                                   "<td style='width:250px;border-bottom:1px solid #e8e7e7;'>" + this.AdiSoyadi + "</td>" +
                                   "<td style='width:80px;border-bottom:1px solid #e8e7e7;'>" + this.GirisSayisi + "</td>" +
                                   "<td style='width:80px;border-bottom:1px solid #e8e7e7;'>" + this.AlisveriSayisi + "</td>" +
                                   "<td style='width:200px;border-bottom:1px solid #e8e7e7;' >" + this.OdemeTipi + "</td>" +
                                   "<td style='width:200px;border-bottom:1px solid #e8e7e7;'>" + this.SiparisDurumu + "</td>" +
                                   "<td style='width:200px;border-bottom:1px solid #e8e7e7;'>" + this.TaksitliGenelToplami + " TL </td>" +
                                   "<td style='width:150px;border-bottom:1px solid #e8e7e7;'>" + this.strSipTarihi + "</td>" +
                                   "<td class='orta' style='width:20px;border-bottom:1px solid #e8e7e7;'> <a href='siparisDetay.aspx?siparisId=" + this.Id + "' target='_blank'  title='Sipariş Detay'><img src='images/duzenle.gif' alt='Detay' border='0'> </a></td>" +
		                       "</tr>";

                        $("#divSiparis").append(strSatir);

                    } catch (e) { }
                });

                if (count == 0) {

                    $(".mesajlar").html("");
                    $(".mesajlar").html("<div id='Mesaj_No' class='Mesaj_No'> <div class='mesaj_clos' ><img  src='images/no.jpg' alt='Mesaji Kapat' /> </div><b>" + baslangicTarih + "</b> ile <b>" + bitisTarih + "</b> Tarihleri aralığında sipariş bulunamadı.</div>");

                }
                else {

                    $("#divSiparis").append("<tbody></table>");

                    $(".mesajlar").html("");

                    $(".mesajlar").html("<div id='Mesaj_Ok' class='Mesaj_Ok' ><div class='mesaj_clos'>" +
                              "<img src='images/yes_no.gif' alt='Mesaji Kapat' /> </div>" +
                              "<b>" + baslangicTarih + "</b> ile <b>" + bitisTarih + "</b> Tarihleri aralığında" +
                              "toplam <b>" + count + "</b> adet sipariş bulundu </br> Toplam Fiyat: <b>" + genelToplam.toMoney() + " TL</b> </div>");

                }

                count = 0;
            },
            error: function () {
                $("#divSiparis").html("Hata oluştu");
            }
        });

        return false;
    })


    Number.prototype.toMoney = function (decimals, decimal_sep, thousands_sep) {
        var n = this,
   c = isNaN(decimals) ? 2 : Math.abs(decimals), 
   d = decimal_sep || ',', 
   t = (typeof thousands_sep === 'undefined') ? '.' : thousands_sep, 
   sign = (n < 0) ? '-' : '',
   i = parseInt(n = Math.abs(n).toFixed(c)) + '',

   j = ((j = i.length) > 3) ? j % 3 : 0;
        return sign + (j ? i.substr(0, j) + t : '') + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : '');
    }

});