<%@ Control Language="C#" AutoEventWireup="true" Inherits="Include_LeftColumn_SSSorular" Codebehind="SSSorular.ascx.cs" %>

<asp:Panel ID="pnlSSSorular" runat="server" >
              <div class="panelBaslik"> <span class="baslik"> SIK SORULANLAR</span> </div>
              <asp:Repeater ID="rptSSSorular" runat="server">
                    <HeaderTemplate>
                    <div class="borderDiv" > 
                    </HeaderTemplate>
                    <ItemTemplate>
                <div class="duyuruDiv">  
                
               <a href="<%# ResolveUrl("~/lens-hakkinda-merakedilenler?goto=" + DataBinder.Eval(Container, "DataItem.id")) %>" >
              <%# BusinessLayer.GenelFonksiyonlar.SSSorularKes(Eval("soru").ToString())%>
              <br />
              <span class="devam"> yazının devamı</span></a>

                </div>   
                    </ItemTemplate>
                    <FooterTemplate>
                  </div>
                    </FooterTemplate>
                </asp:Repeater>
 </asp:Panel>  