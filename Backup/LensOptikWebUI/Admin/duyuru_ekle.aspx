<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="DuyuruEkle" Title="Untitled Page" Codebehind="duyuru_ekle.aspx.cs" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">
                                          <asp:Label ID="lblSayfaBaslik" Text="Duyuru Ekle" runat="server" ></asp:Label>
                                        </h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

			      <table  cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td  colspan="3">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txt_duyurBaslik" ErrorMessage="* Sergi Başlığı Giriniz." 
                        Font-Size="12px"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="lbl_duyuruBaslik" runat="server" Font-Size="12px" 
                        ForeColor="#333333" Text="Duyuru Başlık:"></asp:Label>
            </td>
            <td class="style4">
                <asp:TextBox ID="txt_duyurBaslik" runat="server" Width="350px" CssClass="text-input" ></asp:TextBox>
            </td>
            <td rowspan="3">
           
                <asp:HiddenField ID="Hdd_Duyuru_Id" runat="server" />
                <asp:HiddenField  ID="Hdd_Resim_Ad" runat="server"/>
                <asp:HiddenField  ID="Hdd_Resim_id" runat="server"/>
                <asp:Image ID="img_Ana_Resim" runat="server" Width="150px" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                    Resim:</td>
            <td class="style4">
                <asp:FileUpload ID="txtresim" runat="server" CssClass="form" Height="20px" />
            </td>
        </tr>
        
        <tr>
            <td class="style2">
                <asp:Label ID="lbl_durum" runat="server" Font-Size="12px" ForeColor="#333333" 
                        Text="Durum:"></asp:Label>
            </td>
            <td class="style4">
                <asp:CheckBox ID="ckb_durum" Checked="true" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3" >
               
             <FCKeditorV2:FCKeditor ID="fck_duyuruIcerik" runat="server"   ToolbarSet="Basic" Height="300px">
             </FCKeditorV2:FCKeditor>
                   
            </td>
        </tr>
        <tr>
            <td class="style3">
                    &nbsp;</td>
            <td class="style4">
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                        Text="Yeni Duyur Ekle" CssClass="button" />
                        
                         <asp:Button ID="btnDuyuruGuncelle" runat="server" 
                                    Text="Duyuru Düzenle" Visible="false"  CssClass="button" 
                    Width="200px" onclick="btnDuyuruGuncelle_Click"/>
            </td>
            <td>
                    &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" class="txtLabelTd"  >
            </td>
        </tr>
    </table>
			
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>



</asp:Content>

