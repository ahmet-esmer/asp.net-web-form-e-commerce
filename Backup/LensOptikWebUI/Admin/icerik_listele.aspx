<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="adminIcerikListeleme" Title="Untitled Page" Codebehind="icerik_listele.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">Sayfalar Başlıkları </h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a  id="link1" href="#tab1" class="default-tab current">Sayfalar</a></li> 
			<li><a  id="link2" href="#tab2">Sayfa Ekle</a></li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

	     <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" 
                     CellPadding="4" Width="100%"
                GridLines="None">
                        <Columns>
                          <asp:TemplateField HeaderText="Sayfa Başlıkları">
                            <ItemTemplate>               
                            <a href="icerik_ekle.aspx?id=<%# DataBinder.Eval(Container, "DataItem.id")%>&islem=icerik_getir">
                            <%# BusinessLayer.GenelFonksiyonlar.KategoriCizgi(Convert.ToString(DataBinder.Eval(Container, "DataItem.serial"))) %>
                            <%# DataBinder.Eval(Container, "DataItem.kategoriadi")%></a>
                            </ItemTemplate>
                            <HeaderStyle  Width="400" Font-Bold="true" />
                          </asp:TemplateField>
                          
                          <asp:TemplateField HeaderText="Sayfa İçerik Düzenle">
                            <ItemTemplate>               
                            <a href="icerik_ekle.aspx?id=<%# DataBinder.Eval(Container, "DataItem.id")%>&islem=icerik_getir">
                             Sayfa İçerik Düzenle</a>
                            </ItemTemplate>
                            <HeaderStyle  Width="300" Font-Bold="true" />
                          </asp:TemplateField>

                             <asp:TemplateField HeaderText="Gösterim Alanı">
                            <ItemTemplate>
                             <%# DataBinder.Eval(Container, "DataItem.gosterimAlani")%> 
                            </ItemTemplate>
                            <ItemStyle CssClass="orta" />
                                 <HeaderStyle   Width="100" Font-Bold="true" />
                          </asp:TemplateField>
                          
                           <asp:TemplateField >
                            <ItemTemplate>                           
                         
                              <a href="?islem=siraUst&id=<%# DataBinder.Eval(Container, "DataItem.id") %>&sira=<%# DataBinder.Eval(Container, "DataItem.sira")%>">  <%# BusinessLayer.GenelFonksiyonlar.SiraDurum(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.sira"))) %> </a>
                            </ItemTemplate>
                            <HeaderStyle  Width="20" Font-Bold="true" />
                          </asp:TemplateField>     
                           <asp:TemplateField >
                            <ItemTemplate>                           
                         <a href="?islem=siraAlt&id=<%# DataBinder.Eval(Container, "DataItem.id") %>&sira=<%# DataBinder.Eval(Container, "DataItem.sira")%>"> <img src="images/downArrow.gif" alt="Sayfa Başlık Düzenle" border="0" /></a>
                           
                            </ItemTemplate>
                            <HeaderStyle  Width="20" />
                            </asp:TemplateField> 
                           <asp:TemplateField HeaderText="Sıra">
                            <ItemTemplate>                           
                          <%# DataBinder.Eval(Container, "DataItem.sira")%>
                           
                            </ItemTemplate>
                            <HeaderStyle  Width="50" Font-Bold="true" />
                          </asp:TemplateField>        

                             <asp:TemplateField HeaderText="Durum">
                            <ItemTemplate>
                            <a href="?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&islem=durum&durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>" title="<%# DataBinder.Eval(Container, "DataItem.kategoriadi")%> <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>">
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                            </a>
                            </ItemTemplate>
                            <ItemStyle CssClass="duzen" />
                            <HeaderStyle Width="50" Font-Bold="true" />
                          </asp:TemplateField>
                          
                          <asp:TemplateField HeaderText="Düzenle">
                            <ItemTemplate>
                             <a href="?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&islem=duzenle"  title="<%# DataBinder.Eval(Container, "DataItem.kategoriadi")%> Başlık Düzenle">
                              <img src="images/duzenle.gif" alt="Sayfa Başlık Düzenle" border="0" />
                             </a>
                            </ItemTemplate>
                            <ItemStyle CssClass="orta" />
                                 <HeaderStyle   Width="30" Font-Bold="true" />
                          </asp:TemplateField>

                         
                          
                          <asp:TemplateField HeaderText="Sil" >
                            <ItemTemplate>
                             <a href="?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&islem=sil" title="<%# DataBinder.Eval(Container, "DataItem.kategoriadi")%> Başlık ve İçerigi Sil" onclick="return silKontrol_sayfa()">
                               <img src="images/sil.gif" border="0"   />
                             </a>
                            </ItemTemplate>
                            
                                 <HeaderStyle  Width="80" Font-Bold="true" />
                          </asp:TemplateField>
                          
                          </Columns>
                   
                   <RowStyle  CssClass="RowStyle" />
                     
                       <SelectedRowStyle />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <EditRowStyle  />
                       <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                   </asp:GridView>
						
		</div> <!-- End #tab1 -->
		<div style="display: none;" class="tab-content" id="tab2">
		   
            <table cellpadding="0" cellspacing="0" style="padding-bottom:15px;">
            <tr>
                <td colspan="3">
                
                   
                
                </td>
            </tr>
             <tr>
                <td class="txtLabelTd">
                <label>Sayfa Başlıkları </label>
                
                </td>
                <td class="style3">
                
                    <asp:DropDownList ID="ddlIcerikler" runat="server" CssClass="text-input small" Height="30px">
                    </asp:DropDownList>
                
                </td>
                <td>
                
                    &nbsp;</td>
            </tr>
              <tr>
                <td class="txtLabelTd">
               <label>Gösterim Alanı</label>
                
                </td>
                <td class="style3">
                     <asp:DropDownList ID="ddlGosterimAlani" runat="server" CssClass="text-input small"  >
                        <asp:ListItem Selected="True" Value="2" Text="Ana Menü"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Site Üstü"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Site Alt"></asp:ListItem>
                     </asp:DropDownList>
                </td>
                <td>
                
                    &nbsp;</td>
            </tr>



            
                     
             <tr>
                <td class="txtLabelTd">
                <label>Sayfa Başlığı:</label>
                </td>
                <td>
                 <asp:TextBox ID="txt_icerkBaslik" runat="server" Width="400px" CssClass="text-input" ></asp:TextBox>
                </td>
                <td>
                
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txt_icerkBaslik" ErrorMessage="* Başlık Giriniz."  ValidationGroup="sayfaAdi"
                        Font-Size="12px"></asp:RequiredFieldValidator>
                
                </td>
            </tr>
              <tr>
                <td class="txtLabelTd">
                <label>Sayfa Link:</label>
                </td>
                <td>
                 <asp:TextBox ID="txtLink" runat="server" Width="400px" CssClass="text-input" ></asp:TextBox>
                </td>
                <td>

                </td>
            </tr>
             <tr>
                <td class="txtLabelTd">
                <label> Durum:</label>
                </td>
                <td>
                
                    <asp:CheckBox ID="ckb_durum" runat="server" />
                
                </td>
                <td>
                
                </td>
            </tr>
             <tr>
                <td>
                
                    &nbsp;</td>
                <td colspan="2">
                
                    <asp:Button ID="btbIcerikBaslikGun" runat="server" 
                        Text="İçerik Başlık Güncelle" CssClass="button" onclick="icerik_Baslik_Guncelle_Click" Visible="false"  ValidationGroup="sayfaAdi"  />
                        
                        <asp:Button ID="btnIcerikBaslikEkle" runat="server"
                            Text="Sayfa Başlığı Oluştur" onclick="btnIcerikBaslikEkle_Click" 
                            CssClass="button" Width="150px"  ValidationGroup="sayfaAdi" />
                
                </td>
            </tr>
        </table>
								
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>

</asp:Content>

