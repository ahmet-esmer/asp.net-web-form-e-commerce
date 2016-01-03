<%@ Page Title="Teslimat Adresi | Lens Optik" Language="C#" MasterPageFile="~/Market/Market.master" AutoEventWireup="true"  Inherits="Market_TeslimatBilgisi" EnableEventValidation="false"  Codebehind="ilkAdres.aspx.cs" %>

<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc1" %>

<%@ Register src="MenuMarket.ascx" tagname="MenuMarket" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link rel="Stylesheet" href="../Style/TeslimatBilgileri.css" type="text/css" />
<script src="../Scripts/Market.js"  type="text/javascript" ></script>

<script type="text/javascript">

    $(document).ready(function () {

        $("#imBtnSatinAl").click(function () {
            if (!$("#ckbSozlesmeOnay").is(":checked")) {
                alert("! Gizlilik Politikasını  Okuyup Onaylayın.");
                return false;
            }
        })
    });
         
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
 
 <div id="marketAna" >

     <div id="marketMap">
       <uc2:MenuMarket ID="Menu" runat="server" />
     </div>
     <div id="marketMesaj" style="margin-bottom:20px;" >
       <uc1:Mesajlar ID="Mesaj" runat="server" />
     </div>
     <div id="pnlAdresForm" >
      <span class="formBsalik" > Teslimat Adresinizi Yazınız</span>
      <div>
           <small> Teslim Edilecek Kişi</small>
           <asp:TextBox ID="txtTeslimAlan" runat="server" CssClass="text" Width="350px" ></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="rfvTeslimAlan" runat="server" 
                 ControlToValidate="txtTeslimAlan" Display="Dynamic"
                 CssClass="input-notification error" ForeColor=""
                 ErrorMessage="Lütfen  kargoyo teslim alan kişiyi belirtiniz." 
                 ClientIDMode="Static" ValidationGroup="kayit" ></asp:RequiredFieldValidator>
     </div>
      <div>
          <small>İrtibat Telefonu</small>
          <asp:TextBox ID="txtTelefon" runat="server" CssClass="text"  Width="350px" ></asp:TextBox> <br />
          <asp:RequiredFieldValidator ID="rfvTelefon" runat="server" 
                ControlToValidate="txtTelefon" Display="Dynamic" 
                ErrorMessage="Lütfen  irtibat telefonu belirtiniz." 
                CssClass="input-notification error" ForeColor=""
                ClientIDMode="Static" ValidationGroup="kayit" ></asp:RequiredFieldValidator>
       </div>
      <div>
        <small>Teslimat Adresi</small>
         <asp:TextBox ID="txtAdres" runat="server" Rows="5" TextMode="MultiLine" Height="80px" Width="345px"></asp:TextBox><br />
         <asp:RequiredFieldValidator ID="rfvAdres" runat="server" ControlToValidate="txtAdres"
              Display="Dynamic" ErrorMessage="Lütfen  teslimat adresi belirtiniz." 
              CssClass="input-notification error" ForeColor=""
              ClientIDMode="Static" ValidationGroup="kayit" ></asp:RequiredFieldValidator>
      </div>
       <div>
        <small>Şehir</small>
          <asp:DropDownList ID="ddlSehirler" runat="server" Width="345px" Height="30px"  CssClass="text" /><br />
            
         <br />
          <asp:RequiredFieldValidator ID="rfvSehir" runat="server" ControlToValidate="ddlSehirler" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen  Şehir Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue="0" SetFocusOnError="true" ToolTip="Şehir Seçiniz." 
                        ValidationGroup="kayit"></asp:RequiredFieldValidator>
      </div>

      <div class="adrFatura" > 
      <div>
      Fatura Cinsi: 
      <label><input id="adrBireysel" runat="server" clientidmode="Static" name="adrFatura"
       checked="True" type="radio" />Bireysel</label>
      <label><input id="adrKurumsal" runat="server" clientidmode="Static" name="adrFatura" type="radio" />Kurumsal</label>
     </div>
      <div class="bireysel" >
        <small> TC Kimlik Numarası</small>
         <asp:TextBox ID="txtAdrTCno" runat="server" ClientIDMode="Static" CssClass="text" Width="350px" >
         </asp:TextBox><br />
        
    <%--    <asp:RequiredFieldValidator ID="rfvAdrTCno" runat="server" 
             ControlToValidate="txtAdrTCno" Display="Dynamic" ErrorMessage="Lütfen  T.C no alanını doldurunuz." 
             CssClass="input-notification error" ForeColor="" ClientIDMode="Static"
             ValidationGroup="kayit" ></asp:RequiredFieldValidator>--%>
          
      </div>
      <div class="kurumsal" style="display:none" >
         <small> Vergi No</small>
         <asp:TextBox ID="txtAdrVergiNo" runat="server" ClientIDMode="Static"  CssClass="text" Width="350px">
         </asp:TextBox><br />
         <asp:RequiredFieldValidator ID="rfvAdrVergiNo" runat="server" ControlToValidate="txtAdrVergiNo"
              Display="Dynamic"  ErrorMessage=" Lütfen  vergi numarası alanını doldurunuz." 
              ClientIDMode="Static" CssClass="input-notification error" ForeColor="" 
              ValidationGroup="kayit" Enabled="false" ></asp:RequiredFieldValidator>

      </div>
      <div class="kurumsal" style="display:none" >
         <small> Vergi Dairesi</small>
         <asp:TextBox ID="txtAdrVergiDaire" runat="server" ClientIDMode="Static"   CssClass="text" Width="350px">
         </asp:TextBox><br />

         <asp:RequiredFieldValidator ID="rfvAdrVergiDaire" runat="server" ControlToValidate="txtAdrVergiDaire" 
              Display="Dynamic" ErrorMessage="Lütfen  vergi dairesi alanını doldurunuz." ClientIDMode="Static"
              CssClass="input-notification error" ForeColor="" ValidationGroup="kayit"  Enabled="false" ></asp:RequiredFieldValidator>
      </div>
      
      </div>
      <div>
         <label style="color:#dd003e" >Fatura adresi için yeni adres girmek istiyorum. 
         <asp:CheckBox ID="ckbAdres" ClientIDMode="Static" runat="server" /></label>
      </div>
      <div>
      </div>
     </div>
     <div id="pnlFaturaForm" style="display:none;" >
     <span class="formBsalik" >Fatura Adresinizi Yazınız</span>
    <div class="faturaSecim" >
      Fatura Cinsi:
      <label><input id="fatBireysel" runat="server" clientidmode="Static" name="adres" checked="True" type="radio" />Bireysel</label>
      <label><input id="fatKurumsal" runat="server" clientidmode="Static" name="adres" type="radio" />Kurumsal</label>
   </div>
    <div id="bireysel" >
      <div>
          <small>Ad Soyad</small>
          <asp:TextBox ID="txtBirAdsoyad" runat="server" CssClass="text" Width="350px" ></asp:TextBox><br />
          <asp:RequiredFieldValidator ID="rfvBirAdsoyad" runat="server" ControlToValidate="txtBirAdsoyad" Display="Dynamic"
               ErrorMessage=" Lütfen  ad soyad alanını doldurunuz." CssClass="input-notification error" ForeColor=""
               ClientIDMode="Static" ValidationGroup="kayit" Enabled="False" >
               </asp:RequiredFieldValidator> 
      </div>
      <div>
         <small> TC Kimlik Numarası</small>
         <asp:TextBox ID="txtBirTcNo" runat="server" CssClass="text" Width="350px" ></asp:TextBox><br />
         <asp:RequiredFieldValidator ID="rfvBirTcNo" runat="server" ControlToValidate="txtBirTcNo" Display="Dynamic"
              ErrorMessage="Lütfen  T.C no alanını doldurunuz."  CssClass="input-notification error" ForeColor=""
              SetFocusOnError="true" ValidationGroup="kayit" ClientIDMode="Static" Enabled="False" >
              </asp:RequiredFieldValidator>
      </div>
      <div>
          <small>Adres</small>
          <asp:TextBox ID="txtBirAdres" runat="server" Rows="5" TextMode="MultiLine" Height="100px" Width="345px"></asp:TextBox><br />
          <asp:RequiredFieldValidator ID="rfvBirAdres" runat="server" ControlToValidate="txtBirAdres" Display="Dynamic"
               ErrorMessage="Lütfen  fatura adresi belirtiniz." CssClass="input-notification error" ForeColor=""
               SetFocusOnError="true" ValidationGroup="kayit" ClientIDMode="Static" Enabled="False" >
               </asp:RequiredFieldValidator>
      </div>
  </div>

    <div id="kurumsal" style="display:none" >
      <div>
          <small>Fatura Ünvanı</small>
          <asp:TextBox ID="txtKurUnvan" runat="server" CssClass="text" Width="350px" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvKurUnvan" runat="server" ControlToValidate="txtKurUnvan" 
               Display="Dynamic" ErrorMessage="Lütfen  ünvan alanını doldurunuz." ClientIDMode="Static"
               CssClass="input-notification error" ForeColor="" ValidationGroup="kayit" Enabled="false" >
               </asp:RequiredFieldValidator>
      </div>
      <div>
          <small>Vergi No</small> 
          <asp:TextBox ID="txtKurVergiNo" runat="server" CssClass="text" Width="350px" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvKurVergiNo" runat="server" ControlToValidate="txtKurVergiNo"
               Display="Dynamic" ErrorMessage=" Lütfen  vergi numarası alanını doldurunuz." ClientIDMode="Static"
               CssClass="input-notification error" ForeColor="" ValidationGroup="kayit" Enabled="false" >
               </asp:RequiredFieldValidator>
      </div>
      <div>
          <small>Vergi Dairesi</small>
          <asp:TextBox ID="txtKurVergiDairesi" runat="server" CssClass="text" Width="350px" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvKurVergiDairesi" runat="server" ControlToValidate="txtKurVergiDairesi" 
              Display="Dynamic" ErrorMessage="Lütfen  vergi dairesi alanını doldurunuz." ClientIDMode="Static" 
              CssClass="input-notification error" ForeColor="" ValidationGroup="kayit" Enabled="false" >
              </asp:RequiredFieldValidator>
      </div>
      <div>
          <small>Adres</small>
          <asp:TextBox ID="txtKurAdres" runat="server" Rows="5" TextMode="MultiLine" Height="100px" Width="345px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvKurAdres" runat="server" ControlToValidate="txtKurAdres"
              Display="Dynamic" ErrorMessage="Lütfen  fatura adresi belirtiniz." ClientIDMode="Static"
              CssClass="input-notification error" ForeColor="" ValidationGroup="kayit" Enabled="false" >
              </asp:RequiredFieldValidator>
      </div>

        
  </div>

  </div>                  
     <div style="clear:both; text-align:right;" >
       
        <asp:CheckBox ID="ckbSozlesmeOnay" ClientIDMode="Static"  runat="server" /> 
         <a href="#hediye_box" id="hlHediye" style=" color: #DD003E;">
                      Gizlilik Politikası
                    </a>
                    <div id="hediye_box" style="display:none" >
                        <div class="formBsalik" ><b> Gizlilik Politikası  </b></div>
                        <div  style="text-align:left;line-height:18px; " >
                         www.lensoptik.com.tr tarafınızdan talep ettiği kişisel bilgilerin gizliliğini korumayı 
                         ve almak istediğiniz hizmetin size ulaşabilmesi dışında amaçlar için kullanılmayacağını,
                          kesinlikle üçüncü şahıslarla paylaşılmayacağını garanti eder. Bu bilgiler yalnızca satın
                          alınan ürün ve hizmetlerin gönderimini sağlamak, fatura etmek için kullanılmaktadır.
                        </div>
                    
			        </div>
     
     </div>
     <div class="clear" style="text-align:right;" >


          <asp:ImageButton ID="imBtnSatinAl" ImageUrl="~/images/buttonlar/devam.gif" 
                 runat="server" onclick="imBtnSatinAl_Click" ValidationGroup="kayit" ClientIDMode="Static"  /> 

                  
     </div>
   
     <script type="text/javascript">

     $(document).ready(function () {

         var bireysel = ["rfvBirAdsoyad", "rfvBirTcNo", "rfvBirAdres"];
         var kurumsal = ["rfvKurUnvan", "rfvKurVergiNo", "rfvKurVergiDairesi", "rfvKurAdres"];
         var adrBireysel = ["rfvAdrTCno"];
         var adrKurumsal = ["rfvAdrVergiNo", "rfvAdrVergiDaire"];


         $("#ckbAdres").click(function () {

             var deger = $(this);
             if (deger.is(':checked')) {
                 $("#pnlFaturaForm").show();
                 $(".adrFatura").hide();
                 $("#txtAdrTCno").val("");

                 ValidationHandler(adrBireysel.concat(adrKurumsal, kurumsal), false);
                 ValidationHandler(bireysel, true);
             }
             else {
                 $("#pnlFaturaForm").hide();
                 $(".adrFatura").show();

                 ValidatorEnable($('#rfvAdrTCno')[0], true);
                 ValidationHandler(kurumsal.concat(bireysel), false);
             }
         });


         $("#adrBireysel").click(function () {
             $(".bireysel").show();
             $(".kurumsal").hide()

             ValidationHandler(adrBireysel, true);
             ValidationHandler(adrKurumsal,false);
         });


         $("#adrKurumsal").click(function () {
             $(".kurumsal").show();
             $(".bireysel").hide();

             ValidationHandler(adrBireysel, false);
             ValidationHandler(adrKurumsal, true);
         });

         $("#fatBireysel").click(function () {
             $("#bireysel").show();
             $("#kurumsal").hide()

             ValidationHandler(bireysel, true);
             ValidationHandler(kurumsal, false);
         });

         $("#fatKurumsal").click(function () {
             $("#kurumsal").show();
             $("#bireysel").hide();

             ValidationHandler(bireysel, false);
             ValidationHandler(kurumsal, true);
         });



     });


    function ValidationHandler(dizi,islem) {

        $.each(dizi, function (index, item) {
            if (islem) {
                ValidatorEnable(document.getElementById(item), true);
                $("#" + item).hide();
            }
            else {
                ValidatorEnable(document.getElementById(item), false);
            }
        });
    }

 </script>

    </div>
</asp:Content>

