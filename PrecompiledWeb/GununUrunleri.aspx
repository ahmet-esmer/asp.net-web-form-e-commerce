<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="gunun_urunleri" ViewStateMode="Disabled" Codebehind="GununUrunleri.aspx.cs" %>

<%@ Register src="Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>
<%@ Register src="Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc7" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<link rel="stylesheet" href="../Style/MarketGenel.css" type="text/css" />

    <style type="text/css">
    
    .gUrunDiv
    {
        margin:10px 0px;    
        border-top:1px solid #bebdbd;
    }
    
    .gResimDiv
    {
      float:left;
      display:inline;  
      width:150px;
      height:130px;
      text-align:center;
    }
    
    .gBilgiDiv
    {
       float:left;
       display:inline;
    }
    
    .gBilgi1
    {
       float:left;
       display:inline;
       width:250px;
    }
    
    .gBilgi2
    {
       float:left;
       display:inline;
       width:80px;
       height:80px;
       padding-top:30px;
       padding-left:22px;
       color:#FFF;
       background: url(Images/Icons/indirimBg.jpg) no-repeat 0px 15px;
    }
    
    .gBilgi2Miktar
    {
      font-size:23px; 
    }
    
    .gBilgiIdirim
    {
    margin-left:-5px;
    font-weight:bold;
    font-size:11px;
    }
    
    
     .gBilgi3
    {
       float:left;
       display:inline;
       width:180px;
       text-align:center;
    }
    .asilFiyat
    {
    font-size:15px;
    color:#ff5300;
    text-decoration:line-through;
    padding:6px 0px;
    display:block;

    }
    
    .indirimYazi
    {
     display:inline;
     float:left;
     padding-top:3px;
     color:#222;
     float:left;
     
    }
    
    .indirmliFiyat
    {
       display:inline;
       float:left;
       font-size:15px;
       display:block;
       font-weight:bold;
       color:#222;
    }
    
    .detay
    {
      margin-top:50px;  
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
     <!-- sol panel -->
     <div  id="solPanel" >
         <uc3:Kategoriler ID="Kategoriler1" runat="server" />
         <uc4:Markalar ID="Markalar1" runat="server" />
         <uc5:Facebook ID="Facebook1" runat="server" />
         <uc6:SSSorular ID="SSSorular1" runat="server" />
    </div>
    <!-- sol panel sonu -->

    <div id="ortaSag">
        <div id="ortaSagIc"> 
        <!-- Günün Ürünleri-->
         <table style="width:100%; margin-bottom:30px;" >
            <tr>
                <td class="icerik-Baslik"   >
                      <h5> Gelecek Günlere Ait Ürünler </h5>  
                </td>
            </tr>
              <tr>
                <td>
                    <div style="width:650px;padding:10px;" >  
                        <uc7:Mesajlar ID="Mesaj" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater ID="rptGunuUrunleri" runat="server">
                    <ItemTemplate>
                      <div class="gUrunDiv" >
                        <div class="gResimDiv" >

                       <a href="<%# ResolveUrl(Eval("Link").ToString()) %>" > 
                       <img onload="ResizeImage(this,130,130);" src="<%# ResolveUrl("~/Products/Little/"+ Eval("ResimAdi").ToString()) %>" alt="<%# Eval("UrunAdi").ToString()%>" />
                       </a> 
       
                         </div>
                         </div>
                         <div class="gBilgiDiv" >
                             <div class="gBilgi1" >
                              <div class="" > <h5><%# Eval("UrunAdi") %> </h5> </div> 
                              <div class="" > <%# Eval("Aciklama")%> </div>
                              
                              <span class="asilFiyat" >
                              <%# Convert.ToDecimal(Eval("UrunFiyat")).ToString("N")%> <%# Eval("Doviz")%>
                                <%# BusinessLayer.AritmetikIslemler.UrunKDV(Convert.ToInt32(Eval("KDV"))) %>
                              </span>
                               
                              <div>
                                  <span class="indirimYazi"> Günün Fiyatı:&nbsp;  </span>
                                  <span class="indirmliFiyat" > 
                                  <%# Convert.ToDecimal(Eval("SatisFiyat")).ToString("N")%> <%# Eval("Doviz")%> 
                                  <%# BusinessLayer.AritmetikIslemler.UrunKDV(Convert.ToInt32(Eval("KDV"))) %>
                                  </span>
                              </div>
                             

                             </div>
                             <div class="gBilgi2" >
                              %<span  class="gBilgi2Miktar" ><%# Eval("IndirimYuzde")%></span><br /><span class="gBilgiIdirim" >İNDİRİM</span> 
                             </div>
                             <div class="gBilgi3" >
                               <h6> <%# BusinessLayer.DateFormat.TarihGun(Eval("Tarih").ToString())%> </h6> 
            
                                <div class="detay" >
                                <a href="<%# ResolveUrl(Eval("Link").ToString()) %>" > 
                               <img src="<%# ResolveUrl("~/images/icons/UrunDetay.jpg") %>" alt="Detay" />
                               </a>
                                </div>
                               

                             </div>

                         </div>
                         <div class="clear"></div>
                     </div>
                    </ItemTemplate>
                    </asp:Repeater>
               </td>
            </tr>
            </table>
            <div class="clear" >
            </div>
        <!-- Siparişler sonu -->
        </div>
    </div>



          <script type="text/javascript" >

              $("#ortaSag").corner("round 6px");
              $("#ortaSagIc").corner("round 6px");





              $(document).ready(function () {

                  var go = getQuerystring("goto");

                  if (go != null) {

                      $(".icerik-Baslik").slideto({ highlight_color: "#0f96e7", highlight_duration: 10000 });
                  }

                  function getQuerystring(key, default_) {
                      if (default_ == null) default_ = "";
                      key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                      var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
                      var qs = regex.exec(window.location.href);
                      if (qs == null)
                          return default_;
                      else
                          return qs[1];
                  }

              });

       </script>

</asp:Content>

