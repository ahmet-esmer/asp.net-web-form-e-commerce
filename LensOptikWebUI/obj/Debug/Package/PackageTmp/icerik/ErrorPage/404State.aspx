<%@ Page Title="404 Aradığınız Sayfa Bulunamadı" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="ErrorPage_404State" Codebehind="404State.aspx.cs" %>


<%@ Register src="../../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>
<%@ Register src="../../Include/RightColumn/Satilanlar.ascx" tagname="Satilanlar" tagprefix="uc7" %>
<%@ Register src="../../Include/RightColumn/KargoImage.ascx" tagname="KargoImage" tagprefix="uc8" %>
<%@ Register src="../../Include/RightColumn/ZiyaretciDefteri.ascx" tagname="ZiyaretciDefteri" tagprefix="uc9" %>
<%@ Register src="../../Include/RightColumn/Anket.ascx" tagname="Anket" tagprefix="uc10" %>
<%@ Register src="../../Include/RightColumn/AltImage.ascx" tagname="AltImage" tagprefix="uc11" %>
<%@ Register src="../../Include/RightColumn/AltFlash.ascx" tagname="AltFlash" tagprefix="uc12" %>

<%@ Register src="../../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc1" %>

<%@ Register src="../../Include/ProductList.ascx" tagname="ProductList" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
    
    #mesaj-404
    {
    margin:10px 50px 10px 10px;    
    }
    
    #mesaj-404 .title {
    font-weight: bold;
    font-size: 30px;
    padding-bottom: 15px;
    display: block;
    }
    
    #mesaj-404 .appText {
    font-size: 15px;
    padding-top: 7px;
    padding-bottom: 50px;
    line-height:20px;
    
    }

    #mesaj-404 b {
    color :#ab120d;
    }
    
    </style>
</asp:Content>

   <asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

    <div id="mesaj-404" >
        <uc1:Mesajlar ID="ucMesaj" runat="server" />
    </div>
     <div  id="solPanel" >
         <uc3:Kategoriler ID="ucKategoriler" runat="server" />
         <uc4:Markalar ID="ucMarkalar" runat="server" />
         <uc5:Facebook ID="ucFacebook" runat="server" />
         <uc6:SSSorular ID="ucSSSorular" runat="server" />
         <uc11:AltImage ID="ucAltImage" runat="server" />
    </div>
    <!-- sol panel sonu -->
    <div id="ortaDis-0" > 
    <div id="mesaj-sgk" >
    <div id="mesaj-sgk-1" >
    <asp:Image ID="Image2" ImageUrl="~/Images/icons/sgkUnlem.jpg" runat="server" />
    </div>
    <div id="mesaj-sgk-2" >
    <span>Sitemizde Satılan Tüm Ürünler CE, FDA ve SAĞLIK BAKANLIĞI izinlidir.<br />
     Distribütör firmalarının vermiş olduğu tüm garanti şartlarına haizdir.<br />
     Lensoptik.com.tr olarak kesinlikle tarihi geçmiş,kaçak,sahte,yada uzak doğu çıkışlı hiçbir ürün satmamaktayız.</span>
    </div>  
    </div>
    <div id="ortaDis">
      <div id="ortaIc"> 
          <uc2:ProductList ID="ucProductList" runat="server" />
        </div>
      </div>
    </div>
    <!-- sağ panel --> 
    <div id="sagPanel" >
         <uc7:Satilanlar ID="ucSatilanlar" runat="server" />
         <uc8:KargoImage ID="ucKargoImage" runat="server" />
         <uc9:ZiyaretciDefteri ID="ucZiyaretciDefteri" runat="server" />
         <uc10:Anket ID="ucAnket" runat="server" />
    
         <uc12:AltFlash ID="ucAltFlash" runat="server" />
    </div>
    <!-- sağ panel sonu -->
  <script type="text/javascript">
      $(document).ready(function () {
          $('.urunZemin:even').css("border-right", "solid 1px #bebdbd");
      });
</script>
</asp:Content>

