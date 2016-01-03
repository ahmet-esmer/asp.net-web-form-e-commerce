<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Kullanici_Fiyat_Alarm" Codebehind="FiyatAlarm.aspx.cs" %>

<%@ Import Namespace="BusinessLayer" %>

<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>
<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
  <link rel="stylesheet" href="../Style/Kullanici.css" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

     <!-- Günün ürünü -->
         <uc1:GununUrunu ID="GununUrunu1" runat="server" />
     <!-- Günün ürünü sonu -->
     <!-- banner -->
          <uc2:Banner ID="Banner1" runat="server" />
     <!-- baner sonu -->
     <!-- sol panel -->
     <div  id="solPanel" >
         <uc3:Kategoriler ID="Kategoriler1" runat="server" />
         <uc4:Markalar ID="Markalar1" runat="server" />
         <uc5:Facebook ID="Facebook1" runat="server" />
         <uc6:SSSorular ID="SSSorular1" runat="server" />
    </div>
    <!-- sol panel sonu -->

    <div id="ortaSag">
        <div id="ortaSagIc"> 
        <!-- Favoriler -->
         <table style="width:100%; margin-bottom:30px;" >
            <tr>
                <td class="icerik-Baslik"   >
                      <h5> Favori Ürünler </h5>  
                </td>
            </tr>
              <tr>
                <td>
                    <div style="width:650px;padding:10px;" >  
                        <uc7:Mesajlar ID="Mesaj" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
     
                    <asp:GridView ID="gvwListe" ClientIDMode="Static" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
           GridLines="None" Width="100%"  DataKeyNames="fiyatId" 
           onrowcommand="grwKulaniciFiyatIndirim_RowCommand" PageSize="10" AllowPaging="True"  >
                       <Columns>
                           <asp:TemplateField  >
                               <ItemTemplate>
                                   <asp:HyperLink ID="hlUrun" NavigateUrl='<%#"~/Urun/"+ Eval("urunId")+"/"+ UrlTR.Replace(Eval("urunAdi").ToString())+".aspx"%>'  ClientIDMode="Static" runat="server" >
                    <asp:Image ID="img" ImageUrl='<%# "~/Products/Little/"+ Eval("resimAdi").ToString() %>'
                     ClientIDMode="Static"  runat="server"   CssClass="sepetResim" />
                   </asp:HyperLink> 
                               </ItemTemplate>
                               <HeaderStyle Width="100" HorizontalAlign="Left" />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Ürün Adı"  >
                               <ItemTemplate>
                             <asp:HyperLink ID="hlUrun1"   NavigateUrl='<%#"~/Urun/"+ Eval("urunId")+"/"+ UrlTR.Replace(Eval("urunAdi").ToString())+".aspx"%>' ClientIDMode="Static" runat="server" CssClass="link" >
                             <%# DataBinder.Eval(Container, "DataItem.urunAdi")%>
                   </asp:HyperLink> 
                               </ItemTemplate>
                               <HeaderStyle Width="200" HorizontalAlign="Left" />
                           </asp:TemplateField>

                             <asp:TemplateField HeaderText="Tutar"  >
                               <ItemTemplate>
                           <%#   AritmetikIslemler.panelFiyat(Convert.ToDecimal(Eval("urunFiyat")), Convert.ToDecimal(Eval("uIndirimFiyat")), Eval("doviz").ToString())%>
                               </ItemTemplate>
                               <HeaderStyle Width="200" HorizontalAlign="Left" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Favori Sil"  >
                            <ItemTemplate>                           
                         <asp:ImageButton 
                       ID="imgBtnSil" 
                       ImageUrl="~/images/icons/sepetSil.png"
                       CommandName="fiyatIndirimSil"
                       CommandArgument='<%# DataBinder.Eval(Container, "DataItem.fiyatId")%>' 
                       OnClientClick="return confirm('Bu Ürününü Fiyat Alarm Listesinden silmek istediğinizden emin misiniz?');"
                       runat="server" />
                            </ItemTemplate>
                            <HeaderStyle  Width="80" HorizontalAlign="Left" />
                          </asp:TemplateField>
                       </Columns>
                       <RowStyle  CssClass="RowStyle" />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                       <PagerStyle CssClass="PagerStyle" HorizontalAlign="Right" ForeColor="White" />
                   </asp:GridView>

                </td>
            </tr>
            </table>
                <div class="clear"> </div>
        <!-- Favoriler sonu -->
        </div>
    </div>

       <script type="text/javascript" >
           $("#ortaSag").corner("round 6px");
           $("#ortaSagIc").corner("round 6px");
        </script>
</asp:Content>

