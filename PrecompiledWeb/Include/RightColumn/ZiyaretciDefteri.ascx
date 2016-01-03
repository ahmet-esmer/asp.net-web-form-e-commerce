<%@ Control Language="C#" AutoEventWireup="true" Inherits="Include_RightColumn_ZiyaretciDefteri" Codebehind="ZiyaretciDefteri.ascx.cs" %>

    <asp:Panel ID="pnlDuyuru" Visible="false" runat="server" >

              <div class="panelBaslik"> <span class="baslik"> ZİYARETÇİ DEFTERİ</span> </div>
              <asp:Repeater ID="rptZiyaretci" runat="server">
                    <HeaderTemplate>
                    <div  class="borderDiv" > 
               <marquee width="190"  direction="up" height="200" scrollamount="2" scrolldelay="50" onMouseOver="this.stop();" onmouseout="this.start();"  >
                    </HeaderTemplate>
                    <ItemTemplate>
                <div class="duyuruDiv">  
               <a href='<%# ResolveUrl("~/ZiyaretciDefteri?goto="+DataBinder.Eval(Container, "DataItem.id"))%>' >
               <%# BusinessLayer.GenelFonksiyonlar.ZiyaretciDefteriKes(Eval("yorum").ToString())%></a><br />
                </div>   
                    </ItemTemplate>
                    <FooterTemplate>
                  </marquee>
                  </div>
                    </FooterTemplate>
                </asp:Repeater>

  </asp:Panel>