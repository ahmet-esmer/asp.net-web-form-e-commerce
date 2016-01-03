using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace ImageLibrary
{
    public  class Images
    {
        internal static string imagePath { get; set; }
        internal static string serverPath = HttpContext.Current.Server.MapPath("~");
        internal static string mainImage = serverPath + "/Products/";
        internal static string mainI;
        internal static Images instance;
        internal bool retVal;

        public static Images LittleImage
        {
            get
            {
                imagePath = serverPath + "/Products/Little/";
            
                if (instance == null)
                {
                    lock (new object())
                    {
                        if (instance == null)
                            instance = new Images();
                    }
                }
                return instance;
            }
        }
        public static Images BigImage
        {
            get
            {
                imagePath = serverPath + "/Products/Big/";

                if (instance == null)
                {
                    lock (new object())
                    {
                        if (instance == null)
                            instance = new Images();
                    }
                }
                return instance;
            }
        }
        public static Images SmallImage
        {
            get
            {
                imagePath = serverPath + "/Products/Small/";

                if (instance == null)
                {
                    lock (new object())
                    {
                        if (instance == null)
                            instance = new Images();
                    }
                }
                return instance;
            }
        }
        public static Images DefaultImage
        {
            get
            {
                imagePath = serverPath + "/Products/Flash/";

                if (instance == null)
                {
                    lock (new object())
                    {
                        if (instance == null)
                            instance = new Images();
                    }
                }
                return instance;
            }
        }


        public bool Save(string imageName, int maxSize, bool delete=false)
        {
            mainI =  mainImage + imageName;

            imageName = imagePath + imageName;

            Bitmap bmpKaynak = null;
            bmpKaynak = new Bitmap(mainI);


            Size orjinalBoyut = bmpKaynak.Size;
            Size yeniBoyut = new Size(0, 0);

            bmpKaynak.Dispose();

            decimal maxYukseklikDecimal = Convert.ToDecimal(maxSize);
            decimal maxGenislikDecimal = Convert.ToDecimal(maxSize);

            decimal boyutlandirmaKatsayisi = default(decimal);

            if ((orjinalBoyut.Height > orjinalBoyut.Width))
            {
                // Düşey
                boyutlandirmaKatsayisi = Convert.ToDecimal(decimal.Divide(orjinalBoyut.Height, maxYukseklikDecimal));
                yeniBoyut.Height = maxSize;
                yeniBoyut.Width = Convert.ToInt32(orjinalBoyut.Width / boyutlandirmaKatsayisi);
            }
            else
            {
                // kare ya da dikdörtgen
                boyutlandirmaKatsayisi = Convert.ToDecimal(decimal.Divide(orjinalBoyut.Width, maxGenislikDecimal));
                yeniBoyut.Width = maxSize;
                yeniBoyut.Height = Convert.ToInt32(orjinalBoyut.Height / boyutlandirmaKatsayisi);
            }

            Bitmap kaynakBitmap = null;
            Bitmap hedefBitmap = null;
            Graphics grafikler = null;

            try
            {
                kaynakBitmap = new Bitmap(mainI);

                int yeniGenislik = yeniBoyut.Width;
                int yeniYukseklik = yeniBoyut.Height;

                hedefBitmap = new Bitmap(yeniGenislik, yeniYukseklik);

                grafikler = Graphics.FromImage(hedefBitmap);

                grafikler.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                grafikler.DrawImage(kaynakBitmap, new Rectangle(0, 0, yeniGenislik, yeniYukseklik));
                kaynakBitmap.Dispose();

                // resmi kaydet
                hedefBitmap.Save(imageName, ImageFormat.Jpeg);

                retVal = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                if ((grafikler != null))
                {
                    grafikler.Dispose();
                }
                if (kaynakBitmap != null)
                {
                    kaynakBitmap.Dispose();
                }
                if (hedefBitmap != null)
                {
                    hedefBitmap.Dispose();
                }

                if (delete)
                {
                    if (File.Exists(mainI))
                        File.Delete(mainI);
                }
            }

            return retVal;
        }

        public void Save(string imageName)
        {
            try
            {
                mainI = mainImage + imageName;
                imageName = imagePath + imageName;

                File.Copy(mainI, imageName);
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string imageName)
        {
            try
            {
                imageName = imagePath + imageName;

                if (File.Exists(imageName))
                {
                    File.Delete(imageName);
                    retVal = true;
                }

            }
            catch (Exception)
            {
                throw;
            }

            return retVal;
        }

        public static string GetImageName(string FileName)
        {
            string extention = Path.GetExtension(FileName);

            string str = Convert.ToString(DateTime.Now).ToString().Replace(".", "").Replace(":", "").Replace(" ", "").Replace("/", "");
            Random random = new Random();
            return ((Convert.ToString(random.Next()) + str) + extention);
        }

        public static string GetPath(string name)
        {
            return HttpContext.Current.Server.MapPath("~/Products/"+ name);
        }

        public static string GetPathBig(string name)
        {
            return HttpContext.Current.Server.MapPath("~/Products/Big/" + name);
        }

        public static string GetFlashPath(string name)
        {
            return HttpContext.Current.Server.MapPath("~/Products/Flash/" + name);
        }
    }
}
