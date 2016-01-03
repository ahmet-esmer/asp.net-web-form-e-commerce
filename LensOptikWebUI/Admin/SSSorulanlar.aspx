<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" ValidateRequest="false"  Inherits="AdminSSSorular" Title="Untitled Page" Codebehind="SSSorulanlar.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">Sik Sorulan Sorular</h3>

            <ul style="display: block;" class="content-box-tabs">
			<li style="cursor:pointer;"  >
               <asp:HyperLink ID="btnShow" CssClass="default-tab current" runat="server">Soru Kayıt</asp:HyperLink>
            </li>
		    </ul>
            <div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" Width="100%" 
                    onrowcommand="GridView1_RowCommand">
                       <Columns>
                          <asp:TemplateField HeaderText="Soru">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.soru")%>
                               </ItemTemplate>
                               <HeaderStyle Width="200" Font-Bold="true" />
                           </asp:TemplateField>  
                          
                           <asp:TemplateField HeaderText="Cevap">
                               <ItemTemplate>
                          <%# DataBinder.Eval(Container, "DataItem.cevap")%> 
                               </ItemTemplate>
                               <HeaderStyle Width="300" Font-Bold="true" />
                           </asp:TemplateField>
                               <asp:TemplateField HeaderText="Sıra">
                            <ItemTemplate>  
      
                         <asp:TextBox ID="txtKatSira" 
                                     Text='<%# DataBinder.Eval(Container, "DataItem.sira") %>'
                                     CssClass="form"
                                     Width="50px"
                                     autocomplete="off"
                                     ToolTip='<%# DataBinder.Eval(Container, "DataItem.id") %>' 
                                     ontextchanged="SSSorulanlarSira" 
                                     AutoPostBack="true" 
                                     runat="server"></asp:TextBox>     
                                <cc1:FilteredTextBoxExtender ID="filtleKdv" runat="server" TargetControlID="txtKatSira" FilterType="Numbers" ></cc1:FilteredTextBoxExtender>
                            </ItemTemplate>
                            <HeaderStyle  Width="50" Font-Bold="true" />
                          </asp:TemplateField>  

                           <asp:TemplateField HeaderText="Panel">
                            <ItemTemplate>                           
                          
                          <asp:CheckBox ID="ckbPanel" ToolTip='<%# Eval("id")%>'
                    Checked='<%# DataBinder.Eval(Container, "DataItem.panel")%>' AutoPostBack="true" runat="server" 
                    oncheckedchanged="ckbPanel_CheckedChanged" />

                            </ItemTemplate>
                            <HeaderStyle  Width="50"  Font-Bold="true" />
                          </asp:TemplateField> 
                           
                         
                           <asp:TemplateField HeaderText="Detay">
                               <ItemTemplate>
                                 <asp:ImageButton ID="btnGuncell" CommandArgument='<%# Eval("id")%>' 
                                 CommandName="btnGuncell" runat="server" ImageUrl='images/duzenle.gif' />
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="80" Font-Bold="true" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dur.">
                               <ItemTemplate>
   <a title=" Soruyu  <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>"
    href='?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&islem=durum&durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container, "DataItem.durum")))%>
                                   </a>
               
                               </ItemTemplate>
                               <ItemStyle HorizontalAlign="Center" />
                               <HeaderStyle Width="20" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&' onclick=" return silKontrol()" title="Soruyu Sil">
                                       <img src="images/sil.gif" border="0"  /> </a>
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
                 
     <div class="pagination">
                <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
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
                <div id="messages" style="width:400px" >

                 <p>
                    <label>Soru: </label>
                    <asp:TextBox ID="txtZiyaretciYorum" runat="server" CssClass="form" Rows="5" 
                                       TextMode="MultiLine" Width="390px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtZiyaretciYorum" ValidationGroup="zDuzenle" CssClass="mesaj"></asp:RequiredFieldValidator>
                 </p>

                    <p>
                    <label>Cevap: </label>
                    <asp:TextBox ID="txtCevap" runat="server" CssClass="form" Rows="5" 
                                       TextMode="MultiLine" Width="390px" ></asp:TextBox>
                 
                 </p>

                    <label>Durum:<asp:CheckBox ID="ckbDurum" runat="server" /> </label>
          
                 <p>
                   <asp:Button ID="btnSSSoruGuncelle" runat="server" CssClass="button"  Visible="false"
                   Width="120px" Text="Düzenle"  ValidationGroup="zDuzenle" onclick="btnSSSoruGuncelle_Click"/>

                    <asp:Button ID="btnSSSoruKaydet" runat="server" CssClass="button" 
                   Width="120px"                    Text="Kaydet"  ValidationGroup="zDuzenle" onclick="btnSSSoruKaydet_Click"/>

                     <asp:HiddenField ID="hdfId" runat="server" />
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

</asp:Content>

