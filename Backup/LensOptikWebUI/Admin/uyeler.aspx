<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="Admin_Uyeler" Title="Untitled Page" Codebehind="uyeler.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
        <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">Üyeler</h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">
                <asp:Panel ID="pnlUyeArama" BorderStyle="none" GroupingText=" Üye Arama Formu " Height="160px" Width="100%" runat="server">      
        <table style="width: 100%; margin-top:10px;">
            <tr>
             <td style="width:300px;">   
             
                    <asp:TextBox ID="txt_UyeAdi" runat="server" autocomplete="off" 
                        AutoPostBack="true" CssClass="text-input"
                        Width="250px" ontextchanged="txt_UyeAdi_TextChanged"></asp:TextBox>
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
                        TargetControlID="txt_UyeAdi" 
                        WatermarkText="Üye Adına Göre Ara....">
                    </cc1:TextBoxWatermarkExtender>
             
                </td>
                <td style="padding-top:12px;">
                    <cc1:TextBoxWatermarkExtender TargetControlID="txtTarih_1" WatermarkText="Başlanğıç Tarihi Seçiniz.." ID="TextBoxWatermarkExtender2" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                 <asp:TextBox ID="txtTarih_1" runat="server" CssClass="text-input" Width="180px"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="vaTarih1" CssClass="input-notification error" ControlToValidate="txtTarih_1" runat="server" ErrorMessage="Başlanğıç Tarihi Seçiniz.." ForeColor="" ></asp:RequiredFieldValidator>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtTarih_1" Format="dd.MM.yyyy" >
                    </cc1:CalendarExtender>
                   </td>
                <td style="padding-top:12px;">
                     <cc1:TextBoxWatermarkExtender TargetControlID="txtTarih_2" WatermarkText="Bitiş Tarihi Seçiniz.." ID="TextBoxWatermarkExtender3" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                    
                 <asp:TextBox ID="txtTarih_2" runat="server" CssClass="text-input" Width="180px"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="vaTarih2" CssClass="input-notification error" ControlToValidate="txtTarih_2" runat="server" ErrorMessage="Bitiş Tarihi Seçiniz.." ForeColor="" ></asp:RequiredFieldValidator>
                 
                    <cc1:CalendarExtender ID="CalendarExtender2"  CssClass="cal_Theme1" runat="server" TargetControlID="txtTarih_2" Format="dd.MM.yyyy"  >
                    </cc1:CalendarExtender>
                    
                    </td>
                    <td style="padding-bottom:10px;">
                    <asp:Button ID="btnUyeTarihAra" runat="server" CssClass="button" Width="130px" 
                         Height="24px" Text="Tarih Aralığı Arama" onclick="btnUyeTarihAra_Click" />
                    </td>
               
            </tr>
        </table>
         <asp:Panel ID="PagingPanel" CssClass="alfabePanel" runat="server">
           <a href="uyeler.aspx?islem=genel"  class="letters" >Hepsi</a>
         </asp:Panel>
    </asp:Panel>

                <asp:GridView ID="grwUyeler" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                      ForeColor="#333333" GridLines="None"  Width="100%"  >
                       <Columns>
                           <asp:TemplateField HeaderText="ID">
                               <ItemTemplate>
                               <b> <%# DataBinder.Eval(Container, "DataItem.id")%></b>
                               </ItemTemplate>
                               <HeaderStyle  Width="50" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Üye Adı" >
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.adiSoyadi")%><br />
                             <div class="uyeIl">   <%# DataBinder.Eval(Container, "DataItem.sehir")%> </div> 
                               </ItemTemplate>
                               <HeaderStyle Width="200" Font-Bold="true" />
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="E-Posta">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.ePosta")%>
                               </ItemTemplate>
                               <HeaderStyle Width="230" Font-Bold="true" />
                                 <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Şifre">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.sifre")%>
                               </ItemTemplate>
                               <HeaderStyle Width="100" Font-Bold="true" />
                                 <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Telefon">
                               <ItemTemplate>
                               <%# DataBinder.Eval(Container, "DataItem.Gsm")%>
                               </ItemTemplate>
                               <HeaderStyle Width="100" Font-Bold="true" />
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Giriş">
                               <ItemTemplate>
                               <%# DataBinder.Eval(Container, "DataItem.girisSayisi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="40" Font-Bold="true" />
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>
                             <asp:TemplateField HeaderText="Cinsiyet">
                               <ItemTemplate>
                               <%# DataBinder.Eval(Container, "DataItem.cinsiyet")%>
                               </ItemTemplate>
                               <HeaderStyle Width="60" Font-Bold="true"  />
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>
                           
                           <asp:TemplateField HeaderText="Tarih">
                               <ItemTemplate>
                             <%# BusinessLayer.DateFormat.TarihSaatSiparis(DataBinder.Eval(Container, "DataItem.kayitTarihi").ToString())%> 
                               </ItemTemplate>
                               <HeaderStyle Width="130" Font-Bold="true" />
                                <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mail">
                               <ItemTemplate>
                            <a href="mail_gonder.aspx?Ad=<%# DataBinder.Eval(Container, "DataItem.adiSoyadi")%>&ePosta=<%# DataBinder.Eval(Container, "DataItem.ePosta")%> " title=" <%# DataBinder.Eval(Container, "DataItem.adiSoyadi")%> Mail Gönder" /><img src="images/ePosta.gif" width="20px" border="0"  /> </a> 
                               </ItemTemplate>
                               <HeaderStyle Width="40" Font-Bold="true" />
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Dur.">
                               <ItemTemplate>
                                   <a title="<%# DataBinder.Eval(Container, "DataItem.adiSoyadi")%> <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&amp;islem=durum&amp;durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                               
                               <HeaderStyle Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&' 
                                       onclick=" return silKontrol()" title="<%# DataBinder.Eval(Container, "DataItem.adiSoyadi")%> Üye Sil"><img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <ItemStyle VerticalAlign="Top" CssClass="uyeBosluk" />
                               <HeaderStyle  Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                       </Columns>
                   <RowStyle  CssClass="RowStyle" />
                   <HeaderStyle CssClass="HeaderStyle" />
                   <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>
                   
            <div class="pagination">
                <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
            </div>   
    
			  
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>
   

     
</asp:Content>
