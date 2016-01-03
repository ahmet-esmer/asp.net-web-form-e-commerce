<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Icerik_RenkSecim" Codebehind="RenkSecim.aspx.cs" %>
<%@ Register src="../../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

<title>Renk Seçimi</title>
<meta name="description" content="Renk Seçimi,renkli lens,lens,göz rengini değiştir,göz rengi seç,mavi göz,gri, hareli,haresiz,göz rengini deneyin,göz rengini seçin,renkleri deneyin." />
<meta name="keywords" content="Renk Seçimi" />

<style type="text/css" >

    #icerikDiv
    {  
     padding:15px;
     text-align:center;
    }


</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">


    <!-- Günün ürünü -->
         <uc1:GununUrunu ID="GununUrunu1" runat="server" />
     <!-- Günün ürünü sonu -->

     <!-- banner -->
          <uc2:Banner ID="Banner1" runat="server" />
     <!-- baner sonu -->

   <div id="ortaTam">
        <div id="ortaTamIc"> 
          <div class="icerik-Baslik"  >
                  <h5> <asp:Literal ID="ltlBaslik" runat="server"></asp:Literal></h5>   
          </div>
            <div id="icerikDiv" >
     
            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/...version=5,0,0,0" width="750" height="550" >
               <param name="wmode" value="transparent">
               <param name="movie" value="<%: ResolveUrl("~/icerik/Renk/icer.swf") %>">
               <param name="quality" value="high">
               <embed src="<%: ResolveUrl("~/icerik/Renk/icer.swf") %>" quality="high" pluginspage="http://www.macromedia.com/shockwave...=ShockwaveFlash" type="application/x-shockwave-flash" width="750" height="550" wmode="transparent"></object>


               <asp:Literal ID="ltlSayfaIcerik" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

      <script type="text/javascript" >
          $('#ortaTam').corner("round 6px");
          $('#ortaTamIc').corner("round 6px");
     </script>
</asp:Content>

