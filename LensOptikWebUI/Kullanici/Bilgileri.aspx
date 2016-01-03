<%@ Page Title="Kullanıcı Bilgileri | Lens Optik" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Kullanici_Bilgi" Codebehind="Bilgileri.aspx.cs" %>

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
        <!-- Kulanıcı bilgi güncelleme-->

        <table class="uyuTablo">
            <tr>
                <td class="icerik-Baslik" colspan="2"  >
                      <h5> Kulanıcı Bilgileri </h5>  
                </td>
            </tr>
              <tr>
                <td colspan="2"  >
                    <div style="width:650px;padding:10px;" >  
                        <uc7:Mesajlar ID="Mesaj" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="uyuBilgiYazi" colspan="2">
                   Sayın üyemiz size daha iyi hizmet verebilmemiz için kişisel bilgerinizi doğru 
                    bir şekilde girdiğinizden emin olun. Gerekli alanları doldurup &quot;Değişiklikleri 
                    Kaydet&quot; düğmesini tıklayarak kişisel bilgilerinizi değiştirebilirsiniz. </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top" >
                    Adınız / Soyadınız: </td>
                <td >
                    <asp:TextBox ID="txtAdSoyad" Width="250px"  CssClass="text" runat="server" ></asp:TextBox><br />
                     <asp:RequiredFieldValidator ID="rfvAd" runat="server" 
                        Display="Dynamic"  CssClass="input-notification error" ForeColor=""
                        ErrorMessage="Lütfen  Ad ve Soyad Alanını Doldurunuz." 
                        ControlToValidate="txtAdSoyad"  ValidationGroup="guncellme"></asp:RequiredFieldValidator>


                </td>
            </tr>
         
               
              
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    E-mail Adresiniz: </td>
                <td>

                    <asp:TextBox ID="txtEposta" runat="server" Width="250px"  CssClass="text" ></asp:TextBox>
                     <br />

                     <asp:RequiredFieldValidator ID="rfvMail" runat="server" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen E-Posta Adresi Giriniz." 
                        CssClass="input-notification error" ForeColor="" 
                        ControlToValidate="txtEposta" ValidationGroup="guncellme"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="rfvMail2" runat="server" 
                        ControlToValidate="txtEposta" Display="Dynamic" ErrorMessage="Lütfen  Geçerli E-Posta Adresi Giriniz." 
                        ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                        ValidationGroup="guncellme" CssClass="input-notification error" 
                        ForeColor="" ></asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Şifre:</td>
                <td>
                    <asp:TextBox ID="txtSifre" MaxLength="15" runat="server" Width="250px"  CssClass="text"   TextMode="Password" ></asp:TextBox><br />
                      <asp:RequiredFieldValidator ID="rfvSifre" runat="server" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen  Şifre Alanını Doldurunuz."  
                        CssClass="input-notification error" ForeColor=""
                        ControlToValidate="txtSifre" ValidationGroup="guncellme" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Şifre Tekrarı:</td>
                <td>
                     <asp:TextBox ID="txtSifreTekrari" MaxLength="15" Width="250px"  CssClass="text"  runat="server" TextMode="Password" ></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvSifre2" runat="server" 
                         Display="Dynamic" 
                         ErrorMessage="Lütfen  Şifre Tekrarı Alanını Doldurunuz."  
                         CssClass="input-notification error" ForeColor=""
                         ControlToValidate="txtSifreTekrari" ValidationGroup="guncellme" ></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="va6" runat="server" 
                         ControlToCompare="txtSifre" ControlToValidate="txtSifreTekrari" 
                         ValidationGroup="guncellme" Display="Dynamic" 
                         CssClass="input-notification error" ForeColor=""
                         ErrorMessage="Şifreniz Farklı Tekrar Şifre Giriniz."></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Telefon:</td>
                <td>
                    <asp:TextBox ID="txtTelefon" ClientIDMode="Static" Width="250px" MaxLength="11"  CssClass="text"  runat="server"  ></asp:TextBox><br />
                    <span id="errTel" style="display:none" class="input-notification error" ></span>  
                    <asp:RequiredFieldValidator ID="rfvTelefon" runat="server" 
                        ControlToValidate="txtTelefon" Display="Dynamic" 
                        ErrorMessage="Lütfen  Telefon Numarası Giriniz." 
                        CssClass="input-notification error" ForeColor=""
                        SetFocusOnError="true" ToolTip="Şehir Seçiniz."
                        ValidationGroup="guncellme"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Bulunduğunuz Şehir: </td>
                <td>
                <asp:DropDownList ID="ddlSehirler" runat="server" Width="253px" Height="30px"  CssClass="text"  /><br />

                  <asp:RequiredFieldValidator ID="rfvSehir" runat="server" ControlToValidate="ddlSehirler" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen  Şehir Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue="0" SetFocusOnError="true" ToolTip="Şehir Seçiniz." 
                        ValidationGroup="guncellme"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Cinsiyetiniz:</td>
                <td>
                    <asp:RadioButtonList ID="rdbCinsiyet" runat="server" 
                        RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">Erkek</asp:ListItem>
                    <asp:ListItem Value="False">Kadın</asp:ListItem>
                    </asp:RadioButtonList>
                    
                      <asp:RequiredFieldValidator runat="server" ID="rfvCinsiyet" 
                        ControlToValidate="rdbCinsiyet" Display="Dynamic" 
                        ErrorMessage="Lütfen  Cinsiyetinizi Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        ValidationGroup="guncellme" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Doğum Tarihi:</td>
                <td>
                    <asp:DropDownList ID="ddlGun" Height="30px"   CssClass="text" runat="server">
                    </asp:DropDownList>
                     
                    <asp:DropDownList ID="ddlAy" Height="30px"  CssClass="text" runat="server">
                    <asp:ListItem Value="0"> Ay </asp:ListItem>
                    <asp:ListItem Value="01"> Ocak </asp:ListItem>
                    <asp:ListItem Value="02"> Şubat </asp:ListItem>
                    <asp:ListItem Value="03"> Mart </asp:ListItem>
                    <asp:ListItem Value="04"> Nisan </asp:ListItem>
                    <asp:ListItem Value="05"> Mayıs </asp:ListItem>
                    <asp:ListItem Value="06"> Haziran  </asp:ListItem>
                    <asp:ListItem Value="07"> Temmuz </asp:ListItem>
                    <asp:ListItem Value="08"> Ağustos </asp:ListItem>
                    <asp:ListItem Value="09"> Eylül </asp:ListItem>
                    <asp:ListItem Value="10"> Ekim </asp:ListItem>
                    <asp:ListItem Value="11"> Kasım </asp:ListItem>
                    <asp:ListItem Value="12"> Aralık </asp:ListItem>                         
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlYil" Height="30px"   CssClass="text" runat="server">
                    </asp:DropDownList> <br />
                      <asp:RequiredFieldValidator ID="rfvGun" runat="server" 
                        ControlToValidate="ddlGun" Display="Dynamic" 
                        ErrorMessage="Lütfen Gün Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue="0" SetFocusOnError="true" ToolTip="Gün Seçiniz." 
                        ValidationGroup="guncellme"></asp:RequiredFieldValidator>

                        <asp:RequiredFieldValidator ID="rfvAy" runat="server" 
                        ControlToValidate="ddlAy" Display="Dynamic" 
                        ErrorMessage="Lütfen Ay Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue="0" SetFocusOnError="true" ToolTip="Ay Seçiniz." 
                        ValidationGroup="guncellme"></asp:RequiredFieldValidator>

                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="ddlYil" Display="Dynamic" 
                        ErrorMessage="Lütfen Yıl Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue=" " SetFocusOnError="true" ToolTip="Yıl Seçiniz." 
                        ValidationGroup="guncellme"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top" style="height: 34px" >
                    Mesaj Servisi:</td>
                <td>
                   <asp:CheckBox ID="ckbEposta" Text="E-Posta almak istiyorum" runat="server" /> &nbsp;
                    <asp:CheckBox ID="ckbSms" Text="SMS Almak İstiyorum" runat="server" /></td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnUyeBilgiGuncelle" Width="200px" runat="server" 
                        Text="Değişiklikleri Kaydet" CssClass="button" 
                        onclick="btnUyeBilgiGuncelle_Click" ValidationGroup="guncellme" /><br /> <br /> <br />
                </td>
            </tr>
            </table>

        <!-- Kulanıcı bilgi güncelleme sonu -->
        </div>
    </div>
    <script type="text/javascript">

            $(document).ready(function () {

                $("#txtTelefon").keypress(function (e) {
                    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                        //display error message
                        $("#errTel").html("Telefon numarası sayısal degel olmalıdır.").show().fadeOut(5000);
                        return false;
                    }
                });

            $("#ortaSag").corner("round 6px");
            $("#ortaSagIc").corner("round 6px");
        });

     </script>

</asp:Content>

