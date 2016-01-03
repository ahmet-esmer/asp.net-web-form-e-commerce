<%@ Control Language="C#" AutoEventWireup="true" Inherits="IncludeMarkalarFilitre" Codebehind="MarkalarFilitre.ascx.cs" %>
<div class="panelBaslik"><span class="baslik"> MARKALAR</span> </div> 
     <div class="panelIcerik">       
       <asp:Repeater ID="rptMarkalar" runat="server" onitemdatabound="rptMarkalar_ItemDataBound">
        <HeaderTemplate>
			        
        <ul class="kategoriler">
        </HeaderTemplate>
        <ItemTemplate>
            <li><h4>
                <asp:Hyperlink 
                runat="server" Text=<%# Eval("marka_adi").ToString().ToUpper() %>
                NavigateUrl=<%# HttpContext.Current.Request.Url.AbsolutePath.ToString() + "?markaId=" + Eval("id") %>
                ID="hlMarka" CssClass="kategori"/>
                <asp:HiddenField ID="hdfMarkaId" Value=<%# Eval("id")%> runat="server"   />
            </h4></li>
        </ItemTemplate>
        <FooterTemplate>
		</ul>
        </FooterTemplate>
        </asp:Repeater>
    </div>