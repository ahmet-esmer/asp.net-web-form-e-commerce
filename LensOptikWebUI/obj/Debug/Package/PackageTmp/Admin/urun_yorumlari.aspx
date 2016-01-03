<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="Admin_Urun_Yorumlari" EnableViewState="true" Codebehind="urun_yorumlari.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
    <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
			     <h3 style="cursor:s-resize;">Urun Yorumları</h3>
			     <div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content" >
		    <div style="display: block;" class="tab-content default-tab" id="tab1" >

	        <asp:Panel ID="pnlYorumArama" BorderStyle="none" GroupingText=" Ürün Yorumu Arama Formu " Height="160px" Width="100%" runat="server">
       
        <table style="width:100%" >
            <tr>
                <td style="width:280px;">
                 <asp:DropDownList ID="ddlArama" runat="server" CssClass="text-input" Width="250px" 
                        onselectedindexchanged="ddlArama_SelectedIndexChanged" AutoPostBack="true" >
                       <asp:ListItem Value="0">Degerlendirme Kıriterine Göre Ara</asp:ListItem>
                       <asp:ListItem Value="5">Mükemmel</asp:ListItem>
                       <asp:ListItem Value="4">Çok İyi</asp:ListItem>
                       <asp:ListItem Value="3">İyi</asp:ListItem>
                       <asp:ListItem Value="2">Fena Degil</asp:ListItem>
                       <asp:ListItem Value="1">Çok Kötü</asp:ListItem>
                 </asp:DropDownList>
                </td>
                <td>
                 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Ürüne Yapılan Yorumları  Ara...." TargetControlID="txt_urunId" runat="server">
                </cc1:TextBoxWatermarkExtender>
                 <asp:TextBox ID="txt_urunId" runat="server"  CssClass="text-input" Width="300px" AutoPostBack="true" ontextchanged="txt_urunAdi_TextChanged"></asp:TextBox> 
                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txt_urunId" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <cc1:TextBoxWatermarkExtender TargetControlID="txtTarih_1" WatermarkText="Başlanğıç Tarihi Seçiniz.." ID="TextBoxWatermarkExtender2" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                 <asp:TextBox ID="txtTarih_1" runat="server" CssClass="text-input" Width="170px"></asp:TextBox>  &nbsp; &nbsp; İle <br />
                    <asp:RequiredFieldValidator ValidationGroup="tarih" ID="RequiredFieldValidator2" CssClass="input-notification error" ForeColor="" ControlToValidate="txtTarih_1" runat="server" ErrorMessage="Başlanğıç Tarihi Seçiniz.."></asp:RequiredFieldValidator>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtTarih_1" Format="dd.MM.yyyy" >
                    </cc1:CalendarExtender>
                   </td>
                <td>
                     <cc1:TextBoxWatermarkExtender TargetControlID="txtTarih_2" WatermarkText="Bitiş Tarihi Seçiniz.." ID="TextBoxWatermarkExtender3" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                 <asp:TextBox ID="txtTarih_2" runat="server" CssClass="text-input" Width="170px"></asp:TextBox>
                 <asp:Button ID="btnYorumTarihAra" runat="server" 
                        onclick="btnYorumTarihAra_Click" CssClass="button" Width="130px" Height="23px" Text="Tarih Aralığı Arama" ValidationGroup="tarih" />
                 <br />
                    <asp:RequiredFieldValidator ValidationGroup="tarih" ID="RequiredFieldValidator3" CssClass="input-notification error" ForeColor="" ControlToValidate="txtTarih_2" runat="server" ErrorMessage="Bitiş Tarihi Seçiniz.."></asp:RequiredFieldValidator>
                 
                    <cc1:CalendarExtender ID="CalendarExtender2"  CssClass="cal_Theme1" runat="server" TargetControlID="txtTarih_2" Format="dd.MM.yyyy"  >
                    </cc1:CalendarExtender>
                    </td>
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>

            <asp:GridView ID="grwUrunYorumlari" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" 
                    Width="100%" DataKeyNames="id"  >
                      <Columns>
                          <asp:TemplateField HeaderText="ID">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.id")%>
                               </ItemTemplate>
                              <HeaderStyle  Width="50" Font-Bold="true" />
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="Yorum Yapan">
                               <ItemTemplate>
                                <%# BusinessLayer.GenelFonksiyonlar.ToTitleCase(Eval("adiSoyadi").ToString())%><br />
                              <div class="uyeIl"> 
                               <%# DataBinder.Eval(Container, "DataItem.sehir")%> 
                                </div>                         
                               </ItemTemplate>
                               <ItemStyle  VerticalAlign="Bottom" />
                               <HeaderStyle Width="250"  Font-Bold="true" />
                               </asp:TemplateField>  
                          <asp:TemplateField HeaderText="Degerlendirme">
                               <ItemTemplate>
                                <asp:Image ID="Image2" ImageUrl='<%#"~/images/icons/rating"+ DataBinder.Eval(Container, "DataItem.degerKiriteri")+".gif"%>' runat="server" />

                               </ItemTemplate>
                               <HeaderStyle Width="100" Font-Bold="true" />
                               
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="Ürün Adı">
                               <ItemTemplate>
                          <%# DataBinder.Eval(Container, "DataItem.urunAdi")%> 
                               </ItemTemplate>
                               <HeaderStyle Width="300" Font-Bold="true" />
                           
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="Eklenme Tarih">
                            <ItemTemplate>                           
                          <%#  BusinessLayer.DateFormat.Tarih(DataBinder.Eval(Container, "DataItem.tarih").ToString())%>
                            </ItemTemplate>
                            <HeaderStyle  Width="170" Font-Bold="true" />
                          </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Durum">
                               <ItemTemplate>
                   <a title='<%# Eval("urunAdi")%> <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(Eval("durum"))) %>' 
                href='?id=<%# Eval("id") %>&islem=durum&durum=<%# Convert.ToInt32(!Convert.ToBoolean(Eval("durum"))) %>&uyeId=<%# Eval("UyeId") %>'>
                <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(Eval("durum"))) %>
                   </a>
               
                               </ItemTemplate>
                               <ItemStyle HorizontalAlign="Center" />
                               <HeaderStyle Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="Düzenle">
                               <ItemTemplate>          
                          
                      <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&islem=duzenle' >
                      <img src="../Admin/images/duzenle.gif" alt="Sil"   style="border:none;"  /> 
                      </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                          <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>

                              <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&' onclick=" return silKontrol()" title=" <%# DataBinder.Eval(Container, "DataItem.urunAdi")%> Adlı Ürün Yorumunu Sil"><img src="images/sil.gif" alt="Sil"   style="border:none;"  /> </a>
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
			
         <asp:ImageButton ID="btnShow" runat="server" Style="display: none;" />
         <cc1:ModalPopupExtender ID="ModalPopupExtender" BackgroundCssClass="popUpBg" runat="server"  TargetControlID="btnShow" PopupControlID="pnlPopup" >
         </cc1:ModalPopupExtender>
         <asp:Panel ID="pnlPopup" runat="server" Style="display: none">
    <div id="facebox" > 
   <div class="popup">    
        <table>        
           <tbody>             
           <tr>              
              <td class="tl"></td>
              <td class="b"></td>
              <td class="tr"></td>           
              </tr>           
              <tr>            
              <td class="b"></td>    
              <td class="body">  
               <div class="content" style="display: block; ">
                <div id="messages" style="width:400px" >
                
           
                 <label>Yorumu Ekleyen: <asp:Label ID="lblYorumEkleyen" CssClass="secenek" Font-Size="12px" runat="server"></asp:Label> </label>
         

                <label> Ürün Adı:<asp:Label ID="lblUrunAdi" CssClass="secenek" Font-Size="12px"  runat="server"></asp:Label></label> 
                
             
                <p>
                 <label>Değerlendirmelendirme Kıriteri: </label>
                  <asp:DropDownList ID="ddlDegerlendirme" runat="server" Width="400px" CssClass="text-input">
                       <asp:ListItem Value="0">Ürün Degerlendirme Kıriteri Seçiniz</asp:ListItem>
                       <asp:ListItem Value="5">Mükemmel</asp:ListItem>
                       <asp:ListItem Value="4">Çok İyi</asp:ListItem>
                       <asp:ListItem Value="3">İyi</asp:ListItem>
                       <asp:ListItem Value="2">Fena Degil</asp:ListItem>
                       <asp:ListItem Value="1">Çok Kötü</asp:ListItem>
                   </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                         ControlToValidate="ddlDegerlendirme"  CssClass="input-notification error"
                         ErrorMessage="Degerlendirme Kıriteri Şeçmelisiniz." Font-Size="12px" 
                         InitialValue="0" ForeColor="" ></asp:RequiredFieldValidator>
                </p>
                <p>
                 <label>Ürün Yorumu:</label>
                  <asp:TextBox ID="txtUrunYorum" runat="server" CssClass="form" Rows="5"  TextMode="MultiLine" Width="400px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUrunYorum" CssClass="input-notification error" ErrorMessage="Form İçerigi Giriniz." ForeColor="" ></asp:RequiredFieldValidator>

                </p>
                <p>
                 <asp:HiddenField ID="hdYorumId" runat="server" />
                 <asp:Button ID="btnYorumGuncelle" runat="server" CssClass="button" 
                                       Text="Yorumu Düzenle" onclick="btnYorumGuncelle_Click" />
                </p>
        
    
                </div>
                </div> 
                 <div class="footer" style="display: block; ">     
                   
                   <asp:ImageButton ID="imgKapat" ImageUrl="images/closelabel.gif" 
                   CausesValidation="False"  CssClass="close_image" runat="server" />

                  </div>               
                 </td>              
                 <td class="b"></td>         
                </tr>        
              <tr> 
                  <td class="bl"></td>
                  <td class="b"></td>
                  <td class="br"></td>        
              </tr>        
             </tbody>        
        </table>    
   </div>
</div>
                </asp:Panel>
                       
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>
 
    
            <div class="pagination">
                <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
            </div>  

    
    
</asp:Content>

