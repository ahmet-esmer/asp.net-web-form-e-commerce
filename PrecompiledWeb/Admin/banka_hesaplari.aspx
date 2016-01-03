<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="bankaHesaplari" Title="Untitled Page" Codebehind="banka_hesaplari.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">
            Banka Hesapları
         </h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a id="link1" href="#tab1" class="default-tab current">Hesaplar</a></li> 
			<li><a id="link2"  href="#tab2">Banka Hesabı Ekle</a></li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

       <asp:GridView ID="gvwBankaHesap" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" Width="100%">
                       <Columns>
                           <asp:TemplateField HeaderText="Sıra">
                               <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                               <HeaderStyle  Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Banka Adı">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.bankaAdi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="170" Font-Bold="true" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Şube">
                               <ItemTemplate>
                                 <%#DataBinder.Eval(Container, "DataItem.sube")%>
                               </ItemTemplate>
                               <HeaderStyle Width="150" Font-Bold="true" />
                           </asp:TemplateField>
                             <asp:TemplateField HeaderText="Hesap Adı">
                               <ItemTemplate>
                                 <%#DataBinder.Eval(Container, "DataItem.hesapAdi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="290"  Font-Bold="true"/>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Hesap No">
                               <ItemTemplate>
                                 <%#DataBinder.Eval(Container, "DataItem.hesapNo")%>
                               </ItemTemplate>
                               <HeaderStyle Width="100" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="İban">
                               <ItemTemplate>
                                 <%#DataBinder.Eval(Container, "DataItem.iban")%>
                               </ItemTemplate>
                               <HeaderStyle Width="100" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Dur.">
                               <ItemTemplate>
                                   <a title=" <%# DataBinder.Eval(Container, "DataItem.bankaAdi")%>  Hesabını <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&amp;islem=durum&amp;durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container, "DataItem.durum")))%>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="duzen" />
                               <HeaderStyle Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Düz.">
                               <ItemTemplate>
                                   <a href='banka_hesaplari.aspx?bankaAdi=<%# DataBinder.Eval( Container, "DataItem.bankaAdi") %>&islem=duzenle&id=<%# DataBinder.Eval(Container, "DataItem.id")%>' title=" <%# DataBinder.Eval(Container, "DataItem.bankaAdi")%> Hesabını Düzenle">
                                    <img src="images/duzenle.gif" alt="Düzenle" border="0" /> </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20"  Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&' 
                                       onclick=" return silKontrol()" title=" <%# DataBinder.Eval(Container, "DataItem.bankaAdi")%>  Hesabını Sil"><img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <HeaderStyle  Width="30" Font-Bold="true" />
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
					
        <table align="center" cellpadding="0" cellspacing="0" style="height:auto; width:100%; padding-bottom:20px; padding-top:15px;">
            <tr>
                <td class="txtLabelTd" style="width:150px;">
                    <label>Banka Adı:</label>
                </td>
                <td style="width: 414px">
                    <asp:TextBox ID="txtBankaAdi" runat="server" CssClass="text-input medium-input"></asp:TextBox>
               </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtBankaAdi" CssClass="input-notification error" 
                        ErrorMessage="Lütfen Banka Adı Yazınız." ForeColor="" ></asp:RequiredFieldValidator>
                </td>
            </tr>
               <tr>
                <td class="txtLabelTd">
               <label>Şube:</label>   
                </td>
                <td>
                    <asp:TextBox ID="txtSube" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
               </td>
                   <td>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                           ControlToValidate="txtSube" CssClass="input-notification error" 
                           ErrorMessage="Lütfen Şube Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
                   </td>
            </tr>
                <tr>
                <td class="txtLabelTd">
                  <label>Şube Kodu:</label>
                </td>
                <td>
                    <asp:TextBox ID="txSubeKodu" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
               </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txSubeKodu" CssClass="input-notification error" 
                            ErrorMessage="Lütfen Şube Kodu Alanını Doldurunuz." ForeColor=""></asp:RequiredFieldValidator>
                    </td>
            </tr>
                <tr>
                <td class="txtLabelTd">
                 <label>Hesap No:</label>
                 </td>
                <td>
                 <asp:TextBox ID="txtHesapNo" runat="server" CssClass="text-input medium-input"> </asp:TextBox>
                </td>
                <td>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtHesapNo" CssClass="input-notification error"  
                            ErrorMessage="Lütfen Hesap Numarası Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
                </td>
            </tr>
                   <tr>
                <td class="txtLabelTd">
                 <label>Iban:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtIban" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
               </td>
                       <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                               ControlToValidate="txtIban" CssClass="input-notification error"  ErrorMessage="Lütfen İban Alanını Doldurunuz." ForeColor=""></asp:RequiredFieldValidator>
                       </td>
            </tr>
                     <tr>
                <td class="txtLabelTd">
                 <label>Hesap Adı:</label>
                </td>
                <td>
                    <asp:TextBox ID="txtHesapAdi" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
               </td>
                         <td>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                 ControlToValidate="txtHesapAdi" CssClass="input-notification error" 
                                 ErrorMessage="Lütfen Hesap Adı Alanını Doldurunuz." ForeColor=""></asp:RequiredFieldValidator>
                         </td>
            </tr>
            
                          <tr>
                <td class="txtLabelTd">
                  <label>Hesap Tipi:</label>
                </td>
                <td>
                   <asp:DropDownList ID="ddlHesapTipi" CssClass="form" Width="100px" runat="server">
                    <asp:ListItem Value="TL"  Text="TL" Selected="True" ></asp:ListItem>
                    <asp:ListItem Value="USD" Text="USD" ></asp:ListItem>
                    <asp:ListItem Value="EUR" Text="EUR" ></asp:ListItem>
                    </asp:DropDownList>
                 
               </td>
                  <td>
                &nbsp;</td>
            </tr> 
            <tr>
                <td class="txtLabelTd" valign="top">
                 <label>Durum:</label>
                </td>
                <td colspan="2">
                    <asp:CheckBox ID="ckbDurum" Checked="true" runat="server" />
               </td>
            </tr>
            <tr>
                <td class="txtLabelTd">
                    &nbsp;</td>
                <td colspan="2">
                    <asp:Button ID="btnBankaHesapEkle" runat="server" CssClass="button" 
                        Font-Size="12px"   Width="170px" 
                        Text="Banka Hesabı Kaydet" onclick="btnBankaHesapEkle_Click"   ></asp:Button>
                        
                              <asp:Button ID="btnHesapDuzenle" runat="server" CssClass="button" 
                        Font-Size="12px"   Width="170px" 
                        Text="Banka Hesabı Düzenle" Visible="false" onclick="Button1_Click"   ></asp:Button>
                </td>
             
            </tr>
        </table>
					
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>
</asp:Content>

