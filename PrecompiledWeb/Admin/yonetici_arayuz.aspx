<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="adminYoneticiArayuz" Title="Untitled Page" Codebehind="yonetici_arayuz.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">Admin</h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">
                   
                    <div class="panelMenu">
        <div class="menuBaslik"><h5>Ürün Kataloğu</h5> </div>
               <ul class="panel_Menu_Link">
                     <li><a href="urunler.aspx?urunParemetre=anasayfa">Anasayfa Ürünleri</a></li>
				     <li><a href="kategoriler.aspx">Kategoriler</a>	</li>
                     <li><a href="markalar.aspx">Markalar</a></li>
				     <li> <a href="urun_yorumlari.aspx">Ürün Yorumları</a></li>
                     <li><a href="gununUrunler.aspx">Günün Ürünleri</a></li>
                     <li><a href="topluIslemler.aspx">Toplu İşlemler</a></li>
               </ul>
        </div>
                    <div class="panelMenu">
        <div class="menuBaslik"><h5>Sipariş Yönetimi</h5> </div>
              <ul class="panel_Menu_Link">
				     <li> <a href="siparisler.aspx">Siparişler</a></li>
                     <li> <a href="siparisler.aspx?SDurum=0">Yeni Siparişler</a></li>
                     <li> <a href="siparisler.aspx?SDurum=1">Sipariş hazırlanıyor</a></li>
                     <li> <a href="siparisler.aspx?SDurum=2">Gönderildi</a></li>
                     <li> <a href="siparisler.aspx?SDurum=3">İptal edildi</a></li>
                     <li> <a href="siparisler.aspx?SDurum=7">Tedarik sürecinde</a></li>
			  </ul>
        </div>
                    <div class="panelMenu">
        <div class="menuBaslik"><h5>İçerik Yönetimi</h5></div>
           <ul  class="panel_Menu_Link">
				<li><a href="icerik_listele.aspx">Sayfalar</a></li>
			    <li><a href="reklamlar.aspx">Reklamlar</a></li>
			    <li><a href="banner.aspx">Bannerlar</a></li>
                <li><a href="ziyaretciDefteri.aspx?tumu=ok">Doktor Soruları</a></li>
                <li><a href="anketler.aspx"> Anketler</a></li>
			</ul>
        </div>
                    <div class="panelMenu">
        <div class="menuBaslik"><h5>Kullanıcı İşlemleri</h5></div>
            <ul class="panel_Menu_Link" >
			     <li><a href="yoneticiler.aspx">Yöneticiler</a> </li>
				 <li><a href="uyeler.aspx?islem=genel">Üyeler</a></li>
				 <li><a href="mail_gonder.aspx">Mail Gönderme</a></li>
				 <li><a href="sms_gonder.aspx">SMS Gönderme</a></li>
                 <li><a href="mailListesi.aspx">Mail Listesi</a></li>
		    </ul>
        </div>
         <div class="panelMenu">
        <div class="menuBaslik"><h5>Ayarlar</h5></div>
             <ul class="panel_Menu_Link" >
				 <li><a href="genel_ayarlar.aspx">Genel Ayarlar</a></li>
                 <li><a href="bankalar.aspx">Sanal Poslar</a></li>
				 <li ><a href="banka_hesaplari.aspx">Hesap Numaraları</a></li>
				 <li><a href="kargolar.aspx">Kargolar</a></li>
                 <li><a href="hataMesajlari.aspx">Hata Mesajları</a></li>
			</ul>
        </div>

        <div style="clear:both;"> </div>
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>

</asp:Content>

<asp:Content ID="Content2" runat="server"  contentplaceholderid="ContentPlaceHolder2">
   
</asp:Content>


