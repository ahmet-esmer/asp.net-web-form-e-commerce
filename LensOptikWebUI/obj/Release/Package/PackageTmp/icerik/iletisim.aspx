<%@ Page Title="Lens Optik iletişim bilgileri" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Icerik_Iletisim" Codebehind="iletisim.aspx.cs" %>

<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>
<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc7" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

    <!-- Günün ürünü -->
         <uc1:GununUrunu ID="GununUrunu1" runat="server" />
     <!-- Günün ürünü sonu -->

     <!-- banner -->
          <uc2:Banner ID="Banner1" runat="server" />
     <!-- baner sonu -->

     <!-- sol panel -->
     <div  id="solPanel" >
         <uc3:Kategoriler ID="Kategoriler1" runat="server" />
         <uc4:Markalar ID="Markalar1" runat="server" />
         <uc5:Facebook ID="Facebook1" runat="server" />
         <uc6:SSSorular ID="SSSorular1" runat="server" />
    </div>
    <!-- sol panel sonu -->

    <div id="ortaSag">
        <div id="ortaSagIc"> 
         <table style="width:100%;" >
              <tr>
                <td class="icerik-Baslik" colspan="2"  >
                      <h5> İletişim Bilgileri </h5>  
                </td>
            </tr>
              <tr>
                <td colspan="2">
                  <div style="width:650px;padding:10px;" >
                    <uc7:Mesajlar ID="Mesaj" runat="server" />
                 </div>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi"  >
                <span class="yazi1" >Firma Adı: </span>    </td>
               <td >
                   <asp:Label ID="lblFirma" runat="server"></asp:Label>
                </td>
            </tr>
            <tr  runat="server" id="rvYetkili" visible="false"  >
                <td class="uyuFormBilgi" style="height: 12px" >
                   <span class="yazi1" > Yetkili Kişi</span> </td>
               <td style="height: 12px"    >
                   <asp:Label ID="lblYetkili" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" >
                  <span class="yazi1" >  Telefon:</span>  </td>
               <td    >
                   <asp:Label ID="lblTelefon" runat="server"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="rvFaks" visible="false" >
                <td class="uyuFormBilgi" style="height: 12px" >
                 <span class="yazi1" >Faks: </span>    </td>
               <td style="height: 12px"    >
                   <asp:Label ID="lblFaks" runat="server"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="rvVergiNo" visible="false"  >
                <td class="uyuFormBilgi" >
                  <span class="yazi1" > Vergi Numarası:</span>   </td>
               <td    >
                   <asp:Label ID="lblVergiNo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr runat="server" id="rvVergiDa" visible="false"  >
                <td class="uyuFormBilgi" >
                 <span class="yazi1" > Vergi Dairesi:</span>   
                </td>
               <td    >
                   <asp:Label ID="lblVergiDaire" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
              <td class="uyuFormBilgi" >
              <span class="yazi1" >Mail:
               <a class="msnAdd link" href="msnim:add?contact=info@lensoptik.com.tr ">(MSN destek)</a> </span> 
              </td>
               <td    >
                   <asp:Label ID="lblEposta" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" >
                  <span class="yazi1" >   </span>  </td>
               <td style="line-height:20px;padding-bottom:20px;"    >
                   <asp:Label ID="lblAdres" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="icerik-Baslik" style="border-top: 1px solid #ccc;"  colspan="2"  >
                      <h5> İletişim / Dilek, görüş, öneri ve şikayet formu </h5>  
                </td>
            </tr>
            <tr>
                <td class="uyuBilgiYazi" colspan="2">
                  Dilek, görüş, öneri ve şikayetleriniz için bize yazabilirsiniz. İlgili birim en kısa süre içinde size cevap verecektir.
                    </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top" >
                 <span class="yazi1" > Adınız / Soyadınız: </span>    </td>
                <td>
                    <asp:TextBox ID="txtAdSoyad" runat="server" Width="250px"  CssClass="text"  ></asp:TextBox><br />
                     <asp:RequiredFieldValidator ID="rfvAdsoyad" runat="server" 
                     CssClass="input-notification error" ForeColor="" Display="Dynamic" ErrorMessage="Lütfen  Ad ve Soyad Alanını Doldurunuz." ValidationGroup="iletisim" ControlToValidate="txtAdSoyad"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                 <span class="yazi1" >  E-mail Adresiniz:</span>   </td>
                <td>
          
                    <asp:TextBox ID="txtEposta" runat="server" Width="250px"  CssClass="text" ></asp:TextBox><br />
                      <asp:RequiredFieldValidator ID="rfvEposta" runat="server" Display="Dynamic" 
                       ErrorMessage="Lütfen E-Posta Adresi Giriniz."  ValidationGroup="iletisim"  
                       CssClass="input-notification error" ForeColor=""
                       ControlToValidate="txtEposta"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="rxvEposta" runat="server" Display="Dynamic"
                       ControlToValidate="txtEposta" ErrorMessage="Lütfen geçerli E-Posta adresi giriniz." 
                       ValidationGroup="iletisim" CssClass="input-notification error" ForeColor="" 
                       ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                 <span class="yazi1" >İrtibat Telefonu: </span>   </td>
                <td>
                    <asp:TextBox ID="txtTelefon" runat="server" Width="250px"  CssClass="text"  ></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                <span class="yazi1" >  Konu:</span>   </td>
                <td>
                    <asp:TextBox ID="txtKonu" runat="server" Width="250px"  CssClass="text" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" style="vertical-align:top">
                <span class="yazi1" >  Mesajınız:</span>   </td>
                <td>
                    <asp:TextBox ID="txtMesaj" runat="server" Height="100px" 
                         Rows="4" TextMode="MultiLine" Width="400px" ></asp:TextBox><br />
                      <asp:RequiredFieldValidator ID="rfvMesaj" runat="server" 
                         Display="Dynamic" ValidationGroup="iletisim"
                         CssClass="input-notification error" ForeColor=""
                         ErrorMessage="Lütfen Mesaj  Giriniz."  
                         ControlToValidate="txtMesaj"></asp:RequiredFieldValidator>
               </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="padding:20px 0px;" >
                    <asp:Button ID="btnMesajGonder" Width="130px" runat="server" Text="Formu Gönder" 
              CssClass="button" ValidationGroup="iletisim" onclick="btnMesajGonder_Click" />
           
                </td>
               
            </tr>
        </table>
        </div>
    </div>
    <script type="text/javascript" language="javascript" >
        $('#ortaSag').corner("round 6px");
        $('#ortaSagIc').corner("round 6px");
    </script>     
</asp:Content>

