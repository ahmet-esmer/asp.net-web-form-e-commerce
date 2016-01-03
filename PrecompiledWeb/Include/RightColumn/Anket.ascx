<%@ Control Language="C#" AutoEventWireup="true" Inherits="Include_RightColumn_Anket" Codebehind="Anket.ascx.cs" %>
              
   <asp:Panel ID="pnlAnket" runat="server" ClientIDMode="Static">
      <div class="panelBaslik" style="margin-top:15px;" > <span class="baslik">Anket</span> </div>
      <div class="borderDiv">
      <div class="anketBaslik">
      <asp:Label ID="lblAnketBaslik" runat="server"></asp:Label>
      </div>
          
          <asp:RadioButtonList ID="rblAnketSorular"  ClientIDMode="Static"  runat="server" CssClass="anketListe"  >
          </asp:RadioButtonList>

              <asp:Repeater ID="rptAnketSorulari" runat="server">
              <ItemTemplate>
                  <div class="rdbAnket" >
                   <input  name="anketAd" type="radio" value='<%# Eval("soruId").ToString()%>'  /> 
                        <%# Eval("soru").ToString()%>
                  </div>
              </ItemTemplate>
              </asp:Repeater>
          <br />

           <asp:HyperLink ID="hlAnketOyla"  onclick='AnketOyla()'  ClientIDMode="Static" CssClass="anketOylama"  runat="server">Oyla</asp:HyperLink>

          <asp:HyperLink ID="hlAnketGoster" ClientIDMode="Static" CssClass="anketGoster"  ToolTip="Anket Sonuçlarını Görüntüle." runat="server">Sonuçlar</asp:HyperLink>

           <div id="lblAnketBilgi" class="mesajA " >
            
           </div>
           <input id="hdfLink" value="<%: ResolveUrl("~/Include/Ajax/AnketOylama.aspx/Kayit")  %>" type="hidden" />

        </div>
   </asp:Panel>   