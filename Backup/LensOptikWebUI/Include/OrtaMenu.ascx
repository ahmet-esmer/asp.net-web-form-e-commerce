<%@ Control Language="C#" AutoEventWireup="true" Inherits="include_orta_menu" Codebehind="OrtaMenu.ascx.cs" %>
       <asp:Repeater ID="rptOrtaMenu" runat="server">
       <ItemTemplate>
           <div class="menu">
             <h1><a href="<%# ResolveUrl(Eval("link").ToString()) %>" > <%# Eval("kategoriadi")%></a></h1>
           </div>
        </ItemTemplate>
       </asp:Repeater>

       <asp:Panel ID="pnlKulanici" ClientIDMode="Static" runat="server">
           <div class="menuKayit">
              <a href="<%: ResolveUrl(string.Format("{0}Kullanici/Kayit", ConfigurationManager.AppSettings["sslSitePath"]) ) %>" >ÜYE GİRİŞİ</a>

             
           </div>
           <div class="menuKayit" >
               <a href="<%: ResolveUrl(string.Format("{0}Kullanici/Kayit", ConfigurationManager.AppSettings["sslSitePath"]) ) %>" >ÜYE OL</a>
           </div>
        </asp:Panel>
       <asp:Panel ID="pnlKulaniciList" ClientIDMode="Static" Visible="false" runat="server" >
            <img id="bilgiBaslik" src='<%: ResolveUrl("~/Images/buttonlar/KullaniciListe.jpg") %>' />
            <ul class="bilgiIcerik">
                <li><a href="<%: ResolveUrl("~/Market/Sepet.aspx") %>">Sepetim</a></li>
                <li><a href="<%: ResolveUrl("~/Kullanici/Bilgileri.aspx") %>">Kulanıcı Bilgilerim</a></li>
                <li><a href="<%: ResolveUrl("~/Kullanici/Siparisler.aspx") %>">Siparişlerim</a></li>
                <li><a href="<%: ResolveUrl("~/Kullanici/Puanlar.aspx") %>">Puanlarım</a></li>
                <li><a href="<%: ResolveUrl("~/Kullanici/Favoriler.aspx") %>">Favori Ürünlerim</a></li>
                <li><a href="<%: ResolveUrl("~/Kullanici/FiyatAlarm.aspx") %>">Fiyat Alarm Listesi</a></li>
                <li runat="server" id="kulaniciAdmin" visible="false" >
                <a target="_blank" href="<%: ResolveUrl("~/Admin/yonetici_arayuz.aspx") %>"> Yönetici Paneli</a>
                </li>
            </ul>
        </asp:Panel>
       <div class="clear"></div>

     