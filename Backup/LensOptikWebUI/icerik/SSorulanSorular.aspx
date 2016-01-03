<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Icerik_SSorulanSorular" ViewStateMode="Disabled" Codebehind="SSorulanSorular.aspx.cs" %>

<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

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
        <!-- Sık sorulan sorular -->

        <asp:Repeater ID="rptSSorulari" runat="server">
         <HeaderTemplate>
            <div class="icerik-Baslik zBaslik"><h5>Sık Sorulan Sorular</h5></div>
            <div class="icerik-Baslik yorumEkle">
          </div>
        </HeaderTemplate>
        <ItemTemplate>
        <div id='<%# DataBinder.Eval(Container, "DataItem.id")%>'  class="soruBaslik" >
       <span class="yazi1"><b>Soru: </b></span><%# DataBinder.Eval(Container, "DataItem.soru")%> 
        </div>
         <div class="yorum">
        <span class="yazi1"><b>Cevap: </b></span> <%# DataBinder.Eval(Container, "DataItem.cevap")%> 
        </div>
               
        </ItemTemplate>
        </asp:Repeater>

          <div class="pagination">
                <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
            </div> 
         <!-- sık sorulan sorular sonu -->
        </div>
    </div>

       <script type="text/javascript" >

           $('#ortaSag').corner("round 6px");
           $('#ortaSagIc').corner("round 6px");


           $(document).ready(function () {

               var go = getQuerystring('goto');

               if (go != null) {

                   $("#" + go).slideto({ highlight_color: '#0f96e7', highlight_duration: 10000 });
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

