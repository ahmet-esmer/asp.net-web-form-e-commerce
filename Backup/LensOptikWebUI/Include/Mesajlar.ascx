<%@ Control Language="C#" AutoEventWireup="true" Inherits="include_mesajlar" Codebehind="Mesajlar.ascx.cs" %>



      <div id="Mesaj_Info" clientidmode="Static"  runat="server" visible="false"  >
                  <div  class="mesaj_clos" >
                    <a   onclick="document.getElementById('Mesaj_Info').style.display='none'"  >
                   <asp:Image ID="img1" ImageUrl="~/Admin/images/info_no.jpg"   runat="server" />
                    </a>
                </div>
               <asp:Label ID="Mesaj_yaz_Info" CssClass="Mesaj_yaz_Info" runat="server" > </asp:Label>
      </div>

      <div id="Mesaj_Ok" clientidmode="Static"  runat="server" visible="false">
                <div  class="mesaj_clos">
                    <a   onclick="document.getElementById('Mesaj_Ok').style.display='none'"  >
                    <asp:Image ID="img2" ImageUrl="~/Admin/images/yes_no.gif"   runat="server" />
                    </a>
                </div>
                <asp:Label ID="Mesaj_yaz_ok" runat="server" ></asp:Label>
      </div>

      <div id="Mesaj_No"  clientidmode="Static" runat="server" visible="false">
                 <div class="mesaj_clos" >
                    <a   onclick="document.getElementById('Mesaj_No').style.display='none'"  >
                    <asp:Image ID="img3" ImageUrl="~/Admin/images/no.jpg"   runat="server" />
                    </a>
                </div>
                <asp:Label ID="Mesaj_yaz_no" runat="server" ></asp:Label>
      </div>  
              
      <div id="Mesaj_Sis" clientidmode="Static"   runat="server" visible="false">
                 <div class="mesaj_clos">
                    <a onclick="document.getElementById('Mesaj_Sis').style.display='none'"  >
                    <asp:Image ID="img4" ImageUrl="~/Admin/images/sis_no.gif"  AlternateText="Mesaji Kapat"  runat="server" />
                    </a>
                </div>
                <asp:Label ID="Mesaj_yaz_sis" runat="server" ></asp:Label>
      </div>