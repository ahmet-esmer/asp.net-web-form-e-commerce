using System;
using System.Data;
using System.Data.SqlClient;
using ModelLayer;
using LoggerLibrary;

namespace DataAccessLayer
{
   public class KrediKartDB :BaseDB
   {
       public static void Kaydet(BankRequest bankRequest)
      {
          try
          {
              SqlParameter[] parameter = new SqlParameter[5];
              parameter[0] = new SqlParameter("@siparisId", SqlDbType.Int);
              parameter[0].Value = bankRequest.SiparisId;
              parameter[1] = new SqlParameter("@kartNo", SqlDbType.NVarChar);
              parameter[1].Value = bankRequest.KrediKart.No.Substring(0, 4) +
                     "-XXXX-XXXX-" + bankRequest.KrediKart.No.Substring(12);
              parameter[2] = new SqlParameter("@kartAd", SqlDbType.NVarChar);
              parameter[2].Value = bankRequest.KrediKart.AdSoyad;
              parameter[3] = new SqlParameter("@onayKodu", SqlDbType.NVarChar);
              parameter[3].Value = bankRequest.PaymentMessage.OnayKodu;
              parameter[4] = new SqlParameter("@referansNo", SqlDbType.NVarChar);
              parameter[4].Value = bankRequest.PaymentMessage.ReferansNo;
   
              SqlHelper.ExecuteNonQuery("siparis_OdemeBilgisi", parameter);
          }
          catch (Exception ex)
          {
              LogManager.SqlDB.Write("Sipariş Ödeme Bilgisi Kayıt: ", ex);
          }
      }
   }
}
