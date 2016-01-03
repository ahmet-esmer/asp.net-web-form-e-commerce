<%@ Page Title="Anket Sonuçları" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Icerik_Anketler" Codebehind="Anketler.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
<style type="text/css" >

.anketlerBaslik {
width:603px;
background-color:#f2f3f4;
height:25px;
padding-top:10px;
padding-left:15px;
font-size:13px;
font-weight:700;
color:#525252;
border:1px solid #d9dada;
margin:0 10px;
}


.anketTabloaAlt {
height:25px;
padding-top:10px;
padding-right:15px;
text-align:right;
font-size:12px;
color:#525252;
border-top:1px solid #d9dada;
margin:0 10px;
}

.anketOy {
width:120px;
height:25px;
padding-top:20px;
padding-right:15px;
text-align:right;
font-size:12px;
color:#525252;
border-top:#eee solid 1px;
}

.anketSoru {
width:300px;
height:25px;
padding-top:15px;
padding-left:15px;
font-size:12px;
color:#525252;
border-top:#eee solid 1px;
}

.anketZemin {
width:700px;
height:40px;
background-color:#fafafa;
border:1px solid #d9dada;
border-top:none;
}

.btnGenelBaslik {
background-color:#fafafa;
border:0;
color:#069;
cursor:pointer;
}

.anketSol {
width:400px;
display:inline;
float:left;
color:#069;
padding:10px 0 0 15px;
}

.anketSag {
width:190px;
display:inline;
padding-right:10px;
padding-top:10px;
float:left;
color:#069;
text-align:right;
}

.anketTablo {
width:100%;
padding:0;
}

#progress {
z-index:2;
}

#sayi {
z-index:-1;
width:60px;
height:15px;
margin-top:-16px;
margin-left:70px;
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

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
        <div id="ortaSagIc" style="padding-bottom:50px;"> 

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
       
         <table class="anketTablo">
	          <tr>
              <td colspan="2" class="icerik-Baslik" > 
               <h5> <asp:Label ID="lblAnketSonucBaslik"  runat="server" ></asp:Label> </h5> 
               </td>
             
               <td class="icerik-Baslik" > 
                  <asp:Label ID="lblTarih" runat="server" ></asp:Label> 
               </td> 
              </tr>
         <asp:Repeater ID="rptAnketGoster" runat="server">
         <ItemTemplate>
             <tr>
    	        <td class="anketSoru" >
                <%# DataBinder.Eval(Container, "DataItem.soru")%>
                </td>
                <td width="290px" style="border-top:#eeeeee solid 1px;" >
                  <div id="progeses" >  
                    <img width='<%# BusinessLayer.GenelFonksiyonlar.AnketDivGenislik(290, Convert.ToDecimal( Eval("toplamOy")), Convert.ToDecimal( Eval("anketOy"))) %>px' height="20px" src="../Images/Slices/transparent_bg.png" /> 
                    <div id="sayi" >
                    <span> % <%# BusinessLayer.GenelFonksiyonlar.AnketOylanma(100, Convert.ToDecimal(Eval("toplamOy")), Convert.ToDecimal(Eval("anketOy")))%>  </span>
                    </div> 
                  </div>     
                </td>
                <td class="anketOy" >
                   <%# Eval("anketOy")%>
                </td>
             </tr>
         </ItemTemplate>
         </asp:Repeater>
          <tr>
         <td colspan="3" class="anketTabloaAlt" >
          <asp:Label ID="lblToplamOy" runat="server" ></asp:Label> 
          </td> 
         </tr> 
         </table>
          
         <asp:Repeater ID="rptGenelAnketler" runat="server" onitemcommand="rptGenelAnketler_ItemCommand">
         <HeaderTemplate>
         <div id="rptHeader" class="icerik-Baslik" >
             <h5> Bütün Anketler</h5>
         </div>
         </HeaderTemplate>
         <ItemTemplate>
             <div class="anketZemin" >
                  <div   class="anketSol" > 
            <asp:Button ID="btnGenelBaslik" CssClass="btnGenelBaslik" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.anketBaslik")%>' CommandName="btnAnketBaslik" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id")%>' />
           
             </div>
                  <div class="anketSag" >
                
             <asp:Label ID="lblGenelTarih" runat="server" Text='<%# BusinessLayer.DateFormat.Tarih(Convert.ToString(DataBinder.Eval(Container, "DataItem.tarih")))%>'></asp:Label>
             </div>
             </div>
         </ItemTemplate>
         </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>

          <script type="text/javascript" >
              $('#ortaSag').corner("round 6px");
              $('#ortaSagIc').corner("round 6px");
        </script>
        </div>
    </div>


</asp:Content>

