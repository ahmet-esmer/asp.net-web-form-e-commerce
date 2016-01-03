using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.PagingLink
{
    public class AjaxPaging
    {
        public static string List(int sayfaGosterim, int SayfaNo, int toplamKayit)
        {

            if (toplamKayit <= sayfaGosterim)
            {
                return " ";
            }

            int ToplamSayfaSayisi = 0;

            StringBuilder sb = new StringBuilder();

            if (toplamKayit % sayfaGosterim == 0)
            {
                ToplamSayfaSayisi = toplamKayit / sayfaGosterim;
            }
            else
            {
                ToplamSayfaSayisi = toplamKayit / sayfaGosterim + 1;
            }


            #region Sayfa 10 Adet Gösterme
            int ilkGosterim = SayfaNo;
            int sonGosterim = SayfaNo;

            if (ToplamSayfaSayisi - 1 == SayfaNo)
                ilkGosterim = ilkGosterim - 9;

            else if (ToplamSayfaSayisi - 2 == SayfaNo)
                ilkGosterim = ilkGosterim - 8;

            else if (ToplamSayfaSayisi - 3 == SayfaNo)
                ilkGosterim = ilkGosterim - 7;

            else if (ToplamSayfaSayisi - 4 == SayfaNo)
                ilkGosterim = ilkGosterim - 6;

            else
                ilkGosterim = ilkGosterim - 5;


            switch (sonGosterim)
            {
                case 0: sonGosterim = 10; break;
                case 1: sonGosterim = 10; break;
                case 2: sonGosterim = 10; break;
                case 3: sonGosterim = 10; break;
                case 4: sonGosterim = 10; break;
                default: sonGosterim = sonGosterim + 5; break;
            }

            #endregion

            string ilkYaz = null;
            string geriYaz = null;
            string ileriYaz = null;
            string sonYaz = null;

            #region İlk Sayfa ve En son Sayfa İşlemi

            if (!(ToplamSayfaSayisi <= 10))
            {
                if (SayfaNo == ToplamSayfaSayisi - 1)
                {
                    sonYaz = "<span class='Last Page' > Son Sayfa </span>\n";
                }
                else
                {
                    sonYaz = "<a class='Last Page' rel=" + Convert.ToString(ToplamSayfaSayisi - 1) + "> Son Sayfa </a>";
                }


                if (SayfaNo == 0)
                {
                    ilkYaz = "<span class='First Page'   > İlk Sayfa </span>\n";
                }
                else
                {
                    ilkYaz = "<a class='First Page' rel==0> İlk Sayfa </a>";
                }
            }
            #endregion



            for (int i = 0; i < ToplamSayfaSayisi; i++)
            {
                if (i >= ilkGosterim && i < sonGosterim)
                {
                    if (i == SayfaNo)
                    {
                        sb.Append("<a class='current' rel=" + +(i) + " >" + (i + 1) + "</a>\n");
                    }
                    else
                    {
                        sb.Append("<a class='number' rel=" + +(i) + ">" + (i + 1) + "</a>\n");
                    }
                }
            }

            #region Sayfa İleri Geri İşlemi
            SayfaNo = SayfaNo + 1;
            int geri = SayfaNo - 2;

            if (geri < 0)
            {
                geriYaz = "<span class='Previous' > « Geri </span>\n";
            }
            else
            {
                geriYaz = "<a class='PreviousPage' rel=" + geri.ToString() + "> « Geri </a>\n";
            }

            if (SayfaNo >= ToplamSayfaSayisi)
            {
                ileriYaz = "<span class='Next' > İleri » </span>\n";
            }
            else
            {
                ileriYaz = "<a class='NextPage' rel=" + SayfaNo.ToString() + "> İleri » </a>\n";
            }


            #endregion

            string toplamMetin = "<div class='pageTotal' > Toplam: " + toplamKayit.ToString() + "</div>";
            return ilkYaz + geriYaz + sb.ToString() + ileriYaz + sonYaz + toplamMetin;
        }
    }
}
