<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="admin_toplu_hediye_ekle" Codebehind="topluHediyeEkle.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">Kategori İşlemleri</h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a id="link1" href="#tab1" class="default-tab current">Toplu Ürün Hediye Ekleme</a></li> 
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

		   <table border="0" cellpadding="0" cellspacing="0" class="ortaTd">
                    <tr>
                        <td style="width:150px; height:30px;">
                        <label>Kategoriler </label>
                        </td>
                        <td style="width:500px;">
                            <asp:DropDownList ID="ddlkategoriler" CssClass="text-input"
                              Width="300px" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height:30px; ">
                        <label>Ürün Id: </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUrunId" runat="server" CssClass="text-input" ></asp:TextBox>
                        </td>
                        <td>

                        <asp:RequiredFieldValidator ID="rfvUrunId" runat="server" 
                        ControlToValidate="txtUrunId" ErrorMessage="Geçerli ürün ID Yazınız." 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>

                        <cc1:FilteredTextBoxExtender ID="filtleUrunId" runat="server" 
                        TargetControlID="txtUrunId" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>

                        </td>
                    </tr>
                    <tr>
                          <td style="height:30px; padding-bottom:30px;">
                          </td>
                          <td>
                          <asp:Button ID="btnUrunHediye" runat="server" CssClass="button" 
                               Text="Toplu Hediye Ekle"  ValidationGroup="kategori" 
                                  onclick="btnUrunHediye_Click" />
                            
                          </td>
                          <td>
                          </td>
                    </tr>
                 
                </table>	 
				
		</div> <!-- End #tab1 -->
		</div> <!-- End .content-box-content -->
</div>
                
   </asp:Content>

