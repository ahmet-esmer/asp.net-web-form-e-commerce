<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductList.ascx.cs" Inherits="LensOptikWebUI.Include.inculude_product_list" %>

  <asp:Repeater ID="rptUrun" runat="server" >
         <ItemTemplate>
         <div class="urunZemin" >
         <div class="urunBaslik" >
         <%# BusinessLayer.GenelFonksiyonlar.UrunAdiSergiKesme(DataBinder.Eval(Container, "DataItem.urunAdi").ToString())%>
         </div>
        
         <div class="urunResmi" >
        <a href="<%# ResolveUrl(Eval("link").ToString()) %>" title="<%# Eval("urunAdi")%>" > 
        <img src="<%# ResolveUrl("~/Products/Little/"+ Eval("resimAdi").ToString()) %>" alt="<%# Eval("urunAdi")%>" />
        </a>           
    </div>

         <div class="urunFiyatDiv" >
             
<%# BusinessLayer.AritmetikIslemler.UrunFiyatlari(Eval("urunFiyat").ToString(), Eval("uIndirimFiyat").ToString(), Eval("doviz").ToString(), Eval("kdv").ToString())%>

         </div> 
        <div class="urunDetay" >
        <a href="<%# ResolveUrl(Eval("link").ToString()) %>" title="<%# Eval("urunAdi")%>" > 
         <img src="<%# ResolveUrl("~/images/icons/detay.jpg") %>" alt="Detay" />
        </a>
        </div>  
       </div>
         </ItemTemplate>
       </asp:Repeater>

       <div style="clear:both;"> </div>
       <div class="pagination">
             <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
       </div> 


  <script type="text/javascript">
    
    $(document).ready(function () {
        $('.urunZemin:even').css("border-right", "solid 1px #bebdbd");
    });

</script>
