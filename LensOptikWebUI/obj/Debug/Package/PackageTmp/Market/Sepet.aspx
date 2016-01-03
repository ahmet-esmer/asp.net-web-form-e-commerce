<%@ Page Title="Alişveriş Sepeti | Lens Optik " Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Market_Sepet" Codebehind="Sepet.aspx.cs" %>

<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

<link rel="stylesheet" href="../Style/MarketGenel.css" type="text/css" />
<script src="../Scripts/Market.js"  type="text/javascript" ></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div id="marketAna">
    <div id="marketMesaj">
      <uc1:Mesajlar ID="Mesaj" runat="server" />
    </div>

    <table id="tblSepet" clientidmode="Static" runat="server" >
          <tr>
               <td colspan="2">
               <asp:GridView ID="gvwSepet" runat="server"  AutoGenerateColumns="false" CellPadding="4" 
            ForeColor="#333333" GridLines="None"  DataKeyNames="Id"   Width="100%" onrowcommand="gvwSepet_RowCommand"  >
                   <Columns>
                   <asp:TemplateField  HeaderStyle-CssClass="orta" >
                   <ItemTemplate>
                   <a href="<%# ResolveUrl(Eval("UrunLink").ToString()) %>" > 
                     <img src="<%# ResolveUrl("~/Products/Little/"+ Eval("Resim").ToString()) %>" class="sepetResim" />
                   </a>
                   </ItemTemplate>
                   <ItemStyle  />
                   <HeaderStyle Width="100"  />
                   </asp:TemplateField>
                            
                   <asp:TemplateField HeaderText="Ürün Adı" HeaderStyle-CssClass="orta" >
                   <ItemTemplate>
                    <br />
                     <a class="urunAdiLink" href="<%# ResolveUrl(Eval("UrunLink").ToString()) %>" > 
                      <%# Eval("Urun")%>
                     </a>
                     <br />
                     <span class="urunAdiLink"><%# Eval("SagBilgi") %> </span>
                     <span class="urunAdiLink"><%# Eval("SolBilgi") %> </span>
                     <br />
                     <%# Eval("HediyeHTML")%>
                     <%# Eval("HediyeUrunTekHTML")%>
                     </ItemTemplate>
                     <ItemStyle CssClass="orta"  />
                     <HeaderStyle />
                     <HeaderStyle Width="420" Font-Bold="true"  />
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="Birim Fiyatı" HeaderStyle-CssClass="orta" >
                      <ItemTemplate>
                         <%# Eval("Fiyat") %>
                      </ItemTemplate>
                      <ItemStyle CssClass="orta" />
                      <HeaderStyle Width="100" Font-Bold="true" />
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="KDV" HeaderStyle-CssClass="orta" >
                         <ItemTemplate>
                         <%# Eval("KDV")%> 
                         </ItemTemplate>
                         <ItemStyle CssClass="orta" />
                         <HeaderStyle Width="80" Font-Bold="true"  />
                     </asp:TemplateField>
                          
                      <asp:TemplateField HeaderText="Adet" HeaderStyle-CssClass="orta" >
                      <ItemTemplate>
          <%# Eval("SagAdet").ToString() == "0" || Eval("SolAdet").ToString() == "0" ? Eval("Miktar") + " " + Eval("StokCins") : "<br/><div class='gozDuz' > Sağ </div>  Göz: " + Eval("SagAdet") + " " + Eval("StokCins") + "<br/> <div class='gozDuz' > Sol  </div> Göz: " + Eval("SolAdet") + " " + Eval("StokCins")%>
                      </ItemTemplate>
                      <ItemStyle CssClass="orta" />
                      <HeaderStyle Width="140" Font-Bold="true" />
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="Birim Toplam" HeaderStyle-CssClass="orta" >
                      <ItemTemplate>
                       <%# Eval("Birim")%>
                      </ItemTemplate>
                      <ItemStyle CssClass="orta" />
                      <HeaderStyle Width="120" Font-Bold="true" />
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="Sil" HeaderStyle-CssClass="ortaSil" >
                      <ItemTemplate> 
                        <asp:ImageButton   ID="imgBtnSil"  ImageUrl="~/images/icons/sepetSil.png" CommandName="sepetSil"
                        CommandArgument='<%# Eval("Id")%>' 
                        OnClientClick="return confirm('Bu ürünü silmek istediğinizden emin misiniz?');" runat="server" />
                      </ItemTemplate>
                      <ItemStyle CssClass="ortaSil" HorizontalAlign="Right"/>
                      <HeaderStyle  Width="50" HorizontalAlign="Right" Font-Bold="true" />
                      </asp:TemplateField>
                    </Columns>
                    <RowStyle  CssClass="RowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>

              </td>
              </tr>
              <tr>
                  <td class="toplamBilgi" style="border-top:1px solid #d9dada;" >
                      Birim Toplam:</td>
                  <td class="toplamFiyat" style="border-top:1px solid #d9dada;"  >
                      <asp:Label ID="lblBirimToplam" runat="server"></asp:Label>
                  </td>
              </tr>

                <tr runat="server" id="trIndirim" visible="false" >
                  <td class="toplamBilgi" style="color:#d2063f;" >
                      İndirim:</td>
                  <td class="toplamFiyat" style="color:#d2063f;" >
                      <asp:Label ID="lblIndirim" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="toplamBilgi">
                      KDV Toplam:</td>
                  <td class="toplamFiyat">
                      <asp:Label ID="lblKdvToplam" runat="server"></asp:Label>
                  </td>
              </tr>
               <tr>
                  <td class="toplamBilgi">
                     Kargo Ücreti:
                  </td>
                  <td class="toplamFiyat">
                      <asp:Label ID="lblKargoToplam" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="toplamBilgi" style="border-bottom:1px solid #d9dada;" >
                      Sepet Toplam:</td>
                  <td class="toplamFiyat" style="border-bottom:1px solid #d9dada;" >
                   <asp:Label ID="lblToplam" runat="server"></asp:Label>

                  </td>
              </tr>
        </table>

    <div id="sepetBos" class="sepetBos" runat="server" visible="false" > </div>
    <div class="btnAlisverisDevam">
         <a href="<%: ResolveUrl("~/Default.aspx") %>" > 
         <asp:ImageButton ID="imgBtnAlisverisDevam" ImageUrl="~/images/buttonlar/AlisverisDevam.gif" runat="server"  />
         </a>
        </div>

    <div class="btnSatinAl">
          <asp:ImageButton ID="imBtnSatinAl" PostBackUrl="~/Market/ilkAdres.aspx" ImageUrl="~/images/buttonlar/satinAl.gif" runat="server" />
   </div>
   <div style="clear:both;" ></div>
   </div>


</asp:Content>

