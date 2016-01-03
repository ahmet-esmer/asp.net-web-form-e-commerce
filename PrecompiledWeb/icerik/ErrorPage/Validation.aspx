<%@ Page Language="C#" AutoEventWireup="true" Inherits="Icerik_Validation" Codebehind="Validation.aspx.cs" %>

<%@ Register src="../../Include/Mesajlar.ascx" tagname="Mesajlar" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title>Güvenlik Uyarısı</title>
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
               <div style="width:860px; margin-left:10px; margin-bottom:200px; " > 
               <uc1:Mesajlar ID="Mesaj" runat="server" />
               </div>
                
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
