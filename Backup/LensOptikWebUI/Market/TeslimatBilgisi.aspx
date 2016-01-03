<%@ Page Title="" Language="C#" MasterPageFile="~/Market/Market.master" AutoEventWireup="True"  Inherits="Market_Teslimat_Bilgisi" EnableEventValidation="false"  Codebehind="TeslimatBilgisi.aspx.cs" %>

<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc1" %>
<%@ Register src="MenuMarket.ascx" tagname="MenuMarket" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

<link rel="Stylesheet" href="../Style/TeslimatBilgileri.css" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
 </asp:ScriptManager>

  <script type="text/javascript">

      var bireysel = ["rfvBirAdsoyad", "rfvBirTcNo", "rfvBirAdres"];
          var kurumsal = ["rfvKurUnvan", "rfvKurVergiNo", "rfvKurVergiDairesi", "rfvKurAdres"];

          function fnKurumsal() {
              $("#kurumsal").show();
              $("#bireysel").hide();

              ValidationHandler(bireysel, false);
              ValidationHandler(kurumsal, true);
          }

          function fnBireysel() {

              $("#bireysel").show();
              $("#kurumsal").hide()

              ValidationHandler(bireysel, true);
              ValidationHandler(kurumsal, false);
          }

          function ValidationHandler(dizi, islem) {

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

          function Adres(obje) {

              var adresId = $(obje).val()

              $('#hdfSecAdres').val(adresId.toString());

              $('input[name="adres"]').parent().parent().removeClass("aktiv");

              $(obje).parent().parent().addClass("aktiv");

          }


          function Fatura(obje) {

              var adresId = $(obje).val()

              $('#hdfSecFatura').val(adresId.toString());

              $('input[name="fatura"]').parent().parent().removeClass("aktiv");

              $(obje).parent().parent().addClass("aktiv");

          }

          $(document).ready(function () {

              $("input[name='adres']").first().attr("checked", true).parent().parent().addClass("aktiv");
              $("input[name='fatura']").first().attr("checked", true).parent().parent().addClass("aktiv");

              $("#btnOnay").click(function () {

                  var adresId = $("#hdfSecAdres").val();
                  var faturaId = $("#hdfSecFatura").val();


                  if (faturaId == 0 && adresId == 0) {
                      alert("* Lütfen teslimat adresi  seçiniz.\n * Lütfen fatura bilgisi seçiniz.");
                      return false;
                  }
                  if (faturaId == 0) {
                      alert("* Lütfen fatura bilgisi seçiniz.");
                      return false;
                  }
                  if (adresId == 0) {
                      alert("* Lütfen teslimat adresi  seçiniz.");
                      return false;
                  }

              })


          });
         
  </script>

 <div id="marketAna" >
     <div id="marketMap">
       <uc2:MenuMarket ID="Menu" runat="server" />
     </div>
     <div id="marketMesaj" style="margin-bottom:20px;" >
       <uc1:Mesajlar ID="Mesaj" runat="server" />
     </div>
     
    
     <asp:Panel ID="adreslerBilgi" runat="server">
        <div class="icerik-Baslik Border"><a name="fatura"></a><h5> Adres Bilgileri </h5></div>
        <div class="bilgi">
      <div class="bilgi1">
      Siparişinizin teslimat adresi olarak aşağıdaki size ait adreslerden birini seçebilir, dilerseniz yeni bir adres girebilirsiniz.
     </div>
            <div class="bilgi2" >
            <asp:ImageButton ID="btnYeniAdres" ImageUrl="~/Images/buttonlar/adresOlustur.gif" 
                    runat="server" onclick="btnYeniAdres_Click"  />
            </div>
        </div>
    </asp:Panel>

   <asp:Repeater ID="rptAdresler" runat="server" onitemcommand="rptAdresler_ItemCommand" >
      <ItemTemplate>
      <div class="adresDiv" >
        <div class="secenekDiv" >

        <label>
           <input name="adres" onclick="Adres(this);" type="radio" value='<%# DataBinder.Eval(Container, "DataItem.id")%>'  />
           Bu Adresimi Kullan 
        </label>

         </div>
        <div class="Adres" >
             <b>Teslim Edilecek Kişi: </b><%# DataBinder.Eval(Container, "DataItem.TeslimAlan")%><br /><b>Telefon: </b>   <%# DataBinder.Eval(Container, "DataItem.Telefon")%><br />
             <b> Adres: </b><%# DataBinder.Eval(Container, "DataItem.Adres")%>
             <br />
             <b>Şehir:</b> <%# DataBinder.Eval(Container, "DataItem.Sehir")%></div>           
        <div>

          <asp:Button ID="btnAdresDuzenle" runat="server" CssClass="btn duzenle" Text="Düzenle" 
          CommandName="adresDuzenle" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id")%>'  />

           <asp:Button ID="btnAdresSil" runat="server" CssClass="btn sil" CommandName="adresSil" 
          OnClientClick="return confirm('Adres bilgisini silmek istediğinizden emin misiniz?');" 
          CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id")%>' Text=" Adres Sil " />

          <asp:Button ID="btnYeniAdres" runat="server" CssClass="btn duzenle" Text="Adres Ekle" 
          CommandName="adresEkle" />

        </div>

      </div>
      </ItemTemplate>
   </asp:Repeater>
   <div class="clear"></div>

   <div class="icerik-Baslik Border"><a name="fatura"></a><h5> Fatura Bilgileri </h5></div>
   <div class="bilgi" id="faturaBilgi">
     <div class="bilgi1">
Fatura bilgileri olarak aşağıdaki size ait fatura bilgisi seçebilir, dilerseniz yeni bir fatura profili tanımlayabilirsiniz.
    </div>
     <div class="bilgi2" >
            <asp:ImageButton ID="btnYeniFaturaAc" ImageUrl="~/Images/buttonlar/faturaOlustur.gif" 
                    runat="server"  onclick="btnYeniFaturaAc_Click" />
            </div>

   </div>

        <asp:Repeater ID="rptFaturaBilgi" runat="server" onitemcommand="rptFaturaBilgi_ItemCommand"  >
        <ItemTemplate>
        <div class="adresDiv" >
            <div class="secenekDiv" >

            <label>
            <input name="fatura" checked="checked" onclick="Fatura(this);" type="radio" value='<%# DataBinder.Eval(Container, "DataItem.id")%>'  />
               <%# Eval("faturaCinsi").ToString() == "True" ? " Bireysel fatura bilgilerim kullan  " : " Kurumsal fatura bilgilerim kullan." %> 
            </label>

            </div>
            <div class="Adres" >
                    <%# DataBinder.Eval(Container, "DataItem.adSoyad")%>
                    <%# DataBinder.Eval(Container, "DataItem.unvan")%>

                    <%# Eval("tcNo").ToString()    ==  " " ? "  "  :  "<br/><b> T.C No: </b> " + Eval("tcNo") %>
                    <%# Eval("vergiNo").ToString() != " " ? "<br/><b> Vergi No:</b> " + Eval("vergiNo") : " "%>
                    <%# Eval("vergiDairesi").ToString() != " " ? "<br/> <b>Vergi Dairesi: </b> " + Eval("vergiDairesi") : " "%>
                   <br/>
                  <b>Fatura Adresi: </b>  <%# DataBinder.Eval(Container, "DataItem.faturaAdresi")%>
            </div>
       <div>
       <asp:Button ID="faturaBilgisiDuzenle" CssClass="btn duzenle" Text="Düzenle"
           CommandName="faturaDuzenle" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id")%>' runat="server" />

       <asp:Button ID="faturaBilgisiSil" CommandName="faturaSil" 
               CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id")%>' 
               OnClientClick="return confirm('Bu fatura bilgisini silmek istediğinizden emin misiniz?');" 
               runat="server" CssClass="btn sil" Text="Fatura Sil" />

       <asp:Button ID="btnYeniFatura" CssClass="btn duzenle" Text="Fatura Ekle"
           CommandName="yeniFatura"  runat="server" />
         </div>
        </div> 
        </ItemTemplate>
        </asp:Repeater>


    <div class="clear" style="text-align:right;" >
    <input id="btnShow" type="button"  runat="server" style="display:none" />
    <asp:HiddenField ID="hdfSecAdres" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hdfSecFatura" ClientIDMode="Static" runat="server" Value="0" />

      <asp:ImageButton ID="btnOnay" ImageUrl="~/images/buttonlar/devam.gif" 
            runat="server"  ClientIDMode="Static" onclick="btnOnay_Click" />
                 
    </div>
     
     
   <cc1:ModalPopupExtender ID="mpeAdresForm" BackgroundCssClass="popUpBg" runat="server"  
        TargetControlID="btnShow" PopupControlID="pnlAdresPopup" >
   </cc1:ModalPopupExtender>


   



   <asp:Panel ID="pnlAdresPopup" runat="server" Style="display: none">
            
   <div id="facebox" > 
   <div class="popup">    
        <table>        
           <tbody>             
           <tr>              
              <td class="tl"></td>
              <td class="b"></td>
              <td class="tr"></td>           
              </tr>           
              <tr>            
              <td class="b"></td>    
              <td class="body">  
               <div class="content" style="display: block; ">
                <div id="messages" style="width:400px" >
                 <span class="formBsalik" style="height:20px;padding-top:10px; font-weight:bold;" > Teslimat Adresinizi Yazınız  </span>
                   <div>
           <small> Teslim Edilecek Kişi</small>
           <asp:TextBox ID="txtTeslimAlan" runat="server" CssClass="text" Width="350px" ></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="vaTeslimAlan" runat="server" 
                 ControlToValidate="txtTeslimAlan" Display="Dynamic"
                 CssClass="input-notification error" ForeColor=""
                 ErrorMessage="Lütfen  kargoyo teslim alan kişiyi belirtiniz." 
                 SetFocusOnError="true" ValidationGroup="adresKayit" ></asp:RequiredFieldValidator>
     </div>
                   <div>
          <small>İrtibat Telefonu</small>
          <asp:TextBox ID="txtTelefon" runat="server" CssClass="text"  Width="350px" ></asp:TextBox> <br />
          <asp:RequiredFieldValidator ID="vaTelefon" runat="server" 
                ControlToValidate="txtTelefon" Display="Dynamic" 
                ErrorMessage="Lütfen  irtibat telefonu belirtiniz." 
                CssClass="input-notification error" ForeColor=""
                SetFocusOnError="true" ValidationGroup="adresKayit" ></asp:RequiredFieldValidator>
       </div>
                   <div>
        <small>Teslimat Adresi</small>
         <asp:TextBox ID="txtAdres" runat="server" Rows="5" TextMode="MultiLine" Height="100px" Width="345px"></asp:TextBox><br />
         <asp:RequiredFieldValidator ID="vaAdres" runat="server" ControlToValidate="txtAdres"
              Display="Dynamic" ErrorMessage="Lütfen  teslimat adresi belirtiniz." 
              CssClass="input-notification error" ForeColor=""
              SetFocusOnError="true" ValidationGroup="adresKayit" ></asp:RequiredFieldValidator>
      </div>

        <div>
        <small>Şehir</small>
         <asp:DropDownList ID="ddlSehirler" runat="server" Width="345px" Height="30px"  CssClass="text" /><br />
         <br />
         <asp:RequiredFieldValidator ID="rfvSehir" runat="server" ControlToValidate="ddlSehirler" 
              Display="Dynamic" ErrorMessage="Lütfen  Şehir Seçiniz." 
              CssClass="input-notification error" ForeColor=""
              InitialValue="0" SetFocusOnError="true" ToolTip="Şehir Seçiniz." 
              ValidationGroup="kayit"></asp:RequiredFieldValidator>
      </div>

            <div style="padding-top:15px;" >
            <asp:HiddenField ID="hdAdresId" runat="server" />

            <asp:Button ID="btnAdresKayit" runat="server" Text=" Kaydet " CssClass="btn sil" 
            onclick="btnAdresKayit_Click" ValidationGroup="adresKayit" Font-Bold="true" />     
            <asp:Button ID="btnAdresDuzenle" runat="server" Text=" Düzenle " CssClass="btn sil" 
            onclick="btnAdresDuzenle_Click" Visible="False" Font-Bold="true" />

           </div>


                </div>
                </div> 
                 <div class="footer" style="display: block; ">     
                   
                   <asp:ImageButton ID="imgKapat" ImageUrl="~/Admin/images/closelabel.gif" 
                   CausesValidation="False"  CssClass="close_image" runat="server" />

                  </div>               
                 </td>              
                 <td class="b"></td>         
                </tr>        
              <tr> 
                  <td class="bl"></td>
                  <td class="b"></td>
                  <td class="br"></td>        
              </tr>        
             </tbody>        
        </table>    
   </div>
</div>
   </asp:Panel>


   <cc1:ModalPopupExtender ID="mpeFaturaForm" BackgroundCssClass="popUpBg" runat="server"  
        TargetControlID="btnShow" PopupControlID="pnlFaturaPopup"   >
   </cc1:ModalPopupExtender>
 
   <asp:Panel ID="pnlFaturaPopup" runat="server" Style="display: none">
            
   <div id="facebox" > 
   <div class="popup">    
        <table>        
           <tbody>             
           <tr>              
              <td class="tl"></td>
              <td class="b"></td>
              <td class="tr"></td>           
              </tr>           
              <tr>            
              <td class="b"></td>    
              <td class="body">  
               <div class="content" style="display: block; ">
                <div style="width:400px; " >
                 <span class="formBsalik" style="height:20px;padding-top:10px; font-weight:bold;" >
                  Fatura Adresinizi Yazınız </span>
              
              
      <div class="faturaSecim" >

      <small>Fatura Cinsi: </small> 

    
      <label>
      <input id="fatBireysel" runat="server" onclick="fnBireysel()" clientidmode="Static" name="adres" checked="True" type="radio" />
      Bireysel</label>
      <label>
      <input id="fatKurumsal" runat="server" onclick="fnKurumsal()" clientidmode="Static" name="adres" type="radio" />
      Kurumsal</label>

   </div>

       <div id="bireysel" >
      <div>
          <small>Ad Soyad</small>
          <asp:TextBox ID="txtBirAdsoyad" runat="server" CssClass="text" Width="350px" ></asp:TextBox><br />
          <asp:RequiredFieldValidator ID="rfvBirAdsoyad" runat="server" 
               ControlToValidate="txtBirAdsoyad" Display="Dynamic"
               ErrorMessage=" Lütfen  ad soyad alanını doldurunuz. "
               CssClass="input-notification error" ForeColor="" Clientidmode="Static"
               SetFocusOnError="true" ValidationGroup="fatKaydet" ></asp:RequiredFieldValidator> 
      </div>
      <div>
        <small> T.C No:</small>
        <asp:TextBox ID="txtBirTcNo" runat="server" CssClass="text" Width="350px" ></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="rfvBirTcNo" runat="server" 
             ControlToValidate="txtBirTcNo" Display="Dynamic"
             ErrorMessage="Lütfen  T.C no alanını doldurunuz." 
             CssClass="input-notification error" ForeColor="" ClientIDMode="Static"
             SetFocusOnError="true" ValidationGroup="fatKaydet" ></asp:RequiredFieldValidator>
      </div>
      <div>
          <small>Adres:</small>
          <asp:TextBox ID="txtBirAdres" runat="server" Rows="5" TextMode="MultiLine" Height="100px" Width="345px"></asp:TextBox><br />
          <asp:RequiredFieldValidator ID="rfvBirAdres" runat="server" 
               ControlToValidate="txtBirAdres" Display="Dynamic"
               ErrorMessage="Lütfen  fatura adresi belirtiniz." 
               CssClass="input-notification error" ForeColor="" ClientIDMode="Static"
               SetFocusOnError="true" ValidationGroup="fatKaydet" ></asp:RequiredFieldValidator>
      </div>
    
  </div>

       <div id="kurumsal" style="display:none" >
      <div>
          <small>Fatura Ünvanı</small>
          <asp:TextBox ID="txtKurUnvan" runat="server" CssClass="text" Width="350px" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvKurUnvan" runat="server" ControlToValidate="txtKurUnvan" 
               Display="Dynamic" ErrorMessage="Lütfen  ünvan alanını doldurunuz." Enabled="false"
               CssClass="input-notification error" ForeColor="" ValidationGroup="fatKaydet" ClientIDMode="Static" >
               </asp:RequiredFieldValidator>
      </div>
      <div>
          <small>Vergi No</small> 
          <asp:TextBox ID="txtKurVergiNo" runat="server" CssClass="text" Width="350px" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvKurVergiNo" runat="server" ControlToValidate="txtKurVergiNo"
               Display="Dynamic" ErrorMessage=" Lütfen  vergi numarası alanını doldurunuz." Enabled="false"
               CssClass="input-notification error" ForeColor="" ValidationGroup="fatKaydet" ClientIDMode="Static" >
               </asp:RequiredFieldValidator>
      </div>
      <div>
          <small>Vergi Dairesi</small>
          <asp:TextBox ID="txtKurVergiDairesi" runat="server" CssClass="text" Width="350px" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvKurVergiDairesi" runat="server" ControlToValidate="txtKurVergiDairesi" 
              Display="Dynamic" ErrorMessage="Lütfen  vergi dairesi alanını doldurunuz." Enabled="false" 
              CssClass="input-notification error" ForeColor="" ValidationGroup="fatKaydet" ClientIDMode="Static" >
              </asp:RequiredFieldValidator>
      </div>
      <div>
          <small>Adres</small>
          <asp:TextBox ID="txtKurAdres" runat="server" Rows="5" TextMode="MultiLine" Height="100px" Width="345px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="rfvKurAdres" runat="server" ControlToValidate="txtKurAdres"
              Display="Dynamic" ErrorMessage="Lütfen  fatura adresi belirtiniz." Enabled="false"
              CssClass="input-notification error" ForeColor="" ValidationGroup="fatKaydet" ClientIDMode="Static" >
              </asp:RequiredFieldValidator>
      </div>
  
  </div>


    <div style="padding-top:15px" >
    <asp:HiddenField ID="hdFaturaId" runat="server" />

      <asp:Button ID="btnFaturaKaydet" runat="server" CssClass="btn sil" Text=" Kaydet "
       OnClick="btnFaturaKaydet_Click" ValidationGroup="fatKaydet" />

     <asp:Button ID="btnFaturaDuzenle" runat="server" CssClass="btn sil" 
      Text=" Düzenle " OnClick="btnFaturaDuzenle_Click" Visible="false" ValidationGroup="fatKaydet"  />


      </div>

                </div>
                </div> 
                 <div class="footer" style="display: block; ">     
                   
                   <asp:ImageButton ID="ImageButton1" ImageUrl="~/Admin/images/closelabel.gif" 
                   CausesValidation="False"  CssClass="close_image" runat="server" />

                  </div>               
                 </td>              
                 <td class="b"></td>         
                </tr>        
              <tr> 
                  <td class="bl"></td>
                  <td class="b"></td>
                  <td class="br"></td>        
              </tr>        
             </tbody>        
        </table>    
   </div>
</div>
   </asp:Panel>


  

   </div>

</asp:Content>

