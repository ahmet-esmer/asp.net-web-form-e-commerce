<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="True" Inherits="adminAnket" Title="Untitled Page" Codebehind="anketler.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  

    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">
            Anketler
         </h3>
		<ul style="display: block;" class="content-box-tabs">
			<li><a href="#tab1" class="default-tab current">Anketler</a></li> 
			<li><a href="#tab2">Anket Ekle</a></li>
		</ul>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">
	  
            <asp:GridView ID="gvwAnketler" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" Width="100%" >
                       <Columns>
                           <asp:TemplateField HeaderText="Sıra">
                               <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                               <HeaderStyle  Width="20px" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Anket Başlık Adı">
                               <ItemTemplate>
                                <%# DataBinder.Eval(Container, "DataItem.anketBaslik")%>
                               </ItemTemplate>
                               <HeaderStyle Width="400px" Font-Bold="true" />
                           </asp:TemplateField>
                            <asp:TemplateField HeaderText="Toplam Oylama">
                               <ItemTemplate>
                                 <%#DataBinder.Eval(Container, "DataItem.oylanma")%>
                               </ItemTemplate>
                               <HeaderStyle Width="150px" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Eklenme Tarih ">
                               <ItemTemplate>
                                 <%# BusinessLayer.DateFormat.Tarih( DataBinder.Eval(Container, "DataItem.tarih").ToString())%>
                               </ItemTemplate>
                               <HeaderStyle Width="150px" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Dur.">
                               <ItemTemplate>
                                   <a title=" <%# DataBinder.Eval(Container, "DataItem.anketBaslik")%>  Anketi  <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.id") %>&amp;islem=durum&amp;durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="duzen" />
                               <HeaderStyle Width="30px" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Düz.">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=duzenle&ad=<%# DataBinder.Eval(Container, "DataItem.anketBaslik")%> ' title=" <%# DataBinder.Eval(Container, "DataItem.anketBaslik")%> Anketi Düzenle">
                                    <img src="images/duzenle.gif" alt="Düzenle" border="0" /> </a>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="20px" Font-Bold="true" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sil">
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.id") %>&amp;islem=sil&' 
                                       onclick=" return silKontrol()" title=" <%# DataBinder.Eval(Container, "DataItem.anketBaslik")%>  Anketi Sil"><img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <HeaderStyle  Width="30px" Font-Bold="true" />
                           </asp:TemplateField>
                       </Columns>
                   <RowStyle  CssClass="RowStyle" />
                     
                       <SelectedRowStyle   />
                       <HeaderStyle CssClass="HeaderStyle" />
                       <EditRowStyle />
                       <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>
      			
		</div> <!-- End #tab1 -->
		<div style="display: none;" class="tab-content" id="tab2">
            <asp:UpdatePanel ID="upAnket" runat="server">
            <ContentTemplate>
            
	        <asp:Panel ID="pnlAnketEkle"  runat="server">
        <table align="center" cellpadding="0" cellspacing="0" style="height:auto; width:100%; padding-bottom:20px; padding-top:15px;">
            <tr>
                <td  valign="top"  style="width:150px;" >
                  <label>Anket Başlık Adı: </label>
                </td>
                <td  style="width: 414px" valign="top">
                    <asp:TextBox ID="txtAnketAdi" runat="server" CssClass="text-input medium-input" ></asp:TextBox>  
                    <br />
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtAnketAdi" CssClass="input-notification error"  Display="Dynamic" ValidationGroup="anketBaslik" ErrorMessage="Lütfen Anket Adı Alanını Doldurunuz." ForeColor="" ></asp:RequiredFieldValidator>
               </td>
                <td valign="top" >
                <asp:Button ID="btnAnketBaslikEkle" runat="server" CssClass="button" 
                        Font-Size="12px"  Text="Anket Başlık Ekle" ValidationGroup="anketBaslik"
                        Width="172px" onclick="btnAnketBaslikEkle_Click"  />
                   
                </td>
            </tr>
        </table>
        </asp:Panel>
  
            <asp:Panel ID="pnlAnketDuzenle" Visible="false" runat="server">
      <table align="center" cellpadding="0" cellspacing="0" style="height:auto; width:100%; padding-bottom:20px; padding-top:15px;">
            <tr>
                <td class="txtLabelTd" style="width:150px;">
                    <label> Anket Başlık Adı: </label>
                </td>
                <td style="width:414px" valign="top" >
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtAnketDuzenle" CssClass="input-notification error" Display="Dynamic" ValidationGroup="anketBaslik" ErrorMessage="Lütfen Anket Adı Yazınız." ForeColor="" ></asp:RequiredFieldValidator><br />
                    <asp:TextBox ID="txtAnketDuzenle" runat="server" CssClass="text-input medium-input"  AutoPostBack="True" ontextchanged="txtAnketDuzenle_TextChanged" ></asp:TextBox>
                  
               </td>
                <td>
                
                <asp:HiddenField ID="hdfAnketId" runat="server" />
                
                </td>
                
            </tr>
          
               <tr>
                <td class="txtLabelTd" colspan="3">
                
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CellPadding="4" 
                       ForeColor="#333333" GridLines="None" Width="100%"
                        onrowcommand="GridView2_RowCommand"  DataKeyNames="soruId"     >
                       <Columns>
                           <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                              <label>  Anket Soru: <%# Container.DataItemIndex + 1 %> </label>
                               </ItemTemplate>
                               <ItemStyle  Width="115px" CssClass="anketGrig"  />
                   
                           </asp:TemplateField>
                           <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                                 <asp:TextBox ID="txtAnketDuzenle" runat="server"  CssClass="text-input medium-input" Text='<%# Eval("soru")%>' ToolTip="Anket Soru Düzenle" ></asp:TextBox>
                               </ItemTemplate> 
                               <ItemStyle Width="400px" CssClass="anketGrig" />                     
                           </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                                 <asp:TextBox ID="txtAnketOylama" runat="server" Width="50px" CssClass="text-input" Text='<%# Eval("anketOy")%>' ToolTip="Anket Oylama Düzenle" ></asp:TextBox>
                               </ItemTemplate> 
                               <ItemStyle Width="50px" CssClass="anketGrig" />                     
                           </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                                   <asp:ImageButton  ToolTip="Soru Degişikligini Kaydet" ID="btnSoruKaydet" CommandName="SoruKaydet" ImageUrl="~/admin/images/kaydet.gif" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.soruId") %>' runat="server" />
                               </ItemTemplate> 
                               <ItemStyle Width="40px" CssClass="anketGrig" />  
                           </asp:TemplateField>
                           <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                                   <a title=" <%# DataBinder.Eval(Container, "DataItem.soru")%>  Soru  <%# BusinessLayer.GenelFonksiyonlar.DurumYazi(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>" href='?id=<%# DataBinder.Eval(Container, "DataItem.soruId") %>&islem=anketSoru&durum=<%# Convert.ToInt32(!Convert.ToBoolean( DataBinder.Eval(Container,"DataItem.durum"))) %>'>
                            <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(DataBinder.Eval(Container,"DataItem.durum"))) %>
                                   </a>
                               </ItemTemplate>
                               <ItemStyle Width="27px" CssClass="anketGrig" />     
                           </asp:TemplateField>
                           <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                                   <a href='?id=<%# DataBinder.Eval( Container, "DataItem.soruId") %>&anketSoru=sil&' 
                                       onclick=" return silKontrol()" title=" <%# DataBinder.Eval(Container, "DataItem.soru")%>  Soru Sil"><img src="images/sil.gif" border="0"  /> </a>
                               </ItemTemplate>
                               <ItemStyle Width="30px" CssClass="anketGrig" />  
                           </asp:TemplateField>
                           <asp:TemplateField ShowHeader="false" >
                               <ItemTemplate>
                             
                               </ItemTemplate>
                               <ItemStyle Width="230px" CssClass="anketGrig" />  
                           </asp:TemplateField>
                       </Columns>
             
                   </asp:GridView>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="txtLabelTd" style="width:150px;">
                  <label> Anket Soru Ekle:</label>
                </td>
                <td style="width:414px" valign="top">
                    <asp:TextBox ID="txtAnketSoru" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtAnketSoru" CssClass="input-notification error" Display="Dynamic" ErrorMessage="Lütfen Anket Soru Alanını Doldurunuz." ValidationGroup="AnketSoru" ForeColor="" ></asp:RequiredFieldValidator>
                </td>
                <td valign="top">
                    <asp:Button ID="btnAnketSoruEkle" runat="server" CssClass="button" 
                        Font-Size="12px" onclick="btnAnketSoruEkle_Click" Text="Anket Sorusu Ekle" 
                        ValidationGroup="AnketSoru" Width="172px" />
                </td>
            </tr>
            <tr>
                <td class="txtLabelTd">
                    &nbsp;</td>
                <td colspan="2">
                   
                </td>
            </tr>
        </table>
    </asp:Panel>

            </ContentTemplate>
            </asp:UpdatePanel>	
		<div class="clear"></div><!-- End .clear -->
		</div> <!-- End #tab2 -->    
		</div> <!-- End .content-box-content -->
</div>

</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">


</asp:Content>


