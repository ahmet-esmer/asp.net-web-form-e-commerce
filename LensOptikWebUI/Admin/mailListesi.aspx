<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="AdminUyeler" Title="Untitled Page" Codebehind="mailListesi.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
				<h3 style="cursor: s-resize;">Mail Listesi </h3>
			<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

			  <asp:GridView ID="GridView1" runat="server" 
                       AllowSorting="true" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None"  Width="100%" 
        PageSize="1">
                       <Columns>
                           <asp:TemplateField HeaderText="Sıra">
                               <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                               <HeaderStyle  Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                          <%-- <asp:TemplateField HeaderText="Adı Soyadı">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.ad_soyad")%><br />
                               
                               </ItemTemplate>
                               <HeaderStyle Width="200" Font-Bold="true" />
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>--%>
                           <asp:TemplateField HeaderText="E-Posta">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.mail")%>
                               </ItemTemplate>
                               <HeaderStyle Width="230" Font-Bold="true"  />
                                 <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mail">
                               <ItemTemplate>
                            <a href="mail_gonder.aspx?Ad=<%# DataBinder.Eval(Container, "DataItem.ad_soyad")%>&ePosta=<%# DataBinder.Eval(Container, "DataItem.mail")%> " title=" <%# DataBinder.Eval(Container, "DataItem.ad_soyad")%> Mail Gönder" /><img src="images/ePosta.gif" width="20px" /> </a> 
                               </ItemTemplate>
                               <HeaderStyle Width="40" Font-Bold="true"  />
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&' onclick=" return silKontrol()" title="<%# DataBinder.Eval(Container, "DataItem.ad_soyad")%> Listeden Sil"><img src="images/sil.gif"  /> </a>
                               </ItemTemplate>
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                               <HeaderStyle  Width="30" Font-Bold="true"  />
                           </asp:TemplateField>
                       </Columns>
                   <RowStyle  CssClass="RowStyle" />
                     
                       <SelectedRowStyle   />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <EditRowStyle />
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>
                   <br />
                    <div class="pagination">
        <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
    </div>  
                   
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>

            

</asp:Content>
