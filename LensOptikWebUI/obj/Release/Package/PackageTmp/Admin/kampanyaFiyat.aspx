<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true" Inherits="Admin_KampanyaFiyat" Codebehind="kampanyaFiyat.aspx.cs" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;"> Kampanya Fiyatı </h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

			  
           <asp:GridView ID="gvwKampanya" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" DataKeyNames="id" 
                    onrowcommand="gvwKampanya_RowCommand"     >
                       <Columns>

                       

                        <asp:TemplateField HeaderText="Kampanya Kodu" >
                               <ItemTemplate>
                                  <%# Eval("puanKod")%> 
                               </ItemTemplate>
                               <ItemStyle  Width="150px" CssClass="anketGrig"  />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Açıklama" >
                               <ItemTemplate>

                               <asp:TextBox ID="txtAciklama" runat="server"  CssClass="text-input" 
                               Text='<%# Eval("aciklama")%>' Width="300px"  ></asp:TextBox>

                               </ItemTemplate>
                               <ItemStyle  Width="300px" CssClass="anketGrig"  />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Fiyat" >
                               <ItemTemplate>

                               <asp:TextBox ID="txtFiyat" runat="server"  CssClass="text-input" 
                               Text='<%#  Convert.ToDecimal(Eval("fiyat")).ToString("N")%>' Width="200px" ></asp:TextBox>

                               </ItemTemplate> 
                               <ItemStyle Width="200px" CssClass="anketGrig" />                     
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="Kaydet" >
                               <ItemTemplate>

                               <asp:ImageButton  ToolTip="Degişikligini Kaydet" ID="btnKaydet"
                                CommandName="Kaydet" ImageUrl="~/admin/images/kaydet.gif"
                                CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id") %>' runat="server" />

                               </ItemTemplate> 
                               <ItemStyle Width="40px" CssClass="anketGrig" />  
                           </asp:TemplateField>
                         

                           <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                               </ItemTemplate>
                               <ItemStyle Width="230px" CssClass="anketGrig" />  
                           </asp:TemplateField>
                       </Columns>
             
                   </asp:GridView>

			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>

</asp:Content>

