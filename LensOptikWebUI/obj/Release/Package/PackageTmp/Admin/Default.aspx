<%@ Page Language="C#" AutoEventWireup="true" Inherits="AdminGiris" ValidateRequest="false" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Yönetici Panel Giriş</title>  
    <link rel="stylesheet" href="adminStyle/login.css" type="text/css" media="screen"/>


</head>
<body >
 <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
 </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
   
<table width="500" border="0" align="center" cellpadding="0" cellspacing="0" class="tablo">
  <tr>
    <td  class="ust" colspan="2">Yönetici Panel Girişi</td>
  </tr>
  <tr>
    <td style="height:30px;" colspan="2" >
             <asp:Label ID="lblMesaj" CssClass="mesaj"  Visible="false" runat="server" Text="Label" ></asp:Label>
             <asp:UpdateProgress ID="UpdateProgress1"  runat="server"  AssociatedUpdatePanelID="UpdatePanel1">
             <ProgressTemplate>
                 <div class="girisYazi">Giriş İşlemi Yapılıyor</div>
                <img src="images/giris.gif" class="girisGif"  style="padding-left:20px;"/>
             </ProgressTemplate>
             </asp:UpdateProgress>
      </td>
  </tr>
  <tr>
    <td class="style1" style="padding-left:20px;">
          <label class="label" >E-Posta</label>
          <asp:TextBox ID="txtEposta" CssClass="form" runat="server" ValidationGroup="gropGiris" size="30"></asp:TextBox>
     
      </td>
      <td >
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
              ControlToValidate="txtEposta" CssClass="hata" 
              ErrorMessage="* E-Posta Edresi Giriniz." ValidationGroup="gropGiris" ></asp:RequiredFieldValidator>
      </td>
  </tr>
  <tr>
    <td  class="yazi" style="height:35px; padding-left:20px;">
    <label class="label" >Şifre</label>
    <asp:TextBox ID="txtsifre"  runat="server" size="30" CssClass="form" TextMode="Password" ValidationGroup="gropGiris"></asp:TextBox>    
    </td>
      <td>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
              ControlToValidate="txtsifre" CssClass="hata" 
              ErrorMessage="* Şifrenizi Giriniz." ValidationGroup="gropGiris"></asp:RequiredFieldValidator>
      </td>
  </tr>
  <tr>
    <td class="yazi" style="height:60px; padding-right:20px; text-align:right;" colspan="2" >
        <label>
        <asp:Button ID="btnOturumAc" runat="server" class="buttonGiris"
            onclick="btnOturumAc_Click" Text="Oturum Aç" ValidationGroup="gropGiris" CommandArgument="UpdateProgress1" />
            
        </label>
      </td>
  </tr>
    <tr>
    <td class="alt">
      <asp:RadioButtonList ID="rbtHatirla" CssClass="beniHatirla" runat="server" RepeatColumns="2" >
      <asp:ListItem Value="hatirla" >Beni Hatırla</asp:ListItem>
      <asp:ListItem Value="unut" >Beni Unut</asp:ListItem>
      </asp:RadioButtonList>            
    
    </td>
      <td class="alt" style="text-align:right; padding-right:10px;">
           Şifremi Unuttum 
          <asp:RadioButton ID="RadioButton1" runat="server"  onClick="document.getElementById('kk').style.display=''; document.getElementById('UpdatePanel1').style.display='none';" />
      </td>
  </tr>
  </table>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnOturumAc" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    
  <table width="500" id="kk" runat="server" border="0" align="center" cellpadding="0" cellspacing="0" style="display:none;" class="tablo">
 <tr>
    <td  class="ust" colspan="2">Şifremi Gönder</td>
  </tr>
  <tr>
    <td style="height:30px; padding-top:10px; padding-left:20px;" colspan="2"   >
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
          ErrorMessage="* E-Posta Alanını Doldurunuz." ControlToValidate="txtSifreGonder" 
          CssClass="mesaj1" ValidationGroup="gropSifre" ></asp:RequiredFieldValidator> <br />
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
          ControlToValidate="txtSifreGonder" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"   ErrorMessage="* Geçerli E-Posta Adresi Giriniz." ValidationGroup="gropSifre" CssClass="mesaj1"
          ></asp:RegularExpressionValidator>
      </td>
  </tr>
     <tr >
  <td style="padding-left:20px; padding-bottom:10px; height:80px;" class="yazi">
 <label class="label" >E-Posta</label>  
  <asp:TextBox ID="txtSifreGonder"  CssClass="form" runat="server" ValidationGroup="gropSifre"></asp:TextBox><br />
      
  </td>
       
  <td style=" padding-right:20px;  height:80px; text-align:right; padding-top:8px;">
  <asp:Button ID="btnSifreGonder" ValidationGroup="gropSifre" runat="server" 
          Text="Şifremi Gönder" class="buttonGiris" onclick="btnSifreGonder_Click" />
  </td>
  </tr>
  <tr>
  <td class="alt_2" colspan="2">
    Yönetici Giriş
          <asp:RadioButton ID="RadioButton2" runat="server"  onClick="document.getElementById('kk').style.display='none'; document.getElementById('UpdatePanel1').style.display='';" />
  </td>
  </tr>
</table>

    </form>
</body>
</html>
