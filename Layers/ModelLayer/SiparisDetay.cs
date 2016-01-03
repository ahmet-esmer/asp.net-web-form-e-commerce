
namespace ModelLayer
{
  public  class SiparisDetay
    {

      public string resimAdi {get; set;}
      public int urunId { get; set; }
      public decimal urunFiyat { get; set; }
      public string urunAdi { get; set; }
      public decimal fiyat { get; set; }
      public decimal uIndirimFiyat { get; set; }
      public int urunKDV { get; set; }
      public int adet { get; set; }
      public string urunKodu { get; set; }
      public int sagAdet { get; set; }
      public int solAdet { get; set; }
      public string sagBilgi { get; set; }
      public string solBilgi { get; set; }
      public string stokCins { get; set; }

      public string Birim { get; set; }
      public string KdvDahilFiyat { get; set; }

      public string HediyeUrunTekHTML { get; set; }
      public string HediyeHTML { get; set; }

      
      public SiparisDetay()
      { 
      
      }
     
    }
}
