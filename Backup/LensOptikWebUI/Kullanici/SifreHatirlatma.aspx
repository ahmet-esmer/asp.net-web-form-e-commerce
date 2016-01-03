<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Kulanici_SifreGonder" Codebehind="SifreHatirlatma.aspx.cs" %>

<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>
<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-size: 12px;
            line-height: 17px;
            color: #069;
            padding: 10px 10px 30px;
            height: 73px;
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
        <div id="ortaSagIc" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'btnSifreGonder')" > 
    <table class="uyuTablo" >
            <tr>
                <td class="icerik-Baslik" colspan="2"  >
                <h5> Şifre Hatırlatma </h5>        
                </td>
            </tr>
            <tr>
                <td colspan="2"  >
                    <div style="width:650px;padding:10px;">  
                      <uc7:Mesajlar ID="Mesaj" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="uyuBilgiYazi" colspan="2">
                   Lütfen üye olurken belirtmiş olduğunuz email adresinizi yazınız. Şifreniz e-posta adresinize 1-2 dakika içerisinde iletilecektir.
                   
              </td>
            </tr>
        
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    E-Mail: </td>
                <td >
                    <asp:TextBox ID="txtSifreGonder" runat="server" Width="250px"  CssClass="text"></asp:TextBox><br />
                  <asp:RequiredFieldValidator ID="rfvMail" runat="server" 
                        Display="Dynamic" ValidationGroup="gropSifre"
                        ErrorMessage=" Lütfen  Ad ve Soyad Alanını Doldurunuz." 
                        CssClass="input-notification error" ForeColor=""
                        ControlToValidate="txtSifreGonder"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator ID="rxvMail" runat="server" 
                      ControlToValidate="txtSifreGonder" 
                      ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                      ErrorMessage="Geçerli E-Posta Adresi Giriniz." 
                      ValidationGroup="gropSifre" 
                      CssClass="input-notification error" ForeColor=""
                      ></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top" style="height: 34px">
                    Güvenlik Kodu:</td>
                <td>
              <asp:TextBox ID="txtGuvenlik"  autocomplete="off" runat="server" Width="250px"  CssClass="text"></asp:TextBox><br />
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                        Display="Dynamic" ValidationGroup="gropSifre"
                        ErrorMessage="Lütfen  Güvenlik Resmini Giriniz." 
                        CssClass="input-notification error" ForeColor=""
                        ControlToValidate="txtGuvenlik"></asp:RequiredFieldValidator>
                    <br />

               <asp:Image ID="imgGuvenlik" ImageUrl="~/include/GuvenlikResmi.aspx" runat="server" />
                 <br />  <br />
                </td>
            </tr>
            <tr>
                <td>
              
                </td>
                <td>
                    <asp:Button ID="btnSifreGonder" ClientIDMode="Static" Width="120px" runat="server" Text="Şifremi Gönder" 
                       ValidationGroup="gropSifre" onclick="btnSifreGonder_Click"  CssClass="button" /> <br /> <br /> <br />
                </td>
               
            </tr>
        </table>
        </div>
    </div>
        <script type="text/javascript" language="javascript" >
            $("#ortaSag").corner("round 6px");
            $("#ortaSagIc").corner("round 6px");
        </script>
</asp:Content>

