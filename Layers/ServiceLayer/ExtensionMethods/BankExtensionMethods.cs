using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using DataAccessLayer;

namespace ServiceLayer.ExtensionMethods
{
    public static class BankExtensionMethods
    {
        /// <summary>
        /// Seçilen taksit vade oranın bulup Aylık taksit oranın ve taksitli toplam miktarı set eder.
        /// </summary>
        /// <param name="bank"> BankRequest Nesnesinin seçili BankaId ve seçili
        /// taksitler atanmış olması gerekiyor.  </param>
        public static void FiyatTaksitUygula(this BankRequest bank)
        {

            bank.VadeFarki = BankaTaksitDB.TaksitVade(bank.BankaId, bank.Taksit);
            bank.TaksitToplam += bank.TaksitToplam * Convert.ToDecimal(bank.VadeFarki) / 100;
            bank.TaksitFiyat = bank.TaksitToplam / bank.Taksit;

        }
    }
}
