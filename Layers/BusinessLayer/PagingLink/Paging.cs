using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace BusinessLayer.PagingLink
{
    public class Paging
    {
        public int TotolItem { get; set; }
        public int ViewItems { get; set; }
        public int StartItem { get; set; }
        public int EndItem {get; set; }
        private int curentPage { get; set; }
        public NameValueCollection QueryString { get; set; }

        private string mesaj;

        public string Mesaj
        {
            get {

                if (this.TotolItem == 0)
                {
                    mesaj = "Ürün Bulunamadı";
                }
                else
                {
                    int bitisGecici = 0;
                    if (this.EndItem > this.TotolItem)
                    {
                        bitisGecici = this.TotolItem;
                    }
                    else
                    {
                        bitisGecici = this.EndItem;
                    }

                    mesaj = string.Format("<b>{0}</b> Kayıt Bulundu. <b> {1}</b> - <b>{2}</b>  Arası Görüntüleniyor.",
                        this.TotolItem, this.StartItem, bitisGecici.ToString());
                }   

                return mesaj;
            }
            set { mesaj = value; }
        }
        
        public int CurentPage
        {
            set
            {
                curentPage = value;

                if (this.ViewItems == 0)
                    this.ViewItems = 8;

                this.StartItem = (curentPage * this.ViewItems) + 1;
                this.EndItem = this.StartItem + this.ViewItems - 1;
            }
        }
    }
}
