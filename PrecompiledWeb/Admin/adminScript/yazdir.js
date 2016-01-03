function yazdir(yazdirilacakBolum) {

    //Creating new page
    var pp = window.open();
    //Adding HTML opening tag with <HEAD> … </HEAD> portion 
    pp.document.writeln('<HTML><HEAD><title>Sipariş Ön İzleme</title><LINK href=Styles.css  type="text/css" rel="stylesheet">')
    pp.document.writeln('<LINK href=adminStyle/siparisPirint.css  type="text/css" rel="stylesheet"  >');
    pp.document.writeln('<LINK href=adminStyle/printGizle.css  type="text/css" rel="stylesheet" media="print"  >');
    pp.document.writeln('</HEAD>')
    //Adding Body Tag
    pp.document.writeln('<body MS_POSITIONING="GridLayout" bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">');
    //Adding form Tag
    pp.document.writeln('<form  method="post">');
    //Creating two buttons Print and Close within a table
    pp.document.writeln('<TABLE width=100%><TR><TD></TD></TR><TR><TD align=right><INPUT ID="PRINT" type="button" value="Yazdır" onclick="javascript:location.reload(true);window.print();"><INPUT ID="CLOSE" type="button" value="Sayfayı Kapat" onclick="window.close();"></TD></TR><TR><TD></TD></TR></TABLE>');
    //Writing print area of the calling page
    pp.document.writeln(document.getElementById(yazdirilacakBolum).innerHTML);
    //Ending Tag of </form>, </body> and </HTML>
    pp.document.writeln('</form></body></HTML>');	
}	