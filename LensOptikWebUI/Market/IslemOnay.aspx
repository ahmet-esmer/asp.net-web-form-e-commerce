<%@ Page Title="Sipariş Onay | Lens Optik" Language="C#" MasterPageFile="~/Market/Market.master" AutoEventWireup="true" Inherits="Market_IslemOnay" Codebehind="IslemOnay.aspx.cs" %>

<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc1" %>
<%@ Register src="MenuMarket.ascx" tagname="MenuMarket" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

<link rel="Stylesheet" href="../Style/TeslimatBilgileri.css" type="text/css" />
<script src="../Scripts/Market.js"  type="text/javascript" ></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

<script type="text/javascript" src="../Scripts/jquery.simplemodal.js"></script>
<script type="text/javascript" src="../Scripts/osx.js"></script>

<script type="text/javascript">

    $(document).ready(function () {

        $("#btnOdeme").click(function () {
            if (!$("#ckbSozlesmeOnay").is(":checked")) {
                alert("! Lütfen Satış Sözleşmesini Okuyup Onaylayın.");
                return false;
            }
        })
    });
         
</script>

 <div id="marketAna" >
     <div id="marketMap">
       <uc2:MenuMarket ID="Menu" runat="server" />
     </div>
     <div id="marketMesaj" style="margin-bottom:20px;" >
       <uc1:Mesajlar ID="Mesaj" runat="server" />
     </div>
     
    <div class="icerik-Baslik Border"><a name="fatura"></a><h5> Adres/Fatura Bilgileri </h5></div>
   

      <div class="adresDiv" >
         <div class="secenekDiv aktiv" style="padding:7px 15px;" >
         <label> Sipariş gönderiminde kullanılacak adres  </label>
         </div>
         <div class="Adres" >
             <b>Teslim Edilecek Kişi: </b>
             <asp:Label ID="lblTesAlan" runat="server" ></asp:Label><br/>
             <b>Telefon: </b> 
             <asp:Label ID="lblTelefon" runat="server" ></asp:Label><br />
             <b> Adres: </b>
             <asp:Label ID="lblTesAdres" runat="server"></asp:Label><br />
             <b> Şehir: </b>
             <asp:Label ID="lblSehir" runat="server"></asp:Label>

             </div>           
         <div>
        </div>

      </div>

      <div class="adresDiv" >
            <div class="secenekDiv aktiv " style="padding:7px 15px;" >
            <label>Fatura bilgisi</label>
            </div>
            <div class="Adres" >
                 <asp:Label ID="lblFatAd" runat="server" ></asp:Label><br />
                 <asp:Label ID="lblFatTC" runat="server" ></asp:Label>
                 <asp:Label ID="lblFatVergiNo" runat="server" ></asp:Label>
                 <asp:Label ID="lblFatVergiDai" runat="server" ></asp:Label>
                 <b>Fatura Adresi: </b>
                 <asp:Label ID="lblFatAdres" runat="server" ></asp:Label>
            </div>
        </div> 
    <div class="clear"></div>

     <table id="tblSepet" >
              <tr>
                  <td colspan="3">
                <asp:GridView ID="gvwIslemOnay" runat="server"  AutoGenerateColumns="false" CellPadding="4" 
            ForeColor="#333333" GridLines="None" Width="100%"  >
                   <Columns>
                   <asp:TemplateField  HeaderStyle-CssClass="orta" >
                   <ItemTemplate>
                   <a href="<%# ResolveUrl(Eval("UrunLink").ToString()) %>" > 
                     <img src="<%# ResolveUrl("~/Products/Little/"+ Eval("Resim").ToString()) %>" class="sepetResim" />
                   </a>
                   </ItemTemplate>
                   <ItemStyle  />
                   <HeaderStyle Width="100"  />
                   </asp:TemplateField>
                            
                   <asp:TemplateField HeaderText="Ürün Adı" HeaderStyle-CssClass="orta" >
                   <ItemTemplate>
                    <br />
                     <a class="urunAdiLink" href="<%# ResolveUrl(Eval("UrunLink").ToString()) %>" > 
                      <%# Eval("Urun")%>
                     </a>
                     <br />
                     <span class="urunAdiLink"><%# Eval("SagBilgi") %> </span>
                     <span class="urunAdiLink"><%# Eval("SolBilgi") %> </span>
                     <br />
                       <%# Eval("HediyeHTML")%>
                       <%# Eval("HediyeUrunTekHTML")%>
                     </ItemTemplate>
                     <ItemStyle CssClass="orta"  />
                     <HeaderStyle />
                     <HeaderStyle Width="420" Font-Bold="true"  />
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="Birim Fiyatı" HeaderStyle-CssClass="orta" >
                      <ItemTemplate>
                         <%# Eval("Fiyat") %>
                      </ItemTemplate>
                      <ItemStyle CssClass="orta" />
                      <HeaderStyle Width="100" Font-Bold="true" />
                     </asp:TemplateField>

                     <asp:TemplateField HeaderText="KDV" HeaderStyle-CssClass="orta" >
                         <ItemTemplate>
                         <%# Eval("KDV")%> 
                         </ItemTemplate>
                         <ItemStyle CssClass="orta" />
                         <HeaderStyle Width="80" Font-Bold="true"  />
                     </asp:TemplateField>
                          
                      <asp:TemplateField HeaderText="Adet" HeaderStyle-CssClass="orta" >
                      <ItemTemplate>

          <%# Eval("SagAdet").ToString() == "0" || Eval("SolAdet").ToString() == "0" ? Eval("Miktar") + " " + Eval("StokCins") : "<br/><div class='gozDuz' > Sağ </div>  Göz: " + Eval("SagAdet") + " " + Eval("StokCins") + "<br/> <div class='gozDuz' > Sol  </div> Göz: " + Eval("SolAdet") + " " + Eval("StokCins")%>
                      </ItemTemplate>
                      <ItemStyle CssClass="orta" />
                      <HeaderStyle Width="140" Font-Bold="true" />
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="Birim Toplam" HeaderStyle-CssClass="orta" >
                      <ItemTemplate>
                       <%# Eval("Birim")%>
                      </ItemTemplate>
                      <ItemStyle CssClass="orta" />
                      <HeaderStyle Width="120" Font-Bold="true" />
                      </asp:TemplateField>
                    </Columns>
                    <RowStyle  CssClass="RowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle  CssClass="AlternatingRowStyle" />
                   </asp:GridView>
                   </td>
              </tr>
              <tr>
                  <td class="toplamBilgi"  colspan="2" >
                      Birim Toplam:</td>
                  <td class="toplamFiyat" style=" width:230px;">
                      <asp:Label ID="lblBirimToplam" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr runat="server" id="trIndirim" visible="false" >
                  <td class="toplamBilgi" style="color:#d2063f;" colspan="2" >
                      İndirim:</td>
                  <td class="toplamFiyat" style="color:#d2063f;" >
                      <asp:Label ID="lblIndirim" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="toplamBilgi" colspan="2">
                      KDV Toplam:</td>
                  <td class="toplamFiyat">
                      <asp:Label ID="lblKdvToplam" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="toplamBilgi" style="text-align:left; padding-left:20px;" >
                      <asp:Image ID="Image1" ImageUrl="~/Images/kargo.gif" runat="server" />
                    </td>
                  <td class="toplamBilgi">
                     Kargo Ücreti:
                  </td>
                  <td class="toplamFiyat">
                      <asp:Label ID="lblKargoToplam" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="toplamBilgi" style="border-bottom:1px solid #d9dada;" colspan="2" >
                       Toplam Ödeme:</td>
                  <td class="toplamFiyat" style="border-bottom:1px solid #d9dada;" >
                   <asp:Label ID="lblToplam" runat="server"></asp:Label>

                  </td>
              </tr>
              <tr>
                  <td  style="border-bottom:1px solid #d9dada;padding:10px 10px;" colspan="2" >
                  <a name="onay"></a>
                  
                     <asp:CheckBox ID="ckbSozlesmeOnay" ClientIDMode="Static"  runat="server" /> 
                      <a href="#" class="osx" >Satış Sözleşmesini Okudum ve Kabul Ediyorum.</a>
      
 
                  </td>
                  <td style="border-bottom:1px solid #d9dada;" >
                      &nbsp;</td>
              </tr>
               <tr>
                  <td style="border-bottom:1px solid #d9dada;padding:30px 20px;" colspan="3" >

                      <div class="line" >
                          <label style="color: #069; display:block;" >Sipariş Notu:</label>
                          <asp:TextBox ID="txtSiparisNot" TextMode="MultiLine" Width="400px"
                           MaxLength="10"  Rows="4" runat="server"></asp:TextBox>
                      </div>
                      <div style=" float:right;" >
                            <input id="btnShow" type="button"  runat="server" style="display:none" />

                            <asp:ImageButton ID="btnOdeme" ImageUrl="~/images/buttonlar/devam.gif" 
                                    runat="server"  ClientIDMode="Static" onclick="btnOdeme_Click"  />
                 
                      </div>

                  </td>
              </tr>
        
        </table>



     
   </div>



   <!-- modal content -->
    <div id="osx-modal-content">
			<div id="osx-modal-title">LENSOPTİK.COM.TR UZAK MESAFELİ SATIŞ SÖZLEŞMESİ</div>
			<div class="close"><a href="#" class="simplemodal-close">x</a></div>
			<div id="osx-modal-data" style="height:600px; overflow:auto; padding:0px 30px;">
                <table style="width: 100%;text-align:left;">
                <tr>
                <td colspan="2">
                        <p class="sozlesmeMadde" > MADDE 1 - TARAFLAR:</p>

                     </td>
                </tr>

                  <tr>
                    <td colspan="2">
                        <p class="sozlesmeMadde" > 1.1 - SATICI BİLGİLERİ:</p>
                     </td>
                </tr>
                <tr>
                    <td>
                    Ad / Soyad / Unvan
                    </td>
                    <td>
                    : Lens Optik
                    </td>
                </tr>
                <tr>
                    <td>
                    Telefon
                    </td>
                    <td>
                        :( 212 ) 442 40 10
                    </td>
                </tr>
                   <tr>
                    <td>
                     Mail
                    </td>
                    <td>
                    : info@lensoptik.com.tr
                    </td>
                </tr>
                   <tr>
                    <td>
                    Web
                    </td>
                    <td>
                    : www.lensoptik.com.tr
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                   <p class="sozlesmeMadde" > 1.2 - ALICI BİLGİLERİ :</p>
               </td>
                </tr>
             <tr>
                 <td>Adı/Soyadı/Ünvanı:</td>
                 <td><asp:Label ID="lblSozAd" runat="server" ></asp:Label></td>
             </tr>

             <tr>
                 <td>Telefon:</td>
                 <td><asp:Label ID="lblSozTel" runat="server" ></asp:Label></td>
             </tr>
           
            <tr>
                 <td>Adres:</td>
                 <td><asp:Label ID="lblSozAdres" runat="server"></asp:Label></td>
             </tr>
            <tr>
                <td colspan="2">
                        <p class="sozlesmeMadde" > MADDE 2 - KONU:</p>
               <p >
                Işbu sözleşmenin konusu, SATICI'nın, ALICI'ya satışını yaptıgı, aşagıda 
                nitelikleri ve satış fiyatı belirtilen ürünün satışı ve teslimi ile ilgili 
                olarak 4077 sayılı Tüketicilerin Korunması Hakkındaki Kanun-Mesafeli 
                Sözleşmeleri Uygulama Esas ve Usulleri Hakkında Yönetmelik hükümleri gereğince 
                tarafların hak ve yükümlülüklerinin saptanmasıdır.</p>

                     </td>
                </tr>
                  <tr>
                    <td colspan="2">
            <p class="sozlesmeMadde" > MADDE 3 - SÖZLEŞME KONUSU ÜRÜN BİLGİLERİ:</p>
            <p >
                Ürünlerin Cinsi ve Türü, Miktarı, Marka/Modeli, Rengi ve Satış Bedeli ve Teslimat Bilgileri aşağıda belirtildiği gibidir.
            </p>
            <p >
                olup, bu vaatler &#8230;&#8230;&#8230;&#8230;&#8230;&#8230; tarihine kadar geçerlidir.
            </p>
              </td>
              </tr>
          <tr>
          <td colspan="2" >
            Mal / Ürün/Hizmet türü:
          </td>
          </tr>
           <tr>
          <td colspan="2" class="sozlesmeListe" >
            <asp:GridView ID="gvwSozlesme" runat="server"  AutoGenerateColumns="false" CellPadding="4" 
          GridLines="None"   Width="100%"  >
                       <Columns>
                            <asp:TemplateField HeaderText="Ürün Adı" HeaderStyle-CssClass="orta" >
                               <ItemTemplate>
                            <%# Eval("Urun")%>
                               </ItemTemplate>
                                <ItemStyle CssClass="orta" />
                                <HeaderStyle />
                               <HeaderStyle Width="420"   />
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="Birim Fiyatı" HeaderStyle-CssClass="orta"  >
                               <ItemTemplate>
                                      <%# Eval("Fiyat") %>
                               </ItemTemplate>
                                <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="100" />
                           </asp:TemplateField>

                             <asp:TemplateField HeaderText="KDV" HeaderStyle-CssClass="orta" >
                               <ItemTemplate>
                                <%# Eval("KDV")%>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="80"  />
                           </asp:TemplateField>
                          
                             <asp:TemplateField HeaderText="Adet" HeaderStyle-CssClass="orta" >
                               <ItemTemplate>
                                   <asp:Label ID="lblMiktar" runat="server" Text='<%# Eval("Miktar") %>' ></asp:Label>
                               </ItemTemplate>
                                <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="100" />
                           </asp:TemplateField>

                             <asp:TemplateField HeaderText=" KDV Dahil Fiyat" HeaderStyle-CssClass="orta" >
                               <ItemTemplate>
                              <%#  Eval("Birim")%>
                               </ItemTemplate>
                               <ItemStyle CssClass="orta" />
                               <HeaderStyle Width="120" />
                           </asp:TemplateField>
                       </Columns>
                   </asp:GridView>
          </td>
          </tr>
          <tr>
              <td> Kargo Ücreti</td>
              <td> : <asp:Label ID="lblSozKargo" runat="server" Text="Label"></asp:Label> </td>
              </tr>  
              <tr>
              <td>Satış Fiyatı (KDVdahil) </td>
              <td>:<asp:Label ID="lblSozToplamFiyat" runat="server" Text="Label"></asp:Label></td>
          </tr>
            <tr>
             <td colspan="2">    
            <p class="sozlesmeMadde" > MADDE 4 - GENEL HÜKÜMLER:</p>
      
            <p >
                4 - 1 ALICI, Madde 4 ' te belirtilen sözleşme konusu ürünün temel nitelikleri, 
                satış fiyatı ve ödeme şekli ile teslimata ilişkin tüm ön bilgileri okuyup bilgi 
                sahibi olduğunu ve elektronik ortamda gerekli teyidi verdiğini beyan eder.</p>
            <p >
                4 - 2 Sözleşme konusu ürün, ALICI' dan başka bir kişi/kuruluşa teslim edilecek 
                ise, teslim edilecek kişi/ kuruluşun teslimatı kabul etmemesinden<span>&nbsp; </span>
                www.lensoptik.com.tr sorumlu tutulamaz.</p>
            <p >
                4 - 3 SATICI, sözleşme konusu ürünün, sağlam eksiksiz, siparişte belirtilen 
                niteliklere uygun varsa garanti belgeleri ve kullanım kılavuzları ile teslim 
                etmesinden sorumludur.</p>
            <p >
                4 - 4 Sözleşme konusu ürün, yasal 30 günlük süreyi aşmamak koşulu ile her bir 
                ürün için ALICI ' nın yerleşim yerinin uzaklığına bağlı olarak ön bilgiler 
                içinde açıklanan süre içinde ALICI veya gösterdiği adresteki kişi/ kuruluşa 
                teslim edilir.</p>
            <p >
                5 - 5 Sözleşme konusu ürünün teslimatı için bu sözleşmenin imzalı nüshasının 
                SATICI' ya ulaştırılmış olması ve bedelinin ALICI' nın tercih ettiği ödeme şekli 
                ile ödenmesi şarttır. Herhangi bir nedenle ürün bedeli ödenmez veya banka 
                kayıtlarında iptal edilir ise, SATICI ürünün teslimi yükümlülüğünden kurtulmuş 
                kabul edilir.</p>
            <p >
                4 - 6 Ürünün tesliminden sonra ALICI ' ya ait kredi kartının ALICI' nın 
                kusurundan kaynaklanmayan bir şekilde yetkisiz kişilerce haksız ve hukuka aykırı 
                olarak kullanılması nedeni ile banka veya finans kuruluşunun ürün bedelini 
                SATICI ' ya ödememesi halinde, ALICI' nın kendisine teslim edilmiş olması 
                kaydıyla ürünün 3 iş günü içerisinde SATICI' ya gönderilmesi zorunludur. Bu 
                taktirde nakliye giderleri ALICI' ya aitttir.</p>
            <p >
                4 - 7 SATICI mücbir sebepler veya nakliyeyi engelleyen hava muhalefeti, ulaşımın 
                kesilmesi gibi olağanüstü durumlar nedeni ile sözleşme konusu ürünü süreci 
                içinde teslim edemez ise, durumu ALICI' ya bildirmekle yükümlüdür.Bu taktirde 
                ALICI' nın siparişin iptal edilmesini, sözleşme konusu ürünün varsa emsali ile 
                değiştirilmesini, ve / veya teslimat süresinin engelleyici durumun ortadan kalkmasına kadar ertelenmesi haklarından birini kullanabilir.ALICI' nın 
                siparişi iptal etmesi durumunda ödediği tutar 10 gün içinde kendisine nakten ve 
                defaten ödenir.</p>
            <p >
                5 - 8 ALICI garanti belgesi ile satılan ürünlerden olan veya olmayan ürünlerin 
                arızalı veya bozuk olanlar, garanti şartları içinde gerekli onarımının yapılması 
                için Ürün yetkili servis merkezi' ne bildirebilir eğer yoksa ürününü SATICI' ya 
                gönderebilir bu taktirde kargo giderleri ALICI' tarafından karşılanacaktır.</p>
            <p >
                4 - 9 İş bu sözleşme, ALICI tarafından imzalanıp SATICI' ya ulaştırılmasından 
                sonra geçerlik kazanır.</p>
            <p >
                4 - 10 Siparişleriniz, banka yada www.lensoptik.com.tr Kredi Kartı güvenlik 
                birimi tarafından gerekli durumlarda veya kart sahibinin farklı olması 
                durumunda, siparişleriniz ile ilgili onay alabilmek için kredi kartı sahibine, 
                sistemlerinde kayıtlı bir telefon numarasından ulaşılamaz ise, siparişinizi 
                kredi kartı sahibinin güvenliğini sağlama amacı ile iptal edebilmekteyiz.</p>
            <p >
                4 - 11 .Her siparişte banka kayıtları ve bilgisayar ip numarası kayıt altına 
                alınarak herhangi bir sahtekarlık durumunda savcılığa bildirilerek sorumlu ip 
                sahibi hakkında sahtekarlık suçu ile hakkında yasal işlem başlatılacaktır.</p>
         
            <p class="sozlesmeMadde" >MADDE 5 - CAYMA HAKKI :</p>
           
            <p >
                ALICI' sözleşme konusu ürünün kendisine veya gösterdiği adresteki kişi / 
                kuruluşa tesliminden ( 7 ) gün içinde cayma hakkına sahiptir. Cayma hakkının 
                kullanılması için bu süre içinde SATICI ' YA faks, email veya telefon ile 
                bildirimde bulunulması ve ürünün 7. madde hükümleri çerçevesinde kullanılmamış 
                olması şarttır. Bu hakkın kullanılması halinde, 3. kişiye veya ALICI' ya teslim 
                edilen ürünün SATICI ya gönderildiğine ilişkin kargo teslim 
                tutanağı örneği ile fatura aslının iadesi zorunludur. Bu belgelerin SATICI ' ya 
                ulaşmasını takip eden 7 gün içerisinde ürün bedeli ALICI' ya iade edilir.Fatura 
                aslı gönderilmez ise KDV ve varsa şair yasal yükümlülükler iade edilmez.Cayma 
                hakkı nedeni ile iade ürünün kargo bedeli ALICI tarafından karşılanır.</p>
         
         
            <p class="sozlesmeMadde" > MADDE 6 - CAYMA HAKKI KULLANILAMAYACAK ÜRÜNLER </p>
          
              <p>Cayma hakkının kullanılması, ürünün ambalanjının açılmamış bozulmamış ve ürünün kullanılmamış olması şartına bağlıdır.</p>


 <p class="sozlesmeMadde" > MADDE 7 - BORÇLUNUN TEMERRÜDÜ </p>
          
              <p> ALICI&#8217;nın temerrüde düşmesi halinde, ALICI, borcun gecikmeli ifasından dolayı SATICI&#8217;nın oluşan zarar ve ziyanını ödemeyi kabul eder.</p>

            <p class="sozlesmeMadde" > MADDE 8 -GARANTİ, İADE VE İPTAL </p>

<p>Ürünün Garantisi; Satışını yaptığımız tüm ürünler, üretim ve benzeri hatalara karşı üretici firma garantisi altındadır. </p>

<p>İade Şartları; 4077 sayılı Tüketicinin Korunması Hakkında Kanun da belirtildiği gibi satın alınan ürünler kutusu açılmamış ve deforme olmamış ise satıştan itibaren 15 gün içerisinde iade alınır ve bedeli ödenir. </p>

<p>Sipariş İptali; Ödeme onayı alınmış ve kargoya teslim edilmemiş siparişin iptal talebini telefon ile müşteri hizmetlerimize iletmeniz yeterlidir. Siparişiniz ve ödemeniz en kısa sürede iptal edilerek, yapılan ödeme kredi katınıza/banka hesabınıza iade edilecektir. Ödemesi alınmış ve kargoya teslim edilmiş siparişlerinizin iadesi KDV+ geliş, gidiş kargo ücreti kesilerek iade edilir. </p>

              <p class="sozlesmeMadde" > MADDE 9 - YETKİLİ MAHKEME </p>

              <p> İşbu sözleşmenin uygulanmasında, Sanayi ve Ticaret Bakanlığınca ilan edilen değere kadar Tüketici Hakem Heyetleri ile SATICI'nın yerleşim yerindeki Tüketici Mahkemeleri yetkilidir.Siparişin gerçekleşmesi durumunda ALICI işbu sözleşmenin tüm koşullarını kabul etmiş sayılır.</p>
            </td>
                </tr>
            <tr>
             <td colspan="2">
             SATICI:&nbsp;  &nbsp; &nbsp;Lens Optik
             </td>
             <td>
             </td>
            </tr>
            <tr>
             <td colspan="2">
             ALICI: &nbsp;  &nbsp; &nbsp;&nbsp;<asp:Literal ID="lblAlici" runat="server"></asp:Literal> 
             </td>
             </tr>
               <tr>
             <td colspan="2">
             TARIH: &nbsp;  &nbsp; &nbsp;<asp:Literal ID="lblSozlesmeTarih" runat="server"></asp:Literal> 
             </td>
             </tr>
            </table>
				<p><button class="simplemodal-close button">&nbsp; Kapat&nbsp;</button></p>
			</div>
		</div>

</asp:Content>

