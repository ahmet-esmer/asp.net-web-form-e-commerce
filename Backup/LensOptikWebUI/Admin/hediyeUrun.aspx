<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="True" Inherits="admin_Hediye_Urun" Codebehind="hediyeUrun.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="content-box"><!-- Start Content Box -->
	<div class="content-box-header">
		<h3 style="cursor: s-resize;">Hediye Kampanya</h3>
		<div class="clear"></div>
		</div> <!-- End .content-box-header -->
		<div style="display: block;" class="content-box-content">
		<div style="display: block;" class="tab-content default-tab" id="tab1">

           <div style="padding-bottom:10px; text-align:right;" >
             <asp:HyperLink ID="hlYeniBaslik" CssClass="button"
                 NavigateUrl="~/Admin/hediyeUrunKayit.aspx" 
                 runat="server">Yeni Kampanya Ekle</asp:HyperLink>
           </div>
            <asp:Repeater ID="rptHediyeUrun" runat="server" onitemdatabound="rptHediyeUrun_ItemDataBound" >
            <HeaderTemplate>
             <table cellspacing="0" cellpadding="4" border="0" style="color:#333333;width:100%;border-collapse:collapse;">
		        <tbody>
                    <tr class="HeaderStyle">
                    <th style="width:110px;"></th>
                        <th style="font-weight:bold;width:400px;">Adı</th>
                        <th style="font-weight:bold;width:100px;">Limit</th>
                        <th style="font-weight:bold;width:100px;">Yeni Ürün</th>
                        <th style="font-weight:bold;width:100px;">Durum</th>
                        <th style="font-weight:bold;width:100px;">Düzenle</th>
                        <th style="font-weight:bold;">Sil</th>
		            </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="AlternatingRowStyle">
                  <td style="vertical-align:middle; padding:5px; " >
                     <img src="../Products/Small/<%# Eval("Resim") %>" width="100px" />
                   </td>
                    <td>
                     <h3><%# Eval("Title") %></h3>  </td>
                    <td><%# Eval("limit") %> </td>
                    <td>
                   

                     <a href='hediyeUrunDetay.aspx?baslikId=<%# Eval("Id") %>&islem=yeni' >
                        <img src="images/pluse.png" border="0"  /> 
                     </a>
                    </td>
                    <td>
                      <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(Eval("Durum")))%>
                    </td>
                    <td>
                      <a href='hediyeUrunKayit.aspx?id=<%# Eval("id") %>&amp;islem=duzenle'  >
                         <img src="images/duzenle.gif" alt="Düzenle" border="0" />
                      </a>
                    </td>
                    <td>
                     <a href='?id=<%# Eval("Id") %>&islem=baslikSil' onclick="return silKontrol()" >
                     <img src="images/sil.gif" border="0"  /> </a>
                    </td>
		        </tr>
                <tr>
                   <td colspan="6" > 
                  <asp:HiddenField ID="hdfTitleId" Value='<%# Eval("id")%>' runat="server" />
                  <asp:Repeater ID="rptHediyeUrunDetay" runat="server">
            <HeaderTemplate>
             <table cellspacing="0" cellpadding="4" border="0"  style="width:100%;color:#333333;border-collapse:collapse;">
		        <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="RowStyle">
                    <td style="width:100px;" >
                      <img src="../Products/Small/<%# Eval("Resim") %>" width="80px" />
                     </td>
                    <td style="width:300px;" >
                    <%# Eval("UrunAdi") %>
                    </td>
                    <td style="width:150px;" >
                    <%# Eval("Marka") %>
                    </td>
                    <td style="width:250px;" >
                     <span class="secenek"> Adet:</span>  <%# Eval("Adet") %>
                    </td>
                    <td style="width:105px;" >
                      <%# BusinessLayer.GenelFonksiyonlar.DurumKontrol(Convert.ToInt32(Eval("Durum")))%>
                    </td>
                    <td style="width:103px;" >
                      <a href='hediyeUrunDetay.aspx?detayId=<%# Eval("id") %>'  >
                         <img src="images/duzenle.gif" alt="Düzenle" border="0" />
                      </a>
                    </td>
                    <td>
                         <a href='?detayId=<%# Eval("Id") %>&islem=detaySil&resim=<%# Eval("Resim") %>'
                         onclick="return silKontrol()" >
                             <img src="images/sil.gif" border="0"  /> 
                         </a>
                    </td>
		        </tr>
            </ItemTemplate>
            <FooterTemplate>
              </tbody>
              </table>
            </FooterTemplate>
            </asp:Repeater>
                </td>
		        </tr>
            </ItemTemplate>
            <FooterTemplate>
              </tbody>
              </table>
            </FooterTemplate>
            </asp:Repeater>	
		</div> <!-- End #tab1 -->
		</div> <!-- End .content-box-content -->
</div>


</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="ContentPlaceHolder2">


</asp:Content>


