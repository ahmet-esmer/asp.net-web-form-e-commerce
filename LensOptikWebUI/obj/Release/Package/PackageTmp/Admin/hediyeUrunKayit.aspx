<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="True" Inherits="admin_Hediye_Urun_Kayit" Codebehind="hediyeUrunKayit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  

    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">Hediye Kampanya Kayıt</h3>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">
	  
         <p>
           <label>Hediye Ürün Başlık </label>
           <asp:TextBox ID="txtHediyeBaslık" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtHediyeBaslık" CssClass="input-notification error" 
                Display="Dynamic" ValidationGroup="hediye" ForeColor=""
                ErrorMessage="Lütfen kampanya başlığı yazınız."  >
                </asp:RequiredFieldValidator><br />
                  
         </p>
         <p>
           <label>Seçileck Ürün Adet</label>

             <asp:DropDownList ID="ddlLimit" runat="server" CssClass="text-input" >
                 <asp:ListItem Value="0" >Ürün Limiti </asp:ListItem>
                 <asp:ListItem Value="1" >1 </asp:ListItem>
                 <asp:ListItem Value="2" >2 </asp:ListItem>
                 <asp:ListItem Value="3" >3 </asp:ListItem>
                 <asp:ListItem Value="4" >4 </asp:ListItem>
                 <asp:ListItem Value="5" >5 </asp:ListItem>
                 <asp:ListItem Value="6" >6 </asp:ListItem>
                 <asp:ListItem Value="7" >7 </asp:ListItem>
                 <asp:ListItem Value="8" >8 </asp:ListItem>
                 <asp:ListItem Value="9" >9 </asp:ListItem>
                 <asp:ListItem Value="10" >10 </asp:ListItem>
             </asp:DropDownList>

           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="ddlLimit" CssClass="input-notification error" 
                Display="Dynamic" ValidationGroup="hediye" ForeColor="" InitialValue="0"
                ErrorMessage="Lütfen seçileck ürün adeti giriniz"  >
                </asp:RequiredFieldValidator><br />
         </p>
          <p>
         <label>Resim</label>
         <asp:FileUpload ID="fluKampanya" runat="server" />
         <asp:Image ID="imgKampanya" runat="server" />

         </p>
         <p>
         <label>Durum</label>
         <asp:CheckBox ID="ckbDurum" runat="server" />

         </p>
         <p>
           <asp:Button ID="btnHediyeBaslik" runat="server" CssClass="button" Font-Size="12px" 
              Text="Hediye Başlık Kaydet ► " 
                 ValidationGroup="hediye" Width="172px" 
        onclick="btnHediyeBaslik_Click" />

        <asp:Button ID="btnHediyeBaslikGuncele" runat="server" CssClass="button" Font-Size="12px" 
              Text="Hediye Başlık Düzenle" Visible="false" 
                 ValidationGroup="hediye" Width="172px" 
                 onclick="btnHediyeBaslikGuncele_Click" />

         </p>

        
		</div> <!-- End #tab1 -->
		<div style="display: none;" class="tab-content" id="tab2">
         
      
     				
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>


</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">


</asp:Content>


