
namespace ModelLayer
{
    public class SepetSiparisKayit
    {
          public int UrunId  { get; set; }
          public int Miktar  { get; set; }
          public int SepetId  { get; set; }
          public decimal Fiyat  { get; set; }
          public int SagAdet { get; set; }
          public int SolAdet { get; set; }
          public string SagBilgi { get; set; }
          public string SolBilgi { get; set; }
          public int HediyeId { get; set; }
          public string HediyeBilgi { get; set; }

          public SepetSiparisKayit()
          { 
          
          }
    }

  
}
