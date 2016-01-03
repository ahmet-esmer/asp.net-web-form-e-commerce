<%@ Page Title="Kapıda Ödeme | Lens Optik " Language="C#" MasterPageFile="~/Market/Market.master" AutoEventWireup="true" Inherits="Market_Odeme_Kapida" Codebehind="OdemeKapida.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc2" %>
<%@ Register src="MenuMarket.ascx" tagname="MenuMarket" tagprefix="uc3" %>

<%@ Register src="MenuOdeme.ascx" tagname="MenuOdeme" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

    <script type="text/javascript">

        function kapiUyari() {

            alert("Kapıda ödeme ile  100,00 TL ye kadar işlem yapabilirsiniz.");
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

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
  <td colspan="3" >
  
   <div id="pnlPuan" class="puan" runat="server" clientidmode="Static" visible="false" >
  Siparişinizde kullanabileceginiz
  <asp:Label ID="lblKulaniciPuan" Font-Bold="true" ClientIDMode="Static" runat="server"  ></asp:Label>  bulunmaktadır
          <br/>
          <label> <asp:CheckBox ID="ckbUyePuan" onClick="puan(this)" ClientIDMode="Static" runat="server" /> Siparişimde Kullanmak İstiyorum </label>
            </div>
  
  </td>
  </tr>
     <tr>
      <td colspan="3"  style="padding-top:20px;" >
         <asp:GridView ID="gvwSepetKapida" ClientIDMode="Static" runat="server"
           AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
            GridLines="None" Width="100%" EnableModelValidation="True"  >
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
         <td class="toplamBilgi" style="border-top:1px solid #d9dada;" colspan="2" >
                      Birim Toplam:
                  </td>
         <td class="toplamFiyat" style="border-top:1px solid #d9dada; border-right:1px solid #d9dada; width:230px;"   >
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
     <tr>
       <td class="toplamBilgi" colspan="2" >
                      KDV Toplam:
       </td>
       <td class="toplamFiyat"  style=" border-right:1px solid #d9dada;" >
                      <asp:Label ID="lblKdvToplam" runat="server"></asp:Label>
       </td>
     </tr>
     <tr>
       <td class="toplamBilgi" style="text-align:left; padding-left:20px;" >
           <asp:Image ID="Image4" ImageUrl="~/images/kargo.gif" runat="server" />
       </td>
      <td class="toplamBilgi">
           Kargo Ücreti:
      </td>
      <td class="toplamFiyat" style=" border-right:1px solid #d9dada;" >
          <asp:Label ID="lblKargoToplam" runat="server"></asp:Label>
       </td>
     </tr>
     <tr>
       <td class="toplamBilgi" >&nbsp;
        </td>
        <td class="toplamBilgi" style="color:#d20940;">
            Kapıda Ödeme Farkı:
        </td>
        <td class="toplamFiyat" style=" border-right:1px solid #d9dada;" >
            <asp:Label ID="lblKapidaFiyatFarki" ForeColor="#d20940" runat="server"></asp:Label>
        </td>
     </tr>
     <tr>
       <td class="toplamBilgi" style="border-bottom:1px solid #d9dada;" colspan="2" >
         Toplam Ödeme:
       </td>
       <td class="toplamFiyat" style="border-bottom:1px solid #d9dada;  border-right:1px solid #d9dada;" >
        <asp:Label ID="lblToplam" runat="server"></asp:Label>
       </td>
     </tr>
     <tr>
       <td colspan="3" style="padding:25px 20px; text-align:right"  >
           <asp:ImageButton ID="btnKapidaOdeme" onclick="btnKapidaOdeme_Click"  runat="server"   ImageUrl="~/images/buttonlar/kapidaOdeme.gif"  />   
      </td>
    </tr>
  </table> 

  </div>

</asp:Content>

