<%@ Page Title="Kullanıcı Kayıt | Lens Optik" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Kulanici_Kayit" Codebehind="Kayit.aspx.cs" %>

<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
   
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
 <!-- Günün ürünü -->
         <uc1:GununUrunu ID="GununUrunu1" runat="server" />
     <!-- Günün ürünü sonu -->

     <!-- banner -->
          <uc2:Banner ID="Banner1" runat="server" />
     <!-- baner sonu -->

  <div id="login"  > 
   <div id="loginIc" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'btnLogin')" >
   
    <table width="278px" >
            <tr>
                <td class="icerik-Baslik"   >
                <h5> Üye Giriş</h5>        
                </td>
            </tr>
            <tr>
                <td class="loginYazi" >
                  E-Posta
                </td>
            </tr>
            <tr>
                <td class="loginForm" >
                  <asp:TextBox ID="txtEpostaGiris" ClientIDMode="Static" Width="230px"  CssClass="text" runat="server" ></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvEpostaGiris" runat="server" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen E-Posta Adresi Giriniz." 
                        CssClass="input-notification error" ForeColor="" ClientIDMode="Static"
                        ControlToValidate="txtEpostaGiris" ValidationGroup="login"></asp:RequiredFieldValidator>

                  <asp:RegularExpressionValidator ID="rgxEpostaGiris" runat="server" 
                        ControlToValidate="txtEpostaGiris" Display="Dynamic" 
                        ErrorMessage="Lütfen  Geçerli E-Posta Adresi Giriniz." 
                        ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" 
                        ClientIDMode="Static"  ValidationGroup="login"
                        CssClass="input-notification error" ForeColor="" ></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="loginYazi" >
                Şifre
                </td>
            </tr>
            <tr>
                <td class="loginForm"  >
                  <asp:TextBox ID="txtSifreGiris" ClientIDMode="Static" TextMode="Password"
                   CssClass="text" runat="server" Width="230px" ></asp:TextBox>

                     <asp:RequiredFieldValidator ID="rfvSifreGiris" runat="server" 
                        Display="Dynamic"  ClientIDMode="Static"
                        ErrorMessage="Lütfen Şifre Giriniz." 
                        CssClass="input-notification error" ForeColor="" 
                        ControlToValidate="txtSifreGiris" ValidationGroup="login"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td class="loginForm"  >
                    <asp:RadioButtonList ID="rdbHatirla" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="hatirla">Beni Hatırla</asp:ListItem>
                    <asp:ListItem Value="unut">Beni Unut</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
            <td  style="padding:10px 0px 2px 13px;" >
            <a class="link" href="<%: ResolveUrl("~/Kullanici/SifreHatirlatma.aspx") %>" >Parolanızı unuttunuz mu?</a>
            </td>
            </tr>
            <tr>
                <td class="loginYazi" style="padding-bottom:20px;" >
                   <asp:Button ID="btnLogin" ValidationGroup="login" ClientIDMode="Static" 
                        CssClass="button" runat="server" Text=" Giriş " onclick="btnLogin_Click" />
                </td>
            </tr>
        </table>
  </div>
  </div>


  <div id="kayit" >
  <div id="kayitIc" onkeypress="javascript:return WebForm_FireDefaultButton(event, 'btnUyeKayit')" > 
     <table class="uyuTablo">
            <tr>
                <td class="icerik-Baslik" colspan="2"  >
                      <h5> Üye Kayıt Formu </h5>  
                </td>
            </tr>
              <tr>
                <td colspan="2" >
                    <div style="width:550px;padding:10px;">  
                        <uc2:Mesajlar ID="Mesaj" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="uyuBilgiYazi" colspan="2">
                    Üyelik kaydınız için aşağıdaki formu doldurmalısınız. Lütfen e-mail adresinizin doğruluğundan emin 
                    olunuz. Üyelik bilgileriniz ve site ile ilgili tüm iletişim bu e-mail adresi 
                    üzerinden yapılacaktır.</td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top" >
                    Adınız / Soyadınız: </td>
                <td>
                    <asp:TextBox ID="txtAdSoyad" Width="250px"  CssClass="text" runat="server" ></asp:TextBox><br />
                     <asp:RequiredFieldValidator ID="rfvAd" runat="server" 
                        Display="Dynamic"  CssClass="input-notification error" ForeColor=""
                        ErrorMessage="Lütfen  Ad ve Soyad Alanını Doldurunuz." 
                        ControlToValidate="txtAdSoyad"  ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    E-mail Adresiniz: </td>
                <td>
                    <asp:TextBox ID="txtEposta" ClientIDMode="Static" runat="server" Width="250px" CssClass="text"  
                    onblur="MailKontlor()" ></asp:TextBox>
                     <br />
                    <a id="hlSifreLink" href="<%: ResolveUrl("~/Kullanici/SifreHatirlatma.aspx") %>" class="input-notification error" style="display:none;" >
                        Sisteme kayıtlı E-Posta bulunmakta bu Adres size aitse şİfre hatırlatma talebinde bulununuz.
                    </a>
                     <asp:RequiredFieldValidator ID="rfvMail" runat="server" 
                        Display="Dynamic" ErrorMessage="Lütfen E-Posta Adresi Giriniz." 
                        CssClass="input-notification error" ForeColor="" ControlToValidate="txtEposta"
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>

                       <asp:RegularExpressionValidator ID="rfvMail2" runat="server" 
                        ControlToValidate="txtEposta" Display="Dynamic"
                        ErrorMessage="Lütfen  Geçerli E-Posta Adresi Giriniz." 
                        ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" ValidationGroup="yeniKayit" 
                        CssClass="input-notification error" ForeColor="" ></asp:RegularExpressionValidator>
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
                        ControlToValidate="txtSifre" ValidationGroup="yeniKayit" ></asp:RequiredFieldValidator>
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
                         ControlToValidate="txtSifreTekrari" ValidationGroup="yeniKayit" ></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="va6" runat="server" 
                         ControlToCompare="txtSifre" ControlToValidate="txtSifreTekrari" 
                         ValidationGroup="yeniKayit" Display="Dynamic" 
                         CssClass="input-notification error" ForeColor=""
                         ErrorMessage="Şifreniz Farklı Tekrar Şifre Giriniz."></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Telefon:</td>
                <td>
                  <asp:TextBox ID="txtTelefon" ClientIDMode="Static" Width="250px"  MaxLength="11" CssClass="text"  runat="server"  ></asp:TextBox><br />
                    
                        <span id="errTel" style="display:none" class="input-notification error" ></span>  
                               
                        <asp:RequiredFieldValidator ID="rfvTelefon" ClientIDMode="Static" runat="server" 
                        ControlToValidate="txtTelefon" Display="Dynamic" 
                        ErrorMessage="Lütfen  Telefon Numarası Giriniz." 
                        CssClass="input-notification error" ForeColor=""
                        SetFocusOnError="true" ToolTip="Şehir Seçiniz."
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Şehir: </td>
                <td>
            <asp:DropDownList ID="ddlSehirler" runat="server" Width="253px" Height="30px"  CssClass="text"  /><br />
            
             <asp:RequiredFieldValidator ID="rfvSehir" runat="server" ControlToValidate="ddlSehirler" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen  Şehir Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue=" " SetFocusOnError="true" ToolTip="Şehir Seçiniz." 
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>

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
                        ValidationGroup="yeniKayit" ></asp:RequiredFieldValidator>
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
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>

                        <asp:RequiredFieldValidator ID="rfvAy" runat="server" 
                        ControlToValidate="ddlAy" Display="Dynamic" 
                        ErrorMessage="Lütfen Ay Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue="0" SetFocusOnError="true" ToolTip="Ay Seçiniz." 
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>

                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="ddlYil" Display="Dynamic" 
                        ErrorMessage="Lütfen Yıl Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue="0" SetFocusOnError="true" ToolTip="Yıl Seçiniz." 
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top" style="height: 34px" >
                    Güvenlik Kodu:</td>
                <td>
              <asp:TextBox ID="txtGuvenlik"  autocomplete="off" CssClass="text" Width="80px" runat="server" ></asp:TextBox><br />
                     <asp:RequiredFieldValidator ID="rfvResim" runat="server" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen  Güvenlik Resmini Giriniz." 
                        CssClass="input-notification error" ForeColor=""
                        ControlToValidate="txtGuvenlik" ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>
                    <br />
              
                 <asp:Image ID="imgGuvenlik" ImageUrl="~/Include/GuvenlikResmi.aspx" runat="server" />
                 <br /> <br />
                </td>
            </tr>
            <tr>
                <td>
              
                </td>
                <td>
                    <asp:Button ID="btnUyeKayit"  ValidationGroup="yeniKayit" Width="150px" runat="server" Text="Bilgileri Kaydet" 
                        onclick="btnUyeKayit_Click" CssClass="button" /> <br /> <br /> <br />
                </td>
               
            </tr>
            </table>
  </div>
  </div>
    


<script type="text/javascript">

    $(document).ready(function () {


        $("#btnLogin").click(function () {

            var cookieEnabled = (navigator.cookieEnabled) ? true : false;

            if (!cookieEnabled) {
                alert("* Tarayıcınızın çerez fonksiyonu kapalı..\n * Lütfen bu fonksiyonu açınız. ");
                return false;
            }
        });


        $("#txtTelefon").keypress(function (e) {
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                //display error message
                $("#errTel").html("Telefon numarası sayısal degel olmalıdır.").show().fadeOut(5000);
                return false;
            }
        });

    });

    $("#kayit").corner("round 6px");
    $("#kayitIc").corner("round 6px");
    $("#login").corner("round 6px");
    $("#loginIc").corner("round 6px");


                  function MailKontlor() {
                      var mail = $("#txtEposta").val()
                      if (isValidEmailAddress(mail)) {
                          $.ajax({
                              type: "POST",
                              url: "../Include/Ajax/EPostaContlor.ashx",
                              data: "EPosta=" + mail,
                              success: function (result) {
                                  if (result == 0) {
                                     $("#hlSifreLink").css("display", "none")
                                  }
                                  else {
                                     $("#hlSifreLink").css("display", "block")
                                  }
                              },
                              error: function () {
                              }
                          });
                      }
                  }
</script>
</asp:Content>

