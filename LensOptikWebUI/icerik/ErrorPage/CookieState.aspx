<%@ Page Language="C#" AutoEventWireup="true" Inherits="Icerik_Cookies_State" Codebehind="CookieState.aspx.cs" %>

<%@ Register src="../../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Cookies Durum Sayfası</title>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-9" />
    <meta http-equiv="Content-Language" content="tr" />
    <meta name="Author" content="Ahmet ESMER : ahmetesmer@windowslive.com" />
    <meta name="copyright" content="Ruhun Gemisi Reklam Tanıtım Halkla İlişkiler Ajansı" />
    <link rel="shortcut icon" href="http://www.lensoptik.com.tr/Images/icons/lensoptik.ico"/>

    <link rel="stylesheet" href="../../Style/Clear.css" type="text/css" />
    <link rel="stylesheet" href="../../Style/Layout.css" type="text/css" />
    <link rel="stylesheet" href="../../Style/MainStyle.css" type="text/css" />

    <style type="text/css">
    
     #ust1{width:901px;float:left;display:inline;
          background-color:#00c2dd; padding:7px 10px;}
    
    .panel
    {
     border:1px solid #cbcbcb;
     margin:10px;    
    }
    
    .panelNot
    {
        font-size:14px;
        line-height:20px;
    }
    
    .resim
    {
        padding:10px;
    }
    
    </style>

</head>
<body>
    <form id="form1" runat="server">
       <div id="anaDiv">
       <div id="anaIcerik"> 
          
          <div id="ust1" >
          </div>
            <div id="ust2" >
             </div>
            <div id="ust3" > 
                <div  id="logo1"> 
                 <asp:HyperLink ID="hlDefault" NavigateUrl="~/Default.aspx" runat="server">
                  <asp:Image ID="LensOptikLogo" AlternateText="Lens Optik Logo" ClientIDMode="Static" ImageUrl="~/Images/LensOptikLogo.jpg" runat="server" />
                 </asp:HyperLink>
                </div>
                 <div id="logo2" > 
                <asp:Image ID="Slogan"  ClientIDMode="Static"  ImageUrl="~/Images/Slogan.jpg" runat="server" />
                </div>
  
            </div>

            <div class="clear">
               <div style="width:860px; margin-left:10px;" > 
               <uc1:Mesajlar ID="Mesaj" runat="server" />
               </div>
               
                <asp:Panel ID="pnlCookieIE8" runat="server" CssClass="panel" Visible="false" >
                <div class="panelNot uyuBilgiYazi" > 
                Tarayıcının Cookie özeligi; web sitemizden yapacağınız alışveriş ile ilgili bilgilerin oturum süresince bilgisayarınızda tutulmasidir, Alışveriş yapabilmeniz için tarayıcının bu özelliğini açmalısınız.<br />

                Aşağıdaki görsellerde Cookie özeliğini nasıl aktif edebileceğiniz gösterilmektedir.
                </div>

                <asp:Image ID="Image1" CssClass="resim" ImageUrl="~/Products/Sayfa_Resim/image1.jpg"
                  runat="server" />
                <asp:Image ID="Image2" CssClass="resim" ImageUrl="~/Products/Sayfa_Resim/image2.jpg"
                  runat="server" />
                <asp:Image ID="Image3" CssClass="resim" ImageUrl="~/Products/Sayfa_Resim/image3.jpg"
                  runat="server" />

                </asp:Panel>

                <asp:Panel ID="pnlCookieIE9" runat="server" CssClass="panel" Visible="false" >
               <div class="panelNot uyuBilgiYazi" > 
                Tarayıcının Cookie özeligi; web sitemizden yapacağınız alışveriş ile ilgili bilgilerin oturum süresince bilgisayarınızda tutulmasidir, Alışveriş yapabilmeniz için tarayıcının bu özelliğini açmalısınız.<br />

                Aşağıdaki görsellerde Cookie özeliğini nasıl aktif edebileceğiniz gösterilmektedir.
                </div>
                    <div>
                    <asp:Image ID="Image4" CssClass="resim" ImageUrl="~/Products/Sayfa_Resim/image-ie-9-1.jpg" runat="server" />
                    </div>
                 

                 <asp:Image ID="Image5" CssClass="resim" ImageUrl="~/Products/Sayfa_Resim/image2.jpg" runat="server" />

                 <asp:Image ID="Image6" CssClass="resim" ImageUrl="~/Products/Sayfa_Resim/image3.jpg" runat="server" />

                   
                </asp:Panel>

           </div>
            <div class="altLink" style="height:10px;" >
            </div>
            <div id="altCopyrihgt">
                <div class="line copy1"> </div> 
                <div class="line copy2"> </div> 
            </div>

       </div>
       </div>
    </form>
</body>
</html>
