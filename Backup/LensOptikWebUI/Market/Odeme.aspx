<%@ Page Title="Ödeme | Lens Optik " Language="C#" MasterPageFile="~/Market/Market.master" AutoEventWireup="true" Inherits="Market_Odeme" Codebehind="Odeme.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc2" %>
<%@ Register src="MenuMarket.ascx" tagname="MenuMarket" tagprefix="uc3" %>
<%@ Register src="MenuOdeme.ascx" tagname="MenuOdeme" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">

 <script type="text/javascript" src="../Scripts/MarketOdeme.js" ></script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">

  <div id="marketAna" >
  <div id="marketMap">
     <uc3:MenuMarket ID="Menu" runat="server" />
  </div>
  <div>
     <uc4:MenuOdeme ID="ucMenuOdeme" runat="server" />
  </div>
 
    <table id="odemeTbl" >
        <tr>
        <td colspan="2" >
            <div id="marketMesaj" style="margin:15px 0;" >
                 <uc2:Mesajlar ID="Mesaj" runat="server" />
           </div>
        </td>
        </tr>
        <tr>
        <td colspan="2" >

            <div id="pnlPuan" class="puan" runat="server" clientidmode="Static" visible="false" >
  Siparişinizde kullanabileceginiz
  <asp:Label ID="lblKulaniciPuan" Font-Bold="true" ClientIDMode="Static" runat="server"  ></asp:Label>  bulunmaktadır
          <br/>
          <label> <asp:CheckBox ID="ckbUyePuan" onClick="puan(this)" ClientIDMode="Static" runat="server" /> Siparişimde Kullanmak İstiyorum </label>
            </div>

        </td>
        </tr>
        <tr>
            <td class="FormBilgi">
            <span class="yazi1" >Kart Türü:</span>    
            </td>
            <td>
                <asp:DropDownList ID="ddlBankalar" ClientIDMode="Static" runat="server" CssClass="text" Width="300px"
                  Height="28px" TabIndex="1" >
                </asp:DropDownList><br />

                <asp:RequiredFieldValidator ID="vaddlBankalar" runat="server"
                  ErrorMessage="Lütfen Kredi Kartınızın Ait Olduğu Bankayı Seçiniz." 
                  CssClass="input-notification error" ForeColor="" Display="Dynamic"
                  ControlToValidate="ddlBankalar" InitialValue="0" ValidationGroup="odeme" ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            &nbsp; 
            </td>
            <td>
                <div id="pnlTaksitler" > </div>
                <div id="progress"  class="endRequest" > 
                  <img id="imgProgress" src="../images/loadingOdeme.gif" style="width:50px;" alt="Progress"  /> 
                </div>
            </td>
        </tr>
        <tr>
            <td class="FormBilgi" >
              <span class="yazi1" >Kart Üzerindeki İsim: </span>  </td>
            <td>
                <asp:TextBox  ID="txtKartAdSoyad" runat="server" CssClass="text" Width="300px" 
                    autocomplete="off" TabIndex="2" ></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvAd" runat="server" 
                    Display="Dynamic" ErrorMessage="Lütfen Kredi Kart Üstündeki İsmi Giriniz."
                    CssClass="input-notification error" ForeColor=""
                    ControlToValidate="txtKartAdSoyad" ValidationGroup="odeme" ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FormBilgi" >
              <span class="yazi1" > Kart Numarası:</span>  </td>
            <td>
               
                <asp:TextBox ID="txtKartNo1" CssClass="text" Width="70px" runat="server" onkeyup="karakter(this,this.value)"
                    MaxLength="4" autocomplete="off" TabIndex="3" ClientIDMode="Static" ></asp:TextBox>
                <asp:TextBox ID="txtKartNo2" CssClass="text" Width="70px" runat="server" onkeyup="karakter(this,this.value)"
                    MaxLength="4" autocomplete="off" TabIndex="4" ClientIDMode="Static" ></asp:TextBox>
                <asp:TextBox ID="txtKartNo3" CssClass="text" Width="70px" runat="server" onkeyup="karakter(this,this.value)"
                    MaxLength="4" autocomplete="off" TabIndex="5" ClientIDMode="Static" ></asp:TextBox>
                <asp:TextBox ID="txtKartNo4" CssClass="text" Width="70px"  runat="server" onkeyup="karakter(this,this.value)"
                    MaxLength="4" autocomplete="off" TabIndex="6" ClientIDMode="Static" ></asp:TextBox>
                
                <br />
                <div style="width:250px""> 
                       <asp:RequiredFieldValidator ID="vaKartNo" runat="server" Display="Dynamic"
                       ErrorMessage="Lütfen Kredi Kart Numarası Giriniz." 
                       CssClass="input-notification error" ForeColor=""
                       ControlToValidate="txtKartNo1" ValidationGroup="odeme" ></asp:RequiredFieldValidator>
                       <asp:RequiredFieldValidator ID="vaKartNo1" runat="server" Display="Dynamic"
                       ErrorMessage="Lütfen Kredi Kart Numarası Giriniz." 
                       CssClass="input-notification error" ForeColor=""
                       ControlToValidate="txtKartNo2" ValidationGroup="odeme" ></asp:RequiredFieldValidator>
                       <asp:RequiredFieldValidator ID="vaKartNo2" runat="server" Display="Dynamic"
                       ErrorMessage="Lütfen Kredi Kart Numarası Giriniz." 
                       CssClass="input-notification error" ForeColor=""
                       ControlToValidate="txtKartNo3" ValidationGroup="odeme" ></asp:RequiredFieldValidator>
                       <asp:RequiredFieldValidator ID="vaKartNo3" runat="server" Display="Dynamic"
                       ErrorMessage="Lütfen Kredi Kart Numarası Giriniz." 
                       CssClass="input-notification error" ForeColor=""
                       ControlToValidate="txtKartNo4" ValidationGroup="odeme" ></asp:RequiredFieldValidator>
             </div>
            </td>
        </tr>
        <tr>
            <td class="FormBilgi" >
           <span class="yazi1" >Son Kullanma Tarihi </span>     </td>
            <td>
                <asp:DropDownList ID="ddlAy" runat="server" CssClass="text" Width="72px" 
                    Height="28px" TabIndex="7" >
                <asp:ListItem Value="0" Text="Ay"  ></asp:ListItem>
                <asp:ListItem Value="01" Text="01"  ></asp:ListItem>
                <asp:ListItem Value="02" Text="02"  ></asp:ListItem>
                <asp:ListItem Value="03" Text="03"  ></asp:ListItem>
                <asp:ListItem Value="04" Text="04"  ></asp:ListItem>
                <asp:ListItem Value="05" Text="05"  ></asp:ListItem>
                <asp:ListItem Value="06" Text="06"  ></asp:ListItem>
                <asp:ListItem Value="07" Text="07"  ></asp:ListItem>
                <asp:ListItem Value="08" Text="08"  ></asp:ListItem>
                <asp:ListItem Value="09" Text="09"  ></asp:ListItem>
                <asp:ListItem Value="10" Text="10"  ></asp:ListItem>
                <asp:ListItem Value="11" Text="11"  ></asp:ListItem>
                <asp:ListItem Value="12" Text="12"  ></asp:ListItem>
                </asp:DropDownList>

                <asp:DropDownList ID="ddlYil" CssClass="text" Width="72px" Height="28px" 
                    runat="server" TabIndex="8">
                </asp:DropDownList><br />

                 <asp:RequiredFieldValidator ID="vaddlAy" runat="server" Display="Dynamic"
                  ErrorMessage="Lütfen Kredi Kartı Son Kullanma Tarihi Ay Olarak Seçiniz" 
                  CssClass="input-notification error" ForeColor=""
                  ControlToValidate="ddlAy" InitialValue="0" ValidationGroup="odeme" ></asp:RequiredFieldValidator><br />

                 <asp:RequiredFieldValidator ID="vaddlYil" runat="server" Display="Dynamic"
                  ErrorMessage="Lütfen Kredi Kartı Son Kullanma Tarihi Yıl Olarak Seçiniz." 
                  CssClass="input-notification error" ForeColor=""
                  ControlToValidate="ddlYil" InitialValue="0" ValidationGroup="odeme" ></asp:RequiredFieldValidator>
            </td>
        </tr>     
        <tr>
            <td class="FormBilgi" >
             <span class="yazi1" >Güvenlik Kodu: </span>   </td>
            <td>
               <asp:TextBox ID="txtGuvenlikKodu" CssClass="text" Width="150px" MaxLength="4" 
                    runat="server" autocomplete="off" TabIndex="9" ></asp:TextBox><br />
                      <asp:RequiredFieldValidator ID="vaGuvenlikKodu" runat="server" Display="Dynamic"
                       ErrorMessage="Lütfen Kredi Kartı Güvenlik Kodunu Giriniz."  
                       CssClass="input-notification error" ForeColor=""
                       ControlToValidate="txtGuvenlikKodu" ValidationGroup="odeme" ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="padding:15px 0;" >
               <asp:ImageButton ID="btnOdemeYap" ValidationGroup="odeme" ClientIDMode="Static" runat="server" 
                     Text="Ödemeyi Tamamla" ImageUrl="~/images/buttonlar/odemeKrediKart.gif" onclick="btnOdemeYap_Click"  />
            </td>
        </tr>
    </table>
   </div>
</asp:Content>

