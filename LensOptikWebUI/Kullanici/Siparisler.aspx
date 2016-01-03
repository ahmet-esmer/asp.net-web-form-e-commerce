<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" Inherits="Kullanici_Siparisler" Codebehind="Siparisler.aspx.cs" %>

<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>
<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc7" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <link rel="stylesheet" href="../Style/Kullanici.css" type="text/css" />
    <style type="text/css">
    
    
    .reOrder
    {
        width:110px;
        margin-right:20px;
        background: url(../Images/Icons/GunIcon.jpg) no-repeat right center;
    }
    
    </style>
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
        <!-- Siparişler-->
         <table style="width:100%; margin-bottom:30px;" >
            <tr>
                <td class="icerik-Baslik"   >
                      <h5> Siparişler </h5>  
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
              
                <asp:GridView ID="gvwListe" runat="server" ClientIDMode="Static" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"  DataKeyNames="id"
        PageSize="10" OnPageIndexChanging="gvwListe_Paging" AllowPaging="True" >
                       <Columns>
                           <asp:TemplateField HeaderText=" &nbsp;Sipariş No">
                               <ItemTemplate>
                            &nbsp;<%# DataBinder.Eval(Container, "DataItem.siparisNo")%>
                               </ItemTemplate>
                              <HeaderStyle  Width="100" HorizontalAlign="Left" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Ödeme">
                               <ItemTemplate>
                                <%# Eval("odemeTipi").ToString() %> 
                               </ItemTemplate>
                               <HeaderStyle Width="130" HorizontalAlign="Left" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Şipariş Durumu">
                               <ItemTemplate>
                          <%#  DataBinder.Eval(Container, "DataItem.siparisDurumu")%> 
                               </ItemTemplate>
                               <HeaderStyle Width="170" HorizontalAlign="Left" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Tutar">
                               <ItemTemplate>
                          <%# string.Format( "{0:C}", DataBinder.Eval(Container, "DataItem.TaksitliGenelToplami"))%> 
                               </ItemTemplate>
                               <HeaderStyle Width="150" HorizontalAlign="Left" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="İşlem Tarih">
                            <ItemTemplate>                           
                          <%# BusinessLayer.DateFormat.TarihSaatSiparis(DataBinder.Eval(Container, "DataItem.SiparisTarihi").ToString())%>
                            </ItemTemplate>
                            <HeaderStyle  Width="250" HorizontalAlign="Left"  Font-Bold="true"/>
                          </asp:TemplateField>

                           <asp:TemplateField HeaderText="Siparişi Yenile">
                            <ItemTemplate>  
                                <div  class="reOrder" >
                                <a href="<%# ResolveUrl("~/Kullanici/Siparisler.aspx?reOrder="+ Eval("id").ToString() )%> " class="link" > Şiparişi Yenile</a> 
                                
                                </div>
                            </ItemTemplate>
                            <HeaderStyle  Width="120" HorizontalAlign="Left"  Font-Bold="true"/>
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
        <!-- Siparişler sonu -->
        <div class="clear"> </div>
        </div>
    </div>

       <script type="text/javascript" >
           $("#ortaSag").corner("round 6px");
           $("#ortaSagIc").corner("round 6px");
        </script>
</asp:Content>

