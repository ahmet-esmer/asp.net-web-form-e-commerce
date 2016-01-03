<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="Admin_Markalar" Title="Untitled Page" Codebehind="markalar.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">Marka İşlemleri</h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a href="#tab1" class="default-tab current">Markalar</a></li> 
			<li style="cursor:pointer;">
           <asp:HyperLink ID="btnShow" runat="server">Yeni Marka Ekle</asp:HyperLink>
            </li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

           <asp:Panel ID="pnlYorumArama" BorderStyle="none" GroupingText=" Marka Arama Formu " Height="140px" Width="100%" runat="server"  >
       
        <table style="width: 100%; padding-top:20px;margin-bottom:10px;">
            <tr>
                <td style="width:200px; ">
               <label> Marka Arama:</label> 
               </td>
                <td style="width:160px;">
             
                    <asp:TextBox ID="txtMarkaAra" runat="server" CssClass="text-input" Width="250" ></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="input-notification error"  ForeColor="" ControlToValidate="txtMarkaAra" ErrorMessage="Marka Adı Giriniz." ValidationGroup="markaArama" Font-Size="12px" Display="Dynamic" ></asp:RequiredFieldValidator>      
         
                </td>
                <td style="padding-left:30px;">

                    <asp:Button ID="btnMarkaAra" Height="23px" CssClass="button"  runat="server" Text="Button" 
                        onclick="btnMarkaAra_Click"  ValidationGroup="markaArama" />

                </td>
            </tr>
            <tr>
                <td colspan="3">
                 <asp:Panel ID="PagingPanel" CssClass="alfabePanel" runat="server">
                 <a href="markalar.aspx?islem=genel"  class="letters" >Hepsi</a>
                 </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
             <br />
             <br />       
      
          

	       <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" Width="100%" 
                onrowcommand="GridView1_RowCommand">
                       <Columns>
                           <asp:TemplateField HeaderText="Sıra">
                               <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                               <HeaderStyle  Width="30"  Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Marka Adı">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.marka_adi")%>
                               </ItemTemplate>
                               <HeaderStyle Width="200" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Ürün Sayısı">
                               <ItemTemplate>
                               <span >Ürün Sayısı:  </span>
                             <b>   <%# DataBinder.Eval(Container, "DataItem.markaUrunSayisi")%> </b>
                               </ItemTemplate>
                               <HeaderStyle Width="250" Font-Bold="true" />
                           </asp:TemplateField>
                         <asp:TemplateField HeaderText="Disbrutor Marka">
                               <ItemTemplate>
                                 <%# Eval("disbrutor").ToString() ==  "True" ? "Evet"  :  "Hayır"  %> 
                               </ItemTemplate>
                               <HeaderStyle Width="150" Font-Bold="true" />
                           </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ürünleri Görüntüle">
                               <ItemTemplate>
                                 
                         <a href="urunler.aspx?markalar=<%# DataBinder.Eval(Container, "DataItem.id")%>&markaAd= ">Ürünleri Görüntüle</a>
                               </ItemTemplate>
                               <HeaderStyle Width="300" Font-Bold="true" />
                           </asp:TemplateField>

                          
                           
                          <asp:TemplateField HeaderText="Sıra">
                            <ItemTemplate>  
      
                         <asp:TextBox ID="txtMarkaSira" 
                                     Text='<%# DataBinder.Eval(Container, "DataItem.sira") %>'
                                     CssClass="form"
                                     Width="50px"
                                     autocomplete="off"
                                     ToolTip='<%# DataBinder.Eval(Container, "DataItem.id") %>' 
                                     ontextchanged="MarkaSiraGuncelleme" 
                                     AutoPostBack="true" 
                                     runat="server"></asp:TextBox>     
                                <cc1:FilteredTextBoxExtender ID="filtleMarka" runat="server" TargetControlID="txtMarkaSira" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                            <HeaderStyle  Width="50" Font-Bold="true" />
                          </asp:TemplateField>         
                           <asp:TemplateField HeaderText="Durum">
                               <ItemTemplate>
                                   <a title=" <%# DataBinder.Eval(Container, "DataItem.marka_adi")%>  <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&amp;islem=durum&amp;durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="duzen" />
                               <HeaderStyle Width="30" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Düzenle">
                               <ItemTemplate>
                                 

                               <asp:ImageButton ID="btnGuncell"
                             CommandArgument='<%# Eval("id")+","+ Eval("durum")+","+ Eval("marka_adi") %>' 
                             CommandName="btnGuncell" runat="server"

                             ImageUrl='images/duzenle.gif' />

                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&'  
                                       onclick=" return silKontrol()" title="Duyuru Sil"><img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <HeaderStyle  Width="50" Font-Bold="true" />
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
               <div id="messages" style="width:400px;"  >
               <table border="0" cellpadding="0" cellspacing="5" style="margin-top:20px;">
                <tr>
                    <td class="style2">
                        <asp:Label ID="lbl_markaAdı" runat="server" BorderStyle="None" Font-Size="12px" 
                            ForeColor="#4E4E4E" Text="Marka Adı:"></asp:Label>
                    </td>
                    <td class="style3">
                        <asp:TextBox ID="txt_markaAd" CssClass="form" Width="250px" runat="server" >
                        </asp:TextBox>
                        <br />
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txt_markaAd" ErrorMessage="* Marka Adı Giriniz."  ValidationGroup="marka"
                            Font-Size="12px"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                     
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbl_durum" runat="server" Font-Size="12px" ForeColor="#4E4E4E" 
                            Text="Durum:"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="ckb_durum" Checked="true" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                  <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Size="12px" ForeColor="#4E4E4E" 
                            Text="Disbrutor Marka:"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox ID="ckbDisbrutor"  runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                
                <tr>
                    <td>
                    </td>
                    <td colspan="2" style="padding-bottom:20px;">
                        <asp:Button ID="btnMarkaEkle" runat="server" CssClass="button" 
                            onclick="btnMarkaEkle_Click1" Text="Marka Ekle" ValidationGroup="marka" />
                        <asp:Button ID="btnMarkaGuncelle" runat="server" CssClass="button" 
                            onclick="btnMarkaGuncelle_Click" Text="Marka Güncelle" Visible="false" ValidationGroup="marka" />
                        <asp:HiddenField ID="hdfMarkaId" runat="server" />
                    </td>
                </tr>
            </table>
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
                       
           
</asp:Content>
