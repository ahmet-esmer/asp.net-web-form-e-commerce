<%@ Page Title="Havale Bildirim Formu | Lens Optik" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Kulanici_HavaleBildirim" Codebehind="HavaleBildirim.aspx.cs" %>

<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>
<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
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

              <table class="uyuTablo" style="width:100%" >
            <tr>
                <td class="icerik-Baslik" colspan="2"  >
                    <h5> Havale Bildirim Formu</h5>   
                </td>
            </tr>
                <tr>
                <td colspan="2" >
                    <div style="width:680px;padding:10px;">  
                      <uc7:Mesajlar ID="Mesaj" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top" style="padding-top:30px; width:160px; ">
                    Adınız / Soyadınız:<span class="formZorunlu">*</span></td>
                <td>
                    <asp:TextBox ID="txtAdSoyad" runat="server" Width="300px"  CssClass="text" ></asp:TextBox> <br />
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" 
                     CssClass="input-notification error" ForeColor=""
                     ErrorMessage="Lütfen  Ad ve Soyad Alanını Doldurunuz." ControlToValidate="txtAdSoyad" ValidationGroup="havaleBildirim" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    E-mail Adresiniz:<span class="formZorunlu">*</span> </td>
                <td>
          
                    <asp:TextBox ID="txtEposta" runat="server" Width="300px"  CssClass="text" ></asp:TextBox>  <br />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" 
                      CssClass="input-notification error" ForeColor=""
                      ErrorMessage="Lütfen E-Posta Adresi Giriniz." ValidationGroup="havaleBildirim" ControlToValidate="txtEposta"></asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEposta" Display="Dynamic"
                        CssClass="input-notification error" ForeColor=""
                        ErrorMessage="Lütfen  Geçerli E-Posta Adresi Giriniz."  ValidationGroup="havaleBildirim"
                        ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Havale Yaptığınız Miktar:<span class="formZorunlu">*</span></td>
                <td>
                    <asp:TextBox ID="txtHavaleMiktari" runat="server" Width="300px"  CssClass="text"  ></asp:TextBox>  <br />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        Display="Dynamic" CssClass="input-notification error" ForeColor=""
                        ErrorMessage="Lütfen İrtibat Telefonu Giriniz."  
                        ControlToValidate="txtHavaleMiktari" ValidationGroup="havaleBildirim" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Sipariş No:<span class="formZorunlu">*</span></td>
                <td>
                    <asp:TextBox ID="txtSiparisNo" runat="server" Width="300px"  CssClass="text" ></asp:TextBox>  <br />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        Display="Dynamic" CssClass="input-notification error" ForeColor=""
                        ErrorMessage="Lütfen Sipariş No Giriniz."  
                        ControlToValidate="txtSiparisNo" ValidationGroup="havaleBildirim" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Havale Yaptığınız Banka:<span class="formZorunlu">*</span></td>
                <td>
                    <asp:RadioButtonList ID="rdbBankaHavale" runat="server">
                    </asp:RadioButtonList>  <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                    CssClass="input-notification error" ForeColor=""
                     ErrorMessage="Lütfen Havale İşlemini Yaptığınız Bankayı Seçiniz."  ControlToValidate="rdbBankaHavale" ValidationGroup="havaleBildirim" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
              
                    &nbsp;</td>
                <td >
                    <asp:Button ID="btnHavaleBildirim" Width="130px" runat="server" Text="Formu Gönder" 
              CssClass="button" ValidationGroup="havaleBildirim" onclick="btnHavaleBildirim_Click" />
              <br />
              <br />
                </td>
               
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Literal ID="ltl" runat="server"></asp:Literal>
                    &nbsp;</td>
               
            </tr>
        </table>

        </div>
    </div>

       <script type="text/javascript" >
           $("#ortaSag").corner("round 6px");
           $("#ortaSagIc").corner("round 6px");
        </script>
</asp:Content>

