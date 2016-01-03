<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>


<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        urlRoutes(RouteTable.Routes);
        
    }


    void urlRoutes(RouteCollection routs)
    {



        routs.MapPageRoute("kullanici", "Kullanici/{dosya}/{sayfaId}", "~/Kullanici/{dosya}.aspx");
        
        routs.MapPageRoute("kulaniciDefault", "Kullanici/{fileName}", "~/Kullanici/{fileName}.aspx");

        routs.MapPageRoute("kampanya", "{kamAdi}/kampanya-{kampanyaId}", "~/KampanyaUrunleri.aspx");
        routs.MapPageRoute("kampanyalar", "kampanyalar", "~/Kampanyalar.aspx");
        routs.MapPageRoute("kategori", "{katAdi}/lens-{katId}/{title}", "~/Urunler.aspx");
        routs.MapPageRoute("gununUrunleri", "gune-ozel-kampanyali-indirimli-lensler", "~/GununUrunleri.aspx");
        routs.MapPageRoute("ziyaretci", "ZiyaretciDefteri", "~/icerik/ZiyaretciDefteri.aspx");
        routs.MapPageRoute("sssorular", "lens-hakkinda-merakedilenler", "~/icerik/SSorulanSorular.aspx");
        routs.MapPageRoute("default1", "default", "~/Default.aspx");
        
        routs.MapPageRoute("iletisim", "icerik/iletisim/{sayfaId}", "~/icerik/iletisim.aspx");
        routs.MapPageRoute("sayfalar", "icerik/{sayfaId}/{sayfaAdi}", "~/icerik/Sayfa.aspx");
        
        routs.MapPageRoute("marka", "Markalar/{markaId}/{markaAdi}", "~/MarkaUrunleri.aspx");
        routs.MapPageRoute("default", "default/{sayfaId}", "~/Default.aspx");
        
        routs.MapPageRoute("tumLensler", "tum-lensler/{sayfaId}", "~/TumLensler.aspx");
        routs.MapPageRoute("yeniUrunler", "yeni-urunler/{sayfaId}", "~/YeniUrunler.aspx");

        //routs.MapPageRoute("renkSecim", "lens-renkleri/{sayfaId}", "~/icerik/Renk/RenkSecim.aspx");

        routs.MapPageRoute("sorular", "lens-hakkinda-merakedilenler/{sayfaId}", "~/icerik/SSorulanSorular.aspx");
        routs.MapPageRoute("altLink", "{dosya}/{sayfaId}", "~/icerik/{dosya}.aspx");

        
        routs.MapPageRoute("resim", "Products/{boyut}/{adi}", "~/Products/{boyut}/{adi}");
        routs.MapPageRoute("urun", "{kategori}/{urunId}/{urunAdi}", "~/UrunDetay.aspx");

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
