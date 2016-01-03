<%@ Page Language="C#" ViewStateMode="Disabled" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="urunler" Codebehind="Urunler.aspx.cs" %>

<%@ Register src="Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>
<%@ Register src="Include/RightColumn/Satilanlar.ascx" tagname="Satilanlar" tagprefix="uc7" %>
<%@ Register src="Include/RightColumn/KargoImage.ascx" tagname="KargoImage" tagprefix="uc8" %>
<%@ Register src="Include/RightColumn/ZiyaretciDefteri.ascx" tagname="ZiyaretciDefteri" tagprefix="uc9" %>
<%@ Register src="Include/RightColumn/Anket.ascx" tagname="Anket" tagprefix="uc10" %>
<%@ Register src="Include/RightColumn/AltImage.ascx" tagname="AltImage" tagprefix="uc11" %>
<%@ Register src="Include/LeftColumn/MarkalarFilitre.ascx" tagname="MarkalarFilitre" tagprefix="uc12" %>

<%@ Register src="Include/RightColumn/AltFlash.ascx" tagname="AltFlash" tagprefix="uc4" %>


<%@ Register src="Include/ProductList.ascx" tagname="ProductList" tagprefix="uc13" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
  
     <!-- Günün ürünü -->
         <uc1:GununUrunu ID="ucGununUrunu" runat="server" />
     <!-- Günün ürünü sonu -->
     <!-- banner -->
          <uc2:Banner ID="ucBanner" runat="server" />
     <!-- baner sonu -->
     <!-- sol panel -->
     <div  id="solPanel" >
         <uc3:Kategoriler ID="ucKategoriler" runat="server" />
         <uc12:MarkalarFilitre ID="ucMarkalarFilitre" runat="server" />
         <uc5:Facebook ID="ucFacebook" runat="server" />
         <uc6:SSSorular ID="ucSSSorular" runat="server" />
         <uc11:AltImage ID="ucAltImage" runat="server" />
    </div>
    <!-- sol panel sonu -->

   <div id="ortaDis">
        <div id="ortaIc"> 
        <!-- Urun Kataloğu -->

      <div class="mevcutSayfa">

      <asp:HyperLink ID="HyperLink1" CssClass="altMenu" NavigateUrl="~/Default.aspx" runat="server" Text="Anasayfa">
      </asp:HyperLink>
      <asp:HyperLink ID="hlkat3" CssClass="altMenu" Visible="false" ClientIDMode="Static" runat="server">
      </asp:HyperLink>
      <asp:HyperLink ID="hlkat6" CssClass="altMenu" Visible="false" ClientIDMode="Static" runat="server">
      </asp:HyperLink>
      <asp:HyperLink ID="hlMarka" CssClass="altMenu" Visible="false" ClientIDMode="Static" runat="server">
      </asp:HyperLink>

       <div class="listelemeBilgisi" style="clear:both" >
           <asp:Label ID="lbllistelemeBilgisi" CssClass="listeYazi" runat="server" ></asp:Label>    
       </div>
      </div>

         <uc13:ProductList ID="ucProductList" runat="server" />

        <!-- İşlem Sonu -->
        </div>
        </div>
    
    <!-- sağ panel --> 
    <div id="sagPanel" >
         <uc7:Satilanlar ID="ucSatilanlar" runat="server" />
         <uc8:KargoImage ID="ucKargoImage" runat="server" />
         <uc9:ZiyaretciDefteri ID="ucZiyaretciDefteri" runat="server" />
         <uc10:Anket ID="ucAnket" runat="server" />
         <uc4:AltFlash ID="ucAltFlash" runat="server" />
    </div>
    <!-- sağ panel sonu -->
</asp:Content>

