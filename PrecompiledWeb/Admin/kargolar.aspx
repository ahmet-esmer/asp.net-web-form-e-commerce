<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="AdminKargolar"  Codebehind="kargolar.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">
            <asp:Label ID="lblSayfaBaslik" runat="server" ></asp:Label>
         </h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a id="link1" href="#tab1" class="default-tab current">Kargolar</a></li> 
			<li><a id="link2" href="#tab2">Yeni Kargo Ekle</a></li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">


  <asp:GridView ID="gvwKargolar" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" Width="100%">
                       <Columns>
                         
                           <asp:TemplateField HeaderText="Kargo Adı">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.kargoAdi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="150" Font-Bold="true" />
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="Kapıda Ödeme">
                               <ItemTemplate>
                                 <%# Eval("kapidaOdeme").ToString() == "True" ? "Evet  " : "Hayır "%>
                             
                               </ItemTemplate>
                               <HeaderStyle Width="70" Font-Bold="true" />
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="Kapıda Ödeme farkı">
                               <ItemTemplate>
                             <%# string.Format("{0:C}", DataBinder.Eval(Container, "DataItem.kapidaOdemeFark"))%>
                               </ItemTemplate>
                               <HeaderStyle Width="70" Font-Bold="true" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="1-3">
                               <ItemTemplate>
                                 <%# string.Format("{0:C}",DataBinder.Eval(Container, "DataItem.desi_1_3"))%>
                               </ItemTemplate>
                               <HeaderStyle Width="60" Font-Bold="true" />
                           </asp:TemplateField>
                             <asp:TemplateField HeaderText="4-10">
                               <ItemTemplate>
                                 <%# string.Format("{0:C}", DataBinder.Eval(Container, "DataItem.desi_4_10"))%>
                               </ItemTemplate>
                               <HeaderStyle Width="60" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="11-20">
                               <ItemTemplate>
                                 <%# string.Format("{0:C}",DataBinder.Eval(Container, "DataItem.desi_11_20"))%>
                               </ItemTemplate>
                               <HeaderStyle Width="60" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="21-30">
                               <ItemTemplate>
                                 <%# string.Format("{0:C}",DataBinder.Eval(Container, "DataItem.desi_21_30"))%>
                               </ItemTemplate>
                               <HeaderStyle Width="60" Font-Bold="true" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="31-40">
                               <ItemTemplate>
                                 <%# string.Format("{0:C}",DataBinder.Eval(Container, "DataItem.desi_31_40"))%>
                               </ItemTemplate>
                               <HeaderStyle Width="60" Font-Bold="true" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="41-50">
                               <ItemTemplate>
                                 <%# string.Format("{0:C}",DataBinder.Eval(Container, "DataItem.desi_41_50"))%>
                               </ItemTemplate>
                               <HeaderStyle Width="60" Font-Bold="true" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="50" >
                               <ItemTemplate>
                                 <%# string.Format("{0:C}",DataBinder.Eval(Container, "DataItem.desi_50"))%>
                               </ItemTemplate>
                               <HeaderStyle Width="60" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Dur.">
                               <ItemTemplate>
                                   <a title=" <%# DataBinder.Eval(Container, "DataItem.kargoAdi")%>  Hesabını <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&amp;islem=durum&amp;durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="duzen" />
                               <HeaderStyle Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Düz.">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=duzenle' title=" <%# DataBinder.Eval(Container, "DataItem.kargoAdi")%> Hesabını Düzenle">
                                    <img src="images/duzenle.gif" alt="Düzenle" border="0" /> </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&' 
                                       onclick=" return silKontrol()" title=" <%# DataBinder.Eval(Container, "DataItem.kargoAdi")%>  Hesabını Sil"><img src="images/sil.gif" border="0"  /> </a>
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
                   <label>Kargo Adı: </label>
                </td>
                <td style="width: 250px">
                    <asp:TextBox ID="txtKargoAdi" runat="server"  CssClass="text-input small" ></asp:TextBox>
               </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtKargoAdi" CssClass="input-notification error" 
                        ErrorMessage="Lütfen Kargo Adı Yazınız." ForeColor="" ></asp:RequiredFieldValidator>
                </td>
            </tr>
               <tr>
                <td class="txtLabelTd">
                 <label>Kapida Ödeme:</label>   
                  </td>
                <td>
                    <asp:DropDownList ID="ddlKapidaOdeme" Width="170px"  CssClass="text-input small" runat="server">
                    <asp:ListItem Text="Hayır" Value="False" ></asp:ListItem>
                    <asp:ListItem Text="Evet" Value="True"></asp:ListItem>
                    </asp:DropDownList>
               </td>
                   <td>
                       &nbsp;</td>
            </tr>
              <tr>
                <td class="txtLabelTd">
                 <label>Kapida Ödeme Farkı:</label>   
                  </td>
                <td>
                  <asp:TextBox ID="txtOdemeFarki" runat="server"  CssClass="text-input small" ></asp:TextBox>  
                  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="txtOdemeFarki" CssClass="input-notification error" 
                        ErrorMessage="Lütfen kargo kapıda ödeme miktarı yazınız." ForeColor="" ></asp:RequiredFieldValidator>
                         
               </td>
                   <td>
                       &nbsp;</td>
            </tr>

                <tr>
                    <td class="txtLabelTd">
                       <label> Desi 1-3: </label> 
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesi_1_3" runat="server" CssClass="text-input small" Width="170px"></asp:TextBox>
                        <cc1:FilteredTextBoxExtender ID="Filtered1" runat="server" 
                            FilterType="Custom, Numbers" TargetControlID="txtDesi_1_3" ValidChars="," />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtDesi_1_3" CssClass="input-notification error" 
                            ErrorMessage="Lütfen Desi Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
                    </td>
            </tr>
                <tr>
                <td class="txtLabelTd">
               <label>Desi  4-10:</label> 
                </td>
                <td>
                    <asp:TextBox ID="txtDesi_4_10" runat="server" Width="170px" CssClass="text-input small"  ></asp:TextBox>
                           <cc1:FilteredTextBoxExtender ID="Filtered2" runat="server" TargetControlID="txtDesi_4_10"
                FilterType="Custom, Numbers" ValidChars="," />
               </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtDesi_4_10" CssClass="input-notification error" 
                            ErrorMessage="Lütfen Desi Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
                    </td>
            </tr>
                <tr>
                <td class="txtLabelTd">
                <label>Desi  11-20: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesi_11_20" runat="server" Width="170px" CssClass="text-input small"  ></asp:TextBox>
               </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtDesi_11_20" CssClass="input-notification error"
                            ErrorMessage="Lütfen Desi Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="Filtered3" runat="server" TargetControlID="txtDesi_11_20"
                FilterType="Custom, Numbers" ValidChars="," />
                    </td>
            </tr>
                   <tr>
                <td class="txtLabelTd">
                    <label>Desi 21-30: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesi_21_30" runat="server" Width="170px" CssClass="text-input small" ></asp:TextBox>
               </td>
                       <td>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                               ControlToValidate="txtDesi_21_30" CssClass="input-notification error"
                               ErrorMessage="Lütfen Desi Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="Filtered4" runat="server" TargetControlID="txtDesi_21_30"
                FilterType="Custom, Numbers" ValidChars="," />
                       </td>
            </tr>
                     <tr>
                <td class="txtLabelTd">
                 <label>Desi  31-40: </label>
                </td>
                <td>
                    <asp:TextBox ID="txtDesi_31_40" runat="server" Width="170px" CssClass="text-input small" ></asp:TextBox>
               </td>
                         <td>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                 ControlToValidate="txtDesi_31_40" CssClass="input-notification error" 
                                 ErrorMessage="Lütfen Desi Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
                                  <cc1:FilteredTextBoxExtender ID="Filtered5" runat="server" TargetControlID="txtDesi_31_40"
                FilterType="Custom, Numbers" ValidChars="," />
                         </td>
            </tr>
            
                          <tr>
                <td class="txtLabelTd">
                <label> Desi   41-50:</label>
                </td>
                <td>
                  <asp:TextBox ID="txtDesi_41_50" runat="server" Width="170px" CssClass="text-input small" ></asp:TextBox>
                 
               </td>
                              <td>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                      ControlToValidate="txtDesi_41_50" CssClass="input-notification error" 
                                      ErrorMessage="Lütfen Desi Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
                                       <cc1:FilteredTextBoxExtender ID="Filtered6" runat="server" TargetControlID="txtDesi_41_50"
                FilterType="Custom, Numbers" ValidChars="," />
                              </td>
            </tr> 
            <tr>
                <td class="txtLabelTd">
                <label>Desi  50 : </label>  </td>
                <td>
                    <asp:TextBox ID="txtDesi_50" runat="server" Width="170px" CssClass="text-input small" ></asp:TextBox></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                        ControlToValidate="txtDesi_50" CssClass="input-notification error" 
                        ErrorMessage="Lütfen Desi Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
                         <cc1:FilteredTextBoxExtender ID="Filtered7" runat="server" TargetControlID="txtDesi_50"
                FilterType="Custom, Numbers" ValidChars="," />
                </td>
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
                    <asp:Button ID="btnKargoEkle" runat="server" CssClass="button" 
                        Font-Size="12px"   Width="170px"   Text="Kargo  Kaydet" 
                        onclick="btnKargoEkle_Click"   ></asp:Button>
                        
                        <asp:Button ID="btnKargoDuzenle" runat="server" CssClass="button" 
                        Font-Size="12px"   Width="170px"  Text="Kargo Bilgisi Düzenle" 
                        Visible="false" onclick="btnKargoDuzenle_Click"   ></asp:Button>
                </td>
             
            </tr>
        </table>
						
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>
     
</asp:Content>

