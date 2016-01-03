<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="bankaMesajlar" Title="Banka Mesajları" Codebehind="bankaMesajlari.aspx.cs" %>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">
 
    <style type="text/css">
   
   
   #bankaMesajlar
   {
   } 
   
   #bankaMesajlar td
   {
   vertical-align:top;   
   padding-bottom:3px;
   padding-top:3px;
   border-bottom: 1px solid #E8E7E7;
   } 
 
 
    .mesaj
    {
      float:left;
      display:inline;
      padding-top:2px;
      padding-bottom:1px;
    }
    
    .bilgi-1
    {
      width:70px;
      color:#0070b0;
    }
    
    .bilgi-2
    {
      width:280px;
      clear:right;
    }
    
    .siparis-1
    {
      width:70px;
      color:#0070b0;
    }
    
    .siparis-2
    {
      width:150px;
      clear:right;
    }
    
     .kart-1
    {
      width:70px;
      color:#0070b0;
    }
    
    .kart-2
    {
      width:200px;
      clear:right;
    }
    
    .siparisVar
    {
        color:Green;
        text-decoration:underline;
    }
    
    
    .siparisYok
    {
        color:Red;
    }
    
    </style>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
  <div class="content-box"><!-- Start Content Box -->
		    <div class="content-box-header">
					<h3 style="cursor: s-resize;">
                     <asp:Label ID="lblSayfaBaslik" runat="server" ></asp:Label>
                    </h3>
					<div class="clear"></div>
			</div> <!-- End .content-box-header -->
			<div style="display: block;" class="content-box-content">
		    <div style="display: block;" class="tab-content default-tab" id="tab1">

			  
          <div style="padding-bottom:10px; text-align:right;" >

            <asp:Button ID="btnHataMesajSil" CssClass="button"  runat="server" 
                  Text="Hata Mesajlarını Sil" Width="160px" onclick="btnHataMesajSil_Click"   />
           </div>
           
                <asp:Repeater ID="rptHatalar" runat="server">
                <HeaderTemplate>
                    <table id="bankaMesajlar" >
                        <tr>
                            <td><label>Banka Mesajı </label></td>
                            <td><label>Kullanıcı </label></td>
                            <td><label>Kart Bilgileri </label></td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                 <tr>
                   <td>
                       <div>
                      
                       <div>
                           <div class="mesaj bilgi-1">Banka</div>
                           <div class="mesaj bilgi-2">:
                           <%# DataBinder.Eval(Container, "DataItem.BankaAdi")%>   </div>
                        </div>

                        <div >
                           <div class="mesaj bilgi-1">Mesaj</div>
                           <div class="mesaj bilgi-2">:
                           <%# DataBinder.Eval(Container, "DataItem.RedMesaj")%>   </div>
                        </div>

                          <div>
                           <div class="mesaj bilgi-1">Kodu</div>
                           <div class="mesaj bilgi-2">:
                            <%# DataBinder.Eval(Container, "DataItem.RedMesajKodu")%>  &nbsp; </div>
                         </div>

                         <div>
                           <div class="mesaj bilgi-1">Tarih</div>
                           <div class="mesaj bilgi-2">:
                           <%# BusinessLayer.DateFormat.TarihSaat( DataBinder.Eval(Container, "DataItem.Tarih").ToString())%></div>
                         </div>

                          <div>
                           <div class="mesaj bilgi-1">Sayı</div>
                           <div class="mesaj bilgi-2">:
                           <%# DataBinder.Eval(Container, "DataItem.HataSayisi")%></div>
                         </div>
                         

                       </div>
                      </td>
                      <td>
                         <div>
                           <div class="mesaj siparis-1">Sipariş No</div>
                           <div class="mesaj siparis-2">:

                      <%# Eval("SiparisId").ToString() ==  "0"
                                                  ? "<span class='siparisYok'>" + Eval("SiparisNo") + "</span>" :
                                                   "<a href='siparisDetay.aspx?siparisId=" + Eval("SiparisId") + "' class='siparisVar'>" +
                          Eval("SiparisNo") + "</a>"%>

           

                         </div>
                           <div>
                            <div class="mesaj siparis-1">Üye Adı</div>
                            <div class="mesaj siparis-2">:
                            <a style="text-decoration:underline; color:#555; "
                             href='uyeler.aspx?kullaniciId=<%# DataBinder.Eval(Container, "DataItem.KullaniciId")%>'>
                            <%# DataBinder.Eval(Container, "DataItem.AdiSoyadi")%>
                            </a> 
                            
                            </div>
                          </div>
                      </td>
                       <td>

                       <div>
                           <div class="mesaj kart-1">Kart Adı</div>
                           <div class="mesaj kart-2">:
                            <%# DataBinder.Eval(Container, "DataItem.KartAd")%>  </div>
                         </div>

                         <div>
                           <div class="mesaj kart-1">Kart No</div>
                           <div class="mesaj kart-2">:
                            <%# DataBinder.Eval(Container, "DataItem.KartNo")%>  </div>
                         </div>
                        
                        <div>
                           <div class="mesaj kart-1">CV2</div>
                           <div class="mesaj kart-2">:
                            <%# DataBinder.Eval(Container, "DataItem.KartCV2")%>  </div>
                         </div>
                         <div>
                           <div class="mesaj kart-1">Taksit</div>
                           <div class="mesaj kart-2">:
                            <%# DataBinder.Eval(Container, "DataItem.Taksit")%>  </div>
                         </div>
                  
                         <div>
                           <div class="mesaj kart-1">Fiyat</div>
                           <div class="mesaj kart-2">:
                            <%# Convert.ToDecimal(Eval("ToplamFiyat")).ToString("c") %>  </div>
                         </div>

                         
                      </td>

                   </tr>
                </ItemTemplate>
                <FooterTemplate>
                 </table>
                </FooterTemplate>
                </asp:Repeater>

           <div class="pagination">
                <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
            </div>  
			
			</div> <!-- End #tab1 -->
			</div> <!-- End .content-box-content -->
	  </div>

  <script type="text/javascript">
      $(document).ready(function () {
          $("#bankaMesajlar tr:even").addClass("RowStyle");

          $("#bankaMesajlar tr:odd").addClass("RowStyle AlternatingRowStyle ");
      });
 </script>

</asp:Content>

