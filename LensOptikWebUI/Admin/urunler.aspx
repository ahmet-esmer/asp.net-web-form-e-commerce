<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="AdminUrunler" Title="Untitled Page" EnableViewState="true" Codebehind="urunler.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">Ürünler</h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

              <div style="padding-bottom:10px; text-align:right;" >
     <asp:Button ID="btnYeniUrunEkle" CssClass="button"  runat="server" 
            Text="Yeni Ürün Ekle" onclick="btnYoonetiEkle_Click" />
  </div>
  
  <asp:Panel ID="pnlUrunArama" BorderStyle="none" GroupingText=" Ürün Arama Formu " Height="160px" Width="100%" runat="server">
       
        <table style="width:100%" >
            <tr>
                <td>
                    <asp:DropDownList ID="ddlkategoriler" runat="server" CssClass="text-input" AutoPostBack="true"
                        Width="250px" onselectedindexchanged="ddlkategoriler_SelectedIndexChanged">

                    </asp:DropDownList>
                </td>
                <td>
                  
                    <asp:DropDownList ID="ddlMarkalar" runat="server" CssClass="text-input" AutoPostBack="true" 
                        Width="250px" onselectedindexchanged="ddlMarkalar_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="Aradığınız Ürünün İlk Harflerini Giriniz...." TargetControlID="txt_urunAdi" runat="server">
                </cc1:TextBoxWatermarkExtender>
                 <asp:TextBox ID="txt_urunAdi" runat="server" autocomplete="off" CssClass="text-input" 
                        Width="300px" AutoPostBack="true" ontextchanged="txt_urunAdi_TextChanged"></asp:TextBox> 
                <cc1:AutoCompleteExtender
                    ID="autoComplete1" 
                    runat="server"
                    TargetControlID="txt_urunAdi" 
                    ServicePath="include/autocomlete.asmx"
                    ServiceMethod="arama"
                    
                    CompletionInterval="1000"
                    EnableCaching="true"
                    CompletionSetCount="20"
                    BehaviorID="AutoCompleteEx"
                   
                    MinimumPrefixLength="2"       
			         CompletionListCssClass="autocomplete_completionListElement" 
                CompletionListItemCssClass="autocomplete_listItem" 
                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                    Enabled="true"> 
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
                   
                </td>
            </tr>
            <tr>
                <td>
                <asp:DropDownList ID="ddlGosterim" runat="server" CssClass="text-input"  AutoPostBack="true" 
                        Width="250px" onselectedindexchanged="ddlGosterim_SelectedIndexChanged">
                        <asp:ListItem Value="0" > Ürün Gösteri Seçeneklerine Göre</asp:ListItem>
                        <asp:ListItem Value="anasayfa" > Anasayfa </asp:ListItem>
                        <asp:ListItem Value="satanlar" > En Çok Satanlar </asp:ListItem>
                        <asp:ListItem Value="yeniUrun" > Yeni Ürünler </asp:ListItem>
                        <asp:ListItem Value="kampanya" > Kampanyalı Ürünler </asp:ListItem>
                    </asp:DropDownList>
                  </td>
                <td>
                  <asp:DropDownList ID="ddlStokDurum" runat="server" Width="315px" AutoPostBack="true" 
                        CssClass="text-input" onselectedindexchanged="ddlStokDurum_SelectedIndexChanged">
                        <asp:ListItem > Stok Durumuna Göre </asp:ListItem>
                        <asp:ListItem Value="kiritikStok" > Kırıtik Stoktaki Ürünler </asp:ListItem>
                        <asp:ListItem Value="stokYok" > Stokta Olmayan Ürünler </asp:ListItem>
                        <asp:ListItem Value="aktif" > Aktif Ürünler </asp:ListItem>
                        <asp:ListItem Value="pasif" > Pasif Ürünler </asp:ListItem>
                    </asp:DropDownList>

                 </td>
                <td>
                    <asp:TextBox ID="txtIdArama" Width="250px"  CssClass="text-input" runat="server"></asp:TextBox>
                    <asp:Button ID="btnIdArama"  Height="22px" CssClass="button" runat="server"  Text="Ürün ID Arama" 
                        onclick="btnIdArama_Click" />
                </td>
            </tr>
        </table>
     
    </asp:Panel>
  
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CellPadding="5" 
                       ForeColor="#333333" GridLines="None" Width="100%" >
                       <Columns>
                           <asp:TemplateField HeaderText="ID">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.id")%>
                               </ItemTemplate>
                              <HeaderStyle  Width="35" Font-Bold="true" />
                            
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Resim">
                                <ItemTemplate>
                                  <a href='urun_ekle.aspx?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=duzenle' title="Ürünü Düzenle">
                                  <img src="../Products/Little/<%# DataBinder.Eval(Container, "DataItem.resimAdi") %>" width="80px" />
                                  </a>
                                </ItemTemplate>

                            <HeaderStyle Width="90px" Font-Bold="true" />
                            </asp:TemplateField>
                          <asp:TemplateField HeaderText="Marka&">
                               <ItemTemplate>
                            <span class="secenek">Kategori </span><br />
                            <span class="secenek">Marka</span> <br />
                               </ItemTemplate>
                           <HeaderStyle Width="50" Font-Bold="true" />
                             </asp:TemplateField>
                              <asp:TemplateField HeaderText="Kategori">
                               <ItemTemplate>
                              <span class="secenek"> &nbsp;:&nbsp; </span> <%# DataBinder.Eval(Container, "DataItem.kategoriadi")%><br />
                              <span class="secenek"> &nbsp;:&nbsp; </span> <%# DataBinder.Eval(Container, "DataItem.markaAdi")%><br />
                               </ItemTemplate>
                           <HeaderStyle Width="170" Font-Bold="true" />
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="">
                               <ItemTemplate>
                            <span class="secenek">Adı </span><br />
                            <span class="secenek">Fiyat </span> <br />
                               </ItemTemplate>
                           <HeaderStyle Width="30" Font-Bold="true" />
                             </asp:TemplateField>
                           <asp:TemplateField HeaderText=" Ürün  Adı">
                               <ItemTemplate>
                           <span class="secenek">&nbsp;:&nbsp;</span><%# DataBinder.Eval(Container, "DataItem.urunAdi")%><br /><span class="secenek">&nbsp;:&nbsp;</span><%# Convert.ToDecimal(Eval("urunFiyat")).ToString("N") %><%# DataBinder.Eval(Container, "DataItem.doviz")%>
                               </ItemTemplate>
                               <HeaderStyle Width="170" Font-Bold="true" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stok">
                            <ItemTemplate>  
                                  
                         <%# BusinessLayer.GenelFonksiyonlar.StokDurum(Convert.ToInt32(DataBinder.Eval(Container, "DataItem.kiritikStok")), Convert.ToInt32(DataBinder.Eval(Container, "DataItem.urunStok")), DataBinder.Eval(Container, "DataItem.stokCins").ToString())%> 
                           
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle  Width="60" Font-Bold="true" HorizontalAlign="Right" />
                          </asp:TemplateField> 
                           <asp:TemplateField HeaderText="Sıra" >
                            <ItemTemplate>                           
                               <asp:TextBox ID="txtUrunSira" 
                                     Text='<%# DataBinder.Eval(Container, "DataItem.sira") %>'
                                     CssClass="form"
                                     Width="50px"
                                     autocomplete="off"
                                     ToolTip='<%# DataBinder.Eval(Container, "DataItem.id") %>' 
                                     ontextchanged="urunSiraGuncelleme" 
                                     AutoPostBack="true" 
                                     runat="server"></asp:TextBox>     
                                <cc1:FilteredTextBoxExtender ID="filtleSira" runat="server" TargetControlID="txtUrunSira" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle  Width="30" Font-Bold="true" />
                          </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Dur.">
                               <ItemTemplate>
     <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                               </ItemTemplate>
                               <ItemStyle HorizontalAlign="Center" />
                               <HeaderStyle Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Düz.">
                               <ItemTemplate>
                                   <a href='urun_ekle.aspx?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=duzenle' title=" <%# DataBinder.Eval(Container, "DataItem.urunAdi")%> Adlı Ürünü Düzenle"  >
                                    <img src="images/duzenle.gif" alt="Düzenle" border="0" /> </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                       </Columns>
                   <RowStyle  CssClass="RowStyle" />
                     
                       <SelectedRowStyle   />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <EditRowStyle />
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>
              <div class="pagination">
                <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
              </div>   
			
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>
</asp:Content>

