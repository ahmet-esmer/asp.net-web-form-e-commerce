<%@ Page Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" Inherits="adminReklamlar" Title="Untitled Page" Codebehind="hataMesajlari.aspx.cs" %>

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
                    <table id="hata" >
                        <tr>
                            <td><label>Hata Adı </label> </td>
                            <td><label>Hata Mesaj </label>  </td>
                        </tr>
                   
                </HeaderTemplate>
                <ItemTemplate>
                 <tr>
                   <td style="width:250px; padding-right:10px; clear:both;">
                       <div style="width:250px; clear:both;" >
                      
                         <%# Server.HtmlEncode(DataBinder.Eval(Container, "DataItem.hataAdi").ToString())%>
                         <br />
                           <div class="uyeIl">
                            <span class="hataBilgi">Tarih: </span>  
                            <%# BusinessLayer.DateFormat.TarihSaatSani(DataBinder.Eval(Container, "DataItem.tarih").ToString())%>
                          </div>
                           <div class="uyeIl">
                            <span class="hataBilgi">Sayı: </span>
                          <%# DataBinder.Eval(Container, "DataItem.hataSayisi")%>
                          </div> 
                       </div>
                      </td>
                      <td>
                         <%# DataBinder.Eval(Container, "DataItem.hataMesaji")%>  
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
          $("#hata tr:even").addClass("RowStyle");

          $("#hata tr:odd").addClass("RowStyle AlternatingRowStyle ");
      });
 </script>

</asp:Content>

