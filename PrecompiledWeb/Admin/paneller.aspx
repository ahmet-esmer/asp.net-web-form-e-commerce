<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="admin_Paneller" Title="Untitled Page" Codebehind="paneller.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">Paneler</h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a href="#tab1" class="default-tab current">Paneler</a></li> 
			<li><a href="#tab2">Yeni Paneler Ekle</a></li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

	      <asp:GridView ID="gvwPaneller" runat="server"  AutoGenerateColumns="false" 
                       ForeColor="#333333" GridLines="None"  Width="100%" >
                       <Columns>
                           <asp:TemplateField HeaderText="Sıra">
                               <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                               <HeaderStyle  Width="30"  Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Resim">
                                <ItemTemplate>
                                  <object width="100" id="flashBanner"  height="50">
                                      <embed  src="../Products/Flash/<%# DataBinder.Eval(Container, "DataItem.resim_adi") %>" 
                                      type="application/x-shockwave-flash" width="100" height="50"></embed>
                                  </object>
                                </ItemTemplate>
                            <HeaderStyle Width="100px" Font-Bold="true" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Gösterim Alanı" >
                                <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.parametre")%>
                                 </ItemTemplate>
                                <HeaderStyle Width="100px" Font-Bold="true" >
                                </HeaderStyle>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Reklam Bilgi">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.resim_baslik")%>
                               </ItemTemplate>
                               <HeaderStyle Width="420" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Durum">
                               <ItemTemplate>
                                   <a title="<%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&amp;islem=durum&amp;durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="duzen"  />
                               <HeaderStyle Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                          <%-- <asp:TemplateField HeaderText="Düzenle">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=duzenle' title="Reklam Düzenle">
                                    <img src="images/duzenle.gif" alt="Düzenle" border="0" /> </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20" />
                           </asp:TemplateField>--%>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href="?resimId=<%# DataBinder.Eval(Container, "DataItem.id") %>&resimadi=<%# DataBinder.Eval(Container, "DataItem.resim_adi") %>" 
                                       onclick=" return silKontrol()" title="Reklam Sil"><img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <HeaderStyle  Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                       </Columns>
                        <RowStyle  CssClass="RowStyle" />
                     
                       <HeaderStyle CssClass="HeaderStyle" />
                       <PagerStyle CssClass="HeaderStyle" HorizontalAlign="Center" />
                      
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>
		
		</div> <!-- End #tab1 -->
		<div style="display: none;" class="tab-content" id="tab2">
					
	<!-- Reklam Ekleme Paneli-->
    <table cellpadding="0" cellspacing="0" class="ortaTablo" width="100%">      
            <tr>
                <td class="txtLabelTd" >
                    Banner Bilgi</td>
                <td>
                  <asp:TextBox ID="txtBannerBaslik"  runat="server" TextMode="MultiLine" Rows="3" 
                        CssClass="text-input form .small" Width="400px" ></asp:TextBox><br />
                     
                </td>
                <td valign="middle" rowspan="5">
                <asp:Image ID="img_Ana_Resim" Visible="false" runat="server" Width="150px" /> 
                  
                <asp:HiddenField  ID="Hdd_Resim_Ad" runat="server"/>
                <asp:HiddenField  ID="Hdd_Resim_id" runat="server"/>
              
                </td>
            </tr>
            
            <tr>
                <td class="txtLabelTd" >
    
                    Banner :</td>
                <td>
               <asp:FileUpload ID="txtresim" runat="server" CssClass="text-input"   /><br />
               <small> Banner Flash Boyutu genişlik 613 px Yükseklik 250 px</small>
                </td>
            </tr>
                 <tr>
                <td class="txtLabelTd" >
                Gösterilecek Yer:
                </td>
                <td>
                     <asp:DropDownList ID="ddlFlashGosterimAlani" runat="server" Width="200" CssClass="form"   >
                         <asp:ListItem Value="0" Text="-- Gösterim Alanı Seçiniz --" ></asp:ListItem>
                         <asp:ListItem Value="solPanel" Text="Sol Panel Flash" ></asp:ListItem>
                         <asp:ListItem Value="sagFlash" Text="Sağ Panel Flash " ></asp:ListItem>
                         <asp:ListItem Value="popUp" Text="Anasayfa Pop Up"></asp:ListItem>
                         <asp:ListItem Value="sagKargo" Text="Sağ Kargo Alanı" ></asp:ListItem>
                         <asp:ListItem Value="sagAlt" Text="Sağ Alt Resim Alanı" ></asp:ListItem>
                         <asp:ListItem Value="hediye" Text="Hediye Kampanya Resim" ></asp:ListItem>
                     </asp:DropDownList>
                 
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="ddlFlashGosterimAlani" CssClass="mesaj" 
                            ErrorMessage="* Flash gösterim alanı seçiniz." InitialValue="0"  ValidationGroup="resimSira"></asp:RequiredFieldValidator>
                     
                </td>
            </tr>
      <%--      <tr>
                <td class="txtLabelTd" >
                Resim No:
                </td>
                <td>
                 <asp:DropDownList ID="dpl_ResimNo" runat="server" Width="200" CssClass="form"   >
                 </asp:DropDownList>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="dpl_ResimNo" CssClass="mesaj" 
                            ErrorMessage="* Resim Listelenme Sırası Giriniz." InitialValue="0"  ValidationGroup="resimSira"></asp:RequiredFieldValidator>
                     
                </td>
            </tr>--%>
            
            <tr>
                <td class="txtLabelTd" >
                    Durum</td>
                <td>
                    <asp:CheckBox ID="ckb_ReklamDurum" runat="server" Checked="True" />
                     
                </td>
            </tr>
            
            <tr>
                <td >
               
                </td>
                <td style=" padding-bottom:15px; padding-top:15px;" >
             <asp:Button ID="btnBannerEkle" runat="server" Text="Site Banner Ekle" 
                               ValidationGroup="resimSira" CssClass="button" Width="200px" onclick="btnBannerEkle_Click" 
                          /> 
                             
               <asp:Button ID="btnBannerGuncelle" runat="server" Text="Banner Güncelle" 
                               Visible="false" 
                         ValidationGroup="resimSira" CssClass="button" Width="150px" onclick="btnBannerGuncelle_Click" 
                        />            
                </td>
                
            </tr>
        </table>
          
                   	
								
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>
</asp:Content>

