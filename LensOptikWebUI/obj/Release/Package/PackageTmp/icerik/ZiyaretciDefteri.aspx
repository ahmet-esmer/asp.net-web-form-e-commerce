<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="Icerik_ZiyaretciDefteri" EnableViewState="false" ValidateRequest="false"  Codebehind="ZiyaretciDefteri.aspx.cs" %>


<%@ Register src="../Include/GununUrunu.ascx" tagname="GununUrunu" tagprefix="uc1" %>
<%@ Register src="../Include/Banner.ascx" tagname="Banner" tagprefix="uc2" %>
<%@ Register src="../Include/LeftColumn/Kategoriler.ascx" tagname="Kategoriler" tagprefix="uc3" %>
<%@ Register src="../Include/LeftColumn/Markalar.ascx" tagname="Markalar" tagprefix="uc4" %>
<%@ Register src="../Include/LeftColumn/Facebook.ascx" tagname="Facebook" tagprefix="uc5" %>
<%@ Register src="../Include/LeftColumn/SSSorular.ascx" tagname="SSSorular" tagprefix="uc6" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

  <script type="text/javascript" src="<%# ResolveUrl("~/Scripts/jquery.json-2.2.js") %>" ></script>
  <script type="text/javascript" src="<%# ResolveUrl("~/Scripts/ziyaretciDefteri.js") %>" ></script>

    <style type="text/css">
    
    .beginRequest
    {
       display:block;
       background-color:Gray;
       position:absolute;
       width:704px; 
       height:292px; 
       padding-top:230px; 
       text-align:center; 
       opacity:0.6;
       filter:alpha(opacity=60); /* For IE8 and earlier */  
    }
   
   .endRequest
   {
       display:none;
   }
    
   </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

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
        <!-- Ziyaretçi defteri -->

        <asp:Repeater ID="rptZiyaretci" runat="server">
         <HeaderTemplate>
            <div class="icerik-Baslik zBaslik"><h5>Ziyaretçi Defteri</h5></div>
            <div class="icerik-Baslik yorumEkle"  ><a id="formLink" class="link" >Siz de Mesaj Bırakın</a></div>
        </HeaderTemplate>
         <ItemTemplate>
 
         <div > </div>
         
            <div class="zAd">
            <b><%#  DataBinder.Eval(Container, "DataItem.adSoyad")%></b> <br />
            <span style="font-size:11px; color:#747679;">  <%#  DataBinder.Eval(Container, "DataItem.sehirAd")%>/
            <%#  DataBinder.Eval(Container, "DataItem.ilceAd")%></span>
    
            </div>
            <div class="zTarih">
              <%# BusinessLayer.DateFormat.Tarih(Convert.ToString(DataBinder.Eval(Container, "DataItem.eklenmeTarihi")))%>
            </div>
         
        
        <div id='<%# Eval("id").ToString() %>'  class="yorum" >
         <%# DataBinder.Eval(Container, "DataItem.yorum")%> 
        </div>

         <asp:Panel ID="pnlCevap" CssClass="yorum" runat="server" Visible='<%# Eval("yorumCevap").ToString() == "0" ? false:true %>'>
            <b> Müşteri Hizmetleri: </b><%# DataBinder.Eval(Container, "DataItem.yorumCevap")%>
            
         </asp:Panel>
        
        </ItemTemplate>
        </asp:Repeater>

        <div class="pagination">
                <asp:Literal ID="ltlSayfalama" runat="server"></asp:Literal>
        </div> 

    <div id="progress"  class="endRequest" > 
          <img id="imgProgress" src="../images/loadingOdeme.gif" style="width:50px;" alt="Progress"  /> 
    </div>

    <table style="margin-bottom:20px; width:703px">
            <tr>
                <td colspan="2" class="icerik-Baslik" style="border-top: 1px solid #ccc;" >
                <a id="zFormm">Siz de Mesaj Bırakın</a>
                </td>
            </tr>
            <tr>
                <td  colspan="2">
                    <div id="mesajlar" style="padding:10px; width:650px;" > 
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="uyuBilgiYazi"   >
                  Lütfen ziyaretçi defterimiz ile ilgili gerekli alanları doldurunuz. Her türlü konuda fikirlerinizi belirtebilirsiniz. Müşteri temsilcileirimiz iletinizle ilgili genel kuralları kontrol ettikten sonra yayına alacaklardır.</td>
            </tr>
            <tr>
                <td class="uyuFormBilgi"  valign="top"  >
                    Adınız / Soyadınız <span class="formZorunlu" > * </span> </td>
                <td>
                    <asp:TextBox ID="txtAdSoyad" ClientIDMode="Static" runat="server" Width="300px"
                     CssClass="text"></asp:TextBox>
                    <span id="msjAdsoyad" class="input-notification error" style="display:none;" >
                     Ad ve Soyad Alanını Doldurunuz.
                    </span>
                </td>
               
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top" >
                    E-mail Adresiniz <span class="formZorunlu" > * </span> </td>
                <td>
                    <asp:TextBox ID="txtEposta" ClientIDMode="Static" runat="server" Width="298px" CssClass="text"></asp:TextBox>
                    <span id="msjEposta" class="input-notification error" style="display:none;" >
                     Geçerli E-Posta Adresi Giriniz. 
                    </span>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Bulunduğunuz Şehir <span class="formZorunlu" > * </span> </td>
                <td>
                <asp:DropDownList ID="ddlSehirler" ClientIDMode="Static" runat="server" Width="300px" Height="30px" CssClass="text"  />
                <span id="msjSehirler" class="input-notification error" style="display:none;" >
                    Şehir Seçiniz.
                </span>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Bulunduğunuz İlçe 
                </td>
                <td>
            <asp:DropDownList ID="ddlilceler" ClientIDMode="Static" runat="server" Width="300px" Height="30px"  CssClass="text" />
            <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server" TargetControlID="ddlSehirler"
                Category="sehir" PromptText="-- Şehir Seç --"  ServicePath="~/Include/CascadingSehirler.asmx"
                ServiceMethod="sehirler" />
            <cc1:CascadingDropDown ID="CascadingDropDown2" runat="server" TargetControlID="ddlilceler"
                ParentControlID="ddlSehirler" PromptText="-- İlçe Seç --" PromptValue="-1" ServiceMethod="ilceler"
                ServicePath="~/Include/CascadingSehirler.asmx" Category="ilce" />
              </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Mesajınız<span class="formZorunlu" > * </span> </td>
                <td>
                    <asp:TextBox ID="txtMesaj" ClientIDMode="Static" runat="server" Height="88px" Width="400px" 
                        CssClass="ZDForm" TextMode="MultiLine"></asp:TextBox>
                    <span id="msjMesaj" class="input-notification error" style="display:none;" >
                    Mesaj Alanını Doldurunuz.
                    </span>
                </td>
            </tr>
            <tr>
                <td class="uyuFormBilgi" valign="top">
                    Güvenlik Kodu:</td>
                <td>
                   <asp:TextBox ID="txtGuvenlik"  ClientIDMode="Static" autocomplete="off" CssClass="text" Width="80px" runat="server" ></asp:TextBox>
                   <span id="msjGuvenlik" class="input-notification error" style="display:none;" >
                   Lütfen Güvenlik Resmini Giriniz.
                   </span>
                    <br/>
                    <asp:Image ID="imgGuvenlik" ImageUrl="~/include/GuvenlikResmi.aspx" runat="server" />
               </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="padding:10px 0px;" >
                    <input id="btnZiyaretciDefteri" type="button" value="Kaydet" style="width:130px;" class="button""  />
                </td>
            </tr>
        </table>  
        </div>
    </div>
</asp:Content>

