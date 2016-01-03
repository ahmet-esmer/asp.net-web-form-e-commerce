<%@ Control Language="C#" AutoEventWireup="true" Inherits="include_banner" Codebehind="Banner.ascx.cs" %>

 <div id="banner" > 

  <div id="lofslidecontent45" class="lof-slidecontent" style="width:613px; height:250px;" >
            <div class="preload"><div></div></div>
             <div class="lof-main-outer" style="width:613px; height:250px;" >
  	<ul class="lof-main-wapper">
    <asp:Repeater ID="rptBannerBuyuk" runat="server">
             <ItemTemplate>
              <li>
              <a href="<%# ResolveUrl(Eval("ResimBaslik").ToString()) %>" ><img src="<%# ResolveUrl("~/Products/Big/"+ Eval("ResimAdi").ToString()) %>" width="613px" alt="Kampanyalı Lensler" /></a>
              </li> 
            </ItemTemplate>
            </asp:Repeater>
      </ul>  	
  </div>
     <div class="lof-navigator-wapper">
      <div class="lof-navigator-outer">
            <ul class="lof-navigator">
               <asp:Repeater ID="rptBannerKucuk" runat="server">
               <ItemTemplate>
                 <li><span> <%# Container.ItemIndex + 1 %></span></li>  
               </ItemTemplate>
              </asp:Repeater>  
            </ul>
      </div>
  </div> 

    </div> 

 </div>
 <div class="clear"></div>