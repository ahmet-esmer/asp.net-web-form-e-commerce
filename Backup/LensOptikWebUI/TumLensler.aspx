<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableViewState="false" Inherits="tum_lensler" Codebehind="TumLensler.aspx.cs" %>

<%@ Register src="Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<style type="text/css" >
    
.satanlar-td
{
   padding-left:15px; 
}

</style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
   <!-- Günün ürünü -->
         <uc1:GununUrunu ID="ucGununUrunu" runat="server" />
     <!-- Günün ürünü sonu -->

     <!-- banner -->
          <uc2:Banner ID="ucBanner" runat="server" />
     <!-- baner sonu -->
     <div class="clear" ></div>
    <div id="ortaTam"  >
        <div id="ortaTamIc" style="padding-bottom:30px;" > 
        <!-- Tüm lensler -->
          <table width="100%" id="tumLensler" >
            <tr>
                <td class="icerik-Baslik" colspan="3"><h5>Tüm Lensler</h5></td>
            </tr>
            <tr>
                <td style="width:280px;"></td>
                <td style="width:270px" ></td>
                <td style="width:260px" ></td>
            </tr>
            <tr>
            <td class="satanlar-td"  >  
             <div class="baslikDiv"><h5>Tüm Lensler</h5></div>
                    <asp:Repeater ID="rptSatilanlar" runat="server" >
                    <HeaderTemplate>
                    <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                    <li>
                     <a href="<%# ResolveUrl(Eval("link").ToString()) %>" > <%# Eval("urunAdi").ToString() %> </a>
                    </li>
                    </ItemTemplate>
                    <FooterTemplate>
                    </ul>
                    </FooterTemplate>
                    </asp:Repeater>
            </td>
            <td class="satanlar-td" style="border-left:1px solid #bebdbd; border-right:1px solid #bebdbd;" >
             <div class="baslikDiv"><h5>Kategori</h5></div>
                    <asp:Repeater ID="rptAnakategoriler" runat="server" onitemdatabound="rptAnakategoriler_ItemDataBound">
                    <HeaderTemplate>
                    <ul> 
                    </HeaderTemplate>
                     <ItemTemplate>
                     <li><a href="<%# ResolveUrl(BusinessLayer.LinkBulding.Kategori(Eval("title"),Eval("kategoriadi"), Eval("serial"))) %>"> <%# Eval("kategoriadi").ToString().ToUpper()%></a></li>
                   <asp:HiddenField ID="hdfSerial" Value=<%# Eval("serial")%> runat="server" />
                       <asp:Repeater ID="rptAltKategori" runat="server" >
                           <HeaderTemplate>
                       <ul> 
                       </HeaderTemplate>
                           <ItemTemplate>
                            <li style="margin-left:25px;" ><a  href="<%# ResolveUrl(BusinessLayer.LinkBulding.Kategori(Eval("title"),Eval("kategoriadi"), Eval("serial")))%>" > <%# Eval("kategoriadi").ToString()%></a> </li>
                           </ItemTemplate>
                           <FooterTemplate>
                           </ul>
                           </FooterTemplate>
                       </asp:Repeater>		        
                    </ItemTemplate>
                    <FooterTemplate>
                    </ul>
                    </FooterTemplate>
                    </asp:Repeater>

                  <!-- Markalar  -->
                  <div class="baslikDiv"><h5>Marka</h5> </div> 
                  <asp:Repeater ID="rptMarkalar" runat="server" >
              <HeaderTemplate>
              <ul>
              </HeaderTemplate>
              <ItemTemplate>
                 <li>
                 <a href="<%# ResolveUrl(BusinessLayer.LinkBulding.Marka( Eval("id"), Eval("marka_adi"))) %>" ><%# Eval("marka_adi").ToString() %> </a>
                 </li>
               </ItemTemplate>
               <FooterTemplate>
               </ul>
               </FooterTemplate>
               </asp:Repeater>

            </td>
            <td class="satanlar-td" >
               <asp:Literal ID="ltlIcerik" runat="server"></asp:Literal>
            </td>
            </tr>
          </table>
        <!-- tümlensler sonu -->
        </div>
    </div>


 <script type="text/javascript"> 
//<![CDATA[
      $("#ortaTam").corner("round 6px");
      $("#ortaTamIc").corner("round 6px");
//]]>
</script>

</asp:Content>

