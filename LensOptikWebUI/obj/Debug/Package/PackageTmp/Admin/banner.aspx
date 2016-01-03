<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="Admin_Banner" Title="Untitled Page" Codebehind="banner.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Reklam Ekleme Paneli-->

    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">
            Anasayfa Banner
         </h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a id="link1" href="#tab1" class="default-tab current">Banner Resimleri</a></li> 
			<li><a id="link2" href="#tab2">Yeni Resim Ekle</a></li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

			
    <asp:GridView ID="gvwBanner" runat="server" AllowPaging="true" 
                       AllowSorting="true" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" Width="100%">
                       <Columns>
                           <asp:TemplateField HeaderText="Sıra">
                               <ItemTemplate>
                             <%# DataBinder.Eval(Container, "DataItem.sira")%>
                               </ItemTemplate>
                               <HeaderStyle  Width="30" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Resim" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <img src="../Products/Big/<%# DataBinder.Eval(Container, "DataItem.ResimAdi") %>" width="60px" />
                                </ItemTemplate>

                             <HeaderStyle Width="100px" Font-Bold="true" />
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="İnternet Adresi">
                               <ItemTemplate>

                                   <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# DataBinder.Eval(Container, "DataItem.ResimBaslik")%>' Target="_blank" runat="server"><%# DataBinder.Eval(Container, "DataItem.ResimBaslik")%></asp:HyperLink>
                                
                               </ItemTemplate>
                               <HeaderStyle Width="420" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Durum">
                               <ItemTemplate>
                                   <a title="<%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.Durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.Id") %>&amp;islem=durum&amp;durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.Durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.Durum"))) %>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="duzen" />
                               <HeaderStyle Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Düzenle">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.Id") %>&amp;islem=duzenle' title="Reklam Düzenle">
                                    <img src="images/duzenle.gif" alt="Düzenle" border="0" /> </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href="?resimId=<%# DataBinder.Eval(Container, "DataItem.Id") %>&resimadi=<%# DataBinder.Eval(Container, "DataItem.ResimAdi") %>" 
                                       onclick=" return silKontrol()" title="Reklam Sil">
                                       <img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <HeaderStyle  Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                       </Columns>
                   <RowStyle  CssClass="RowStyle" />
                     
                       <SelectedRowStyle   />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <EditRowStyle />
                       <PagerStyle CssClass="HeaderStyle" HorizontalAlign="Center" />
                      
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>	
						
		</div> <!-- End #tab1 -->
		<div style="display: none;" class="tab-content" id="tab2">
					
			 <table cellpadding="0" cellspacing="0" class="ortaTablo" width="100%">      
            <tr>
                <td class="txtLabelTd" >
                    İnternet Adresi</td>
                <td>
                    <asp:TextBox ID="txtResimBaslik"  runat="server"
                        CssClass="text-input medium-input" ></asp:TextBox><br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtResimBaslik" CssClass="input-notification error" Display="Dynamic" 
                        ErrorMessage="Geçerli İnternet Adresi Giriniz."  ForeColor=""
                        ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                    <br />
                     
                </td>
                <td valign="middle" rowspan="5">
                <asp:Image ID="img_Ana_Resim" Visible="false" runat="server" Width="100px" /> 
                <asp:HiddenField  ID="Hdd_Resim_Ad" runat="server"/>
                <asp:HiddenField  ID="Hdd_Resim_id" runat="server"/>
              
                </td>
            </tr>
            
            <tr>
                <td class="txtLabelTd" >
    
                    Reklam Resim :</td>
                <td>
                 <asp:FileUpload ID="txtResimEk" runat="server" CssClass="form"     Height="20px" />
                 <br />
                 <small>Küçük Resim  Boyutu genişlik 613 px Yükseklik 250 px</small>    
               
                
                </td>
            </tr>
            
            <tr>
                <td class="txtLabelTd" >
                Resim No:
                </td>
                <td>
                 <asp:DropDownList ID="dpl_ResimNo" runat="server" Width="200" CssClass="form"   >
                 </asp:DropDownList>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="dpl_ResimNo" CssClass="input-notification error" 
                            ErrorMessage="Resim Listelenme Sırası Giriniz." InitialValue="0"  ValidationGroup="resimSira" ForeColor="" ></asp:RequiredFieldValidator>
                     
                </td>
            </tr>
            
            <tr>
                <td class="txtLabelTd" >
                    Durum</td>
                <td>
                    <asp:CheckBox ID="ckb_ReklamDurum" Checked="true" runat="server" />
                     
                </td>
            </tr>
            
            <tr>
                <td >
               
                </td>
                <td style=" padding-bottom:15px; padding-top:15px;" >
             <asp:Button ID="btnReklamEkle" runat="server" Text="Site Reklam Ekle" 
                               ValidationGroup="resimSira" CssClass="button" Width="200px" 
                        onclick="btnReklamEkle_Click"  /> 
                             
               <asp:Button ID="btnReklamGuncelle" runat="server" Text="Resim Güncelle" 
                               Visible="false" 
                         ValidationGroup="resimSira" CssClass="button" Width="150px" 
                        onclick="btnReklamGuncelle_Click" />            
                </td>
                
            </tr>
        </table>		
								
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>

   

</asp:Content>

