<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" EnableEventValidation="false" AutoEventWireup="True" Inherits="adminGununUrunleri" Title="Untitled Page" EnableViewState="true" Codebehind="gununUrunler.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="padding-bottom:10px; text-align:right;" >
    
    </div>

    <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor:s-resize;" > Günün Ürünü </h3>
		    <div class="clear"></div>

			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

			    <asp:Panel ID="pnlUrunArama" BorderStyle="none" GroupingText="&nbsp; Günün Ürünü Ekleme&nbsp; " Height="280px" Width="100%" runat="server">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
           
        <table>
            <tr>
                <td  colspan="3" style="padding-bottom:10px; padding-top:10px;" >
                   <asp:Label ID="lblHataMesaj" ForeColor="Red" Visible="false" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="220px" >
                <label>Ürün Id :</label>
                </td>
                <td width="310px">
                    <asp:TextBox ID="txtUrunId" runat="server" autocomplete="off" AutoPostBack="true"   
                        CssClass="text-input" ontextchanged="txtUrunId_TextChanged" ></asp:TextBox>

                    <cc1:FilteredTextBoxExtender ID="Filtered" runat="server" TargetControlID="txtUrunId"
                FilterType="Numbers"  />
                </td>
                <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                      ControlToValidate="txtUrunId" CssClass="input-notification error" 
                      Display="Dynamic" ErrorMessage="Ürün Id Seçiniz."  ForeColor=""
                      ValidationGroup="GununUrunu"></asp:RequiredFieldValidator>

               <asp:Label ID="lblUrunDurum" runat="server"  ></asp:Label>
               <br />
               <asp:Label ID="lblFiyat" runat="server"  ></asp:Label>

               </td>
            </tr>
            <tr>
              <td >
             <label>Gösterim Tarihi :</label>
              </td>
                <td>
                    <asp:TextBox ID="txtGunTarih" runat="server" AutoPostBack="true" 
                        CssClass="text-input" ontextchanged="txtGunTarih_TextChanged" ></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtGunTarih_TextBoxWatermarkExtender" 
                        runat="server" TargetControlID="txtGunTarih" WatermarkText="Tarihi Seçiniz..">
                    </cc1:TextBoxWatermarkExtender>
                    <cc1:CalendarExtender ID="txtGunTarih_CalendarExtender" runat="server" 
                        CssClass="cal_Theme1" Format="dd.MM.yyyy" TargetControlID="txtGunTarih">
                    </cc1:CalendarExtender>
                </td>
                <td>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                      ControlToValidate="txtGunTarih" CssClass="input-notification error" 
                      Display="Dynamic" ErrorMessage="Ürün Gösterim Tarihi Seçiniz."  ForeColor=""
                      ValidationGroup="GununUrunu"></asp:RequiredFieldValidator>

                 <asp:Label ID="lblTarih" runat="server" CssClass="input-notification error" 
                      Text="Label" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                <label>İndirim Yüzde :</label>
                </td>
                <td>
                <asp:TextBox ID="txtUrunYuzde" runat="server" AutoPostBack="true" CssClass="text-input" ontextchanged="txtUrunYuzde_TextChanged" ></asp:TextBox>
                
                </td>
                <td>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                       ControlToValidate="txtUrunYuzde" CssClass="input-notification error" 
                       Display="Dynamic" ErrorMessage="Ürün indirim alanını doldurunuz." ForeColor=""
                       ValidationGroup="GununUrunu" ></asp:RequiredFieldValidator>

                </td>
            </tr>
            <tr>
                <td width="350px">
                <label>Satış Fiyatı :</label>
                </td>
                <td>
                    <asp:TextBox ID="txtSatisFiyat" CssClass="text-input" Text="0" runat="server"></asp:TextBox>
                </td>
                <td>
                <asp:RequiredFieldValidator ID="rfvSatisFiyat" runat="server" 
                      ControlToValidate="txtSatisFiyat" CssClass="input-notification error" 
                      Display="Dynamic" ErrorMessage="Ürün satış fiyatı yazınız."  ForeColor=""
                      ValidationGroup="GununUrunu"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top">
                <label>Açıklama :</label>
                </td>
                <td>
                    <asp:TextBox ID="txtAciklama" TextMode="MultiLine" Width="300px" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td width="350px">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnGununUrunu" runat="server" CssClass="button" Height="25" 
                        onclick="btnGununUrunu_Click" Text="Günün Ürünü Ekle" 
                        ValidationGroup="GununUrunu" />
                </td>
                <td style=" width:200px; text-align:left;">
                    &nbsp;</td>
            </tr>
        </table>
        <asp:HiddenField ID="hdfGunUrunId" Value="0" runat="server" />
        <asp:HiddenField ID="hdfTarih" Value="1" runat="server" />
        <asp:HiddenField ID="hdfFiyat" Value="0"  runat="server"  />
        </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Panel>
   
    <asp:GridView ID="gvwGununUrunleri" runat="server" AutoGenerateColumns="false" CellPadding="4" ClientIDMode="Static" 
        ForeColor="#333333" GridLines="None" Width="100%" onrowcommand="GridView1_RowCommand" >
                       <Columns>
                           <asp:TemplateField HeaderText="Resim">
                                <ItemTemplate>
                               
                                <img src="../Products/Big/<%# DataBinder.Eval(Container, "DataItem.resimAdi") %>" width="80px" />
                                </ItemTemplate>
                            <HeaderStyle Width="90px" Font-Bold="true"  />
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Tarih">
                               <ItemTemplate>
                             <span class="secenek"> 
                             </span>
                             <%# BusinessLayer.DateFormat.TarihGun( DataBinder.Eval(Container, "DataItem.tarih").ToString())%>
                               </ItemTemplate>
                           <HeaderStyle Width="170" Font-Bold="true"  />
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="">
                               <ItemTemplate>
                            <span class="secenek">Adı </span><br />
                            <span class="secenek">Asıl Fiyat </span> <br />
                            <span class="secenek">İndirim Yüzde </span> <br />
                            <span class="secenek">Günün Fiyatı </span> <br />
                            
                           </ItemTemplate>
                           <HeaderStyle Width="100" Font-Bold="true"  />
                             </asp:TemplateField>
                           <asp:TemplateField HeaderText=" Ürün  Adı">
                               <ItemTemplate>
                           <span class="secenek">&nbsp;:&nbsp;</span>
                           <%# DataBinder.Eval(Container, "DataItem.urunAdi")%><br />

                           <span class="secenek">&nbsp;:&nbsp;</span> <%# Convert.ToDecimal(Eval("urunFiyat")).ToString("N") %> <%# DataBinder.Eval(Container, "DataItem.doviz")%> <br />
                           <span class="secenek">&nbsp;:&nbsp;</span> <%#Eval("IndirimYuzde")%> % <br />

                           <span class="secenek">&nbsp;:&nbsp;</span> <%# Convert.ToDecimal(Eval("satisFiyat")).ToString("N")%> <%# DataBinder.Eval(Container, "DataItem.doviz")%>

                               </ItemTemplate>
                               <HeaderStyle Width="270" Font-Bold="true"  />
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="Açıklama">
                               <ItemTemplate>
              <%# Eval("Aciklama") %>
                              </ItemTemplate>
                            <HeaderStyle Width="100" Font-Bold="true"  />
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="Stok">
                            <ItemTemplate>  
                                  
                         <%# BusinessLayer.GenelFonksiyonlar.StokDurum(Convert.ToInt32(DataBinder.Eval(Container, "DataItem.kiritikStok")), Convert.ToInt32(DataBinder.Eval(Container, "DataItem.urunStok")), DataBinder.Eval(Container, "DataItem.stokCins").ToString())%> 
                           
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle  Width="60" HorizontalAlign="Right" Font-Bold="true"  />
                          </asp:TemplateField> 
                           <asp:TemplateField HeaderText="Dur.">
                               <ItemTemplate>
                       <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                               </ItemTemplate>
                               <ItemStyle HorizontalAlign="Center" />
                               <HeaderStyle Width="20" Font-Bold="true"  />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Düz.">
                               <ItemTemplate>
                                   <a href='urun_ekle.aspx?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=duzenle' title=" <%# DataBinder.Eval(Container, "DataItem.urunAdi")%> Adlı Ürünü Düzenle">
                                    <img src="images/duzenle.gif" alt="Düzenle" border="0" /> </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20" Font-Bold="true"  />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Tarih Sil">
                               <ItemTemplate>
                          <asp:ImageButton  ToolTip="Günün Ürünü Tarih Sil" ID="btnGununUrunuSil" CommandName="gununUrunSil" ImageUrl="~/admin/images/sil.gif" CommandArgument='<%# DataBinder.Eval( Container, "DataItem.gunId") %>' runat="server" />
                               </ItemTemplate>
                               <HeaderStyle  Width="70" Font-Bold="true"  />
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

