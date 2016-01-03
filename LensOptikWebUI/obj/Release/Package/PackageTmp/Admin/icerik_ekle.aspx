<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="adminIcerikEkleme" Title="Untitled Page" Codebehind="icerik_ekle.aspx.cs" %>

<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">Sayfa İçerigi Oluşturma</h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

			  <table cellpadding="0" cellspacing="0" class="ortaTablo" width="100%">
            
            <tr>
                <td  colspan="4" style="height:25px; padding-top:15px; padding-bottom:8px;">
                
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

                    </td>
            </tr>
            <tr>
                <td class="txtLabelTd"  style="width:180px;">
                  <label>İçerikler Başlığı: </label>  
                 </td>
                <td style="width:250px;">
                   
                        <asp:DropDownList ID="ddlIcerikler" runat="server"  
                            ValidationGroup="Icerik" AutoPostBack="true"
                            onselectedindexchanged="ddlIcerikler_SelectedIndexChanged" 
                            CssClass="text-input medium-input">
                        </asp:DropDownList>
                   
                        </td>
                <td>
                   
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="ddlIcerikler" CssClass="mesaj" 
                            ErrorMessage="Sayfa Adı Seçiniz." InitialValue="0">*</asp:RequiredFieldValidator>
                   
                        </td>
                <td rowspan="8">
                   
                        <asp:Image ID="img_Ana_Resim" Visible="false" runat="server" Width="150px" /> 
                        <asp:HiddenField  ID="Hdd_Resim_Ad" runat="server"/>
                        <asp:HiddenField  ID="Hdd_Resim_id" runat="server"/>
                        <br />
                        <br />
                    <asp:HyperLink ID="hypLinkSil"  Visible="false" onclick="return silKontrol();" runat="server">Sil</asp:HyperLink>
                       
                        </td>
            </tr>
            <tr>
                <td class="txtLabelTd">
                    &nbsp;</td>
                <td class="style3">
                   
                        &nbsp;</td>
                <td class="style4">
                   
                        &nbsp;</td>
            </tr>
            <tr>
                <td class="txtLabelTd">
                  <label>Title: </label>  
                </td>
                <td style="width:250px;">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtTitle" CssClass="mesaj" 
                        ErrorMessage="Sayfanın Title Alanını Doldurunuz.">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="txtLabelTd">
                  <label>Keywords: </label> 
                   </td>
                <td >
                   <asp:TextBox ID="txtKeyword" runat="server" Rows="2" TextMode="MultiLine" 
                        Wrap="true"  CssClass="text-input medium-input" Height="80px"></asp:TextBox> 
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtKeyword" CssClass="mesaj" 
                        ErrorMessage="Arama için Anahtar Kelime Giriniz.">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td class="txtLabelTd" valign="top">
                  <label>Description: </label>  
                </td>
                <td >
                   
                     <asp:TextBox ID="txtDescription" runat="server" Rows="2" TextMode="MultiLine" 
                         Wrap="true" CssClass="text-input medium-input" Height="80px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtDescription" CssClass="mesaj" 
                        ErrorMessage="Sayfa Açıklaması Giriniz.">*</asp:RequiredFieldValidator>
                </td>
                
            </tr>
           
            
            <tr>
                <td class="txtLabelTd">
                <label>Doktor Yazı İlk Gösterim </label>    
                </td>
                <td >
                   
                        <asp:TextBox ID="txtSayfaBaslik" runat="server"  Rows="2" TextMode="MultiLine" 
                         Wrap="true" CssClass="text-input medium-input" Height="80px"></asp:TextBox>
                   
                </td>
                <td>
                    &nbsp;</td>
                
            </tr>
           
            
            <tr>
                <td  class="txtLabelTd">
                   <label>Sayfa Resim: </label>
                </td>
                <td style="width:250px;">
                 <asp:FileUpload ID="txtresim" runat="server" CssClass="form"   />
                </td>
                <td>
                &nbsp;
                </td>
            </tr>
           
            <tr>
                <td>
                <label>Durum: </label>    
                </td>
                <td style="width:250px; height:30px;">
                    <asp:CheckBox ID="ckb_durum" runat="server" Checked="true" />
                </td>
                <td></td>
            </tr>
           
            <tr  style="width:100%;" id="kk" runat="server">
                <td colspan="4">
                    <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server"  Height="400px" >
                    </FCKeditorV2:FCKeditor>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td style="padding-bottom:20px; padding-top:15px;">
                        <asp:Button ID="btnIcerikEkle" runat="server"
                            Text="Yeni Sayfa İçerigi Oluştur" Width="200px" CssClass="button" onclick="btnIcerikEkle_Click" Visible="false"  />
                             <asp:Button ID="btnIcerikGuncelle" runat="server"
                            Text="Sayfa İçerik Güncelle" CssClass="button" Width="200px" Visible="false" 
                            onclick="btnIcerikGuncelle_Click" />
                </td>
                <td colspan="2">
                <asp:HiddenField  ID="hdf_Sayfa_id" runat="server"/>
                </td>
            </tr>
        </table>
			
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>
    
</asp:Content>

