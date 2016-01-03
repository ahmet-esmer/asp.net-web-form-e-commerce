<%@ Page Title="Lens Optik Banka Hesapları" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Icerik_BankaHesaplari" Codebehind="BankaHesaplari.aspx.cs" %>
<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

<style type="text/css" >
    
    .hesapBilgi {
    font-size:12px;
    color:#292929;
    width:170px;
    height:33px;
    vertical-align:middle;
    padding-left:10px;
    background-color:#ffffff;
    border-top:1px solid #f2f3f4;
    }

    .hesapBilgi2 {
    font-size:12px;
    color:#292929;
    background-color:#ffffff;
    border-top:1px solid #f2f3f4;
    height:20px;
    vertical-align:middle;
    }

    .hesapBaslik   
    {
      height:22px;
      background-color:#fafafa;
      font-size:12px;
      font-weight:bold;
      color:#069; 
      padding:20px 10px;
      border-top:1px solid #f2f3f4;
     
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
         <table class="uyuTablo" style="width:100%; margin-bottom:25px;" >
            <tr>
                <td class="icerik-Baslik"  >
                    <h5> Firma Banka Hesap Bilgileri</h5>   
                </td>
            </tr>
            <tr>
                <td>
                  <asp:Repeater ID="rptHesapNo" runat="server" >
    <HeaderTemplate>
    <table  style="margin-bottom:30px;width:100%" >
    </HeaderTemplate>
    <ItemTemplate>
           <tr>
                <td class="hesapBaslik" colspan="2"  >
               
                    <%# DataBinder.Eval(Container, "DataItem.bankaAdi") %>
                </td>
            </tr>
            <tr>
                <td class="hesapBilgi"  >
                    Hesap Adı
                </td>
               <td  class="hesapBilgi2"    >
                  
                   <%# DataBinder.Eval(Container, "DataItem.hesapAdi") %>
                </td>
            </tr>
            
            <tr>
                <td class="hesapBilgi" >
                    Şube
                </td>
               <td class="hesapBilgi2">
                 <%# DataBinder.Eval(Container, "DataItem.sube") %>
                </td>
            </tr>
            <tr>
                <td class="hesapBilgi"  >
                    Şube Kodu</td>
               <td  class="hesapBilgi2"   >
                  <%# DataBinder.Eval(Container, "DataItem.subeKod") %>
                </td>
            </tr>
            <tr>
                <td class="hesapBilgi" >
                    Hesap No</td>
               <td  class="hesapBilgi2"  >
                  <%# DataBinder.Eval(Container, "DataItem.hesapNo") %>
                </td>
            </tr>
            <tr>
                <td class="hesapBilgi" >
                    Iban</td>
               <td   class="hesapBilgi2" >
                  <%# DataBinder.Eval(Container, "DataItem.iban") %>
                </td>
            </tr>
            <tr>
                <td class="hesapBilgi" >
                    Hesap Tipi
                </td>
               <td class="hesapBilgi2" >
                 <%# DataBinder.Eval(Container, "DataItem.hesapTipi")%>
                </td>
            </tr>
            
    </ItemTemplate>
    <FooterTemplate>
    </table>
    </FooterTemplate>
    </asp:Repeater>
                </td>
            </tr>
         </table>

    </div>
    </div>

    <script type="text/javascript" >
          $('#ortaSag').corner("round 6px");
          $('#ortaSagIc').corner("round 6px");
        </script>

</asp:Content>

