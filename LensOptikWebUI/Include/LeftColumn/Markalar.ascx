<%@ Control Language="C#" AutoEventWireup="true" Inherits="IncludeMarkalar" Codebehind="Markalar.ascx.cs" %>


<div class="panelBaslik"><span class="baslik">MARKALAR</span> </div> 
     <div class="panelIcerik">       
       <asp:Repeater ID="rptMarkalar" runat="server" onitemdatabound="rptMarkalar_ItemDataBound">
            <HeaderTemplate>
            <ul class="kategoriler">
            </HeaderTemplate>
            <ItemTemplate>
                <li><h4>
                <asp:Hyperlink runat="server" Text='<%# Eval("marka_adi").ToString().ToUpper() %>' 
                NavigateUrl='<%# BusinessLayer.LinkBulding.Marka(Eval("id"), Eval("marka_adi"))%>'
                ID="hlMarka" CssClass="kategori"/>
                <asp:HiddenField ID="hdfMarkaId" Value='<%# Eval("id")%>' runat="server" />
                </h4></li>
            </ItemTemplate>
            <FooterTemplate>
			</ul>
            
            </FooterTemplate>
        </asp:Repeater>
  </div>