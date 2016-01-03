<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="Admin_SiparisDetay" EnableViewState="true" Codebehind="siparisDetay.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">

     <script type="text/javascript" src="adminScript/jquery-1.3.2.min.js"></script>
     <script type="text/javascript" src="adminScript/yazdir.js" ></script> 


  <script type="text/javascript">
//      $(document).ready(function () {
//          $("#sipDetay tr:even").addClass("RowStyle");

//          $("#sipDetay tr:odd").addClass("RowStyle AlternatingRowStyle ");
//      });
 </script>

    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
         <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header"  >
                <div style="padding-top:15px; padding-left:13px;" >
                <asp:HyperLink ID="hlSiparisler" runat="server" CssClass="maplink" >Siparişler</asp:HyperLink> &nbsp;	>>&nbsp; <span> Sipariş Detay</span>
                </div>
			
                
			<div class="clear"></div>

			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">
               <div style="padding-bottom:30px; text-align:right;" >


    <asp:Button ID="btnMailShow"   CssClass="button" runat="server" Text="E-Posta Gönder" />
    <asp:Button ID="print" CssClass="button"  runat="server" 
            Text="Siparis Yazdır" OnClientClick="yazdir('divMain');false;" />

     <asp:Button ID="btnSiparisSil" CssClass="button" Width="120px"  runat="server" 
            Text="Sipariş Sil" onclick="btnSiparisSil_Click" OnClientClick="return confirm('Bu siparişi silmet istediğinizden emin misiniz?');" />

    </div>

      <div id="divMain" >
    <table class="siparisTablo" cellpadding="0" cellspacing="0px" >
        <tr>
          <td class="siparisBaslik" colspan="4" >
          <div class="baslikBilgi">
             <asp:Label ID="lblUyeAdi" runat="server"></asp:Label> /
&nbsp;<asp:Label ID="lblodemeTipi" runat="server"></asp:Label>
      <asp:Label ID="lblKTaksit" Visible="false" runat="server"></asp:Label>
/&nbsp;<asp:Label ID="lblToplamFiyat" runat="server"></asp:Label>
          </div>
          <div class="Tarih">
            <asp:Label ID="lblTarih" Font-Bold="true" runat="server"></asp:Label>
           </div>
            </td>
        </tr>
        <tr >
            <td class="siparisTd" style="padding-top:20px;padding-bottom:10px; border-bottom:1px solid #ececec; width:160px" >
                Sipariş Durumu:</td>
            <td style="padding-top:20px;padding-bottom:10px;border-bottom:1px solid #ececec;"  >
                <asp:DropDownList ID="ddlSiparisDurum" CssClass="text-input" Width="200px"  runat="server">

                    <asp:ListItem Value="0" Text="Onay Bekliyor"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Sipariş hazırlanıyor"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Kargoya Verildi" ></asp:ListItem>
                    <asp:ListItem Value="3" Text="İptal edildi" ></asp:ListItem>
                    <asp:ListItem Value="4" Text="Ödeme onayı Bekleniyor"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Tedarik edilemedi" ></asp:ListItem>
                    <asp:ListItem Value="6" Text="İptal edilecek" ></asp:ListItem>
                    <asp:ListItem Value="7" Text="Tedarik sürecinde" ></asp:ListItem>
                    <asp:ListItem Value="8" Text="Tedarik edilemiyor" ></asp:ListItem>
               
                </asp:DropDownList>
            </td>
            <td style="padding-top:20px;padding-bottom:10px;border-bottom:1px solid #ececec;"  >
                <asp:CheckBox ID="ckbUyePosta" CssClass="ckbEposta" runat="server" Text=" Güncellemeyi E-posta İle Bildir" />
            </td>
            <td style="padding-top:20px;padding-bottom:10px;border-bottom:1px solid #ececec;"  >
                <asp:Button ID="btnSiparisGuncelle" Height="23px" CssClass="button" 
                    runat="server" Text="Sipariş Durum Degiştir" 
                    onclick="btnSiparisGuncelle_Click" />
            </td>
        </tr>
      <tr>
            <td class="siparisTd" >
            Sipariş Notu</td>
            <td colspan="3"  >
                <asp:Label ID="lblSipariNotu" CssClass="siparisTdBilgi" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="siparisTd" >
                Sipariş No:</td>
            <td colspan="3"  >
             <asp:Label ID="lblSiparisNO1" CssClass="siparisTdBilgi"  runat="server" ></asp:Label>
            </td>
        </tr>
        <tr runat="server" id="Taksit1" visible="false" >
            <td class="siparisTd" >
               Taksit Sayısı:</td>
            <td colspan="3"  >
             <asp:Label ID="lblTaksitSayisi" CssClass="siparisTdBilgi"  runat="server" ></asp:Label>
              </td>
        </tr>
            <tr  runat="server" id="Taksit2" visible="false" >
            <td class="siparisTd" >
               Taksit Tutarı:</td>
            <td colspan="3"  >
             <asp:Label ID="lblTaksitTutari" CssClass="siparisTdBilgi" runat="server" ></asp:Label>
              </td>
        </tr>
           <tr  runat="server" id="Taksit3" visible="false" >
            <td class="siparisTd" >
               Ödeme Yapılan Banka:</td>
            <td colspan="3"  >
             <asp:Label ID="lblBankaAdi" CssClass="siparisTdBilgi" runat="server" ></asp:Label>
              &nbsp; 
                <asp:LinkButton ID="hlOdemeDetayShow" CssClass="odemeDetay" runat="server" 
                    onclick="hlOdemeDetayShow_Click">Ödeme detay</asp:LinkButton>
              </td>
        </tr>
     <%--      <tr>
            <td class="siparisTd" >
                Kargo:</td>
            <td colspan="3"  >
                <asp:Label ID="lblSecilenKargo" CssClass="siparisTdBilgi" runat="server"></asp:Label>
              </td>
        </tr>--%>
           <tr  runat="server" id="HavaleSatir" visible="false" >
            <td class="siparisTd" >
                Havale Edilecek Banka:</td>
            <td colspan="3"  >
                <asp:Label ID="havaleBanka" CssClass="siparisTdBilgi" runat="server"></asp:Label>
              </td>
        </tr>
        <tr>
            <td colspan="4" class="siparisBaslik" >
          Teslimat  Bilgileri
            </td>
        </tr>
        <tr>
            <td class="siparisTd">
                Alıcı:</td>
            <td colspan="3"  >
                <asp:Label ID="lblAlici" CssClass="siparisTdBilgi" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="siparisTd">
                E-Posta:</td>
            <td colspan="3"  >
            <asp:Label ID="lblEPosta" CssClass="siparisTdBilgi" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="siparisTd">
                Telefon:</td>
            <td colspan="3"  >
                <asp:Label ID="lblTelefon" CssClass="siparisTdBilgi" runat="server"></asp:Label>
             </td>
        </tr>
         <tr>
            <td class="siparisTd">
                Adres:</td>
            <td colspan="3"  >
                <asp:Label ID="lblAdres" CssClass="siparisTdBilgi" runat="server"></asp:Label>
             </td>
        </tr>
         <tr>
            <td class="siparisTd">
                Şehir:</td>
            <td colspan="3"  >
                <asp:Label ID="lblSehir" CssClass="siparisTdBilgi" runat="server"></asp:Label>
             </td>
        </tr>
        <tr>
            <td colspan="4" class="siparisBaslik" >
            Fatura Bilgileri</td>
        </tr>
        </table>
                <table id="bireyselFatura" runat="server" class="siparisTablo1" visible="false" >
                    <tr>
                        <td class="siparisTd" style="width:160px" >
                            Ad Soyad:</td>
                        <td >
                            <asp:Label ID="lblBirAdSoyad"  CssClass="siparisTdBilgi" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="siparisTd" >
                            T.C No:</td>
                        <td  >
                            <asp:Label ID="lblBirTC" CssClass="siparisTdBilgi" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="siparisTd">
                            Adres:</td>
                        <td  >
                            <asp:Label ID="lblBirAdres" CssClass="siparisTdBilgi" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table  id="kurumsalFatura" runat="server" class="siparisTablo1"  visible="false"  >
                    <tr>
                        <td class="siparisTd" >
                            Fatura Ünvanı:</td>
                        <td  >
                            <asp:Label ID="lblKurUnvan" CssClass="siparisTdBilgi" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="siparisTd">
                            Vergi No:</td>
                        <td class="siparisTdBilgi" >
                            <asp:Label ID="lblKurVergiNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="siparisTd">
                            Vergi Dairesi:</td>
                        <td >
                            <asp:Label ID="lblKurVergiDairesi" CssClass="siparisTdBilgi" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="siparisTd">
                            Adres:</td>
                        <td >
                            <asp:Label ID="lblKurAdres" CssClass="siparisTdBilgi" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>

   
      <table id="sipDetay" cellpadding="0" cellspacing="0"  >
          <tr class="rowSip" >
            <td style="width:8%;" ></td>
            <td style="width:35%;font-weight:bold" >Ürün Adı</td>
            <td style="width:8%;font-weight:bold" >Birim Fiyatı</td>
            <td style="width:8%;font-weight:bold" >Birim KDV Fiyatı</td>
            <td style="width:8%;font-weight:bold" >KDV</td>
            <td style="width:10%;font-weight:bold" >Adet</td>   
            <td style="width:10%;font-weight:bold" >Birim Toplam</td>  
                
        </tr>
         <asp:Repeater ID="rptSiparisDetay" runat="server">
          <ItemTemplate>
            <tr>
              <td>
               <div >
                <a href='urun_ekle.aspx?id=<%# Eval("urunId")%>&islem=duzenle' target="_blank" >
                  <img src="../Products/Small/<%# Eval("resimAdi") %>" width="70px"/>
               </a>
               </div>
              
              </td>
              <td valign="top" >
               <a href='urun_ekle.aspx?id=<%# Eval("urunId")%>&islem=duzenle' class="urunAdiLink" target="_blank" >
                     <%# DataBinder.Eval(Container, "DataItem.urunAdi")%>
                 </a> <br /> <br />
                 <label> <%# Eval("sagBilgi") %> </label>  
                 <label> <%# Eval("solBilgi") %> </label>
                 <br />
                <%# Eval("HediyeHTML")%>
                <%# Eval("HediyeUrunTekHTML")%>
               </td>
              <td>
                <%# string.Format("{0:C}", Eval("fiyat"))%> 
              </td>
              <td>
                  <%# Eval("KdvDahilFiyat") %>
              </td>
              <td>
                  <%# Eval("urunKDV")%> %
              </td>
              <td>
                <%# Eval("sagAdet").ToString() == "0" || Eval("solAdet").ToString() == "0" ? Eval("adet") + " " + Eval("stokCins") : "<br/> Sağ Göz: " + Eval("sagAdet") + " " + Eval("stokCins") + "<br/>  Sol Göz: " + Eval("solAdet") + " " + Eval("stokCins") %>

              </td>
              <td>
                  <%# Eval("Birim")%> 
              </td>
            </tr>
          </ItemTemplate>
         </asp:Repeater>
          <tr class="rowSip" >
            <td colspan="5" >  </td>
            <td><b>Birim Toplam </b></td>
            <td>
            : <asp:Label ID="lblBirimToplam" runat="server"></asp:Label>
            </td>
           </tr>
            <tr class="rowSip renk" runat="server" id="trIndirim" visible="false" >
            <td colspan="5" >  </td>
            <td><b>İndirim</b> </td>
            <td>
               : <asp:Label ID="lblIndirim" runat="server"></asp:Label>
            </td>
          </tr>
            <tr class="rowSip renk" runat="server" id="havaleVeKapiOdeme"  visible="false" >
            <td colspan="5" >  </td>
            <td><asp:Label ID="lblKapidaVeHavale1" Font-Bold="true" runat="server" ></asp:Label> </td>
            <td>
                : <asp:Label ID="lblKapidaVeHavale2" runat="server"></asp:Label>
            </td>
           </tr>
          <tr class="rowSip renk" runat="server" id="trKulPara"  visible="false" >
            <td colspan="5" >  </td>
            <td><b> Para puan Kulanımı </b></td>
            <td>
               : - <asp:Label ID="lblKulPara" runat="server"></asp:Label>
            </td>
          </tr>
          <tr class="rowSip" >
            <td colspan="5" >  </td>
            <td> <b>  KDV Toplam </b> </td>
            <td>
            : <asp:Label ID="lblKdvToplam" runat="server"></asp:Label>
            </td>
          </tr>
          <tr class="rowSip" runat="server" id="trKargo" visible="false" >
            <td colspan="5" >  </td>
            <td><b>  Kargo Ücreti (kvd dahil)</b> </td>
            <td>
               : <asp:Label ID="lblKorgoUcreti" runat="server"></asp:Label>
            </td>
          </tr>
        
          <tr class="rowSip" >
            <td colspan="5" >  </td>
            <td><b>Toplam Ödeme</b></td>
            <td>
              : <asp:Label ID="lblToplam" runat="server"></asp:Label>
            </td>
          </tr>
      </table>

      <asp:HiddenField ID="hdfUyeEposta" runat="server" />
      <asp:HiddenField ID="hdfUyeAdi" runat="server" />
      <asp:HiddenField ID="hdfSiparisNo" runat="server" />

    </div>
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>
   
          <asp:ImageButton ID="btnShow" runat="server" Style="display: none;" />
         <cc1:ModalPopupExtender ID="ModalPopupExtender" BackgroundCssClass="popUpBg" runat="server"  TargetControlID="btnShow" PopupControlID="pnlPopup" >
         </cc1:ModalPopupExtender>
 <asp:Panel ID="pnlPopup" runat="server" Style="display: none">
   <div id="facebox" > 
   <div class="popup">    
        <table>        
           <tbody>             
           <tr>              
              <td class="tl"></td>
              <td class="b"></td>
              <td class="tr"></td>           
              </tr>           
              <tr>            
              <td class="b"></td>    
              <td class="body">  
               <div class="content" style="display: block; ">
                <div id="messages" style="width:400px" >
                
                <p>
                <label> Sipariş No:
                    <asp:Label ID="lblSiparisNo" CssClass="secenek" Font-Size="12px" runat="server"></asp:Label>
&nbsp;Ödeme Bilgisi </label>
                </p>
                <p>
              <label>Ödeme Yapan: <asp:Label ID="lblOdemeYapan" CssClass="secenek" Font-Size="12px" runat="server"></asp:Label></label>
          
              </p>
                <p>
               <label>No: <asp:Label ID="lblKartNo" CssClass="secenek" Font-Size="12px" runat="server"></asp:Label></label>
               </p>
                <p>
               <label>Referans No: <asp:Label ID="lblReferansNo" CssClass="secenek" Font-Size="12px" runat="server"></asp:Label></label>
               </p>
                <p> 
              <label>Onay Kodu: <asp:Label ID="lblOnayKodu" CssClass="secenek" Font-Size="12px" runat="server"></asp:Label> </label> 
               </p>
                    
                </div>
                </div> 
                 <div class="footer" style="display: block; ">     
                   
                   <asp:ImageButton ID="imgKapat" ImageUrl="images/closelabel.gif" 
                   CausesValidation="False"  CssClass="close_image" runat="server" />

                  </div>               
                 </td>              
                 <td class="b"></td>         
                </tr>        
              <tr> 
                  <td class="bl"></td>
                  <td class="b"></td>
                  <td class="br"></td>        
              </tr>        
             </tbody>        
        </table>    
   </div>
</div>
 </asp:Panel>
                       
 
    <cc1:ModalPopupExtender ID="ModalMail" BackgroundCssClass="popUpBg" runat="server"  TargetControlID="btnMailShow" PopupControlID="pnlPopupMail" >
         </cc1:ModalPopupExtender>
 <asp:Panel ID="pnlPopupMail" runat="server" Style="display: none">
   <div id="facebox" > 
   <div class="popup">    
        <table>        
           <tbody>             
           <tr>              
              <td class="tl"></td>
              <td class="b"></td>
              <td class="tr"></td>           
              </tr>           
              <tr>            
              <td class="b"></td>    
              <td class="body"  style=" width:800px;" >  
               <div class="content" style="display: block; width:800px;">
                
            
     <p>
    <label>
    <asp:Label ID="lblEpostaAdi" runat="server" CssClass="secenek" Font-Size="12px" ></asp:Label>
     'e&nbsp;  E-Posta Gönder</label>
     </p>
     <p>
   <span style="font-weight:bold; " >E-Posta Başlık </span> 
   <asp:TextBox ID="txtMailBaslik" CssClass="form" Width="350px" runat="server" ></asp:TextBox>  
        
    </p> 
        <FCKeditorV2:FCKeditor ID="fckSiparisMail" runat="server"   ToolbarSet="kisaTool" Height="350px" >
        </FCKeditorV2:FCKeditor>
    <p>
    <asp:Button ID="btnEpostaGonder" runat="server" CssClass="button" Height="23px" 
                    Text=" E-Posta Gönder " ValidationGroup="ePosta"  onclick="btnEpostaGonder_Click" />
    </p>
                </div> 
                 <div class="footer" style="display: block; ">     
                   
                   <asp:ImageButton ID="ImageButton1" ImageUrl="images/closelabel.gif" 
                   CausesValidation="False"  CssClass="close_image" runat="server" />

                  </div>               
                 </td>              
                 <td class="b"></td>         
                </tr>        
              <tr> 
                  <td class="bl"></td>
                  <td class="b"></td>
                  <td class="br"></td>        
              </tr>        
             </tbody>        
        </table>    
   </div>
</div>
 </asp:Panel>
                       
 
 

    
</asp:Content>




