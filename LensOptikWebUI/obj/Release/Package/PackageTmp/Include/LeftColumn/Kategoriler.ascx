<%@ Control Language="C#" AutoEventWireup="true" Inherits="Include_LeftColumn_Kategoriler" Codebehind="Kategoriler.ascx.cs" %>


   <div class="panelBaslik"><span class="baslik">KATEGORİLER</span></div> 
   <div class="panelIcerik">     
        <div class="sidebarmenu">
            <ul id="sidebarmenu1">
        <asp:Repeater ID="rptAnakategoriler" runat="server" onitemdatabound="rptAnakategoriler_ItemDataBound">
        <ItemTemplate>
	    <li><a href="<%# ResolveUrl(BusinessLayer.LinkBulding.Kategori(Eval("title"),Eval("kategoriadi"), Eval("serial"))) %>"><%# Eval("kategoriadi").ToString().ToUpper()%> </a>
            <asp:HiddenField ID="hdfSerial" Value='<%# Eval("serial")%>' runat="server" />
            <asp:Repeater ID="rptAltKategori" runat="server" >
                <HeaderTemplate>
                <ul>
                </HeaderTemplate>
                <ItemTemplate>
                <li><h1><a href="<%# ResolveUrl(BusinessLayer.LinkBulding.Kategori(Eval("title"),Eval("kategoriadi"), Eval("serial")))%>"><%# Eval("kategoriadi").ToString().ToUpper()%></a></h1></li>
                </ItemTemplate>
                <FooterTemplate>
                </ul>
                </FooterTemplate>
             </asp:Repeater>		        
	    </li>
        </ItemTemplate>
        </asp:Repeater>
            </ul>
        </div>
       <div style="clear:both; margin-bottom:5px; " ></div>    
   </div> 