<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="kampanyalar" ViewStateMode="Disabled" Codebehind="Kampanyalar.aspx.cs" %>

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
    
       .imgDiv
       {
           padding:3px;
           max-width:670px;
           overflow:hidden;
           background-color:#fafafa;
           border:1px solid #bebdbd;
           margin:15px 5px;
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
                <td class="icerik-Baslik" >
                    <h5> Kampanyalar </h5>  
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
                    <asp:Repeater ID="rptKampanyalar" runat="server">
                    <ItemTemplate>
                    <div class="imgDiv"     >

                    <a href="<%# ResolveUrl("~/") + Eval("Link") %>">
                          
                    <img src="<%# ResolveUrl("~/Products/Big/") %><%# Eval("Resim") %>" width="100%"  alt="Kampanya <%# Eval("Id") %>" />
                    </a>

                     </div>
                    </ItemTemplate>
                    </asp:Repeater>
               </td>
            </tr>
            </table>
            <div class="clear" >
            </div>
        </div>
    </div>

    <script type="text/javascript" >

              $("#ortaSag").corner("round 6px");
              $("#ortaSagIc").corner("round 6px");

    </script>

</asp:Content>

