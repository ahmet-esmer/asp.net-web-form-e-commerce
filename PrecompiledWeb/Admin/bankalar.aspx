<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="AdminBankalar" Title="Untitled Page" Codebehind="bankalar.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <asp:ImageButton ID="btnShow" runat="server" Style="display:none;" />
       
   <cc1:ModalPopupExtender ID="ModalPopupExtender" 
        BackgroundCssClass="popUpBg" runat="server"  TargetControlID="btnShow" PopupControlID="pnlPopup" >
         </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlPopup"  runat="server"  Style="display: none">
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
              <td class="body" style="width:400px" >  
               <div class="content" style="display: block; ">
         
        <label>
        <asp:Label ID="lblBankaAdi" CssClass="secenek" Font-Size="12px" runat="server" ></asp:Label> Sanalpos Ayarları</label>
                
             <p>
             <label> Banka Kodu </label>
             <asp:TextBox ID="txtBankaKod" Width="200px" CssClass="text-input small"  runat="server"></asp:TextBox>
             </p>    
             <p>
             <label> Mağaza numarası </label>
             <asp:TextBox ID="txtMagaza" Width="200px" CssClass="text-input small"  runat="server"></asp:TextBox>
             </p>
             <p>
              <label>Api kullanıcı adı </label>
              <asp:TextBox ID="txtApiKulanici" CssClass="text-input small"  runat="server"></asp:TextBox>
             </p>
             <p>
             <label>Api kullanıcı şifresi </label>
             <asp:TextBox ID="txtApiSifre" Width="200px" CssClass="text-input small"  MaxLength="15"  runat="server"></asp:TextBox>
             </p>
              <p>
               <label>Sanalpos tipi </label>
               <asp:DropDownList ID="ddlSanalposTipi" runat="server" CssClass="text-input small"  >
                    <asp:ListItem Value="Standart" Text="Standart"></asp:ListItem>
                    <asp:ListItem Value="3D Secure" Text="3D Secure"></asp:ListItem>
                    </asp:DropDownList>
              </p>
               <p>
               <label>Hedef Sunucu Adresi:</label>
               <asp:TextBox ID="txtHedfSunucu" CssClass="text-input" Width="400px" runat="server"></asp:TextBox>
                <asp:HiddenField ID="hdfbankaSanalpos" runat="server" />
               </p>
               <p>
               <asp:Button ID="btnSanalPos" runat="server" Text="Sanalpos Düzenle" 
                        onclick="btnSanalPos_Click" CssClass="button" />
               </p>
            
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

 <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;"> Sanal Poslar</h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a id="link1" href="#tab1" class="default-tab current">Bankalar</a></li> 
			<li><a id="link2" href="#tab2">Banka Ekle </a></li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

	    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" Width="100%">
                       <Columns>
                           <asp:TemplateField HeaderText="Banka Adı">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.BankaAdi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="400" Font-Bold="true" />
                           </asp:TemplateField>

                               <asp:TemplateField HeaderText="Sıra">
                               <ItemTemplate>
                              
                               <asp:TextBox ID="txt_Sira" 
                                     Text='<%# DataBinder.Eval(Container, "DataItem.Sira") %>'
                                     CssClass="text-input"  
                                     autocomplete="off"
                                     MaxLength="1"
                                     ToolTip='<%# DataBinder.Eval(Container, "DataItem.Id") %>' 
                                     ontextchanged="bankaSira" 
                                     AutoPostBack="true" 
                                     Width="60px"
                                     runat="server"></asp:TextBox>

                                           <cc1:FilteredTextBoxExtender ID="filtle" runat="server" TargetControlID="txt_Sira" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>

                                   <asp:HiddenField ID="hdfBankaId" Value='<%# DataBinder.Eval(Container, "DataItem.Id") %>' runat="server" /> 

                                        <asp:HiddenField ID="hdfSira" Value='<%# DataBinder.Eval(Container, "DataItem.Sira") %>' runat="server" /> 

                               </ItemTemplate>
                               <ItemStyle />
                               <HeaderStyle Width="60" Font-Bold="true" />
                           </asp:TemplateField>


                             <asp:TemplateField HeaderText="Varsayılan">
                               <ItemTemplate>
                              
                               <asp:DropDownList ID="dlVarsayilanBanka" SelectedValue='<%# Eval("Varsayilan") %>'  runat="server" OnSelectedIndexChanged="dlVarsayilanBanka" AutoPostBack="True"  ToolTip='<%# Eval("Id") %>'  CssClass="text-input"  Width="70px" >
                                   <asp:ListItem Value="True" Text="Evet" ></asp:ListItem>
                                   <asp:ListItem Value="False" Text="Hayır" ></asp:ListItem>

                                   </asp:DropDownList>

                               </ItemTemplate>
                               <ItemStyle />
                               <HeaderStyle Width="40" Font-Bold="true" />
                               <HeaderStyle Width="80"  />
                           </asp:TemplateField>


                              <asp:TemplateField HeaderText="Sanalpos">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.Id") %>&amp;islem=sanalpos&ad=<%# DataBinder.Eval(Container, "DataItem.bankaAdi")%> ' >
                                    Sanalpos </a>
                               </ItemTemplate>
                               <ItemStyle />
                               <HeaderStyle Width="80"  Font-Bold="true" />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Taksitler">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.Id") %>&amp;islem=duzenle&ad=<%# DataBinder.Eval(Container, "DataItem.bankaAdi")%> &resim=<%# DataBinder.Eval( Container, "DataItem.BaslikResmi") %>' >
                                    Taksitler </a>
                               </ItemTemplate>
                               <ItemStyle />
                               <HeaderStyle Width="80" Font-Bold="true" />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Durum">
                               <ItemTemplate>
                                   <a title=" <%# DataBinder.Eval(Container, "DataItem.BankaAdi")%>  Anketi  <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.Durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.Id") %>&amp;islem=durum&amp;durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.Durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container, "DataItem.Durum")))%>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle  />
                               <HeaderStyle Width="80" Font-Bold="true" />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.Id") %>&amp;islem=sil&resim=<%# DataBinder.Eval( Container, "DataItem.BaslikResmi") %>' 
                                       onclick=" return silKontrol()" title=" <%# DataBinder.Eval(Container, "DataItem.BankaAdi")%>  Anketi Sil"><img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <HeaderStyle  Width="80" Font-Bold="true" />
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

        <asp:Panel ID="pnlBankaEkle" runat="server">
   
        <table align="center" cellpadding="0" cellspacing="0" style="height:auto; width:100%; padding-bottom:20px; padding-top:15px;">
           
                  <tr>
                <td class="txtLabelTd" style="width:150px;">
                   
                </td>
                <td style="width: 414px" valign="top">
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtAnketAdi"  CssClass="input-notification error"   Display="Dynamic" ValidationGroup="bankaAdi" ErrorMessage="Lütfen Banka Adı Giriniz.." ForeColor=""></asp:RequiredFieldValidator><br />
               </td>
                <td>
                </td>
            </tr> <tr>
                <td  valign="top"  style="padding-top:8px; height:30px;">
                 <label>Banka Adı: </label>
                </td>
                <td valign="top"  >
                    <asp:TextBox ID="txtAnketAdi" ToolTip="Anket Başlık Düenle." runat="server" CssClass="text-input small"  ></asp:TextBox>
                        
               </td>
                <td valign="top" >
                    &nbsp;</td>
            </tr>
                  <tr>
                      <td style="padding-top:8px; height:30px;" valign="top">
                        <label> Başlık Resmi:</label>
                      </td>
                      <td valign="top">
                      <asp:FileUpload ID="txtresim" runat="server" CssClass="text-input small" /><br />
                      <small>Boyutu genişlik: 254px yükseklik: 36px </small>
                      </td>
                      <td valign="top">
                         
                      </td>
                  </tr>
               <tr>
                <td class="txtLabelTd">
                    &nbsp;</td>
                <td colspan="2">
                     <asp:Button ID="btnBankaAdiEkle" runat="server" CssClass="button" 
                              Font-Size="12px" onclick="btnBankaAdiEkle_Click" Text="Banka Kaydet" 
                              ValidationGroup="bankaAdi" Width="172px" />
               </td>
            </tr>
        </table>
  </asp:Panel>

      <asp:Panel ID="pnlBankaDuzenle" Visible="false" runat="server">

        <table align="center" cellpadding="0" cellspacing="0" style="height:auto; width:100%; padding-bottom:20px; padding-top:15px;">
                <tr>
                <td class="txtLabelTd" style="width:140px;">
                    <span id="Span1">Banka Adı:</span>
                </td>
                <td colspan="2" >
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtBankaDuzenle"  CssClass="input-notification error"  Display="Dynamic" ValidationGroup="anketBaslik" ErrorMessage="Lütfen Banka Adı Yazınız." ForeColor=""  ></asp:RequiredFieldValidator><br />
                    <asp:TextBox ID="txtBankaDuzenle" runat="server"  CssClass="text-input small"  ></asp:TextBox>
                  
               </td>
                <td>
                 <asp:HiddenField ID="hdfBankaId" runat="server" />
                 <asp:HiddenField ID="hdResimAdi" runat="server" />
              </td>
            </tr>
          
               <tr>
                   <td class="txtLabelTd" style="width:140px;">
                       Başlık Resmi:</td>
                   <td>
                       <asp:FileUpload ID="txtResimDuz" runat="server" CssClass="text-input small" /><br />
                     <small>Resim Boyutu Genişlik: 254px Yükseklik: 36px </small>
                   </td>
                   <td >
                       <asp:Image ID="imgBankaBaslik" Width="170px" runat="server" />
                   </td>
                   <td>
                     
                   </td>
            </tr>
                 <tr>
                   <td class="txtLabelTd" style="width:140px;">
                      </td>
                   <td colspan="3" >
                   <asp:Button ID="btnBankaGuncelle" runat="server" CssClass="button" 
                           Font-Size="12px" Text="Banka Bilgisi Güncelle" 
                           Width="172px" onclick="btnBankaGuncelle_Click" />
                   </td>
            </tr>
            </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
          
            <table align="center" cellpadding="0" cellspacing="0" style="height:auto; width:100%; padding-bottom:20px; padding-top:15px;">
               <tr>
                <td class="txtLabelTd" colspan="4">
                
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CellPadding="4"  ForeColor="#333333" GridLines="None" Width="100%"
                        onrowcommand="GridView2_RowCommand"  DataKeyNames="id"     >
                       <Columns>
                           <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                              <label>Taksit:</label> 
                               </ItemTemplate>
                            <ItemStyle  Width="115px" CssClass="anketGrig" />
                           </asp:TemplateField>
                           <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                                 <asp:TextBox ID="txtTaksitDuzenle" runat="server" Width="100px" CssClass="text-input" Text='<%# Eval("taksit")%>' ToolTip="Anket Soru Düzenle" ></asp:TextBox>
                               </ItemTemplate> 
                               <ItemStyle Width="115px" CssClass="anketGrig" />                     
                           </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                               Vade % &nbsp;
                                 <asp:TextBox ID="txtVadeFarki" runat="server" Width="100px" CssClass="text-input"   Text='<%# Eval("vadeFarki")%>' ToolTip="Taksit Düzenle" ></asp:TextBox>
                               </ItemTemplate> 
                               <ItemStyle Width="150px" CssClass="anketGrig" />                     
                           </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                                   <asp:ImageButton  ToolTip="Taksit Degişikligini Kaydet" ID="btnTaksitKaydet" ImageUrl="~/admin/images/kaydet.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id") %>' runat="server" CommandName="taksitKaydet" />
                               </ItemTemplate> 
                               <ItemStyle Width="40px" CssClass="anketGrig" />  
                           </asp:TemplateField>
                          
                           <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&taksit=sil&' 
                  onclick=" return silKontrol()"><img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <ItemStyle Width="450px" CssClass="anketGrig" />  
                           </asp:TemplateField>
                       
                       </Columns>
             
                   </asp:GridView>
            </td>
            </tr>
            <tr>
                <td class="txtLabelTd" style="width:170px" valign="top">
                  <label> Kredi Kart Taksit:</label>
               </td>
                <td style="width:170px" valign="top">
                    <asp:TextBox ID="txtTaksit" runat="server" CssClass="text-input"
                        Width="100px"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtTaksit"  CssClass="input-notification error"  Display="Dynamic" ErrorMessage="Lütfen Taksit Giriniz."  ForeColor=""
                        ValidationGroup="bankaTaksit"></asp:RequiredFieldValidator>
                </td>
                <td style="width:170px" valign="top">
                Vade % &nbsp;
                    <asp:TextBox ID="txtVadeFarki" runat="server" CssClass="text-input"
                        Width="100px"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtVadeFarki"  CssClass="input-notification error"  Display="Dynamic" ErrorMessage="Lütfen Vade Farkı Giriniz."  ForeColor=""
                        ValidationGroup="bankaTaksit"></asp:RequiredFieldValidator>
                </td>
                <td valign="top">
                    <asp:Button ID="btnTaksitEkle" runat="server" CssClass="button" 
                        Font-Size="12px" onclick="btnTaksitEkle_Click" Text="Taksit Ekle" 
                        ValidationGroup="bankaTaksit" Width="172px" />
                </td>
            </tr>
            <tr>
                <td class="txtLabelTd">
                    &nbsp;</td>
                <td colspan="3">
                </td>
            </tr>
         </table>

        </ContentTemplate>
        </asp:UpdatePanel>
   </asp:Panel>

       
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>

 
</asp:Content>





