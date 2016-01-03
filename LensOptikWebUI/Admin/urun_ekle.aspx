<%@ Page Language="C#" MasterPageFile="~/Admin/admin.master" AutoEventWireup="true"  ValidateRequest="false" Inherits="AdminUrunEkle"  EnableEventValidation="true"  Codebehind="urun_ekle.aspx.cs" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">
    <script language="javascript" type="text/javascript" >
        function kapat() {
            window.close()
        }

    function moveOver_deger() {
        var src = document.getElementById('secenek_ismi');
        var dest = document.getElementById('secenek_ismi2');

        for (var count = 0; count < src.options.length; count++) {

        var isNew = true;

            if (src.options[count].selected == true) {
                var option = src.options[count];

                var newOption = document.createElement("option");
                newOption.value = option.value;
                newOption.text = option.text;

                try {
                    dest.add(newOption, null); //Standard
                    src.options[count].selected = false;
                  
                } catch (error) {
                    dest.add(newOption); // IE only
                    src.options[count].selected = false;
                }
                count--;
            }
        }
    }

    function removeMe_deger() {
        var boxLength = document.getElementById('secenek_ismi2').length;
        arrSelected = new Array();
        var count = 0;
        for (i = 0; i < boxLength; i++) {
            if (document.getElementById('secenek_ismi2').options[i].selected) {
                arrSelected[count] = document.getElementById('secenek_ismi2').options[i].value;
            }
            count++;
        }
        var x;
        for (i = 0; i < boxLength; i++) {
            for (x = 0; x < arrSelected.length; x++) {
                if (document.getElementById('secenek_ismi2').options[i].value == arrSelected[x]) {
                    document.getElementById('secenek_ismi2').options[i] = null;
                }
            }
            boxLength = document.getElementById('secenek_ismi2').length;
        }
    }

    function clearAllOptions() {
        if (confirm('Seçenekler silinsinmi?')) {
            var oSelect = document.getElementById('secenek_ismi2');
            for (i = oSelect.options.length - 1; i >= 0; i--) {
                oSelect.options[i] = null;

            }
        }
    }

    function saveMe_deger() {
        var strValues = "";
        var boxLength = document.getElementById('secenek_ismi2').length;
        var count = 0;
        if (boxLength != 0) {
            for (i = 0; i < boxLength; i++) {
                if (count == 0) {
                    strValues = document.getElementById('secenek_ismi2').options[i].text;
                } else {
                    strValues = strValues + "|" + document.getElementById('secenek_ismi2').options[i].text;
                }
                count++;
            }
        }
        if (strValues.length == 0) {
            document.getElementById('hdfSecenekler').value = ""
        } else {
            document.getElementById('hdfSecenekler').value = strValues
        }
    }

</script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="content-box"><!-- Start Content Box -->
			<div class="content-box-header">
			 <h3 style="cursor: s-resize;">Sayfa İçerik Düzenle</h3>
			 <ul style="display: block;" class="content-box-tabs">
				<li><a href="#tab1" id="link1" class="default-tab current">Genel Özellikler</a> </li>
                <li><a href="#tab2" id="link2" >Ürün Ayrıntısı</a></li>
				<li><a href="#tab3" id="link3" >Resimler</a></li>
                <li><a href="#tab4" id="link4" >Kampanya İşlemleri</a></li>
                <li><a href="#tab5" id="link5" >Ürün Seçenekleri</a></li>
			 </ul>
			<div class="clear"></div>
			</div> <!-- End .content-box-header -->
            <div style="display: block;"  class="content-box-content">
			<div style="display: block;" class="tab-content default-tab" id="tab1">

        <div style="padding-bottom:10px; text-align:right;" >

        <asp:Button ID="btnUrunEkle" runat="server" Text="Ürün Kaydet" 
                        onclick="btnUrunEkle_Click" OnClientClick="saveMe_deger();" Width="200px" CssClass="button"  />

        <asp:Button ID="btnUrunGuncelle" runat="server" Text="Ürün Düzenle" 
                         CssClass="button" Visible="false" onclick="btnUrunGuncelle_Click" OnClientClick="saveMe_deger();" />

        <asp:Button ID="btnUrunSil" CssClass="button"  runat="server" 
            Text=" Ürünü Sil " onclick="btnUrunSil_Click"  OnClientClick="return confirm('Bu ürünü silmek istediğinizden emin misiniz?');" Visible="false"  />

             <asp:Button ID="btnYorum" runat="server" Text="Ürün Yorum Ekle"  Width="200px" CssClass="button"  />

   </div>

               
             <table cellpadding="0" cellspacing="5" border="0" width="100%">
             <tr>
                <td style="width:200px;" >
                  <label>Kategori Seçimi : </label>
                </td>
                <td style="width:450px;" >
                    <asp:DropDownList ID="ddlkategoriler" runat="server" Width="400px" 
                        CssClass="text-input">
                    </asp:DropDownList>
                </td>
                <td>

                    <asp:RequiredFieldValidator ID="rfvKategori" runat="server" 
                        ErrorMessage="Bir Kategori Şeçmelisiniz." InitialValue="0"  
                        ControlToValidate="ddlkategoriler" CssClass="input-notification error"  ForeColor="" ></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td>
                      <label> Marka Seçimi :</label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMarkalar" runat="server" Width="400px" CssClass="text-input ">
                    </asp:DropDownList>
                </td>
                <td>
                
                    <asp:RequiredFieldValidator ID="rfvMarka" runat="server" 
                        ControlToValidate="ddlMarkalar" ErrorMessage="Bir Marka Seçmelisiniz" 
                        Font-Size="12px" InitialValue="0" CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>
                
                </td>
            </tr>
             <tr>
                <td>
                <label>Ürün Adı:</label>
                </td>
                <td>
                    <asp:TextBox ID="txt_urunAdi" runat="server" Width="400px" CssClass="text-input"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txt_urunAdi" ErrorMessage="Ürün Adı Yazınız." 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>
                </td>
            </tr>
               
             <tr>
                <td>
                     <label> Ürün Kodu:</label>
                </td>
                <td >
                
                    <asp:TextBox ID="txt_urunKodu"  runat="server" Width="200px" CssClass="text-input "></asp:TextBox>
                 </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td>
                    <label> Ürün Fiyatı:</label>
                 </td>
                <td >
                
                    <asp:TextBox ID="txt_urunFiyat" runat="server" Width="100px" CssClass="text-input "></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="Filtered" runat="server" TargetControlID="txt_urunFiyat"
                FilterType="Custom, Numbers" ValidChars=",." />
                    <asp:DropDownList ID="ddlDoviz" CssClass="text-input " Width="100px" runat="server">
                    <asp:ListItem Text="TL" Value="TL" ></asp:ListItem>
                    <asp:ListItem Text="EURO" Value="EURO" ></asp:ListItem>
                    <asp:ListItem Text="USD" Value="USD" ></asp:ListItem>
                    </asp:DropDownList>
                 </td>
                <td>
                
                    <asp:RequiredFieldValidator ID="rfvFiyat" runat="server" 
                        ControlToValidate="txt_urunFiyat" ErrorMessage="Ürün Fiyatı Giriniz." 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>
                 </td>
            </tr>
             <tr>
                <td>
                <label> İndirimli Ürün Fiyatı:</label>
                 </td>
                <td >
                
                    <asp:TextBox ID="txt_urunIndirimFiyat" runat="server" Width="100px" CssClass="text-input " Text="0,00"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="fiFiyat" runat="server" TargetControlID="txt_urunIndirimFiyat"
                FilterType="Custom, Numbers" ValidChars=",." />
                
                   <asp:RequiredFieldValidator ID="rfvİndirim" runat="server" 
                        ControlToValidate="txt_urunIndirimFiyat" ErrorMessage="İndirim Düzenlenmeyecekse 0 giriniz." 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>
                
                </td>
                <td>
               
                </td>
            </tr>
             <tr>
                <td>
                  <label> KDV %: </label>
                </td>
                <td>
                
                    <asp:TextBox ID="txtKdv" Text="0" runat="server" CssClass="text-input " Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvKdv" runat="server" 
                        ControlToValidate="txtKdv" ErrorMessage="KDV Dahil ise 0 giriniz" 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>
                    <cc1:FilteredTextBoxExtender ID="filtleKdv" runat="server" TargetControlID="txtKdv" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                 </td>
                <td>
                
                    &nbsp;</td>
            </tr>
             <tr>
                <td>
                  <label>Havale İndirim %: </label>
                </td>
                <td  >
                
                    <asp:TextBox ID="txtHavale" Text="0" runat="server" CssClass="text-input " 
                        Width="100px"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="rfvHavale" runat="server" 
                        ControlToValidate="txtHavale" ErrorMessage="0 deger giriniz" 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>

                    <cc1:FilteredTextBoxExtender ID="txtHavale_FilteredTextBoxExtender" 
                        runat="server" TargetControlID="txtHavale" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                        </td>
                 <td>
                
                     &nbsp;</td>
            </tr>
             <tr>
                <td>
                 <label>Tıklanma:</label>
                </td>
                <td  >
                
                    <asp:TextBox ID="txtHit" runat="server" Text="1" Width="100px" CssClass="text-input "></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvHit" runat="server" 
                        ControlToValidate="txtHit" ErrorMessage="0 deger giriniz" 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtHit" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                    </td> 
                <td>
                
                    &nbsp;</td>
            </tr>
             <tr>
                <td>
                 <label>Desi Miktarı:</label>
                </td>
                <td  >
                
                    <asp:TextBox ID="txtDesi" runat="server" Text="0"  CssClass="text-input " Width="100px"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="rfvDesi" runat="server" 
                        ControlToValidate="txtDesi" ErrorMessage="0 deger giriniz" 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>
                     </td>
                 <td>
                
                     &nbsp;</td>
            </tr>
             <tr>
                <td>
                <label>Stok Miktar:</label>
                </td>
                <td >
                   <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtStok" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                    <asp:TextBox ID="txtStok" runat="server" Text="1" CssClass="text-input " Width="100px"></asp:TextBox>
                    <asp:DropDownList ID="ddlStokCinsi" runat="server" CssClass="text-input ">
                     <asp:ListItem value="Kutu" Text="Kutu"></asp:ListItem>
                    <asp:ListItem value="Adet" Text="Adet"></asp:ListItem>
                    <asp:ListItem value="Paket" Text="Paket"></asp:ListItem>
                    <asp:ListItem value="Koli" Text="Koli"></asp:ListItem>
                    </asp:DropDownList>

                      <asp:RequiredFieldValidator ID="rfvMiktar" runat="server" 
                        ControlToValidate="txtStok" ErrorMessage="0 deger giriniz" 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>

                 </td>
                 <td>
                     &nbsp;</td>
            </tr>
             <tr>
                <td>
                 <label> Kritik Stok:</label>
                </td>
                <td >
                
                    <asp:TextBox ID="txtKiritikStok" runat="server" Text="10"  CssClass="text-input " 
                        Width="100px"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rfvKiritik" runat="server" 
                        ControlToValidate="txtKiritikStok" ErrorMessage="0 deger giriniz" 
                        CssClass="input-notification error" ForeColor="" ></asp:RequiredFieldValidator>
                           <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtKiritikStok" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                           </td>
                 <td>
                
                     &nbsp;</td>
            </tr>
            <tr>
            <td> 
             <label>Gösterim Opsiyonları :</label>    
            </td>
            <td colspan="2" >
            <table>
            <tr>
            <td> 
             <asp:CheckBox ID="ckbDurum" runat="server" Checked="True" CssClass="secenek" Text="Durum" />
            </td>
            <td>
                 <asp:CheckBoxList ID="ckbOzellikler" runat="server" RepeatDirection="Horizontal"
                 RepeatColumns="8" CssClass="secenek" >
                  <asp:ListItem Text="Anasayfa" Value="anasayfa" ></asp:ListItem>
                  <asp:ListItem Text="En Çok Satanlar" Value="satanlar" ></asp:ListItem>
                  <asp:ListItem Text="Yeni Ürünler" Value="yeniUrun" ></asp:ListItem>
                  <asp:ListItem Text="Hızlı Teslimat" Value="hiz" ></asp:ListItem>
                  <asp:ListItem Text="Bausch & Lomb" Value="bauschLomb" ></asp:ListItem>
                  <asp:ListItem Text="Rainbow"  Value="rainbow" ></asp:ListItem>
                  <asp:ListItem Text="Fresh Look"  Value="freshLook" ></asp:ListItem>
                  <asp:ListItem Text="Sophistic"  Value="sophistic" ></asp:ListItem>
                  <asp:ListItem Text="Star Color"  Value="starColor" ></asp:ListItem>
                  <asp:ListItem Text="Solotica Gold"  Value="solotica" ></asp:ListItem>
                  <asp:ListItem Text="Solotica Hidrocor"  Value="soloticaHidCo" ></asp:ListItem>
                  <asp:ListItem Text="Solotica Natural Colors"  Value="soloticaNaCo" ></asp:ListItem>
                  
                </asp:CheckBoxList>
            </td>
            </tr>
            </table>
            </td>
            </tr>
        </table>
          
			</div> <!-- End #tab1 -->
			<div style="display: none;" class="tab-content" id="tab2">
            <p>
            <label>Kısa Açıklama </label>
            <asp:TextBox ID="txtAciklama" runat="server" Width="100%"  TextMode="MultiLine" Rows="2" CssClass="text-input"></asp:TextBox>
            </p>
            <p>
            <label>Ürün Ayrıntı </label>
              <FCKeditorV2:FCKeditor ID="fck_urunAciklama" runat="server"   ToolbarSet=""  Height="450px" >
              </FCKeditorV2:FCKeditor>
            </p>
            <p>
            <label>Title </label>
            <asp:TextBox ID="txtTitle" runat="server"  CssClass="text-input"
                            Width="100%"></asp:TextBox>
            </p>
            <p>
            <label>Keywords:</label>
            <asp:TextBox ID="txtKeyword" runat="server" CssClass="text-input " Height="80px" 
                            Rows="3" TextMode="MultiLine" Width="100%" Wrap="true"></asp:TextBox>
            </p>
            <p>
        <label>Description </label>  
            <asp:TextBox ID="txtDescription" runat="server" CssClass="text-input " Height="80px" 
                            Rows="3" TextMode="MultiLine" Width="100%" Wrap="true"></asp:TextBox>
            </p>
			</div> <!-- End #tab2 -->  
            <div style="display: none;" class="tab-content" id="tab3">
           
             <asp:Panel ID="pnlResim_1" BorderStyle="none" GroupingText="&nbsp; Ürün Resimi &nbsp; " Width="100%" runat="server" Height="100px" >
              <label> Resim Seçimi :</label>
              <asp:FileUpload ID="txtResimEk" runat="server" CssClass="text-input"   />
              <br />
              <br />
              </asp:Panel> 

             <asp:Panel ID="pnlUrunResim" BorderStyle="none" GroupingText="&nbsp; Ürün Resimleri &nbsp; " Visible="false"  Width="100%" runat="server">
    
             <table cellpadding="0" border="0" cellspacing="0" 
    style="width: 100%">
               
                 <tr>
                     <td colspan="2">
                          <asp:UpdatePanel ID="udpResimler" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
             

                             
                    <asp:Repeater runat="server" ID="dlResimleri" 
                        onitemcommand="dlResimleri_ItemCommand"  >
                             <ItemTemplate>
                             <div id="resimlerDiv"   >
                                <div class="resimDiv">
                                    <img src='../Products/Small/<%# DataBinder.Eval(Container, "DataItem.resimAdi") %>' width="180px" />
                                </div>
                                <div class="resimBaslik" >
                                 <asp:TextBox ID="txtGunResimBaslik"
                                  Text='<%# DataBinder.Eval(Container, "DataItem.resimBaslik")%>' 
                                  CssClass="text-input small-input"
                                  ToolTip='<%# DataBinder.Eval(Container, "DataItem.id") %>'
                                  AutoPostBack="true" TextMode="MultiLine" Rows="3" 
                                  Height="60px" Width="190px" 
                                  ontextchanged="GunResimBaslik_TextChanged" runat="server"></asp:TextBox>

                                </div>

                                <div class="resimAlt">
                                <div  class="resimAlt1" >
                                <asp:ImageButton ID="btnSil" CssClass="imgButtonResim" ImageUrl="images/resimSil.gif" runat="server"  CommandArgument='<%# Eval("resimAdi").ToString()+ " , " + Eval("id").ToString() %>' CommandName="btnSil" />
                                </div>
                                <div class="resimAlt2">
                                   <asp:TextBox ID="txt_resimSira"
                                    Text='<%# DataBinder.Eval(Container, "DataItem.sira") %>'
                                    CssClass="formResimSira"  AutoPostBack="true"
                                    ToolTip='<%# DataBinder.Eval(Container, "DataItem.id") %>'
                                    ontextchanged="Urun_Resim_TextChanged" runat="server"></asp:TextBox>

                                </div>
   
                              <asp:HiddenField ID="hdfResimId" Value='<%# DataBinder.Eval(Container, "DataItem.id") %>' runat="server" /> 

                                </div>
                                </div>
                            </ItemTemplate>
                            </asp:Repeater>


                         <asp:Label ID="lblResimHata"  Visible="false" runat="server" ></asp:Label>
                            </ContentTemplate>
    </asp:UpdatePanel>
                     </td>
                 </tr>
                   <tr>
                    
                     <td colspan="1" style="width:210px; padding-top:30px;" >
                        
                         <asp:FileUpload ID="txtresim" CssClass="text-input " runat="server" Width="190px" />
                        
                    </td>
                     <td style="text-align:left; padding-top:27px;" >
                         <asp:Button ID="btnUrunResimEkle" runat="server" Text="Urun Resim Ekle" 
                             CausesValidation="false" onclick="btnUrunResimEkle_Click" CssClass="button" Height="27px" /></td>
                 </tr>
             </table>
           

    </asp:Panel>
               
		    </div> <!-- End #tab3 -->  
            <div style="display:none;" class="tab-content" id="tab4">
             
                <asp:UpdatePanel ID="upKampanyalar" runat="server">
                <ContentTemplate>

                <asp:Label ID="lblMesaj" runat="server"  ></asp:Label>
                <asp:Panel ID="pnlKampanyaBilgi" BorderStyle="none" GroupingText="&nbsp; Kampanya Bilgileri &nbsp;"
         Width="100%" runat="server">
               
                  <asp:GridView ID="gvwKampanyaBilgi" runat="server" AutoGenerateColumns="false" 
                      CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Id"  
                      onrowcommand="gvwKampanyaBilgi_RowCommand" >
                       <Columns>
                           <asp:TemplateField HeaderText="Sıra">
                               <ItemTemplate>
                                <%# Eval("Sira") %>
                               </ItemTemplate>
                               <HeaderStyle Width="50" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Ürün Kampanya Bilgisi">
                               <ItemTemplate>
                                <%# Eval("Bilgi") %>
                               </ItemTemplate>
                               <HeaderStyle Width="500" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField  HeaderText="Sil" >
                               <ItemTemplate>
                                  <asp:ImageButton  ID="btnKampanyaBilgiSil" 
                                    CommandName="kampanyaBilgiSil" ImageUrl="~/admin/images/sil.gif"
                                    CommandArgument='<%# Eval("Id") %>' runat="server" />
                               </ItemTemplate> 
                               <ItemStyle Width="40px" Font-Bold="true" /> 
                               <HeaderStyle Font-Bold="true" /> 
                           </asp:TemplateField>
                       </Columns>
                       <RowStyle  CssClass="RowStyle" />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                   </asp:GridView>

                  <table>
                       <tr>
                           <td style="padding-left:5px; width:100px;">
                            <label>Kampanya Bilgi: </label>  
                            </td>
                           <td style="width:70px;" >
                              <asp:DropDownList ID="ddlKampanyaSira" Width="50px" runat="server">
                                  <asp:ListItem Value="1" Text="1"  ></asp:ListItem>
                                  <asp:ListItem Value="2" Text="2"  ></asp:ListItem>
                                  <asp:ListItem Value="3" Text="3"  ></asp:ListItem>
                                  <asp:ListItem Value="4" Text="4"  ></asp:ListItem>
                                  <asp:ListItem Value="5" Text="5"  ></asp:ListItem>
                                  <asp:ListItem Value="6" Text="6"  ></asp:ListItem>
                              </asp:DropDownList>
                  
                             </td>
                             <td>
                              <asp:TextBox ID="txtKampanyaBilgi" runat="server"
                               Width="500px" CssClass="text-input" ></asp:TextBox><br />
                  
                              <asp:RequiredFieldValidator ID="rfvKampanyaBilgi" runat="server" 
                                ControlToValidate="txtKampanyaBilgi" Display="Dynamic" ForeColor="" 
                                CssClass="input-notification error" 
                                ErrorMessage="Lütfen indirim oranı giriniz." 
                                ValidationGroup="kampanyaBilgi" ></asp:RequiredFieldValidator>
                             </td>
                           <td>
                             <asp:Button ID="btnKampanyaBilgi"  CssClass="button" runat="server"
                              ValidationGroup="kampanyaBilgi" Text="Kampanya Bilgi Ekle" 
                              onclick="btnKampanyaBilgi_Click" />
                            </td>
                       </tr>            
                   </table>
                 </asp:Panel> 

                 <asp:Panel ID="pnlIndirim" BorderStyle="none" GroupingText="&nbsp; Adet Bazlı İndirim &nbsp; " Width="100%" runat="server">
               
                  <asp:GridView ID="gvwUrunIndirim" runat="server" AutoGenerateColumns="false" 
                      CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Id"  
                      onrowcommand="gvwUrunIndirim_RowCommand" >
                       <Columns>
                           <asp:TemplateField HeaderText="İndirim Uygulanacak Ürün Adet">
                               <ItemTemplate>
                                <%# Eval("Adet") %>
                               </ItemTemplate>
                               <HeaderStyle Width="250" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="İndirm Oranı">
                               <ItemTemplate>
                                <%# Eval("Oran") %>
                               </ItemTemplate>
                               <HeaderStyle Width="150" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField  HeaderText="Sil" >
                               <ItemTemplate>
                                  <asp:ImageButton  ID="btnUrunIndirimSil" 
                                    CommandName="urunUrunIndirimSil" ImageUrl="~/admin/images/sil.gif"
                                    CommandArgument='<%# Eval("Id") %>' runat="server" />
                               </ItemTemplate> 
                               <ItemStyle Width="40px" Font-Bold="true" /> 
                               <HeaderStyle Font-Bold="true" /> 
                           </asp:TemplateField>
                       </Columns>
                       <RowStyle  CssClass="RowStyle" />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                   </asp:GridView>

                  <table>
                       <tr>
                           <td style="padding-left:5px; width:230px;">
                            <label>İndirm Ekle:  </label>  
                            </td>
                           <td style="width:170px;" >
                              <asp:DropDownList ID="ddlUrunIndirimAdet" Width="150px" runat="server">
                                  <asp:ListItem Value="0" Text="Ürün Adet Seçiniz"  ></asp:ListItem>
                                  <asp:ListItem Value="1" Text="1"  ></asp:ListItem>
                                  <asp:ListItem Value="2" Text="2"  ></asp:ListItem>
                                  <asp:ListItem Value="3" Text="3"  ></asp:ListItem>
                                  <asp:ListItem Value="4" Text="4"  ></asp:ListItem>
                                  <asp:ListItem Value="5" Text="5"  ></asp:ListItem>
                                  <asp:ListItem Value="6" Text="6"  ></asp:ListItem>
                                  <asp:ListItem Value="7" Text="7"  ></asp:ListItem>
                                  <asp:ListItem Value="8" Text="8"  ></asp:ListItem>
                                  <asp:ListItem Value="9" Text="9"  ></asp:ListItem>
                              </asp:DropDownList>
                  
                              <asp:RequiredFieldValidator ID="rfvUrunIdirimAdet" runat="server" 
                                ControlToValidate="ddlUrunIndirimAdet" Display="Dynamic" ForeColor="" 
                                CssClass="input-notification error" InitialValue="0"
                                ErrorMessage="Lütfen ürün adet seçiniz." 
                                ValidationGroup="urunIndirimKampaya" ></asp:RequiredFieldValidator>
                             </td>
                             <td style="width:170px;">
                              <asp:TextBox ID="txtIndirimOran" runat="server"
                               Width="130px" CssClass="text-input" ></asp:TextBox><br />
                  
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="txtIndirimOran" Display="Dynamic" ForeColor="" 
                                CssClass="input-notification error" 
                                ErrorMessage="Lütfen indirim oranı giriniz." 
                                ValidationGroup="urunIndirimKampaya" ></asp:RequiredFieldValidator>
                             </td>
                           <td>

                             <asp:Button ID="btnUrunIndirimKampaya"  CssClass="button" runat="server"
                              ValidationGroup="urunIndirimKampaya" Text="Ürün İndirim Ekle" 
                              onclick="btnUrunIndirimKampaya_Click" />
                            </td>
                       </tr>            
                   </table>
               </asp:Panel> 


                 <asp:Panel ID="pnlTavsiye" Visible="false" BorderStyle="none" GroupingText="&nbsp; Tavsiye Ürünler &nbsp; " Width="100%" runat="server">

                   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"  
                                    onrowcommand="GridView1_RowCommand" >
                       <Columns>
                           <asp:TemplateField HeaderText="No">
                               <ItemTemplate>
                            Tavsiye :    <%# Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                               <HeaderStyle  Width="90" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Ürün Adı">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.urunAdi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="500" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Asıl Fiyat">
                               <ItemTemplate>
                               <%# Convert.ToDecimal(Eval("urunFiyat")).ToString("N") %>
                                <%# DataBinder.Eval(Container, "DataItem.doviz")%>
                               </ItemTemplate>
                               <HeaderStyle Width="100" />
                           </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="İndirimli Fiyat">
                               <ItemTemplate>
                                <%# Convert.ToDecimal(Eval("uIndirimFiyat")).ToString("N")%>
                                <%# DataBinder.Eval(Container, "DataItem.doviz")%>

                               </ItemTemplate>
                               <HeaderStyle Width="100" />
                           </asp:TemplateField>
                           <asp:TemplateField  HeaderText="Sil" >
                               <ItemTemplate>
                                   <asp:ImageButton  ToolTip="Tavsiye Sil" ID="btnSoruKaydet" CommandName="urunTavsiyeSil" ImageUrl="~/admin/images/sil.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id") %>' runat="server" />
                               </ItemTemplate> 
                               <ItemStyle Width="40px" />  
                           </asp:TemplateField>
                       </Columns>
                       <RowStyle  CssClass="RowStyle" />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>

                    <table>
                        <tr>
                           <td style="padding-left:5px;width:230px; " >
                           <label>Tavsiye Ekle: </label>   
                            </td>
                            <td style="width:340px;" >
                             <asp:TextBox ID="txtTUrunId" runat="server"  CssClass="text-input small" Width="300px"  ></asp:TextBox><br />
                              <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Tavsiye Ürünün ID Giriniz...." 
                              TargetControlID="txtTUrunId" runat="server">
                              </cc1:TextBoxWatermarkExtender>
                              <asp:RequiredFieldValidator ID="CustomValidator3" runat="server" 
                                    ControlToValidate="txtTUrunId" Display="Dynamic"  ForeColor=""  CssClass="input-notification error"
                                    ErrorMessage="! Lütfen Bir Ürün ID Giriniz " ValidationGroup="urunTavsiye" ></asp:RequiredFieldValidator>

                             
                            </td>
                            <td  >
                             <asp:Button ID="btnUrunTavsiye" Visible="false" CssClass="button" runat="server"
                             ValidationGroup="urunTavsiye" Text="Ürün Tavsiye Ekle" onclick="btnUrunTavsiye_Click" />
                            </td>
                        </tr>                  
                    </table>

                 </asp:Panel> 

                <asp:Panel ID="pnlKampanya" Visible="false" BorderStyle="none" GroupingText="&nbsp; Hediye Ürünü &nbsp; " Width="100%" runat="server">
               
                  <asp:GridView ID="gvwKampanya" runat="server" AutoGenerateColumns="false" 
                            CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"
                             onrowcommand="gvwKampanya_RowCommand" >
                       <Columns>
                           <asp:TemplateField HeaderText="No">
                               <ItemTemplate>
                            Kampanya :    <%# Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                               <HeaderStyle  Width="90" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Ürün Adı">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.urunAdi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="500" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Asıl Fiyat">
                               <ItemTemplate>
                               <%# Convert.ToDecimal(Eval("urunFiyat")).ToString("N") %>
                                <%# DataBinder.Eval(Container, "DataItem.doviz")%>
                               </ItemTemplate>
                               <HeaderStyle Width="100" />
                           </asp:TemplateField>
                         <asp:TemplateField HeaderText="İndirimli Fiyat">
                               <ItemTemplate>
                                <%# Convert.ToDecimal(Eval("uIndirimFiyat")).ToString("N")%>
                                <%# DataBinder.Eval(Container, "DataItem.doviz")%>

                               </ItemTemplate>
                               <HeaderStyle Width="100" />
                           </asp:TemplateField>
                           <asp:TemplateField  HeaderText="Sil" >
                               <ItemTemplate>
                                   <asp:ImageButton  ToolTip="Tavsiye Sil"
                                    ID="btnKampanyaSil" CommandName="urunKampanyaSil" ImageUrl="~/admin/images/sil.gif"
                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id") %>' runat="server" />
                               </ItemTemplate> 
                               <ItemStyle Width="40px" />  
                           </asp:TemplateField>
                       </Columns>
                       <RowStyle  CssClass="RowStyle" />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>

                  <table>
                       <tr>
                           <td style="padding-left:5px; width:230px;">
                            <label>Hediye ürünü Ekle:  </label>  
                            </td>
                           <td style="width:340px;" >
                                 <asp:TextBox ID="txtKampanya" runat="server"  CssClass="text-input small"  Width="300px"  ></asp:TextBox><br />
                                 
                                 <cc1:TextBoxWatermarkExtender ID="wtbKampanya" WatermarkText="Kampanya Ürünün ID Giriniz...."
                                  TargetControlID="txtKampanya" runat="server">
                                 </cc1:TextBoxWatermarkExtender>
                                 <asp:RequiredFieldValidator ID="rfvKapanya" runat="server" 
                                  ControlToValidate="txtKampanya" Display="Dynamic"  ForeColor=""
                                  CssClass="input-notification error" ErrorMessage="! Lütfen Bir Ürün ID Giriniz" 
                                  ValidationGroup="urunKampanya" ></asp:RequiredFieldValidator>
                         
                             </td>
                           <td>
                             <asp:Button ID="btnKampanya"  CssClass="button" runat="server"
                              ValidationGroup="urunKampanya" Text="Ürün Kampanya Ekle" onclick="btnKampanya_Click" />
                            </td>
                       </tr>            
                   </table>
               </asp:Panel> 
                

               <asp:Panel ID="pnlHediyeKampanya" Visible="false" BorderStyle="none" GroupingText="&nbsp; Hedyiye Ürün Kampanya &nbsp; " Width="100%" runat="server">
               
                  <asp:GridView ID="grwHediyeKampanya" runat="server" AutoGenerateColumns="false" 
                      CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"
                      onrowcommand="grwHediyeKampanya_RowCommand" >
                       <Columns>
                           <asp:TemplateField HeaderText="Kampanya">
                               <ItemTemplate>
                                 <%# Eval("Title") %>
                               </ItemTemplate>
                               <HeaderStyle Width="500" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Limit">
                               <ItemTemplate>
                                <%# Eval("Limit") %>
                               </ItemTemplate>
                               <HeaderStyle Width="100" />
                           </asp:TemplateField>
                           <asp:TemplateField  HeaderText="Sil" >
                               <ItemTemplate>
                                  <asp:ImageButton  ID="btnHediyeKampanyaSil" 
                                    CommandName="urunHediyeKampanyaSil" ImageUrl="~/admin/images/sil.gif"
                                    CommandArgument='<%# Eval("Id") %>' runat="server" />
                               </ItemTemplate> 
                               <ItemStyle Width="40px" />  
                           </asp:TemplateField>
                       </Columns>
                       <RowStyle  CssClass="RowStyle" />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <AlternatingRowStyle CssClass="AlternatingRowStyle" />
                   </asp:GridView>

                  <table>
                       <tr>
                           <td style="padding-left:5px; width:230px;">
                            <label>Kampanya ürünü Ekle:  </label>  
                            </td>
                           <td style="width:340px;" >
                              <asp:DropDownList ID="ddlHediyeKampanya" Width="300px" runat="server">
                              </asp:DropDownList>
                  
                              <asp:RequiredFieldValidator ID="rfvKampanya" runat="server" 
                                ControlToValidate="ddlHediyeKampanya" Display="Dynamic" ForeColor="" 
                                CssClass="input-notification error" InitialValue="0"
                                ErrorMessage="Lütfen kampanya seçiniz." 
                                ValidationGroup="urunHediyeKampanya" ></asp:RequiredFieldValidator>
                             </td>
                           <td>

                             <asp:Button ID="btnHediyeKampanya"  CssClass="button" runat="server"
                              ValidationGroup="urunHediyeKampanya" Text="Hediye Kampanya Ekle" 
                              onclick="btnHediyeKampanya_Click" />
                            </td>
                       </tr>            
                   </table>
               </asp:Panel> 

             </ContentTemplate>
             </asp:UpdatePanel>

               
		    </div> <!-- End #tab4 -->
            <div style="display: none;" class="tab-content" id="tab5">
            
               
            <table>
            <tr>
            <td colspan="2" >
            <asp:DropDownList ID="ddlSecenekler" runat="server" Width="500px"
            AutoPostBack="true" CssClass="uyuForm" 
            onselectedindexchanged="ddlSecenekSelectedIndexChanged">
            <asp:ListItem Text="Seçenek Başlığı Seçiniz "  Value="null" Selected="True"> </asp:ListItem> 

            <asp:ListItem Text="Aks"  Value="Aks" > </asp:ListItem> 
            <asp:ListItem Text="Bc" Value="Bc" > </asp:ListItem> 
            <asp:ListItem Text="Dia" Value="Dia" > </asp:ListItem> 
            <asp:ListItem Text="Dioptri" Value="Dioptri" > </asp:ListItem> 
            <asp:ListItem Text="Renk" Value="Renk" > </asp:ListItem> 
            <asp:ListItem Text="Silindirik" Value="Silindirik" > </asp:ListItem> 

            </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSecenekler"  />
                </Triggers>
                <ContentTemplate>
                    <asp:ListBox ID="secenek_ismi" ClientIDMode="Static" SelectionMode="Multiple" Width="500px" Height="500px" runat="server">
                    </asp:ListBox>
                </ContentTemplate>
            </asp:UpdatePanel>
            <input id="ekle" type="button" value="Çoklu Seçimi ekle" class="button" onclick="moveOver_deger();" />
            </td>
            <td>
            <asp:ListBox ID="secenek_ismi2" ClientIDMode=Static SelectionMode="Multiple" Width="500px" Height="500px" runat="server">
            </asp:ListBox><br />
                <asp:HiddenField ID="hdfSecenekler" ClientIDMode="Static"  runat="server" />
               <input id="sil" type="button" class="button" value="Çoklu Seçimi sil" onclick="removeMe_deger();" />
               <input id="tumunuSil" type="button" class="button" value="Tümünü sil" onclick="clearAllOptions();" />
               
            </td>
            </tr>
            </table>
		    </div> <!-- End #tab5 -->    
		    </div> <!-- End .content-box-content -->
 </div>



          <cc1:ModalPopupExtender ID="ModalPopupExtender" BackgroundCssClass="popUpBg" runat="server"  TargetControlID="btnYorum" PopupControlID="pnlPopup" >
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
                
           
                <label> Adı Soyad:</label>
                 <asp:TextBox ID="txtAdSoyad" runat="server" CssClass="text-input " Width="400px"></asp:TextBox> 
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAdSoyad" CssClass="input-notification error" ErrorMessage="Adı Soyad  yazınız." ForeColor="" ValidationGroup="yorum" ></asp:RequiredFieldValidator>
                
             
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
                         InitialValue="0" ForeColor="" ValidationGroup="yorum" ></asp:RequiredFieldValidator>
                </p>
                <p>
                 <label>Ürün Yorumu:</label>
                  <asp:TextBox ID="txtUrunYorum" runat="server" CssClass="form" Rows="5"  TextMode="MultiLine" Width="400px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUrunYorum" CssClass="input-notification error" ErrorMessage="Form İçerigi Giriniz." ForeColor="" ValidationGroup="yorum" ></asp:RequiredFieldValidator>

                </p>
                <p>
                 <asp:Button ID="btnYorumEkle" runat="server" CssClass="button" Text="Yorumu Kaydet"  ValidationGroup="yorum" onclick="btnYorumKaydet_Click" />

                 
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


    <script type="text/javascript" language="javascript" >


              function tabResim() {
                  $('#link1').removeClass('default-tab current');
                  $('#link1').removeClass('current');
                  $('#link3').addClass('default-tab current');


                  $('#tab1').removeClass('tab-content default-tab');
                  $('#tab1').css("display", "none");
                  $('#tab1').addClass('tab - content');

                  $('#tab3').removeClass('tab - content');
                  $('#tab3').addClass('tab-content default-tab');

                  //               $('#link2').text("Duyuru Düzenle");
              }

        </script>
      
</asp:Content>




