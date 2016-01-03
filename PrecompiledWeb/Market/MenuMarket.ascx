<%@ Control Language="C#" AutoEventWireup="true" Inherits="Market_MarketMenu" Codebehind="MenuMarket.ascx.cs" %>


<asp:HyperLink ID="hlSepet" CssClass="marketLink" NavigateUrl="~/Market/Sepet.aspx" ClientIDMode="Static" runat="server">
Sepet</asp:HyperLink>

<asp:HyperLink ID="hlAdres" CssClass="marketLink" NavigateUrl="~/Market/TeslimatBilgisi.aspx" ClientIDMode="Static"
 Enabled="false" runat="server">Teslimat/Fatura Bilgileri</asp:HyperLink>

<asp:HyperLink ID="hlOnay" CssClass="marketLink" NavigateUrl="~/Market/IslemOnay.aspx" ClientIDMode="Static" Enabled="false" runat="server">
Onay</asp:HyperLink>

<asp:HyperLink ID="hlOdeme" CssClass="marketLink" NavigateUrl="~/Market/Odeme.aspx" ClientIDMode="Static" Enabled="false" runat="server">
Ödeme</asp:HyperLink>

<asp:HyperLink ID="hlOzet" CssClass="marketLink" ClientIDMode="Static" Enabled="false" runat="server">
Sipariş Özeti</asp:HyperLink>

<div style="clear:both;"></div>

 