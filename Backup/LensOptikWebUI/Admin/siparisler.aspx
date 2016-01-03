<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="Admin_Siparisler" EnableViewState="true" Codebehind="siparisler.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header" id="header" >
					<h3 style="cursor: s-resize;">
                   <asp:Label ID="lblSayfaBaslik" runat="server" ></asp:Label>
                    </h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">
    <div style="padding-bottom:10px; text-align:right;" >
     </div>
    <asp:Panel ID="pnlYorumArama" BorderStyle="none" GroupingText=" Sipariş Arama Formu " Height="150px" Width="100%" runat="server">
       
        <table style="height:60px; padding-top:20px; border:none;">
            <tr>
                <td style="width:220px">
                <asp:TextBox ID="txt_UyeAdi" runat="server" autocomplete="off" 
                        AutoPostBack="true" CssClass="text-input" 
                        ontextchanged="txt_UyeAdi_TextChanged" Width="180px"></asp:TextBox>

                    <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" 
                        BehaviorID="AutoCompleteEx" CompletionInterval="1000" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                        CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                        EnableCaching="true" Enabled="true" MinimumPrefixLength="2" 
                        ServiceMethod="arama" ServicePath="include/autocomleteUyeler.asmx" 
                        TargetControlID="txt_UyeAdi">
                        <Animations>
                    <OnShow>
                        <Sequence>
                            <%-- Make the completion list transparent and then show it --%>
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            <%--Cache the original size of the completion list the first time
                                the animation is played and then set it to zero --%>
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                </Animations>
                    </cc1:AutoCompleteExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" 
                        TargetControlID="txt_UyeAdi" WatermarkText="Üye Adına Göre Ara....">
                    </cc1:TextBoxWatermarkExtender>
                </td>
                <td style="width:220px" >
                <cc1:TextBoxWatermarkExtender TargetControlID="txtSiparisArama" WatermarkText="Sipariş No İle.." 
                    ID="tweSiparis" runat="server"> </cc1:TextBoxWatermarkExtender>
                <asp:TextBox ID="txtSiparisArama" ClientIDMode="Static" runat="server" CssClass="text-input" Width="180" 
                    ontextchanged="txtSiparisArama_TextChanged" AutoPostBack="true" ></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="filtleSip" runat="server" TargetControlID="txtSiparisArama" 
                    FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                </td>
                <td style="width:220px" >
                 <cc1:TextBoxWatermarkExtender TargetControlID="txtSiparisFiyat" WatermarkText="Fiyat İle.." 
                    ID="TextBoxWatermarkExtender5" runat="server"></cc1:TextBoxWatermarkExtender>
                 <asp:TextBox ID="txtSiparisFiyat" ClientIDMode="Static" runat="server" 
                      CssClass="text-input" Width="180" AutoPostBack="true" 
                      ontextchanged="txtSiparisFiyat_TextChanged" ></asp:TextBox>
                 <cc1:FilteredTextBoxExtender ID="Filtered" runat="server" TargetControlID="txtSiparisFiyat"
                  FilterType="Custom, Numbers" ValidChars=",." />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                        <asp:DropDownList ID="ddlSiparisDurum" ClientIDMode="Static" runat="server" CssClass="text-input"  Width="180px">
                        <asp:ListItem Text="Sipariş Durumuna" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Onay Bekliyor" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Sipariş hazırlanıyor" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Kargoya Verildi" Value="2"></asp:ListItem>
                        <asp:ListItem Text="İptal edildi" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Ödeme onayı Bekleniyor" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Tedarik edilemedi" Value="5"></asp:ListItem>
                        <asp:ListItem Text="İptal edilecek" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Tedarik sürecinde" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Tedarik edilemiyor" Value="8"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                     <cc1:TextBoxWatermarkExtender TargetControlID="txtTarih_1" WatermarkText="Başlanğıç Tarihi Seçiniz.." ID="TextBoxWatermarkExtender2" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                 <asp:TextBox ID="txtTarih_1" ClientIDMode="Static" runat="server" CssClass="text-input" Width="180px"></asp:TextBox><br />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtTarih_1" Format="dd.MM.yyyy" >
                    </cc1:CalendarExtender>
                 </td>
                <td>
                 <cc1:TextBoxWatermarkExtender TargetControlID="txtTarih_2" WatermarkText="Bitiş Tarihi Seçiniz.." ID="TextBoxWatermarkExtender3" runat="server">
                   </cc1:TextBoxWatermarkExtender>
                    
                 <asp:TextBox ID="txtTarih_2" ClientIDMode="Static" runat="server" CssClass="text-input" Width="180px"></asp:TextBox><br />

                    <cc1:CalendarExtender ID="CalendarExtender2"  CssClass="cal_Theme1" runat="server" TargetControlID="txtTarih_2" Format="dd.MM.yyyy"  >
                    </cc1:CalendarExtender> 
                 </td>
                <td>
                  <asp:Button ID="btnSipTarihAra" runat="server" CssClass="button" Height="24px" 
                        onclick="btnSipTarihAra_Click" Text="Sipariş Arama" Width="130px" />
               </td>
            </tr>
        </table>
    </asp:Panel>
    
    <div id="divSiparis" >
     <asp:GridView ID="grwSiparisler" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"  DataKeyNames="id"  >
            <Columns>
                <asp:TemplateField HeaderText="Sipariş No ">
                    <ItemTemplate>
                    <div id="<%# DataBinder.Eval(Container, "DataItem.id")%>" >
                    <b><%# DataBinder.Eval(Container, "DataItem.siparisNo")%></b>
                    </div>
                    </ItemTemplate>
                    <HeaderStyle  Width="100" HorizontalAlign="Left" Font-Bold="true"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Üye İsmi" >
                    <ItemTemplate>
                    <%# DataBinder.Eval(Container, "DataItem.adiSoyadi")%>          
                    </ItemTemplate>
                    <ItemStyle  />
                    <HeaderStyle Width="250" HorizontalAlign="Left" Font-Bold="true" />
                    </asp:TemplateField>  
                <asp:TemplateField HeaderText="Sipariş Şekli">
                    <ItemTemplate>
                <%# Eval("OdemeTipi") %> 
                    </ItemTemplate>
                    <HeaderStyle Width="200" HorizontalAlign="Left" Font-Bold="true"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Şipariş Durumu">
                    <ItemTemplate>
                    <%# Eval("SiparisDurumu").ToString()%>
                    </ItemTemplate>
                    <HeaderStyle Width="200" HorizontalAlign="Left" Font-Bold="true"  /> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tutar">
                <ItemTemplate>
            <%#  Convert.ToDecimal( Eval("TaksitliGenelToplami")).ToString("c") %> 
                </ItemTemplate>
                <HeaderStyle Width="200" HorizontalAlign="Left" Font-Bold="true"  />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sipariş Tarih">
                <ItemTemplate>                           
                <%# BusinessLayer.DateFormat.TarihSaatSiparis(DataBinder.Eval(Container, "DataItem.SiparisTarihi").ToString())%>
                </ItemTemplate>
                <HeaderStyle  Width="220" HorizontalAlign="Left" Font-Bold="true"  />
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Detay">
                <ItemTemplate>     
                <a href='siparisDetay.aspx?siparisId=<%# DataBinder.Eval( Container, "DataItem.id")%><%if (Request.QueryString["Sayfa"] != null)
                    {
                    Response.Write("&retSayfa=" + Request.QueryString["Sayfa"]); 
                    } 
                %>' title="Sipariş Detay">
                <img src="images/duzenle.gif" alt="Detay" border="0" />
                </a>    
                </ItemTemplate>
                <ItemStyle CssClass="orta" />
                <HeaderStyle Width="20" HorizontalAlign="Left" Font-Bold="true"  />
                </asp:TemplateField>
            </Columns>     
            <RowStyle  CssClass="RowStyle" />
            <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" />
            <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
        </asp:GridView>
    </div>         
            <div class="pagination">
                <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
            </div>  
    		</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>
    
     <script type="text/javascript" src="../Scripts/SiparisRapor.js"></script>

      <script type="text/javascript" >

          $(document).ready(function () {

              var go = getQuerystring('goto');

              if (go != null) {
                  $("#" + go).slideto({ highlight: false });
                  $("#" + go).parent().parent().addClass("rowActif");
              }

              function getQuerystring(key, default_) {
                  if (default_ == null) default_ = "";
                  key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                  var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
                  var qs = regex.exec(window.location.href);
                  if (qs == null)
                      return default_;
                  else
                      return qs[1];
              }
          });

      </script>
</asp:Content>

