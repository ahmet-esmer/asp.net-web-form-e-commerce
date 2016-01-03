<%@ Page Title="Havale ile Ödeme | Lens Optik " Language="C#" MasterPageFile="~/Market/Market.master" AutoEventWireup="true" Inherits="Market_Odeme_Havale" Codebehind="OdemeHavale.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="MenuMarket.ascx" tagname="MenuMarket" tagprefix="uc3" %>
<%@ Register src="MenuOdeme.ascx" tagname="MenuOdeme" tagprefix="uc1" %>


<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

     <script type="text/javascript">

         $(document).ready(function () {

             $("#btnHavale").click(function () {

                 if ($("#hdfHesap").val() == "0" ) {
                     alert("! Lütfen Havalenin Yapılacağı Bankayı Seçini.");
                     return false;
                 }
             })

             $(".bankaHesaplari dt a").click(function () {
                 $(".bankaHesaplari dd ul").slideDown('slow').show();
             });

             $(".bankaHesaplari dd ul li a").click(function () {
                 var text = $(this).html();
                 $(".bankaHesaplari dt a span").html(text);
                 $(".bankaHesaplari dd ul").slideUp('slow');
                 $('#hdfHesap').val(getSelectedValue("bankaHesaplari"));
             });

             function getSelectedValue(id) {
                 return $("#" + id).find("dt a span.value").html();
             }

             $(document).bind('click', function (e) {
                 var $clicked = $(e.target);
                 if (!$clicked.parents().hasClass("bankaHesaplari"))
                     $(".bankaHesaplari dd ul").slideUp('slow');
             });

         });

    </script>

  <div  id="marketAna" >
   <div id="marketMap">
       <uc3:MenuMarket ID="Menu" runat="server" />
  </div>

   <div id="marketMesaj" style="margin:15px 0;" >
       <uc2:Mesajlar ID="Mesaj" runat="server" />
    </div>
   <div>
       <uc1:MenuOdeme ID="ucMenuOdeme" runat="server" />
  </div>

   <table id="odemeTbl" >
   <tr>
   <td colspan="3"  >
    
    <div id="pnlPuan" class="puan" runat="server" clientidmode="Static" visible="false" >
  Siparişinizde kullanabileceginiz
  <asp:Label ID="lblKulaniciPuan" Font-Bold="true" ClientIDMode="Static" runat="server"  ></asp:Label>  bulunmaktadır
          <br/>
          <label> <asp:CheckBox ID="ckbUyePuan" onClick="puan(this)" ClientIDMode="Static" runat="server" /> Siparişimde Kullanmak İstiyorum </label>
            </div>
   
   </td>
   </tr>
   <tr>
            <td colspan="3" style="padding-bottom:20px;padding-top:20px; " >
               <div class="odemeHesap1"> Havale Hesapları: </div>
               <div class="odemeHesap2">
                 <dl id="bankaHesaplari" class="bankaHesaplari">
                    <dt><a href="#">
                      <span>Lütfen Havale İşlemi Yapacağınız Bankan Hesabını Seçiniz.</span></a>
                    </dt>
                    <dd style="z-index:1000;" >
                      <ul>
                        <asp:Repeater ID="rptbankaHesaplari" runat="server">
                            <ItemTemplate>
                            <li>
                                <a href="#"> <%# Eval("bankaAdi")%>  
                                <%# Eval("sube")%> Şube Kodu: <%# Eval("subeKod")%> 
                                Hesap Numarası: <%# Eval("hesapNo")%> 
                                <span class="value" style="display:none;" ><%# Eval("bankaAdi")%></span>
                                </a>
                            </li>
                            </ItemTemplate>
                            </asp:Repeater>     
                      </ul>
                   </dd>
                 </dl>
                <asp:HiddenField ID="hdfHesap"  ClientIDMode="Static" Value="0" runat="server" />
              </div>
            </td>
         </tr>
         <tr>
         <td colspan="3" >
         <asp:GridView ID="gvwSepetHavale" runat="server"  AutoGenerateColumns="False" CellPadding="4" 
            ForeColor="#333333" GridLines="None"   Width="100%" EnableModelValidation="True"  >
                       <Columns>
                           <asp:TemplateField >
                                <ItemTemplate>
                              <asp:Image ID="imgHavale" CssClass="sepetResim"
                               ImageUrl='<%#"~/Products/Small/"+ Eval("Resim") %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle CssClass="ortaResim" />
                                <HeaderStyle Width="100px" CssClass="orta"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ürün Adı" >
                               <ItemTemplate>
                                <span class="urunAdiLink" ><%# Eval("Urun")%></span>
                                <br />
                                <label class="urunAdiLink" ><%# Eval("SagBilgi") %></label>   
                                <label class="urunAdiLink" ><%# Eval("SolBilgi") %></label>
                                <br />
                               <%# Eval("HediyeHTML")%>
                               <%# Eval("HediyeUrunTekHTML")%>
                               </ItemTemplate>
                                <ItemStyle CssClass="orta" />
                                <HeaderStyle CssClass="orta" Width="420px" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Birim Fiyatı"  >
                               <ItemTemplate>
                                   <%# Eval("Fiyat") %>
                               </ItemTemplate>
                                <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="100px" CssClass="orta" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="KDV" >
                               <ItemTemplate>
                                   <%# Eval("KDV")%> 
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="80px" CssClass="orta"  />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Adet">
                               <ItemTemplate>
                               <span id="lblMiktar" ><%# Eval("Miktar") %></span>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="100px" CssClass="orta" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText=" Birim Toplam" >
                               <ItemTemplate>
                                 <%# Eval("Birim")%>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="120px" CssClass="orta" />
                           </asp:TemplateField>
                       </Columns>
                       <RowStyle  CssClass="RowStyle" />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>
         </td>
         </tr>
         <tr>
          <td class="toplamBilgi" style="border-top:1px solid #d9dada;" colspan="2"  >
           Birim Toplam:
          </td>
          <td class="toplamFiyat" style="border-top:1px solid #d9dada; border-right:1px solid #d9dada; width:230px;" >
              <asp:Label ID="lblBirimToplam" runat="server"></asp:Label>
          </td>
          </tr>
           <tr runat="server" id="trIndirim" visible="false" >
            <td class="toplamBilgi" style="color:#d2063f;" colspan="2" >
                İndirim:
            </td>
            <td class="toplamFiyat" style="color:#d2063f;" >
                <asp:Label ID="lblIndirim" runat="server"></asp:Label>
            </td>
          </tr>
             <tr runat="server" id="trHavale" visible="false">
              <td class="toplamBilgi">
             </td>
             <td class="toplamBilgi" style="color:#d20940;" >
               Havale İndirimi:
            </td>
            <td class="toplamFiyat" style=" border-right:1px solid #d9dada;" >
             <asp:Label ID="lblHavaleIndirim" ForeColor="#d20940" runat="server" ></asp:Label>
             </td>
          </tr>
         <tr>
           <td class="toplamBilgi" colspan="2" >
               KDV Toplam:
           </td>
            <td class="toplamFiyat"  style=" border-right:1px solid #d9dada;"  >
                <asp:Label ID="lblKdvToplam" runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="toplamBilgi" style="text-align:left; padding-left:20px;" >

                <asp:Image ID="Image1" ImageUrl="~/Images/kargo.gif" runat="server" />
                    
            </td>
            <td class="toplamBilgi">
                <asp:Label ID="lblKargoAdi" runat="server"></asp:Label>
              Kargo Ücreti:
            </td>
            <td class="toplamFiyat">
                <asp:Label ID="lblKargoToplam" runat="server"></asp:Label>
            </td>
            </tr>
         
         <tr>
            <td class="toplamBilgi" style="border-bottom:1px solid #d9dada;" colspan="2"  >
               Toplam Ödeme:
            </td>
            <td class="toplamFiyat" style="border-bottom:1px solid #d9dada;  border-right:1px solid #d9dada;"  >
               <asp:Label ID="lblToplam" runat="server"></asp:Label>
           </td>
          </tr>
         <tr>
            <td colspan="3"  style="padding:25px 20px; text-align:right;"  >   
            <asp:ImageButton ID="btnHavale" ClientIDMode="Static" onclick="btnHavale_Click" ImageUrl="~/images/buttonlar/havaleIleOdeme.gif" runat="server" Text="Havale ile İşlemi Sonlandır" />

           </td>
         </tr>
   </table>
        

   </div>
</asp:Content>

