<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="True" Inherits="admin_Hediye_Urun_Detay" Codebehind="hediyeUrunDetay.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">
    <script language="javascript" type="text/javascript" >


        function moveOver_deger() {
            var src = document.getElementById('secenek_ismi');
            var dest = document.getElementById('secenek_ismi2');

            for (var count = 0; count < src.options.length; count++) {


                var isNew = true;

                if (src.options[count].selected == true) {
                    var option = src.options[count];

                    var newOption = document.createElement("option");
                    newOption.value = option.value;
                    newOption.text = option.text;


                    try {
                        dest.add(newOption, null); //Standard
                        src.options[count].selected = false;

                    } catch (error) {
                        dest.add(newOption); // IE only
                        src.options[count].selected = false;
                    }
                    count--;
                }
            }
        }

        function removeMe_deger() {
            var boxLength = document.getElementById('secenek_ismi2').length;
            arrSelected = new Array();
            var count = 0;
            for (i = 0; i < boxLength; i++) {
                if (document.getElementById('secenek_ismi2').options[i].selected) {
                    arrSelected[count] = document.getElementById('secenek_ismi2').options[i].value;
                }
                count++;
            }
            var x;
            for (i = 0; i < boxLength; i++) {
                for (x = 0; x < arrSelected.length; x++) {
                    if (document.getElementById('secenek_ismi2').options[i].value == arrSelected[x]) {
                        document.getElementById('secenek_ismi2').options[i] = null;
                    }
                }
                boxLength = document.getElementById('secenek_ismi2').length;
            }
        }

        function clearAllOptions() {
            if (confirm('Seçenekler silinsinmi?')) {
                var oSelect = document.getElementById('secenek_ismi2');
                for (i = oSelect.options.length - 1; i >= 0; i--) {
                    oSelect.options[i] = null;

                }
            }
        }

        function saveMe_deger() {
            var strValues = "";
            var boxLength = document.getElementById('secenek_ismi2').length;
            var count = 0;
            if (boxLength != 0) {
                for (i = 0; i < boxLength; i++) {
                    if (count == 0) {
                        var strVal = document.getElementById('secenek_ismi2').options[i].text
                        if (strVal != "") {
                            strValues = strVal;
                        }

                    } else {

                        var strVal_1 = document.getElementById('secenek_ismi2').options[i].text
                        if (strVal_1 != "") {
                            strValues = strValues + "|" + strVal_1;
                        }
                    }
                    count++;
                }
            }
            if (strValues.length == 0) {
                document.getElementById('hdfSecenekler').value = ""
            } else {
                document.getElementById('hdfSecenekler').value = strValues
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">Hediye Ürün Ekle</h3>
		
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">
	  
           <p>
           <label>Hediye Ürün Başlık </label>
           <asp:TextBox ID="txtHediyeBaslık" runat="server" CssClass="text-input medium-input" ></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtHediyeBaslık" CssClass="input-notification error" 
                Display="Dynamic" ValidationGroup="hediyeUrun" ForeColor=""
                ErrorMessage="Lütfen kampanya başlığı yazınız."  >
                </asp:RequiredFieldValidator><br /> 
         </p>
           <p>
           <label>Açıklama</label>
           <asp:TextBox ID="txtMarka" runat="server" CssClass="text-input medium-input"  ></asp:TextBox>
           
           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txtMarka" CssClass="input-notification error" 
                Display="Dynamic" ValidationGroup="hediyeUrun" ForeColor=""
                ErrorMessage="Lütfen marka giriniz"  >
                </asp:RequiredFieldValidator>    
         </p>
         <p>
           <label>Ürün Adet</label>
           <asp:TextBox ID="txtUrunAdet" runat="server" CssClass="text-input" ></asp:TextBox>
           
           <cc1:FilteredTextBoxExtender ID="Filtered" runat="server" TargetControlID="txtUrunAdet"
                FilterType="Custom, Numbers"  />

           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtUrunAdet" CssClass="input-notification error" 
                Display="Dynamic" ValidationGroup="hediyeUrun" ForeColor="" 
                ErrorMessage="Lütfen ürün adeti giriniz"  >
                </asp:RequiredFieldValidator>    
         </p>
        
         <p>
          <label>Resim</label>
            <asp:FileUpload ID="fluUrunResim" runat="server" />

            <asp:RequiredFieldValidator ID="rfvResim" runat="server" 
                ControlToValidate="fluUrunResim" CssClass="input-notification error" 
                Display="Dynamic" ValidationGroup="hediyeUrun" ForeColor=""
                ErrorMessage="Lütfen resim seçiniz."  >
                </asp:RequiredFieldValidator>

            <asp:Image ID="imgUrun" Visible="false"  runat="server" />
         </p>
         <p>
           <label>Seçenek</label>
           <table>
            <tr>
            <td colspan="2" >
            <asp:DropDownList ID="ddlSecenekler" runat="server" Width="300px"
            AutoPostBack="true" CssClass="uyuForm" onselectedindexchanged="ddlSecenekSelectedIndexChanged" >

            <asp:ListItem Text="Seçenek Başlığı Seçiniz "  Value="null" Selected="True"> </asp:ListItem> 
               <asp:ListItem Text="Renk" Value="HediyeRenk" > </asp:ListItem> 
               <asp:ListItem Text="İndirim Uygula Seçimi" Value="Indirim" > </asp:ListItem> 
            </asp:DropDownList>
            </td>
            </tr>
            <tr>
            <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlSecenekler"  />
                </Triggers>
                <ContentTemplate>
                    <asp:ListBox ID="secenek_ismi" ClientIDMode="Static"
                     SelectionMode="Multiple" Width="300px" Height="300px" runat="server">
                    </asp:ListBox>
                </ContentTemplate>
            </asp:UpdatePanel>

            </td>
            <td>
            <asp:ListBox ID="secenek_ismi2" ClientIDMode="Static"
             SelectionMode="Multiple" Width="300px" Height="300px" runat="server">
            </asp:ListBox><br />
                <asp:HiddenField ID="hdfSecenekler" ClientIDMode="Static"  runat="server" />
            </td>
            </tr>
            </table>
         </p>
         <p>
         <label>Durum</label>
         <asp:CheckBox ID="ckbDurum" runat="server" Checked="true" />
         </p>
         <p>
           <asp:Button ID="btnUrun" runat="server" CssClass="button" Font-Size="12px" 
              Text="Hediye Ürün Kaydet"  ValidationGroup="hediyeUrun"
              Width="172px" onclick="btnUrun_Click" OnClientClick="saveMe_deger();" />

           <asp:Button ID="btnUrunGuncelle" runat="server" CssClass="button" Font-Size="12px" 
              Text="Hediye Ürün Düzenle"  ValidationGroup="hediyeUrun"
              Width="172px"  OnClientClick="saveMe_deger();" Visible="false"
              onclick="btnUrunGuncelle_Click" />
         </p>
     				
		</div> <!-- End #tab1 -->
		</div> <!-- End .content-box-content -->
</div>


</asp:Content>




