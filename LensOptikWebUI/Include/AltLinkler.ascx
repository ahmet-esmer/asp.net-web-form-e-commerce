<%@ Control Language="C#" AutoEventWireup="true" Inherits="include_altLinkler" Codebehind="AltLinkler.ascx.cs" %>

     <div id="altLink1" >
          <asp:Repeater ID="rptAltMenu" runat="server"  >
          <ItemTemplate>
             <h3><a href="<%# ResolveUrl(Eval("link").ToString())%>" class="altMenu" ><%# Eval("kategoriadi")%></a></h3>
          </ItemTemplate>
          </asp:Repeater>
           <h3><a href="<%: ResolveUrl("~/icerik/Renk/RenkSecim.aspx")%>" class="altMenu" >Renk Seçimi</a></h3>
     </div>
     <div id="altLink2">
     </div>
     <div id="altLink3">
        <div class="mailYazi" style="margin-bottom:17px;" >Kampanyalarımızdan ve sitemizdeki yeniliklerden  haberdar olmak içi; </div>
        <div id="mailTxtDiv" >
            <asp:TextBox ID="txtMail" Text="E-Posta Adresiniz." ClientIDMode="Static" runat="server"></asp:TextBox>
        </div>
        <div id="mailBtnDiv" >
            <a id="hlMail" onclick="MailKaydet('<%: ResolveUrl("~/Include/Ajax/MailKayit.aspx/Kayit") %>');" > ► </a>
        </div>
     </div>

     <!--Start of Zopim Live Chat Script-->
<script type="text/javascript">
    window.$zopim || (function (d, s) {
        var z = $zopim = function (c) { z._.push(c) }, $ = z.s =
d.createElement(s), e = d.getElementsByTagName(s)[0]; z.set = function (o) {
    z.set.
_.push(o)
}; z._ = []; z.set._ = []; $.async = !0; $.setAttribute('charset', 'utf-8');
        $.src = '//cdn.zopim.com/?Cm4rIFLPjyUQmDe9ezxLZ9dVHx5vEp7x'; z.t = +new Date; $.
type = 'text/javascript'; e.parentNode.insertBefore($, e)
    })(document, 'script');
</script>
<!--End of Zopim Live Chat Script-->