<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" ValidateRequest="false"  Inherits="AdminZiyaretciDefteri" Title="Untitled Page" Codebehind="ziyaretciDefteri.aspx.cs" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">Ziyaretçi Defteri</h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

      <asp:Panel ID="pnlYorumArama" BorderStyle="none" GroupingText=" Yazı Arama Formu " Height="160px" Width="100%" runat="server">
        <table style="width: 100%; margin-top:15px;">
            <tr>
                <td style="width:220px;">
                    <cc1:TextBoxWatermarkExtender TargetControlID="txtTarih_1" WatermarkText="Başlangıç Tarihi Seçiniz.." ID="TextBoxWatermarkExtender2" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                 <asp:TextBox ID="txtTarih_1" runat="server" CssClass="text-input" Width="178px"></asp:TextBox>  &nbsp; &nbsp; İle <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="input-notification error" ControlToValidate="txtTarih_1" runat="server" ValidationGroup="ara" ErrorMessage="Başlanğıç Tarihi Seçiniz.." ForeColor="" ></asp:RequiredFieldValidator>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="cal_Theme1" TargetControlID="txtTarih_1" Format="dd.MM.yyyy" >
                    </cc1:CalendarExtender>
                   </td>
                <td style="width:240px;">
                     <cc1:TextBoxWatermarkExtender TargetControlID="txtTarih_2" WatermarkText="Bitiş Tarihi Seçiniz.." ID="TextBoxWatermarkExtender3" runat="server">
                    </cc1:TextBoxWatermarkExtender>
                 <asp:TextBox ID="txtTarih_2" runat="server" CssClass="text-input" Width="178px"></asp:TextBox>
                 <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="input-notification error" ControlToValidate="txtTarih_2" runat="server" ValidationGroup="ara" ErrorMessage="Bitiş Tarihi Seçiniz.." ForeColor="" ></asp:RequiredFieldValidator>
                 
                    <cc1:CalendarExtender ID="CalendarExtender2"  CssClass="cal_Theme1" runat="server" TargetControlID="txtTarih_2" Format="dd.MM.yyyy"  >
                    </cc1:CalendarExtender>
                    </td>
                <td valign="top" >

                 <asp:Button ID="btnYorumTarihAra" runat="server" CssClass="button" 
                    Height="23px" onclick="btnYorumTarihAra_Click" ValidationGroup="ara" Text="Tarih Aralığı Arama" Width="130px" />
                </td>
            </tr>
        </table>
      </asp:Panel>

      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CellPadding="4" 
       ForeColor="#333333" GridLines="None" Width="100%" onrowcommand="GridView1_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.id")%>
            </ItemTemplate>
            <HeaderStyle  Width="30" Font-Bold="true" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Adı Soyadı">
                <ItemTemplate>
                <%# DataBinder.Eval(Container, "DataItem.adSoyad")%>
                </ItemTemplate>
            <HeaderStyle Width="140" Font-Bold="true" />
            </asp:TemplateField>         
            <asp:TemplateField HeaderText="Yazı">
                <ItemTemplate>
            <%# DataBinder.Eval(Container, "DataItem.yorum")%> 
            </ItemTemplate>
            <HeaderStyle Width="300" Font-Bold="true" />
            </asp:TemplateField>        
            <asp:TemplateField HeaderText="İl / İlçe">
                <ItemTemplate>
                 <%# DataBinder.Eval(Container, "DataItem.sehirAd ")%> / <%# DataBinder.Eval(Container, "DataItem.ilceAd")%> 
            </ItemTemplate>
            <HeaderStyle Width="150" Font-Bold="true" />
            </asp:TemplateField>        
            <asp:TemplateField HeaderText="Tarih">
            <ItemTemplate>                           
            <%# BusinessLayer.DateFormat.TarihSaat(DataBinder.Eval(Container, "DataItem.eklenmeTarihi").ToString())%>
            </ItemTemplate>
            <HeaderStyle  Width="130" Font-Bold="true" />
            </asp:TemplateField>   
            <asp:TemplateField HeaderText="Detay">
             <ItemTemplate>
<%-- <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&islem=düzenle' > <img src="images/duzenle.gif" alt="Düzenle" border="0" /> </a> --%>
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
            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                    </a>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
            <HeaderStyle Width="20" Font-Bold="true" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sil">
                <ItemTemplate>
                    <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&' 
                        onclick=" return silKontrol()" title="Soruyu Sil"><img src="images/sil.gif" border="0"  /> </a>
                </ItemTemplate>
                <HeaderStyle  Width="30" Font-Bold="true" />
            </asp:TemplateField>
        </Columns>
        <RowStyle CssClass="RowStyle" />
        <HeaderStyle CssClass="HeaderStyle" />
        <AlternatingRowStyle CssClass="AlternatingRowStyle" />
    </asp:GridView>

     <div class="pagination">
         <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
     </div> 
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
                
                
                
				 <p>
                     <label>Adı Soyadı: </label>
                     <asp:TextBox ID="txtZiyaretciAd"  runat="server" CssClass="text-input " Width="390px" ></asp:TextBox>
        
                 </p>
                 <p>
                    <label> İl / İlçe : 
                    <asp:Label ID="lbSehir" runat="server" CssClass="secenek" Font-Size="12px"></asp:Label>
                    <asp:Label ID="Label1" runat="server" CssClass="secenek" Text=" / " Font-Size="12px"></asp:Label>&nbsp;
                    <asp:Label ID="lblIlce" runat="server" CssClass="secenek" Font-Size="12px"></asp:Label></label>
                 </p>
                 <p>
                    <label>Ziyaretçi Yazı: </label>
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
                   <asp:Button ID="btnZiyaretciYorumGuncelle" runat="server" CssClass="button" 
                   Width="120px"                    Text="Kaydet"  ValidationGroup="zDuzenle" onclick="btnZiyaretciYorumGuncelle_Click"/>
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

