<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="adminYoneticiler" Title="Untitled Page" Codebehind="yoneticiler.aspx.cs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">
            Yönetici İşlemleri
         </h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a id="link1" href="#tab1" class="default-tab current">Yöneticiler</a></li> 
			<li><a id="link2" href="#tab2">Yönetici Ekle</a></li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

              <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CellPadding="4" Width="100%" ForeColor="#333333" 
                       GridLines="None">
                       <Columns>
                       <asp:TemplateField HeaderText="Sıra">
                       <ItemTemplate>
                       <%# Container.DataItemIndex + 1 %>
                       </ItemTemplate>
                       <HeaderStyle  HorizontalAlign="Left" Width="80" Font-Bold="true" />
                       </asp:TemplateField>
                       
                          <asp:TemplateField HeaderText="Ad Soyad">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.adiSoyadi")%>
                                </ItemTemplate>
                                <HeaderStyle  HorizontalAlign="Left" Width="200" Font-Bold="true" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="E-Posta">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container,"DataItem.ePosta") %>
                                </ItemTemplate>
                                <HeaderStyle  HorizontalAlign="Left" Width="300" Font-Bold="true"/>
                            </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Tarih">
                               <ItemTemplate>
                             <%# BusinessLayer.DateFormat.Tarih(DataBinder.Eval(Container, "DataItem.kayitTarihi").ToString())%> 
                               </ItemTemplate>
                               <HeaderStyle Width="130" Font-Bold="true" />
                                <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>

                             <asp:TemplateField HeaderText="Sil" >
                                <ItemTemplate>
                                    <a href="?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&islem=sil" onclick="return silKontrol();" title="Yönetici Sil" >
                                 <img src="../Admin/images/sil.gif" alt="yönetici sil"  border="0"  />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="100" Font-Bold="true" />
                            </asp:TemplateField>
                         
                       </Columns>
                       <RowStyle  CssClass="RowStyle" />
                       <SelectedRowStyle   />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <EditRowStyle />
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>
						
		</div> <!-- End #tab1 -->
		<div style="display: none;" class="tab-content" id="tab2">
    
        <table>
            <tr>
                <td class="uyuFormBilgi" valign="top" >
                 <label> Adınız / Soyadınız:</label>    </td>
                <td >
                    <asp:TextBox ID="txtAdSoyad" Width="250px"  CssClass="text-input" runat="server" ></asp:TextBox><br />
                     <asp:RequiredFieldValidator ID="rfvAd" runat="server" 
                        Display="Dynamic"  CssClass="input-notification error" ForeColor=""
                        ErrorMessage="Lütfen  Ad ve Soyad Alanını Doldurunuz." 
                        ControlToValidate="txtAdSoyad"  ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                <label> E-mail Adresiniz:</label>     </td>
                <td>
                    <asp:TextBox ID="txtEposta" ClientIDMode="Static" runat="server" Width="250px"  CssClass="text-input"  ></asp:TextBox>

                     <asp:RequiredFieldValidator ID="rfvMail" runat="server" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen E-Posta Adresi Giriniz." 
                        CssClass="input-notification error" ForeColor="" 
                        ControlToValidate="txtEposta" ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>

                       <asp:RegularExpressionValidator ID="rfvMail2" runat="server" 
                        ControlToValidate="txtEposta" Display="Dynamic" ErrorMessage="Lütfen  Geçerli E-Posta Adresi Giriniz." 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ValidationGroup="yeniKayit" CssClass="input-notification error" ForeColor="" ></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                 <label>Şifre: </label>   </td>
                <td>
                    <asp:TextBox ID="txtSifre" runat="server" Width="250px"  CssClass="text-input"   TextMode="Password" ></asp:TextBox><br />
                      <asp:RequiredFieldValidator ID="rfvSifre" runat="server" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen  Şifre Alanını Doldurunuz."  
                        CssClass="input-notification error" ForeColor=""
                        ControlToValidate="txtSifre" ValidationGroup="yeniKayit" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                  <label>Şifre Tekrarı: </label>  </td>
                <td>
                     <asp:TextBox ID="txtSifreTekrari" Width="250px"  CssClass="text-input"  runat="server" TextMode="Password" ></asp:TextBox><br />
                             <asp:RequiredFieldValidator ID="rfvSifre2" runat="server" 
                         Display="Dynamic" 
                         ErrorMessage="Lütfen  Şifre Tekrarı Alanını Doldurunuz."  
                         CssClass="input-notification error" ForeColor=""
                         ControlToValidate="txtSifreTekrari" ValidationGroup="yeniKayit" ></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="va6" runat="server" 
                         ControlToCompare="txtSifre" ControlToValidate="txtSifreTekrari" 
                         ValidationGroup="yeniKayit" Display="Dynamic" 
                         CssClass="input-notification error" ForeColor=""
                         ErrorMessage="Şifreniz Farklı Tekrar Şifre Giriniz."></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                  <label> Telefon:</label>  </td>
                <td>
                    <asp:TextBox ID="txtTelefon" Width="250px"  CssClass="text-input"  runat="server"  ></asp:TextBox><br />
                             
                        <asp:RequiredFieldValidator ID="rfvTelefon" runat="server" 
                        ControlToValidate="txtTelefon" Display="Dynamic" 
                        ErrorMessage="Lütfen  Telefon Numarası Giriniz." 
                        CssClass="input-notification error" ForeColor=""
                        SetFocusOnError="true" ToolTip="Şehir Seçiniz."
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                   <label> Şehir: </label> </td>
                <td>
            <asp:DropDownList ID="ddlSehirler" runat="server" Width="253px" Height="30px"  CssClass="text-input"  /><br />
            
             <asp:RequiredFieldValidator ID="rfvSehir" runat="server" ControlToValidate="ddlSehirler" 
                        Display="Dynamic" 
                        ErrorMessage="Lütfen  Şehir Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue=" " SetFocusOnError="true" ToolTip="Şehir Seçiniz." 
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                  <label> Cinsiyetiniz:</label>  </td>
                <td>
                    <asp:RadioButtonList ID="rdbCinsiyet" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">Erkek</asp:ListItem>
                    <asp:ListItem Value="False">Kadın</asp:ListItem>
                    </asp:RadioButtonList>
                    
                    <asp:RequiredFieldValidator runat="server" ID="rfvCinsiyet" 
                        ControlToValidate="rdbCinsiyet" Display="Dynamic" 
                        ErrorMessage="Lütfen  Cinsiyetinizi Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        ValidationGroup="yeniKayit" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                 <label>Doğum Tarihi: </label>   </td>
                <td>
                    <asp:DropDownList ID="ddlGun" Height="30px"   CssClass="text-input" runat="server">
                    </asp:DropDownList>
                     
                    <asp:DropDownList ID="ddlAy" Height="30px"  CssClass="text-input" runat="server">
                    <asp:ListItem Value="0"> Ay </asp:ListItem>
                    <asp:ListItem Value="01"> Ocak </asp:ListItem>
                    <asp:ListItem Value="02"> Şubat </asp:ListItem>
                    <asp:ListItem Value="03"> Mart </asp:ListItem>
                    <asp:ListItem Value="04"> Nisan </asp:ListItem>
                    <asp:ListItem Value="05"> Mayıs </asp:ListItem>
                    <asp:ListItem Value="06"> Haziran  </asp:ListItem>
                    <asp:ListItem Value="07"> Temmuz </asp:ListItem>
                    <asp:ListItem Value="08"> Ağustos </asp:ListItem>
                    <asp:ListItem Value="09"> Eylül </asp:ListItem>
                    <asp:ListItem Value="10"> Ekim </asp:ListItem>
                    <asp:ListItem Value="11"> Kasım </asp:ListItem>
                    <asp:ListItem Value="12"> Aralık </asp:ListItem>                         
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlYil" Height="30px"   CssClass="text-input" runat="server">
                    </asp:DropDownList> <br />

                      <asp:RequiredFieldValidator ID="rfvGun" runat="server" 
                        ControlToValidate="ddlGun" Display="Dynamic" 
                        ErrorMessage="Lütfen Gün Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue="0" SetFocusOnError="true" ToolTip="Gün Seçiniz." 
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>

                      <asp:RequiredFieldValidator ID="rfvAy" runat="server" 
                        ControlToValidate="ddlAy" Display="Dynamic" 
                        ErrorMessage="Lütfen Ay Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue="0" SetFocusOnError="true" ToolTip="Ay Seçiniz." 
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>

                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="ddlYil" Display="Dynamic" 
                        ErrorMessage="Lütfen Yıl Seçiniz." 
                        CssClass="input-notification error" ForeColor=""
                        InitialValue="0" SetFocusOnError="true" ToolTip="Yıl Seçiniz." 
                        ValidationGroup="yeniKayit"></asp:RequiredFieldValidator>
                </td>
            </tr>
                <tr runat="server" id="rowPosta" visible="false" >
                <td >
                 <label>Mesaj Servisi:</label>
                </td>
                <td>
                   <asp:CheckBox ID="ckbEposta" Text="E-Posta almak istiyorum" runat="server" /> 
                   <asp:CheckBox ID="ckbSms" Text="SMS Almak İstiyorum" runat="server" />
               </td>
            </tr>
            <tr>
                <td>
              
                </td>
                <td>
                    <asp:Button ID="btnYoneticiEkle"  ValidationGroup="yeniKayit" Width="150px" 
                    runat="server" Text="Bilgileri Kaydet" onclick="btnYoneticiEkle_Click" CssClass="button" /> 
                        
                    <asp:Button ID="btnYoneticiGuncelle"  ValidationGroup="yeniKayit" Width="150px"
                    runat="server" Text="Bilgileri Kaydet"  Visible="false"  onclick="btnYoneticiGuncelle_Click" CssClass="button" />
                        
                    <br /> <br /> <br />
                </td>
               
            </tr>
            </table>
			
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>
</asp:Content>

