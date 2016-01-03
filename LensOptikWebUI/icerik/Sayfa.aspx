<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Icerik_Sayfa" ViewStateMode="Disabled" Codebehind="Sayfa.aspx.cs" %>
<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<style type="text/css" >

    #icerikDiv
    {  
     padding:15px;
    }

    #ortaSagIc p {
        overflow:hidden;
        line-height:20px;
        color:#525252;
    }

    #ortaSagIc div {
        overflow:hidden;
        line-height:20px;
        color:#525252;
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
          <div class="icerik-Baslik"  >
                    <h5> <asp:Literal ID="ltlBaslik" runat="server"></asp:Literal></h5>   
          </div>
            <div id="icerikDiv" >
               <asp:Literal ID="ltlSayfaIcerik" runat="server"></asp:Literal>
            </div>
        </div>
    </div>

      <script type="text/javascript" >
              $('#ortaSag').corner("round 6px");
              $('#ortaSagIc').corner("round 6px");
     </script>
</asp:Content>

