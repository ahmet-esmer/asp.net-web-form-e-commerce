<%@ Control Language="C#" AutoEventWireup="true" Inherits="include_hediye_kampanya" Codebehind="HediyeKampanya.ascx.cs" %>
  <div id="pnlKampanya"  visible="false" runat="server" clientidmode="Static"  >
     
      <asp:HyperLink ID="hlHediye" runat="server" ToolTip="Kampanyalı Ürünler" ClientIDMode="Static" >
          <asp:Image ID="imgHediyeKampanya" ToolTip="Hediye Kampanyalar" runat="server" ClientIDMode="Static" /> 
      </asp:HyperLink>
      
     <div id="close" >
      <a href="#" rel="nofollow" title="Kapat" >Kapat</a>
     </div>             
 </div>