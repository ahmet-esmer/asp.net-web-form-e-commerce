using System;
using System.Data;
using System.Data.SqlClient;
using ConfigLibrary;
using LoggerLibrary.Abstract;

namespace LoggerLibrary.ConcreteLoggers
{

    public class DbLogger: LoggerBase
    {
        protected override void WriteCore(string baslik, string mesaj)
        {
            base.Baslik = baslik;
            base.Mesaj = mesaj;
            Core();
        }
        protected override void WriteCore(Exception mesaj)
        {
            base.Baslik = mesaj.Message.ToString();
            base.Mesaj = mesaj.ToString();
            Core();
        }
        protected override void WriteCore(string baslik, Exception mesaj)
        {
            base.Baslik = baslik;
            base.Mesaj = mesaj.ToString();
            Core();
        }

        protected override void Core()
        {
         
            using (SqlConnection baglan = new SqlConnection(ConfigHelper.GetConnectionString()))
            {
                baglan.Open();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = baglan;
                    cmd.CommandText = "hataMesajiEkle";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@hataAdi", SqlDbType.NVarChar);
                    cmd.Parameters["@hataAdi"].Value = base.Baslik;
                    cmd.Parameters.Add("@hataMesaji", SqlDbType.NVarChar);
                    cmd.Parameters["@hataMesaji"].Value = base.Mesaj;
                    cmd.Parameters.Add("@deger_dondur", SqlDbType.Bit);
                    cmd.Parameters["@deger_dondur"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                }

            }
        }

    }
}
