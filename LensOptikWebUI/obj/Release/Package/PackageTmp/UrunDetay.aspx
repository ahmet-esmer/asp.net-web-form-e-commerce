<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" ViewStateMode="Disabled" Inherits="urun_detay" Codebehind="UrunDetay.aspx.cs" %>

<%@ Register src="Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc2" %>
<%@ Import Namespace="System.Configuration" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

    <script>

    var sSitePath =  '<%# ConfigurationManager.AppSettings["sslSitePath"] %>';
    </script>
    <link rel="stylesheet" href="../../Style/urunDetay.css" />

    <script type="text/javascript" src="../../Scripts/cloud-zoom.1.0.2.js" ></script>
    <script type="text/javascript" src="../../Scripts/jquery.tabify.source.js" ></script>
    <script type="text/javascript" src="../../Scripts/jquery.json-2.2.js" ></script>
    <script type="text/javascript" src="../../Scripts/fancyzoom.js" ></script>
    <script type="text/javascript" src="../../Scripts/urunDetay.js" ></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div id="urunDetayDiv">
    <div class="divHataDetay" >
        <uc2:Mesajlar ID="ucMesaj" runat="server" />
    </div>
    <div class="mevcutSayfa"  >
      <asp:HyperLink ID="hlAna" ClientIDMode="Static" CssClass="altMenu" NavigateUrl="~/Default.aspx"  runat="server" Text="Anasayfa" ></asp:HyperLink>
       <asp:Repeater ID="rptMevcutLink" runat="server">
       <ItemTemplate>
            <a href="<%# ResolveUrl(BusinessLayer.LinkBulding.Kategori(Eval("title"),Eval("kategoriadi"), Eval("serial"))) %>" class="altMenu" ><%# Eval("kategoriadi")%></a>
       </ItemTemplate>
       </asp:Repeater>
       <div class="goruntu" >
       <asp:Label ID="lblGoruntu" Font-Size="18px" ClientIDMode="Static" runat="server"></asp:Label> &nbsp;Defa  Görüntülendi
     </div>
    </div>
    <div id="marka"  >
        <asp:Repeater ID="rptMarkalar" runat="server" >
            <ItemTemplate>
            <div class="digerMarka" >
                <a href="<%# ResolveUrl(Eval("Link").ToString())%>" ><%# Eval("MarkaAdi") %> <span>(<%# Eval("Adet") %>) </span></a>
            </div>
            </ItemTemplate>
        </asp:Repeater> 
  <div class="clear"></div>
     </div>
    <div id="urunGaleri">
        <div id="resimOrtaDiv" runat="server"  >
        <!-- Orta Resim -->
            <div class="ortaResimDiv" >
             <div ID="pnlRenkliLens"  runat="server" clientidmode="Static" ></div>
              <asp:HyperLink ID="hlResimBuyuk"  ClientIDMode="Static"  runat="server" class="cloud-zoom" rel="adjustX: 30, adjustY:-4 , zoomWidth:600, zoomHeight:300 " ><asp:Image ID="imgResimOrta"  CssClass="imgOrtala" ClientIDMode="Static" runat="server"  />
              </asp:HyperLink>
            </div>
        </div>
        <div class="ortaResimDiv">
             <asp:Image ID="imgResimYok" Visible="false" Width="230px" runat="server" />
        </div>
        <div class="mercekDiv">
            <a onclick="openpopup()" style="cursor:pointer;" target="_blank" ><span class="resimMercek" >Resmi Büyüt</span> 
                <asp:Image ID="imgMercek" ImageUrl="~/images/icons/resimBuyut.jpg" runat="server" />
            </a>
        </div>
        <div class="resimkucukDiv" >
            <asp:Repeater ID="rptUrunResimleri" runat="server">
            <ItemTemplate> 
              <a href="<%# ResolveUrl("~/Products/Big/"+ Eval("ResimAdi")) %>" class="cloud-zoom-gallery" rel="useZoom: 'hlResimBuyuk', smallImage: '<%# ResolveUrl("~/Products/Small/"+ Eval("ResimAdi")) %>'" >
              <img src="<%# ResolveUrl("~/Products/Little/"+ Eval("ResimAdi")) %>" class="kucukResim" onload="ResizeImage(this,57,52);" alt="<%# Eval("ResimBaslik") %>"  />
              </a>
            </ItemTemplate>
            </asp:Repeater>
        </div>
     </div>
    <div id="urunBilgi" >
         <table>
             <tr>
                <td colspan="2" id="urunAdi" >
                   <h1 runat="server" clientidmode="Static" id="lblUrunAdi"> </h1>
                </td>
               <td rowspan="11" id="urunIcons" >
                   <div id="urunIcon" >
                    <img id="imgSepetGoruntu" src="../../../Images/buttonlar/sepet.gif" alt="Sepet" />
                    <img id="imgFavoriEkle" src="../../../Images/buttonlar/favori.gif" alt="Favori Ürünler"  />
                    <img id="imgArkadasaGonder" src="../../../Images/buttonlar/arkadasaGonder.gif" alt="Arkadaşına Gönder" />
                    <img id="imgIzlemeyeAl" src="../../../Images/buttonlar/izlemeyeAl.gif" alt="Fiyat Takip" />
                    <img id="imgYorum" src="../../../Images/buttonlar/yorumEkle.gif" alt="Yorum Ekle" />
                   </div>

             
                    <div class="addthis_toolbox addthis_default_style" style="margin-top:20px;" >
                    <div class="addthis_toolbox addthis_default_style">
                    <a href="http://www.addthis.com/bookmark.php?v=250&amp;username=xa-4c3210e07db1b18d" class="addthis_button_compact" style="color:#292929;font-size:11px;">Paylaş </a>
                    <span class="addthis_separator">|</span>
                    <a class="addthis_button_facebook"></a>
                    <a class="addthis_button_myspace"></a>
                    <a class="addthis_button_google"></a>
                    <a class="addthis_button_twitter"></a>
                    </div>
                    <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=xa-4c3210e07db1b18d"></script>
                    </div>
      

                    <asp:HiddenField ID="hdfUrunId" ClientIDMode="Static"  runat="server" />  
                    <asp:HiddenField ID="hdfUyeId" ClientIDMode="Static" Value="0" runat="server" /> 
                    <asp:HiddenField ID="hdfUrunFiyat" ClientIDMode="Static" Value="0" runat="server" /> 

                    <input id="hdfGeridonusUrl" value="<%=  Request.RawUrl.ToString() %>" type="hidden" />
                    <input id="hdfDizin" value="<%= ResolveUrl("~")%>" type="hidden" />
                    <input id="hdfHediyeUrun" value="0" type="hidden" />
             </td>
             </tr>
             <tr id="trKisaAciklama" runat="server" visible="false" >
                 <td colspan="2" class="UDaciklama" >
                     <asp:Label ID="lblAciklama" runat="server"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="urun key" >
                     Marka</td>
                 <td class="urun value">
                 : <asp:Label ID="lblMarka" runat="server"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="urun key" >Fiyat</td>
                 <td class="urun value">
                 : <asp:Label ID="lblFiyat" runat="server"></asp:Label>
                 </td>
             </tr>
             <tr id="trIndirim" runat="server" clientidmode="Static" >
                 <td class="urun key">İndirimli Fiyat</td>
                 <td class="urun value">
                 : <asp:Label ID="lblFiyatIndirim" runat="server"></asp:Label>
                 </td>
             </tr>
             <tr id="dovizTlFiyat" visible="false" runat="server" clientidmode="Static" >
                 <td class="urun key">TL Fiyatı</td>
                 <td class="urun value">
                 : <asp:Label ID="lblDovizFiyat" runat="server" ></asp:Label>
                 </td>
             </tr>
             <tr id="trKDVToplamFiyat" runat="server" clientidmode="Static" >
                 <td class="urun key"> Toplam</td>
                 <td class="urun value">
                  : <asp:Label ID="lblFiyatToplam" runat="server"></asp:Label>
                 </td>
             </tr>
             <tr id="trHavaleIndirim" runat="server" clientidmode="Static" >
                 <td class="urun key">Havale İndirimi<asp:Label ID="lblHavale" runat="server"></asp:Label>
                 </td>
                 <td class="urun value">
                 : <asp:Label ID="lblHavaleile" runat="server"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="urun key">Stok</td>
                 <td class="urun value">
                 : <asp:Label ID="lblStok" ClientIDMode="Static" runat="server"></asp:Label>
                 </td>
             </tr>
              <tr id="trHediye" runat="server" clientidmode="Static" >
                 <td class="urun key">Hediye Ürün</td>
                 <td class="urun value">
                 :  <a href="#hediye_box" id="hlHediye">
                      <asp:Literal ID="ltHediye" runat="server"></asp:Literal>
                    </a>
                    <div id="hediye_box" style="display:none" >
                        <div class="kamBaslik" > Hediye Ürün </div>
                        <div class="kamResim">
                        <asp:Image ID="imgKamResim" Width="200px" runat="server" />
                        </div>
                        <div class="kamBilgi" >
                        <asp:Label ID="lblKamUrun" CssClass="basAlt" runat="server" ></asp:Label>
                        <br />
                        <br />
                        <span>Asıl Fiyat:</span> 
                         <asp:Label ID="lblKamFiyat" CssClass="kamFiyat" runat="server" ></asp:Label>
                        </div>
			        </div>

                 </td>
             </tr>
         </table>
        <div id="kampanya" class="line kampanya"> 
   
            <asp:Repeater ID="rptKampanyaBilgi" runat="server">
                <HeaderTemplate>
                  <div id="kampanyaBilgi" >Kampanyalar</div>
                  <ul>
                </HeaderTemplate>
                <ItemTemplate>
                 <li><%# Eval("Bilgi") %> </li>
                </ItemTemplate>
                <FooterTemplate>
                </ul>
                </FooterTemplate>
            </asp:Repeater>

        </div>
         <div class="line topluList"> 
          
         <asp:Repeater ID="rptTopluListe" runat="server">
                <HeaderTemplate>
               <div id="kampanyaBilgi" style=" border-width:0px; " >Çoklu alım fiyatları</div>
               <table cellspacing="2" >
               
                    <tr>
                       <th style="width:50px;">&nbsp; Adet</th>
                       <th style="width:70px;"></th>
                       <th >Kazanciniz</th>
                       <th >Toplam</th>
                    </tr>  
                </HeaderTemplate>
                <ItemTemplate>
                <tr>
                    <td>&nbsp; <%# Eval("Adet") %> </td>
                    <td> <%# Eval("Islem") %> </td>
                    <td  class="red" ><%# Eval("Indirim") %> </td>
                    <td> = <%# Eval("Toplam") %> </td>
                </tr>
                </ItemTemplate>
                <FooterTemplate>
                     </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
     

    </div>
    <div id="secenekDiv" >
         <table style="width:100%" >
          <tr>
               <td> </td>
               <td>
                <asp:Label ID="lblBc" runat="server" Visible="False" CssClass="yazi2">Bc</asp:Label>
               </td>
               <td>
                 <asp:Label ID="lblDia" runat="server" Visible="False" CssClass="yazi2">Dia</asp:Label>
               </td>
              
               <td>
                  <asp:Label ID="lblDioptri" runat="server" Visible="False" CssClass="yazi2">Dioptri</asp:Label>
               </td>
               <td>
                 <asp:Label ID="lblSilindirik" runat="server" Visible="False" CssClass="yazi2">Silindirik</asp:Label>
               </td>
               <td>
                  <asp:Label ID="lblAks" runat="server" Visible="False" CssClass="yazi2">Aks</asp:Label>
               </td>
               <td>
                 <asp:Label ID="lblRenk" runat="server" Visible="False" CssClass="yazi2">Renk</asp:Label>
               </td>
               <td>
                  <asp:Label ID="lblAdet" runat="server" CssClass="yazi2">Adet</asp:Label>
              </td>
              <td> 
              </td>
         </tr>
         <tr>
             <td class="style2"> 
              <asp:Label ID="lblSagText" Text="Sag Göz:" CssClass="yazi1"  runat="server" />
             </td>
             <td class="style2">
              <asp:DropDownList ID="ddlBc" ClientIDMode="Static" runat="server" Visible="False">
              </asp:DropDownList>
             </td>
             <td>
             <asp:DropDownList ID="ddlDia" ClientIDMode="Static" runat="server" Visible="False">
                 </asp:DropDownList>
                
             </td>
             
             <td>
                 <asp:DropDownList ID="ddlDioptri" ClientIDMode="Static" runat="server" Visible="False">
                 </asp:DropDownList>
             </td>
             <td>
                  <asp:DropDownList ID="ddlSilindirik" ClientIDMode="Static" runat="server" Visible="False">
                 </asp:DropDownList>
             </td>
             <td>
                 <asp:DropDownList ID="ddlAks" ClientIDMode="Static" runat="server" Visible="False">
                 </asp:DropDownList>
             </td>
             <td>
                 <asp:DropDownList ID="ddlRenk" ClientIDMode="Static" runat="server" Visible="False">
                 </asp:DropDownList>
             </td>
             <td>
                 <asp:DropDownList ID="ddlAdet" ClientIDMode="Static" runat="server">
                 </asp:DropDownList>
             </td>
             <td id="tdSepetEkle" rowspan="2"  >
              <img id="imgSepeteEkle"  src="../../../images/buttonlar/sepeteEkle.gif" alt="Sepete Ekle" /> 
             </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSolText" Text="Sol Göz:" CssClass="yazi1"  runat="server" />
            </td>
            <td>
            <asp:DropDownList ID="ddlBc1" ClientIDMode="Static" runat="server" Visible="False" >
                  </asp:DropDownList>
               
            </td>
            <td>
             <asp:DropDownList ID="ddlDia1" ClientIDMode="Static" runat="server" Visible="False" >
                  </asp:DropDownList>
                 
            </td>
            <td>
                <asp:DropDownList ID="ddlDioptri1" ClientIDMode="Static" runat="server" Visible="False" >
                </asp:DropDownList> 
            </td>
            <td>
                  <asp:DropDownList ID="ddlSilindirik1" ClientIDMode="Static" runat="server" Visible="False" >
                  </asp:DropDownList>
            </td>
             <td>
                  <asp:DropDownList ID="ddlAks1" ClientIDMode="Static" runat="server" Visible="False" >
                  </asp:DropDownList>
            </td>
            <td>
                  <asp:DropDownList ID="ddlRenk1" ClientIDMode="Static" runat="server" Visible="False" >
                  </asp:DropDownList>
            </td>
            <td>
                  <asp:DropDownList ID="ddlAdet1" ClientIDMode="Static" runat="server" >
                  </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="trSenekInfo" clientidmode="Static" >
        <td colspan="9" style="line-height:20px;"  >

         <b>Not:</b>
         <a href="#bilgi_box" id="bilgi" >
          Ürün siparişi ile ilgili bilgi almak için lütfen tıklayınız.
         </a>
        <br />
        <b>*</b> + (artı)değerler hipermetrop - (eksi)değerler miyop derecelerini gösterir.
         <div id="bilgi_box" style="display:none;" >
             <table class="bilgi_box" >
                    <tr>
                     <td class="fjkTabloOzelik" colspan="2" ><b>Ürün Bilgilendirme Tablosu </b></td>
                 </tr>
                 <tr>
                     <td class="fjkTabloOzelik" >Dioptri</td>
                     <td class="fjkTabloAciklama" >
                     : Sph olarakta kullanılabilir.&#8217;+&#8217; veya &#8216;-&#8216; ile ifade edilir.+ 
                     (artı)değerler hipermetrop - (eksi)değerler miyop derecelerini gösterir.
                     </td>
                 </tr>
                 <tr>
                     <td class="fjkTabloOzelik" >Silindirik</td>
                     <td class="fjkTabloAciklama" >:(CYL ) :Astigmatlı (Toric) lenslere özgü bir değerdir. &#8211; (eksi) işareti ile ifade edilir.  </td>
                 </tr>
                  <tr>
                     <td class="fjkTabloOzelik" >Aks </td>
                     <td class="fjkTabloAciklama" >: Astigmatlı (Toric) lenslere özgü bir değerdir.0 ile 180 arasında bir değerdir. </td>
                 </tr>
                  <tr>
                     <td class="fjkTabloOzelik" >B.C </td>
                     <td class="fjkTabloAciklama">: Base curve gözün temel eğrisidir. </td>
                 </tr>
                  <tr>
                     <td class="fjkTabloOzelik">Dıa</td>
                     <td class="fjkTabloAciklama"> : Kontak lensin dış çap ölçüsünü belirtir. </td>
                 </tr>
                  <tr>
                     <td class="fjkTabloOzelik">Kutu Seçimi  </td>
                     <td class="fjkTabloAciklama">
                    :İki göz numaranız aynı ise sağ ya da sol seçeneklerinden bir tanesini doldurmanız yeterlidir. 
                     </td>
                 </tr>
             </table>
	     </div>

         </td>
       </tr>
     </table>
   </div>
    <div id="hediye" >
     
    </div>
    <div id="urunTab"  >
        <ul id="menuTab" class="menuTab">
			<li class="active"><a href="#ayrinti">Ürün Ayrıntıları</a></li>
			<li><a href="#odemeSec" id="odemeSecenkleri" >Ödeme Seçenekleri</a></li>
            <li><a href="#arkadas" >Arkadaşına Gönder</a></li>
		</ul>
		<div id="ayrinti" class="content" >
			  <asp:Literal ID="ltUrunDetay" runat="server"></asp:Literal>
		</div>
		<div id="odemeSec" class="content" >

        </div>
        <div id="arkadas" class="content" >
	    <table style=" width:100%;"   >
            <tr>
                <td  colspan="2" class="uyuBilgiYazi" style="padding-top:30px; text-indent:5px; padding-bottom:20px;"  >
                  <span>  Tavsiye bilgilerini yazıp gönderim işlemini gerçekleştirebilirsiniz. <span class="formZorunlu" >*</span> Lütfen İşaretli Yerleri Boş Bırakmayınız.</span>
               </td>
            </tr>
            <tr >
                <td class="formBilgi" style="padding-top:20px; width:230px;">
                    Adınız / Soyadınız <span class="formZorunlu" > * </span> </td>
                <td  style="padding-top:10px; width:500px"  >
                <asp:TextBox ID="txtGoAdi" runat="server" Width="450px" CssClass="text"  ></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="rfvAd" runat="server" Display="Dynamic" ErrorMessage="Ad ve Soyad Alanını Doldurunuz." CssClass="input-notification error" ForeColor="" ControlToValidate="txtGoAdi" ValidationGroup="arkadasinaGonder" ></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
            <td class="formBilgi" >
                Tavsiye Edeceğiniz Kişinin Adı / Soyadı<span class="formZorunlu" >*</span> </td>
            <td>
                <asp:TextBox ID="txtAlicAdi" runat="server" Width="450px" CssClass="text" ></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="rfvTavAd" runat="server" Display="Dynamic" ErrorMessage="Ad ve Soyad Alanını Doldurunuz." CssClass="input-notification error" ForeColor="" ControlToValidate="txtAlicAdi" ValidationGroup="arkadasinaGonder" ></asp:RequiredFieldValidator>
            </td>
        </tr>
             <tr>
            <td class="formBilgi">
                Tavsiye Edeceğiniz Kişinin E-mail Adresi<span class="formZorunlu" >*</span></td>
            <td>
            <asp:TextBox ID="txtAliciAdres" runat="server" Width="450px" CssClass="text" Height="22px" ></asp:TextBox>
            <div class="not" >E-Mail adresi hiçbir şekilde lensoptik.com.tr tarafından kullanılmayacaktır! </div>
                <asp:RequiredFieldValidator ID="rfvMail" runat="server" Display="Dynamic" 
                  ErrorMessage="Geçerli E-Posta Adresi Giriniz."  CssClass="input-notification error" ForeColor="" 
                  ControlToValidate="txtAliciAdres" ValidationGroup="arkadasinaGonder" >
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rxvMail" runat="server" ControlToValidate="txtAliciAdres" 
                 Display="Dynamic" ErrorMessage=" Geçerli E-Posta Adresi Giriniz." ForeColor=""
                 CssClass="input-notification error" ValidationGroup="arkadasinaGonder" 
                 ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
            <tr>
                <td class="formBilgi"  style="vertical-align:top;" >Mesajınız<span class="formZorunlu" > * </span> </td>
                <td>
                    <asp:TextBox ID="txtTavsiyeMesaj" runat="server" Height="100px" Width="450px" CssClass="text" Text="lensoptik.com.tr internet sitesinde gördüğüm bu ürün, ilgini çekebileceğini düşündüm." TextMode="MultiLine"></asp:TextBox><br />
                     <asp:RequiredFieldValidator ID="rfvMesaj" runat="server" Display="Dynamic" ErrorMessage=" Mesaj Alanını Doldurunuz." CssClass="input-notification error" ForeColor="" ControlToValidate="txtTavsiyeMesaj" ValidationGroup="arkadasinaGonder"  ></asp:RequiredFieldValidator>
                </td>
            </tr>
                <tr >
                <td class="formBilgi" >
                 Güvenlik Kodu: <span class="formZorunlu" > * </span> </td>
                <td>
                     <asp:TextBox ID="txtGuvenlikKodu" runat="server"   CssClass="text" Width="80px"  autocomplete="off" ></asp:TextBox><br /> 
                     <asp:RequiredFieldValidator ID="rfvGuv" CssClass="input-notification error" ForeColor=""
           runat="server" Display="Dynamic" ErrorMessage=" Güvenlik Kodu Alanını Doldurunuz." 
           ControlToValidate="txtGuvenlikKodu" ValidationGroup="arkadasinaGonder" ></asp:RequiredFieldValidator><br />
                     <asp:Image ID="imgGuv" ClientIDMode="Static" ImageUrl="~/include/GuvenlikResmi.aspx" runat="server" />
                </td>
            </tr>
            <tr>
                <td >
                    &nbsp;</td>
                <td  style="padding:15px 0;"  >
                     <asp:Button ID="btnArkasinaGonder" ValidationGroup="arkadasinaGonder"   CssClass="button" runat="server" Text="Arkadaşına Gönder" Width="160px" Height="23px" onclick="btnArkasinaGonder_Click" />
                </td>
            </tr>
        </table>
		</div>

     </div>
        <div class="clear" ></div>
           <div id="yorumBaslik" >Ürün Yorumları</div>
           <div id="yorum" >
                 <div id="yorumlar" > </div>
                 <div class="pagination"> </div>
                 <a name="yorumEkle"></a>
           <asp:Panel ID="pnlUrunYorumFormu" Visible="false" runat="server">
           <table style="border-top:1px solid #d9dada; margin-top:50px; width:100%;"   >
            <tr>
                <td colspan="2" class="icerik-Baslik">
                <h5> Siz de Ürüne Yorumunuzu Yapın </h5>
                </td>
            </tr>
            <tr>
               <td colspan="2" class="uyuBilgiYazi"  >
                  Lütfen ürün yorumuna ait  alanları doldurunuz. Her türlü 
                  konuda fikirlerinizi belirtebilirsiniz. Müşteri temsilcileirimiz iletinizle 
                  ilgili genel kuralları kontrol ettikten sonra yayına alacaklardır.</td>
             </tr>
            <tr>
                <td class="formBilgi" >
                    Değerlendirme <span class="formZorunlu" > * </span> </td>
                <td style="padding-top:10px;" >
                <asp:DropDownList CssClass="ZDForm" ID="ddlDegerlendirme" Height="28px" Width="300px"  runat="server">
                    <asp:ListItem Value="0" >Ürün Degerlendirme Kıriteri Seçiniz</asp:ListItem>
                    <asp:ListItem Value="5" >Mükemmel</asp:ListItem>
                    <asp:ListItem Value="4" >Çok İyi</asp:ListItem>
                    <asp:ListItem Value="3" >İyi</asp:ListItem>
                    <asp:ListItem Value="2" >Fena Degil</asp:ListItem>
                    <asp:ListItem Value="1" >Çok Kötü</asp:ListItem>
                </asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="rfvUrunDeger" runat="server" Display="Dynamic" 
                        ErrorMessage="Degerlendirme Kıriteri Şeçmelisiniz." InitialValue="0" 
                        CssClass="input-notification error" ControlToValidate="ddlDegerlendirme"
                        ForeColor="" ValidationGroup="urunYorum" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="formBilgi" style="vertical-align:top;" >
                Mesajınız<span class="formZorunlu" >*</span> </td>
                <td>
                   <asp:TextBox ID="txtMesaj" runat="server" Height="88px" Width="400px" TextMode="MultiLine" ></asp:TextBox><br />
                   <asp:RequiredFieldValidator ID="rfvYorum" runat="server" ForeColor=""
                      Display="Dynamic" ErrorMessage=" Mesaj Alanını Doldurunuz."
                      CssClass="input-notification error" ControlToValidate="txtMesaj" 
                      ValidationGroup="urunYorum" ></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="padding:10px 0px;" >
       <asp:Button ID="btnUrunYorum" ValidationGroup="urunYorum" CssClass="button" runat="server" Text="Yorum Ekle" Width="120px" onclick="btnYorumGonder_Click" />
                </td>
            </tr>
        </table>
        </asp:Panel>
		   </div>
         <asp:Repeater ID="rptUrunTavsiye" runat="server" Visible="false"  >
         <HeaderTemplate>
         <table style="width:100%; margin:20px 0px;" >
             <tr>
                <td colspan="4" class="icerik-Baslik" style="border:1px solid #d9dada;" >
                <h5>Tavsiye Edilen Ürünler </h5> 
                </td>
             </tr>
         </HeaderTemplate>
         <ItemTemplate>
          <tr class="UDsatir" >
            <td width="60px" >
                <a href="<%# ResolveUrl(Eval("Link").ToString()) %>" > 
                <img class="UDtavsiyeImg" src="../../Products/Little/<%# Eval("ResimAdi").ToString() %>" alt="<%# Eval("UrunAdi") %>" />
                </a>
            </td>
            <td width="470px"  >
                <a href="<%# ResolveUrl(Eval("Link").ToString()) %>"  > 
                    <span class="UDtavsiyeTD"><%# Eval("UrunAdi").ToString() %> </span>
                </a>
            </td>
            <td width="100px" >
                <span class="UDtavsiyetdBosluk" >
                <%# BusinessLayer.AritmetikIslemler.UrunFiyatIndirim(Convert.ToDecimal(Eval("UrunFiyat")),
                Convert.ToDecimal(Eval("UIndirimFiyat")), Eval("doviz").ToString())%>
                </span>
            </td>
            <td width="100px"  >
                <span class="UDtavsiyetdBosluk" >
                <%# BusinessLayer.AritmetikIslemler.UrunFiyatIndirimVarmi(Convert.ToDecimal(Eval("UrunFiyat")),
                Convert.ToDecimal(Eval("UIndirimFiyat")), Eval("Doviz").ToString())%>
                </span>
            </td>
           </tr>
         </ItemTemplate>
         <FooterTemplate>
            <tr>
              <td colspan="4" style="border-bottom:1px solid #d9dada;" >
              </td>
            </tr>
          </table>
         </FooterTemplate>
         </asp:Repeater>
   </div>

</asp:Content>

