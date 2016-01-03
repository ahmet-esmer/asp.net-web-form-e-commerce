<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="AdminKategoriler" Codebehind="kategoriler.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">Kategori İşlemleri</h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a id="link1" href="#tab1" class="default-tab current">Kategoriler</a></li> 
			<li><a id="link2" href="#tab2">Yeni Kategori Ekle</a></li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

		   <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CellPadding="4" Width="100%" GridLines="None">
                        <Columns>
                          <asp:TemplateField HeaderText="Ürün Kategorileri">
                            <ItemTemplate>  
                    
                          <%# BusinessLayer.GenelFonksiyonlar.KategoriCizgi(Convert.ToString(DataBinder.Eval(Container, "DataItem.serial"))) %>
                          <%# DataBinder.Eval(Container, "DataItem.kategoriadi")%>
                  
                            </ItemTemplate>
                            <HeaderStyle  Width="420" Font-Bold="true" />
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sıra">
                            <ItemTemplate>  
      
                         <asp:TextBox ID="txtKatSira" 
                                     Text='<%# DataBinder.Eval(Container, "DataItem.sira") %>'
                                     CssClass="form"
                                     Width="50px"
                                     autocomplete="off"
                                     ToolTip='<%# DataBinder.Eval(Container, "DataItem.id") %>' 
                                     ontextchanged="kategoriSiraGuncelleme" 
                                     AutoPostBack="true" 
                                     runat="server"></asp:TextBox>     
                                <cc1:FilteredTextBoxExtender ID="filtleKdv" runat="server" TargetControlID="txtKatSira" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                            <HeaderStyle  Width="50" Font-Bold="true" />
                          </asp:TemplateField>           
                             <asp:TemplateField HeaderText="Durum">
                            <ItemTemplate>
                            <a href="?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&islem=durum&durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>" title="<%# DataBinder.Eval(Container, "DataItem.kategoriadi")%> <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %> ">
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                            </a>
                            </ItemTemplate>
                            <ItemStyle CssClass="duzen" />
                            <HeaderStyle Width="50" Font-Bold="true" />
                          </asp:TemplateField>
                          
                          <asp:TemplateField HeaderText="Düzenle">
                            <ItemTemplate>
                             <a href="kategoriler.aspx?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&islem=duzenle"  title="<%# DataBinder.Eval(Container, "DataItem.kategoriadi")%> Başlık Düzenle">
                              <img src="images/duzenle.gif" alt="Sayfa Başlık Düzenle" border="0" />
                             </a>
                            </ItemTemplate>
                            <ItemStyle CssClass="orta" />
                            <HeaderStyle   Width="30" Font-Bold="true" />
                          </asp:TemplateField>
                          
                          <asp:TemplateField HeaderText="Sil">
                            <ItemTemplate>
                             <a href="?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&flaSerial=<%# DataBinder.Eval(Container, "DataItem.serial") %>&islem=sil" title="<%# DataBinder.Eval(Container, "DataItem.kategoriadi")%> Başlık ve İçerigi Sil" onclick="return silKontrol_sayfa()">
                               <img src="images/sil.gif" border="0"   />
                             </a>
                            </ItemTemplate>
                            
                          <HeaderStyle  Width="30" Font-Bold="true" />
                          </asp:TemplateField>
                          
                          </Columns>
                   
                   <RowStyle CssClass="RowStyle" />
                     <HeaderStyle CssClass="HeaderStyle" />
                     <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                   </asp:GridView>
				
		</div> <!-- End #tab1 -->
		<div style="display: none;" class="tab-content" id="tab2">
				
           <table border="0" cellpadding="0" cellspacing="0" class="ortaTd">
                    <tr>
                        <td style="width:150px; height:30px;">
                        <label>Kategoriler </label>
                        </td>
                        <td style="width:500px;">
                            <asp:DropDownList ID="ddlkategoriler"  CssClass="text-input"  Width="300px" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">
                        <label>Kategori Adı</label>
                        </td>
                        <td class="style4">
                            <asp:TextBox ID="txtkategoriadi" runat="server" CssClass="text-input" Width="300"  ></asp:TextBox>
                        </td>
                        <td class="style4">
                            <asp:RequiredFieldValidator ID="vaKatAdi" ControlToValidate="txtkategoriadi" runat="server" ErrorMessage="Lütfen kategori adı yazınız!" ForeColor="" CssClass="input-notification error" ValidationGroup="kategori" ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
   
                    <tr>
                        <td style="height:30px; ">
                        <label>Title: </label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="vaTitle" runat="server" 
                                ControlToValidate="txtTitle" CssClass="input-notification error" 
                                ErrorMessage="Sayfa Başlığı Giriniz." ForeColor="" 
                                ValidationGroup="kategori" ></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                 
                      <tr>
                          <td style="height:30px; ">
                          <label>Keywords: </label>
                          </td>
                          <td>
                              <asp:TextBox ID="txtKeyword" runat="server" CssClass="form" Height="60px" 
                                  Rows="2" TextMode="MultiLine" Width="500px" Wrap="true"></asp:TextBox>
                          </td>
                          <td>
                              <asp:RequiredFieldValidator ID="vaKeyword" runat="server" 
                                  ControlToValidate="txtKeyword" CssClass="input-notification error" 
                                  ErrorMessage="Arama için Anahtar Kelime Giriniz."
                                  ValidationGroup="kategori" ForeColor="" ></asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr>
                          <td style="height:30px; ">
                           <label> Description:</label>   
                          </td>
                          <td>
                              <asp:TextBox ID="txtDescription" runat="server" CssClass="text-input medium-input" Height="60px" 
                                  Rows="2" TextMode="MultiLine" Width="500px" Wrap="true"></asp:TextBox>
                          </td>
                          <td>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                  ControlToValidate="txtDescription" CssClass="input-notification error" 
                                  ErrorMessage="Sayfa Açıklaması Giriniz." 
                                  ValidationGroup="kategori" ForeColor="" ></asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr id="rowFlash" runat="server" style="display:none"  >
                          <td style="height:30px; ">
                              <label>Flash Banner:</label>
                              
                          </td>
                          <td>
                             <asp:FileUpload ID="txtresim" runat="server" CssClass="form"   />
                               <asp:HiddenField ID="hddResimAd" runat="server" />
                          </td>
                          <td>
                              <asp:Literal ID="ltlFlashKategori" runat="server"></asp:Literal>
                         </td>
                      </tr>
                      <tr>
                          <td style="height:30px; ">
                          <label>Durum: </label>
                          </td>
                          <td>
                              <asp:CheckBox ID="cbkategoridurum" runat="server" Checked="True" />
                          </td>
                          <td>
                    &nbsp;</td>
                      </tr>
                 
                      <tr>
                          <td style="height:30px; padding-bottom:30px;">
                          </td>
                          <td>
                              <asp:Button ID="btnKategoriKaydet" runat="server" CssClass="button" 
                                  onclick="btnKategoriKaydet_Click" Text="Kategori Kaydet" 
                                  ValidationGroup="kategori" />
                            
                              <asp:Button ID="btnKategoriGuncelle" runat="server" CssClass="button" 
                                  Text="Kategori Güncelle" 
                                  ValidationGroup="kategori" Visible="false" 
                                  onclick="btnKategoriGuncelle_Click" />
                          </td>
                          <td>
                          </td>
                      </tr>
                 
                </table>	
								
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>
                
   </asp:Content>

