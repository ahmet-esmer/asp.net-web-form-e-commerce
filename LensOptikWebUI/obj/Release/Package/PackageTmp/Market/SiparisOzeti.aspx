<%@ Page Title="Sipariş Özeti | Lens Optik" Language="C#" MasterPageFile="~/Market/Market.master" AutoEventWireup="true" Inherits="Market_Siparis_Ozeti" Codebehind="SiparisOzeti.aspx.cs" %>

<%@ Register src="MenuMarket.ascx" tagname="MenuMarket" tagprefix="uc1" %>
<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

 <div id="marketAna" >
     <div id="marketMap">
         <uc1:MenuMarket ID="MenuMarket1" runat="server" />
     </div>
     <div id="marketMesaj" style="margin-bottom:20px;" >
         <uc2:Mesajlar ID="Mesaj" runat="server" />
     </div>
    <table style="width:100%;border:1px solid #d9dada;background-color:#fafafa;" >
        <tr>
            <td colspan="2" class="icerik-Baslik Border" >
            <h5>  Sipariş No: <asp:Literal ID="lblsiparisId" runat="server"></asp:Literal> </h5>  
            </td>
        </tr>
        <tr>
            <td class="siparisBilgi" colspan="2" >
                Sayın <asp:Label ID="lblUyeAdi" Font-Bold="true" runat="server" ></asp:Label>
                 <asp:Label ID="lblTarih" runat="server"></asp:Label> Günü lensoptik.com.tr internet sitesinden <asp:Label ID="lblSiparisBaslik" runat="server"></asp:Label> yöntemi ile 
                <asp:Label ID="lblToplam" Font-Bold="true" runat="server" ></asp:Label> lik alışverişiniz tarafımıza 
                ulaşmıştır.
            </td>
        </tr>
        <tr runat="server" id="havaleBilgi" visible="false" >
            <td colspan="2" >
           <table cellpadding="0" cellspacing="0" style="width:100%;"  >
             <tr>
               <td colspan="2" class="icerik-Baslik Border">
                    <h5> Havale yapılacak banka bilgisi </h5>  
               </td>
            </tr>
               <tr>
                <td class="siparisTd">
                    Hesap Adı
                </td>
                <td>
                    : <asp:Label ID="lblHesapAdi" runat="server" ></asp:Label>
               </td>
                     
            </tr>
            <tr>
                <td class="siparisTd" >
                    Banka Adı
                </td>
                <td>
                    : <asp:Label ID="lblBankaAdi" runat="server"></asp:Label> 
               </td>
            </tr>
            
               <tr>
                <td class="siparisTd">
                    Şube
                </td>
                <td>
                  : <asp:Label ID="lblSube"  runat="server"></asp:Label>
               </td>
            </tr>
                <tr>
                <td class="siparisTd">
                    Şube Kodu
                </td>
                <td>
                    : <asp:Label ID="lblSubeKodu" runat="server"></asp:Label>
               </td>
            </tr>
                <tr>
                <td class="siparisTd">
                    Hesap No</td>
                <td>
                    : <asp:Label ID="lblHesapNo" runat="server"></asp:Label>
               </td>
            </tr>
                   <tr>
                <td class="siparisTd">
                    Iban
                </td>
                <td>
                    : <asp:Label ID="lblIban" runat="server"></asp:Label>
               </td>      
             </tr>
            
            <tr>
                <td class="siparisTd">
                    Hesap Tipi
                </td>
                <td>
                    : <asp:Label ID="lblHesapTuru" runat="server" ></asp:Label>
               </td>
            </tr> 
            </table>

            </td>
        </tr>
            <tr>
            <td colspan="2" class="icerik-Baslik Border" >
           <h5> Teslimat Bilgileri </h5>  
            </td>
        </tr>
        <tr>
            <td class="siparisTd" >
               Alıcı 
            </td>
            <td>
            : <asp:Label ID="lblTeslimAlan" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="siparisTd" >
            Adres
            </td>
            <td style="padding-bottom:30px;" >
            : <asp:Label ID="lblTeslimatAdresi" runat="server" ></asp:Label>
            </td>
        </tr>
        
    </table>

     <div style="text-align:right" >
        
        <asp:HyperLink ID="hlAnasayfa" NavigateUrl="~/Default.aspx" runat="server">
            <asp:Image ID="imgAnasayfa" ImageUrl="~/Images/buttonlar/Anasayfa.gif" runat="server" />
         </asp:HyperLink>
     </div>

 </div>

</asp:Content>

