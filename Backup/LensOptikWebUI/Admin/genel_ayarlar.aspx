<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="adminGenelAyarlar" Title="Untitled Page" Codebehind="genel_ayarlar.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <div class="content-box"><!-- Start Content Box -->
				
			<div class="content-box-header">
			 <h3 style="cursor: s-resize;">Genel Ayarlar</h3>
			 <ul style="display: block;" class="content-box-tabs">
				<li><a href="#tab1" id="link1" class="default-tab current">Site İletişim Bilgileri</a> </li>
				<li><a href="#tab2" id="link2" >Mail Server Ayarları</a></li>
                <li><a href="#tab3" id="link3" >SMS Ayarları</a></li>
			 </ul>
			<div class="clear"></div>
			</div> <!-- End .content-box-header -->
            <div style="display: block;"  class="content-box-content">
			<div style="display: block;" class="tab-content default-tab" id="tab1">
               
               <table align="center" cellpadding="0" cellspacing="0" style="height:auto; width:100%; padding-bottom:20px; padding-top:15px;">
            <tr>
                <td class="txtLabelTd" style="width:170px;">
                    <label>Firma Adı: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtFirmaAdi" runat="server" CssClass="text-input medium-input"  ></asp:TextBox>
               </td>
            </tr>
               <tr>
                <td class="txtLabelTd">
                   <label>Yetkili Kişi: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtYetkili" runat="server"  CssClass="text-input medium-input" ></asp:TextBox>
               </td>
            </tr>
                <tr>
                <td class="txtLabelTd">
                    <label>Telefon: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtTelefon" runat="server"  CssClass="text-input medium-input" ></asp:TextBox>
               </td>
            </tr>
                   <tr>
                <td class="txtLabelTd">
                    <label> Faks:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtFaks" runat="server"  CssClass="text-input medium-input" ></asp:TextBox>
               </td>
            </tr>
             <tr>
                <td class="txtLabelTd">
                 <label>Vergi Dairesi: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtVergiDairesi" runat="server" CssClass="text-input medium-input"  ></asp:TextBox>
               </td>
            </tr>
            <tr>
                <td class="txtLabelTd">
                    <label>Vergi Numarası: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtVergiNo" runat="server"  CssClass="text-input medium-input"  ></asp:TextBox>
               </td>
            </tr>
                <tr>
                <td class="txtLabelTd">
               <label>E-Posta: </label>     
                </td>
                <td>
                    <asp:TextBox ID="txtEposta" runat="server"  CssClass="text-input medium-input"  ></asp:TextBox>
               </td>
            </tr> 
            <tr>
                <td class="txtLabelTd" valign="top">
                    <label>Adres: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtAdres" runat="server" TextMode="MultiLine" Rows="3" Height="50px" CssClass="text-input medium-input" ></asp:TextBox>
               </td>
            </tr>
            <tr>
                <td class="txtLabelTd">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnIletisimBilgisiDuzenle" runat="server" CssClass="button" 
                        Font-Size="12px" OnClick="btnIletisimBilgisiDuzenle_Click"  Width="170px" Text="Site İletişim Bilgisi Düzenle"   ></asp:Button>
                </td>
             
            </tr>
        </table>
                  
			</div> <!-- End #tab1 -->
			<div style="display: none;" class="tab-content" id="tab2">

             <table style="width: 100%">
            <tr>
                <td style="height: 18px; width:170px;">
                   <label>Mail Server: </label>
                </td>
                <td style="height: 18px;width:310px;">
                    <asp:TextBox ID="txtMailServer" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                </td>
                <td style="height: 18px">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Lütfen Mail Server Alanını Doldurunuz." CssClass="input-notification error"
                        ControlToValidate="txtMailServer" ValidationGroup="mailServer" ForeColor="" ></asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td>
                  <label> E-Mail Adresi:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtGonderenMail" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtGonderenMail" ValidationGroup="mailServer" CssClass="input-notification error" 
                        ErrorMessage="Lütfen Gönderen Mail Daresi Giriniz." ForeColor=""></asp:RequiredFieldValidator><br />
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtGonderenMail" ValidationGroup="mailServer"
                        ErrorMessage="Lütfen Geçerli E-Posta Adresi Giriniz." CssClass="input-notification error" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="" ></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                   <label>Kullanıcı Adı: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtkullaniciAdi" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtkullaniciAdi" CssClass="input-notification error" 
                        ErrorMessage="Lütfen Kullanıcı Alanını Doldurunuz." 
                        ValidationGroup="mailServer" ForeColor=""></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                  <label>Şifre: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtMailSifre" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtMailSifre"
                        ErrorMessage="Lütfen Sifre Alanının Doldurunuz." CssClass="input-notification error" 
                        Display="Dynamic" ForeColor="" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                  <label>Şifre Tekrarı:</label>  </td>
                <td>
                    <asp:TextBox ID="txtMailSifreTekrari" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtMailSifreTekrari"  CssClass="input-notification error" 
                        ErrorMessage="Lütfen Şifre Tekrarı Alanını Doldurunuz." 
                        ValidationGroup="mailServer" ForeColor="" ></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtMailSifre" ControlToValidate="txtMailSifreTekrari" 
                        CssClass="input-notification error" ErrorMessage="Şifreniz Farklı Tekrar Şifre Giriniz." 
                        ValidationGroup="mailServer" ForeColor="" ></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnMailServer" runat="server" CssClass="button" 
                        Font-Size="12px" onclick="btnMailServer_Click" Text="Mail Server Düzenle" 
                        ValidationGroup="mailServer" Width="170px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

			</div> <!-- End #tab2 -->  
            <div style="display: none;" class="tab-content" id="tab3">
             
              <table style="width: 100%">
           <tr>
                <td style="height: 18px; width:170px;">
                   <label>Originator:</label> </td>
                <td style="height: 18px; width:300px;">
                    <asp:TextBox ID="txtOriginator" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtOriginator" CssClass="input-notification error" 
                        ErrorMessage="* Lütfen Originatorı Alanını Doldurunuz." 
                        ValidationGroup="SMS_kayit" ForeColor="" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td  style="height: 18px; width:170px;">
                 <label> SMS Gelecegi Numara:</label>
                </td>
                <td style="height: 18px; width:300px;">
                    <asp:TextBox ID="txtSistemSmsTelefon" runat="server" CssClass="text-input medium-input"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="txtSistemSmsTelefon" CssClass="input-notification error" 
                        ErrorMessage="Lütfen Telefon Numarası Alanını Doldurunuz." 
                        ValidationGroup="SMS_kayit" ForeColor=""></asp:RequiredFieldValidator>
                   <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureAMPMPlaceholder=""
                         CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder=""
                         CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""
                         Enabled="True" Mask="+\90 (999) 999-9999" MaskType="Number" TargetControlID="txtSistemSmsTelefon">
                         </cc1:MaskedEditExtender>
                </td>
            </tr>
            <tr>
                <td  style="height: 18px; width:170px;">
                   <label>Kullanıcı Adı: </label> 
                </td>
                <td style="height: 18px; width:300px;">
                    <asp:TextBox ID="txtSmsKullanici" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                        ControlToValidate="txtSmsKullanici" CssClass="input-notification error" 
                        ErrorMessage="Lütfen Kullanıcı Alanını Doldurunuz." 
                        ValidationGroup="SMS_kayit" ForeColor=""  ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                  <label>Şifre:</label> 
                </td>
                <td>
                    <asp:TextBox ID="txtSmsSifre" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="txtSmsSifre" ValidationGroup="SMS_kayit" CssClass="input-notification error" 
                        ErrorMessage="Lütfen Sifre Alanının Doldurunuz." 
                        Display="Dynamic" ForeColor="" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                   <label>Şifre Tekrarı:</label> 
                </td>
                <td>
                    <asp:TextBox ID="txtSmsSifreTekrari" runat="server" CssClass="text-input medium-input"  Width="400px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="txtSmsSifreTekrari" CssClass="input-notification error" 
                        ErrorMessage="Lütfen Şifre Tekrarı Alanını Doldurunuz." 
                        ValidationGroup="SMS_kayit" ForeColor="" ></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToCompare="txtSmsSifre" ControlToValidate="txtSmsSifreTekrari" 
                        CssClass="input-notification error" ErrorMessage="Şifreniz Farklı Tekrar Şifre Giriniz." 
                        ValidationGroup="SMS_kayit" ForeColor=""></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSMSkayit" runat="server" CssClass="button" 
                        Font-Size="12px"  Text="SMS Bilgi Düzenle" 
                        ValidationGroup="SMS_kayit" Width="170px" onclick="btnSMSkayit_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            
        </table>
               
		    </div> <!-- End #tab3 -->  
		    </div> <!-- End .content-box-content -->
				
 </div>

</asp:Content>

