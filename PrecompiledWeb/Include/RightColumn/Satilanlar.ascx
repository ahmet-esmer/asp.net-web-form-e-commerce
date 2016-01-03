<%@ Control Language="C#" AutoEventWireup="true" Inherits="Include_RightColumn_Satilanlar" Codebehind="Satilanlar.ascx.cs" %>
  


  <div class="panelBaslik"> <span class="baslik"> EN ÇOK SATANLAR</span> </div>
              <asp:Repeater ID="rptPanelSatilanler" runat="server">
            <ItemTemplate>
              <div class="panel_R_Y_Zemin" >
              <div class="panelResim"> 
                 <asp:HyperLink ID="hlUrun" NavigateUrl='<%# Eval("link") %>' runat="server">
                   <img src='<%# ResolveUrl("~/Products/Little/" +Eval("resimAdi").ToString()) %>'
                    alt="<%# DataBinder.Eval(Container, "DataItem.urunAdi").ToString().ToUpper()%>"
                    onload="ResizeImage(this,60,45);" />
               </asp:HyperLink>
               &nbsp;
               </div>
              
               <div class="panelYazi">
                    <div class="yazi_1">
                   <%# BusinessLayer.GenelFonksiyonlar.UrunAdiKesme( DataBinder.Eval(Container, "DataItem.urunAdi").ToString()) %>
                    </div>
                    <div class="yazi_2" >
                    <span class='solfiyat'>
                   <%#  BusinessLayer.AritmetikIslemler.panelFiyat(Convert.ToDecimal(Eval("urunFiyat")), Convert.ToDecimal(Eval("uIndirimFiyat")), Eval("doviz").ToString())%>
                   </span>
                    </div>
               </div>
            </div>
            </ItemTemplate>
            </asp:Repeater>