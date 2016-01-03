<%@ Control Language="C#" AutoEventWireup="true" Inherits="include_gununUrunu" Codebehind="GununUrunu.ascx.cs" %>
   
 <div id="gunDis" >
    <div id="gunIc" > 
            
    <div id="gunBaslik" > 
                  Günün Kampanyalı Ürünü
              </div>
    <div class="gununUrunResim"  >
    
        <div class="gResim1" >
                <asp:HyperLink ID="hlUrunDetay" runat="server">
                <asp:Image ID="imgGununUrunu" runat="server" Width="140px" />
               </asp:HyperLink> 
        </div>  
        <div class="gResim2" >
          %<asp:Label ID="lblYuzde" CssClass="gBilgi2Miktar" ClientIDMode="Static" runat="server"></asp:Label><br />
          <span class="gBilgiIdirim" >İNDİRİM</span> 
        </div>   
              </div>
    <div class="gununUrunAdi" >
               <asp:Label ID="lblGunUrunAdi" runat="server"  ></asp:Label> 
              </div>
    <div class="gun1" > 
               <div class="gununUrunFiyat">
                <asp:Label ID="lblUrunFiyat1" runat="server"  ></asp:Label>
                <asp:Label ID="lblUrunFiyat1Kdv" runat="server" CssClass="fiyatIndirim"  ></asp:Label>
               </div>
               <div class="gununUrunFiyat" >
                <asp:Label ID="lblUrunFiyat2" runat="server"  ></asp:Label>
                <asp:Label ID="lblUrunFiyat2Kdv" runat="server" CssClass="fiyat"  ></asp:Label>
               </div>
             </div>
    <div class="gun2" >
            <%--   <asp:HyperLink ID="gun" CssClass="link" NavigateUrl="~/gune-ozel-kampanyali-indirimli-lensler?goto=ok" 
               ClientIDMode="Static" runat="server"> Diger Günler </asp:HyperLink>--%>
             </div>

     </div>
 </div>