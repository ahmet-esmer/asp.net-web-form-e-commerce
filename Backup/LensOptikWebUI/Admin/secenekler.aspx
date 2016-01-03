<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="True" Inherits="admin_secenekler"  Codebehind="secenekler.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		 <h3 style="cursor: s-resize;">
            Seçenaklaer
         </h3>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

        <asp:UpdatePanel ID="upAnket" runat="server">
        <ContentTemplate>

            <div style="padding-left:150px;" >
                 <asp:DropDownList ID="ddlSecenekler" runat="server" Width="350px"
                    AutoPostBack="true" CssClass="uyuForm" onselectedindexchanged="ddlSecenekSelectedIndexChanged">

                    <asp:ListItem Text="Seçenek Başlığı Seçiniz "  Value="null" Selected="True"> </asp:ListItem> 
                    <asp:ListItem Text="Hediye Renk" Value="HediyeRenk" > </asp:ListItem> 
                    <asp:ListItem Text="Aks"  Value="Aks" > </asp:ListItem> 
                    <asp:ListItem Text="Bc" Value="Bc" > </asp:ListItem> 
                    <asp:ListItem Text="Dia" Value="Dia" > </asp:ListItem> 
                    <asp:ListItem Text="Dioptri" Value="Dioptri" > </asp:ListItem> 
                    <asp:ListItem Text="Renk" Value="Renk" > </asp:ListItem> 
                    <asp:ListItem Text="Silindirik" Value="Silindirik" > </asp:ListItem> 
               
                  </asp:DropDownList>
            </div>
    <table align="center" cellpadding="0" cellspacing="0" style="height:auto; width:100%; padding-bottom:20px; padding-top:15px;">
        <tr runat="server" id="trKaydet" visible="false" >
            <td class="txtLabelTd" style="width:150px;height:50px;">
                <label> Seçenek Ekle:</label>
            </td>
            <td style="width:370px; " >
                <asp:TextBox ID="txtSecenek" runat="server" CssClass="text-input" Width="350px" ></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvSecenek" runat="server" 
                    ControlToValidate="txtSecenek" CssClass="input-notification error" Display="Dynamic" 
                    ErrorMessage="Lütfen Seçenek Alanını Doldurunuz." ValidationGroup="secenek"
                    ForeColor="" ></asp:RequiredFieldValidator>
            </td>
            <td >
                <asp:Button ID="btnSecenekEkle" runat="server" CssClass="button" 
                    Font-Size="12px" onclick="btnSecenekEkle_Click" Text="Seçenek Ekle" 
                    ValidationGroup="secenek" Width="110px" />
            </td>
        </tr>
        <tr>
            <td class="txtLabelTd" colspan="3">      
            <asp:GridView ID="gvwSecenek" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" 
                    onrowcommand="gvwSecenek_RowCommand" DataKeyNames="Value"     >
                    <Columns>
                        <asp:TemplateField HeaderText="No" >
                            <ItemTemplate>
                               <label><%# Container.DataItemIndex + 1 %> </label>
                            </ItemTemplate>
                            <ItemStyle Width="50px" CssClass="anketGrig"  />
                            <HeaderStyle Font-Bold="true" BackColor="AliceBlue" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Grup" >
                            <ItemTemplate>
                             <%# Eval("Name") %>  
                            </ItemTemplate>
                            <ItemStyle  Width="100px" CssClass="anketGrig"  />
                            <HeaderStyle Font-Bold="true" BackColor="AliceBlue" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Seçenek" >
                            <ItemTemplate>
                                <asp:TextBox ID="txtSecenekDuzenle" runat="server"
                                  CssClass="text-input"  Width="350px"
                                  Text='<%# Eval("Value")%>' ToolTip="Düzenle" ></asp:TextBox>

                            </ItemTemplate> 
                            <ItemStyle Width="370px" CssClass="anketGrig" />  
                             <HeaderStyle Font-Bold="true" BackColor="AliceBlue" />                   
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Düzenle">
                            <ItemTemplate>

                                <asp:ImageButton  ToolTip="Degişikligi Kaydet"
                                 ID="btnSoruKaydet" CommandName="SecenekKaydet"
                                 ImageUrl="~/admin/images/kaydet.gif"
                                 CommandArgument='<%# Eval("Value") %>' runat="server" />

                            </ItemTemplate> 
                            <ItemStyle Width="50px" CssClass="anketGrig" />  
                             <HeaderStyle Font-Bold="true" BackColor="AliceBlue" />
                        </asp:TemplateField>
                
                        <asp:TemplateField  HeaderText="Sil" >
                            <ItemTemplate>

                            <asp:ImageButton  ToolTip="Sil"
                                 ID="btnSecenekSil" CommandName="SecenekSil"
                                 ImageUrl="~/admin/images/sil.gif"
                                 CommandArgument='<%# Eval("Value") %>' runat="server" />

                            </ItemTemplate>
                            <ItemStyle Width="60px" HorizontalAlign="Left" CssClass="anketGrig" />  
                             <HeaderStyle Font-Bold="true" BackColor="AliceBlue" />
                        </asp:TemplateField>
                    </Columns>
             
                </asp:GridView>
           </td>
        </tr>
       
    </table>

        </ContentTemplate>
        </asp:UpdatePanel>	
           	
		</div> <!-- End #tab1 --> 
		</div> <!-- End .content-box-content -->
</div>

</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">


</asp:Content>


