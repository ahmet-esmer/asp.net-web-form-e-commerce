<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="uyeMailGonder" Codebehind="mail_gonder.aspx.cs" %>

<%@ Register assembly="FredCK.FCKeditorV2" namespace="FredCK.FCKeditorV2" tagprefix="FCKeditorV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">
                                          <asp:Label ID="lblSayfaBaslik" runat="server" ></asp:Label>
                                        </h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

			 
<%-- <asp:Timer ID="tmEposta" Enabled="false"  OnTick="ePostaGonder" runat="server" Interval="3000" />  --%>

  <asp:Timer ID="tmEposta" Enabled="false"  OnTick="ePostaGonder" runat="server" Interval="60000" />
    
<%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tmEposta" />
        </Triggers>

     <ContentTemplate>--%>
    <table cellpadding="0" cellspacing="0" class="ortaTablo" width="100%">
    <tr>
    <td colspan="2">
<%--    <asp:UpdateProgress ID="UpdateProgress1"  runat="server"  AssociatedUpdatePanelID="UpdatePanel1" >
             <ProgressTemplate>
                 <asp:Label ID="Label1" CssClass="txtLabelTd" runat="server" Text="İşlemi Yapılıyor"></asp:Label><br />
                <img src="images/giris.gif" class="girisGif"  style="padding-bottom:15px; padding-top:10px; width:180px;"/>
             </ProgressTemplate>
             </asp:UpdateProgress>--%>
    
    </td>
    <td >
             <div style="padding-bottom:10px; text-align:right;" >
     <asp:Button ID="btnmManuelAc" CssClass="button" Width="170px"  runat="server" 
            Text="Manuel Mail Gönder" onclick="btnmManuelAc_Click" />
        
     <asp:Button ID="btnTopluAc" CssClass="buton"  runat="server" 
            Text="Toplu Mail Gönder" Visible="false"  Width="170px"  onclick="btnTopluAc_Click" />
            
            </div>
    </td>
    </tr>

    <tr>
    <td colspan="3">

    </td>
    </tr>
            <tr>
                <td class="txtLabelTd" style="width: 150px">
                    Alıcı:</td>
                <td class="style3" style="width:410px;">
                   
                    <asp:DropDownList ID="ddlMailGonder" Width="400px" CssClass="text-input small-input" AutoPostBack="true" runat="server" onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
                    <asp:ListItem Value="0" Selected="True" Text=" Mail Gönderme Listesi" ></asp:ListItem>    
                    <asp:ListItem Value="butunUyeler" Text=" Müşteriler ve Üyeler Tamamı"></asp:ListItem>
                    <asp:ListItem Value="siteUyeleri" Text=" Alişveriş Yapmayan Site Üyeleri"></asp:ListItem>
                    <asp:ListItem Value="SiteMusteri" Text=" Alişveriş Yapan Site Üyeler"></asp:ListItem>
                    <asp:ListItem Value="enZiyaret" Text=" Siteyi En Çok Ziyaret Edenler Üyeler"></asp:ListItem>
                    <asp:ListItem Value="mailList"  Text=" Site Mail Listesi"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblAlici" Visible="false" runat="server"></asp:Label>
                        
                     <asp:TextBox ID="txtMailManuel" Visible="false" runat="server" 
                        Width="400px" CssClass="text-input small-input"  ></asp:TextBox>
                
                        </td>
                <td class="style4">
                  
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtMailManuel" CssClass="input-notification error"  ForeColor=""
                            ErrorMessage="Mail Alanını Doldurunuz." ValidationGroup="mail"></asp:RequiredFieldValidator><br />

                        <asp:RequiredFieldValidator ID="RangeValidator3" runat="server" InitialValue="0"  
                            ControlToValidate="ddlMailGonder" CssClass="input-notification error" 
                            ValidationGroup="topluMail"  Display="Dynamic" ForeColor=""
                            ErrorMessage="Lütfen Mail Gönderi  Listesinden Bir Kategori Seçiniz."></asp:RequiredFieldValidator><br />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="txtMailManuel" CssClass="input-notification error"  Display="Dynamic"
                            ErrorMessage="Geçerli E-Posta Adresi Giriniz." ForeColor=""
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                         
                </td>
            </tr>
          
            <tr>
                <td class="txtLabelTd" style="width:150px">
                  Konu:  </td>
                <td style="width:250px;">
                    <asp:TextBox ID="txtKonu" runat="server" Width="400px" CssClass="text-input small-input"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtKonu" CssClass="input-notification error"  ForeColor="" Display="Dynamic"
                        ErrorMessage="*Mail Konu Alanını Doldurunuz." ValidationGroup="mail"></asp:RequiredFieldValidator>
                        
                </td>
                
            </tr>
        </table>
         
 
   
       <table cellpadding="0" cellspacing="0" class="ortaTablo" width="100%">
            <tr  style="width:100%;" id="Tr1" runat="server">
                <td colspan="3">

                   <FCKeditorV2:FCKeditor Width="100%" ID="FCKeditor1" runat="server" Height="400px">
                   </FCKeditorV2:FCKeditor>

                </td>
            </tr>
            <tr>
                <td style="width: 150px">
          
             <asp:Label ID="lblHata" runat="server" ></asp:Label> 
             <asp:HiddenField ID="hdfNo"  Value="0" runat="server" />  
            <asp:HiddenField ID="hdfSayfaNo" Value="0" runat="server" />  
            <asp:HiddenField ID="hdfTolamSayfa" runat="server" />
                </td>
              <td style="padding-bottom:20px; padding-top:15px;">
               
                 
                      <asp:Button ID="btnMailGonder" runat="server"
                            Text="Mail Gönder" Width="150px" CssClass="button"
                            onclick="btnMailGonder_Click" Visible=false ValidationGroup="mail" />
                            
                            <asp:Button ID="btnTopluMail" runat="server"
                            Text="Toplu Mail Gönder" Width="170px" CssClass="button" 
                            onclick="btnTopluMail_Click" ValidationGroup="topluMail" />
                            
                            <asp:Button ID="btnManuelGonder" runat="server"
                            Text="Mail Gönder" Width="150px" CssClass="button"
                             Visible=false onclick="btnManuelGonder_Click" ValidationGroup="mail" />
                  
                  <br />
                 
                        </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr> 
            <td colspan="2" >
             <asp:Literal ID="lblTamami" runat="server"></asp:Literal>

           
                </div>
                </span></span>

            </td>
            </tr>
        </table> 
         <%-- </ContentTemplate>
             </asp:UpdatePanel>--%> 
			
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>
</asp:Content>

