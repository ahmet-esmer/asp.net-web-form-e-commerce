<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="Siparisler" EnableViewState="true" Codebehind="odemeRaporu.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">


    <style type="text/css">

    .RowStyle
     {
        border-bottom:1px solid #e8e7e7;
     }
 
    .AlternatingRowStyle
    {
      border-bottom:1px solid #e8e7e7;
    }

    </style>
    </asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header" id="header" >
					<h3 style="cursor: s-resize;">Siparişler Ödemleri</h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

    <div style="padding-bottom:10px; text-align:right;" >
     </div>
    <asp:Panel ID="pnlYorumArama" BorderStyle="none" GroupingText=" Ödeme Arama Formu " Height="150px" Width="100%" runat="server">
       
        <table style="height:60px; padding-top:20px; border:none;">
            <tr>
                <td style="width:200px;" >
                 <asp:TextBox ID="txtIslemNo" runat="server" 
                        AutoPostBack="true" CssClass="text-input" 
                        ontextchanged="txtIslemNo_TextChanged" Width="180px"></asp:TextBox>

                        <cc1:TextBoxWatermarkExtender ID="tweIslemNo" runat="server" 
                        TargetControlID="txtIslemNo" WatermarkText="Referans/İşlem  No">
                    </cc1:TextBoxWatermarkExtender>
                 </td>
                <td>
                    <asp:TextBox ID="txtKartSahibi" runat="server" autocomplete="off" 
                        AutoPostBack="true" CssClass="text-input" 
                        ontextchanged="txtKartSahibi_TextChanged" Width="180px"></asp:TextBox>

                    <cc1:AutoCompleteExtender ID="autoComplete1" runat="server" 
                        BehaviorID="AutoCompleteEx" CompletionInterval="1000" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" 
                        CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="20" 
                        EnableCaching="true" Enabled="true" MinimumPrefixLength="2" 
                        ServiceMethod="arama" ServicePath="include/KartSahibi.asmx" 
                        TargetControlID="txtKartSahibi">
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

                    <cc1:TextBoxWatermarkExtender ID="tweKartSahibi" runat="server" 
                        TargetControlID="txtKartSahibi" WatermarkText="Kart Sahibi....">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
        </table>
    </asp:Panel>
    
     <asp:GridView ID="grwSiparisOdeme" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%"  >
                       <Columns>
                           <asp:TemplateField HeaderText="Sipariş No">
                               <ItemTemplate>
                                 <b><%# DataBinder.Eval(Container, "DataItem.SiparisNo")%></b>
                               </ItemTemplate>
                              <HeaderStyle  Width="80" HorizontalAlign="Left" Font-Bold="true"  />
                           </asp:TemplateField>

                              <asp:TemplateField HeaderText="Üye Adı" >
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.UyeAdi")%>               
                               </ItemTemplate>
                               <ItemStyle  />
                               <HeaderStyle Width="160" HorizontalAlign="Left" Font-Bold="true" />
                               </asp:TemplateField>  

                          <asp:TemplateField HeaderText="Kart Sahibi" >
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.KartSahibi")%> <br />
                       
                                <%# DataBinder.Eval(Container, "DataItem.KartNo")%>
                                              
                               </ItemTemplate>
                               <ItemStyle  />
                               <HeaderStyle Width="200" HorizontalAlign="Left" Font-Bold="true" />
                               </asp:TemplateField>  
                           <asp:TemplateField HeaderText="Tutar">
                               <ItemTemplate>
                               <%#  Convert.ToDecimal(Eval("TaksitliGenelToplami")).ToString("c") %> 
                               </ItemTemplate>
                               <HeaderStyle Width="100" HorizontalAlign="Left" Font-Bold="true"  />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Taksit">
                               <ItemTemplate>
                               <%# Eval("TaksitMiktari").ToString()%>  
                               </ItemTemplate>
                               <HeaderStyle Width="50" HorizontalAlign="Left" Font-Bold="true"  />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Onay Kodu">
                               <ItemTemplate>
                               <%# DataBinder.Eval(Container, "DataItem.OnayKodu")%>
                               </ItemTemplate>
                               <HeaderStyle Width="100" HorizontalAlign="Left" Font-Bold="true"  />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Referans No">
                               <ItemTemplate>
                               <%# DataBinder.Eval(Container, "DataItem.ReferansNo")%>
                               </ItemTemplate>
                               <HeaderStyle Width="150" HorizontalAlign="Left" Font-Bold="true"  />
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="Banka">
                               <ItemTemplate>
                                       <%# DataBinder.Eval(Container, "DataItem.BankaAdi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="150" HorizontalAlign="Left" Font-Bold="true"  />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Şipariş Durumu">
                               <ItemTemplate>
                              <%# Eval("SiparisDurumu").ToString()%>
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
                              
                            
        <a href='siparisDetay.aspx?siparisId=<%# DataBinder.Eval( Container, "DataItem.SiparisId")%>' title="Sipariş Detay">
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
                
            <div class="pagination">
                <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
            </div>  

    
    		</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>
    
      <script type="text/javascript" >

          $(document).ready(function () {
              var go = getQuerystring('goto');

              if (go != null) {
                  $("#" + go).slideto({highlight: false});
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

