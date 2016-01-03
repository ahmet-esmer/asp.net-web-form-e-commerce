<%@ Control Language="C#" AutoEventWireup="true" Inherits="include_pop_up" Codebehind="popUp.ascx.cs" %>
<asp:Panel ID="pnlPopUp" Visible="false" runat="server">



  <script type="text/javascript">

      $(document).ready(function () {

          if ($.cookie('lnspopup') != null) {
              return false;
          }

          var id = '#dialog';

          //Get the screen height and width
          var maskHeight = $(document).height();
          var maskWidth = $(window).width();

          //Set heigth and width to mask to fill up the whole screen
          $('#mask').css({ 'width': maskWidth, 'height': maskHeight });

          //transition effect		
          $('#mask').fadeIn(1000);
          $('#mask').fadeTo("slow", 0.8);

          //Get the window height and width
          var winH = $(window).height();
          var winW = $(window).width();

          //Set the popup window to center
          $(id).css('top', winH / 2 - $(id).height() / 2);
          $(id).css('left', winW / 2 - $(id).width() / 2);

          //transition effect
          $(id).fadeIn(2000);

          //if close button is clicked
          $('.window .close').click(function (e) {
              //Cancel the link behavior
              e.preventDefault();

              $('#mask').hide();
              $('.window').hide();
          });

          //if mask is clicked
          $('#mask').click(function () {
              $(this).hide();
              $('.window').hide();
          });

          $.cookie('lnspopup', 'true');


          $("#ckbSiparis").click(function () {

              var deger = $(this);

              $('#mask').hide();
              $('.window').hide();

              if (deger.is(':checked')) {

                  var Id = $('#hdfId').val();

                  $.ajax({
                      type: 'POST',
                      url: '<%= Page.ResolveUrl("~/Include/Ajax/ReOrderPopUp.aspx/Kayit")%>',
                      data: '{ "uyeId":"' + Id.toString() + '" }',
                      contentType: 'application/json; charset=utf-8',
                      dataType: 'json',
                      success: function () {
                      },
                      error: function () {
                          alert('İşlem hata ile sonuçlandı.');
                      }
                  });
              }
          });

      });


  </script>
  
 <style type="text/css">
a {color:#333; text-decoration:none}
a:hover {color:#ccc; text-decoration:none}


#mask 
{
  position:absolute;
  left:0;
  top:0;
  z-index:9998;
  width:100%;
  height:100px;
  background-color:#000;
  display:none;
} 
 
 
#boxes .window {
  position:absolute;
  left:0;
  top:0;
  display:none;
  z-index:9999;
  height:100%;
  padding:0px;
}


#boxes #dialog 
{
  padding:5px;
  background-color:#ffffff;
  top: 199.5px; 
  left: 551.5px;
  display: none;
  overflow:hidden;
} 


</style>

 <div id="boxes" >

 <asp:Panel ID="dialog" ClientIDMode="Static" runat="server" CssClass="window">
       
    <asp:HyperLink ID="hlLink" runat="server">
        <asp:Image ID="imgPopUp" runat="server" BorderWidth="0px" />
    </asp:HyperLink>
     
    <div>
        
        <div style="float:left; display:inline; color:#666666;padding-top:10px;">   
         <asp:HiddenField ID="hdfId" ClientIDMode="Static" runat="server" />
         <asp:CheckBox ID="ckbSiparis" ClientIDMode="Static" Text="Bu mesajı Birdaha gösterme." Visible="false" runat="server" />
        </div>
        
        <div style="float:right; display:inline; padding-top:10px;" >
        <a href="#" class="close" >
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/logoKapat.gif" BorderWidth="0px" />
            </a>
        </div>
    <div style="clear:both" ></div>
    </div>

</asp:Panel>

   <div id="mask"  style="width: 1478px; display: none; opacity: 0.8;" ></div>      
</div>
</asp:Panel>