
namespace ModelLayer
{
    public class Sepet
    {

          public string ResimAdi { get; set; }
          public string UrunAdi  { get; set; }
          public string StokCins  { get; set; }
          public int UrunId  { get; set; }
          public int UrunStok  { get; set; }
          public int UrunKDV  { get; set; }
          public int Miktar  { get; set; }
          public int UyeId  { get; set; }
          public int SepetId  { get; set; }
          public decimal UrunFiyat  { get; set; }

          public decimal UIndirimFiyat  { get; set; }

          public decimal BirimGrid { get; set; }
          public int HavaleIndirim  { get; set; }
          public string HavaleTL  { get; set; }
          public string Doviz { get; set; }

          public int SagAdet { get; set; }
          public int SolAdet { get; set; }
          public string SagBilgi { get; set; }
          public string SolBilgi { get; set; }
          public int HediyeUrunTekId { get; set; }
          
          public int HediyeId { get; set; }
          public int HediyeBilgi { get; set; }

          public UrunHediye ObjHediye  {get; set;}


          public Sepet()
          { 
          
          }
    }

  
}
