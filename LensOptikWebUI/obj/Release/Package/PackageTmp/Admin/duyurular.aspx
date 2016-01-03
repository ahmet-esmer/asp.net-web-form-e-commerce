<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="AdminDuyurular" Title="Untitled Page" Codebehind="duyurular.aspx.cs" %>




  
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">Duyurular</h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

             <div style="padding-bottom:10px; text-align:right;" >
             <asp:Button ID="btnYoonetiEkle" CssClass="button"  runat="server" 
            Text="Yeni Duyuru Ekle" onclick="btnYoonetiEkle_Click" /></div>

            <asp:GridView ID="GridView1" runat="server" AllowPaging="true" 
                       AllowSorting="true" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" OnPageIndexChanging="GridView1_Paging" 
                       PageSize="10" Width="100%">
                       <Columns>
                           <asp:TemplateField HeaderText="Sıra">
                               <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                               <HeaderStyle  Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Duyuru Adı">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.duyuru_adi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="420" Font-Bold="true" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eklenme Tarihi">
                               <ItemTemplate>
                       
                                 <%# BusinessLayer.DateFormat.TarihSaat(Convert.ToString(DataBinder.Eval(Container, "DataItem.eklenme_tarihi")))%>
                               </ItemTemplate>
                               <HeaderStyle Width="150" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Durum">
                               <ItemTemplate>
                                   <a title=" <%# DataBinder.Eval(Container, "DataItem.duyuru_adi")%> <%# GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&amp;islem=durum&amp;durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>'>
                            <%# GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="duzen" />
                               <HeaderStyle Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Düzenle">
                               <ItemTemplate>
                                   <a href='duyuru_ekle.aspx?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=duzenle' title=" <%# DataBinder.Eval(Container, "DataItem.duyuru_adi")%>Duyuru Düzenle">
                                    <img src="images/duzenle.gif" alt="Düzenle" border="0" /> </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&' 
                                       onclick=" return silKontrol()" title=" <%# DataBinder.Eval(Container, "DataItem.duyuru_adi")%> Duyuru Sil"><img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <HeaderStyle  Width="30" Font-Bold="true"  />
                           </asp:TemplateField>
                       </Columns>
                   <RowStyle  CssClass="RowStyle" />
                     
                       <SelectedRowStyle   />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <EditRowStyle />
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>
			  
			
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>

</asp:Content>

