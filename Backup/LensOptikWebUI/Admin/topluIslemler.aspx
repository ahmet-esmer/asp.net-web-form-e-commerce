<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="Admin_Toplu_Islemler" Title="Untitled Page" Codebehind="topluIslemler.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
               <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">Toplu Fiyat & Havale indirmi </h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

                 <div style="padding-bottom:10px; text-align:right;" >
     <asp:Button ID="btnFiyatGoster" CssClass="button" Width="150px"  runat="server" 
                  Text="Toplu Fiyat İndirimi" onclick="btnFiyatGoster_Click"  />
                  <asp:Button ID="btnHaveleGoster" CssClass="button" Width="180px"  runat="server" 
                  Text="Toplu Havele İndirimi" onclick="btnHaveleGoster_Click"  />
            </div>

			     <table border="0" cellpadding="0" cellspacing="0" class="ortaTd">
                    <tr>
                        <td style="width:150px; height:30px;">
                        <span> Kategoriler</span>
                        </td>
                        <td style="width:160px;">
                            <asp:DropDownList ID="ddlkategoriler"  CssClass="form"  Width="250px" 
                                runat="server" onselectedindexchanged="ddlkategoriler_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                        </td>
                        <td style="width:300px;">
                            
                        </td>
                    </tr>
                    <tr runat="server" id="rowFiyat"  >
                        <td class="style4" valign="top" >
                  <span> Fiyat İndirim: </span>
                        </td>
                        <td class="style4" valign="top"  >
                            <asp:TextBox ID="txtFiyatYuzde" runat="server" CssClass="form" Width="250"  ></asp:TextBox>
                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtFiyatYuzde"
                FilterType="Custom, Numbers" />
                <br />
                <br />
                <br />

                  <asp:Button ID="btnFiyatIndirim" runat="server" CssClass="button"  Text="Toplu Fiyat İndirimi" 
                                  ValidationGroup="kategori" onclick="btnFiyatIndirim_Click" />
                        </td>
                        <td class="style4" valign="top"  >
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtFiyatYuzde" runat="server" ErrorMessage="* Lütfen Fiyat Yüzde yazınız!" ValidationGroup="kategori" ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
   
                   <tr runat="server" id="rowHavale"  visible="false" >
                        <td class="style4" valign="top" >
                  <span> Havale İndirim: </span>
                        </td>
                        <td class="style4" valign="top"  >
                            <asp:TextBox ID="txtHavale" runat="server" CssClass="form" Width="250" ></asp:TextBox>
                              <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtHavale" FilterType="Custom, Numbers" />
                <br />
                <br />
                <br />
                  <asp:Button ID="btnHavaleIndirim" runat="server" CssClass="button"  
                                Text="Toplu Havale İndirimi"  ValidationGroup="havale" 
                                onclick="btnHavaleIndirim_Click"  />
                        </td>
                        <td class="style4" valign="top"  >
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtHavale" runat="server" ErrorMessage="* Lütfen Havale İndirim Mıktarı Giriniz!" ValidationGroup="havale"  ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
			
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>
               
         
</asp:Content>

