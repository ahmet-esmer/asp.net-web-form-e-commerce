using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;
using ModelLayer;
using ServiceLayer.Payment.Abstract;

namespace ServiceLayer.Payment.Concrete
{
    public class GarantiBankPayment : PaymentServiceBase
    {
        public GarantiBankPayment(BankaApi api)
        {
            base.bankaApi = api;
        }

        public override PaymentMessage PeymentCore(BankRequest bankRequest)
        {
            PaymentMessage mesaj = new PaymentMessage();

            string strMode = "PROD"; // gerçek ortam 
            string strVersion = "v0.01";
            string strTerminalID = "10016630"; //8 Haneli TerminalID yazılmalı.
            string _strTerminalID = "0" + strTerminalID;
            string strMerchantID = bankaApi.ClientId; //Üye İşyeri Numarası   9115910
            string strUserID = bankaApi.ApiName;//"PROVAUT";
            string strProvUserID = bankaApi.ApiName;//"PROVAUT";
            string strProvisionPassword = bankaApi.ApiPassword;//TerminalProvUserID şifresi
            string strIPAddress = HttpContext.Current.Request.UserHostAddress;

            string strOrderID = bankRequest.SiparisNo;
            string strNumber = bankRequest.KrediKart.No.Trim();
            string strExpireDate = bankRequest.KrediKart.Ay + bankRequest.KrediKart.Yil.Substring(2);
            string strCVV2 = bankRequest.KrediKart.CV2.Trim();

            string grtFiyatToplam = bankRequest.TaksitToplam.ToString("N");
            string strAmount = grtFiyatToplam.Replace(",", "").Replace(".", "").Trim();

            string strType = "sales";
            string strCurrencyCode = "949";

            string strCardholderPresentCode = "0";
            string strMotoInd = "N";
            string strInstallmentCount = "";

            if (bankRequest.Taksit != 1)
            {
                strInstallmentCount = bankRequest.Taksit.ToString();
            }


            char crNull = ' ';
            string adSoyad = bankRequest.KrediKart.AdSoyad.Trim();
            int index = adSoyad.IndexOf(crNull);
            string[] _adSoyad = new string[2];

            if (index > 1)
            {
                _adSoyad[0] = adSoyad.Substring(0, index);
                _adSoyad[1] = adSoyad.Substring(index);
            }
            else
            {
                _adSoyad[0] = adSoyad;
                _adSoyad[1] = adSoyad;
            }


            string SecurityData = GetSHA1(strProvisionPassword + _strTerminalID).ToUpper();
            string HashData = GetSHA1(strOrderID + strTerminalID + strNumber + strAmount + SecurityData).ToUpper();

            string strXML = null;

            strXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + "<GVPSRequest>" + "<Mode>" + strMode + "</Mode>" + "<Version>" + strVersion + "</Version>" + "<Terminal><ProvUserID>" + strProvUserID + "</ProvUserID><HashData>" + HashData + "</HashData><UserID>" + strUserID + "</UserID><ID>" + strTerminalID + "</ID><MerchantID>" + strMerchantID + "</MerchantID></Terminal>" + "<Customer><IPAddress>" + strIPAddress + "</IPAddress><EmailAddress></EmailAddress></Customer>" + "<Card><Number>" + strNumber + "</Number><ExpireDate>" + strExpireDate + "</ExpireDate><CVV2>" + strCVV2 + "</CVV2></Card>" + "<Order><OrderID>" + strOrderID + "</OrderID><GroupID></GroupID><AddressList><Address><Type>S</Type><Name>" + _adSoyad[0] + "</Name><LastName>" + _adSoyad[1] + "</LastName><Company></Company><Text></Text><District></District><City></City><PostalCode></PostalCode><Country></Country><PhoneNumber></PhoneNumber></Address></AddressList> <CommentList><Comment><Number>1</Number><Text>" + KulaniciAdiKes(bankRequest.KrediKart.AdSoyad) + "</Text></Comment></CommentList></Order>" + "<Transaction>" + "<Type>" + strType + "</Type><InstallmentCnt>" + strInstallmentCount + "</InstallmentCnt><Amount>" + strAmount + "</Amount><CurrencyCode>" + strCurrencyCode + "</CurrencyCode><CardholderPresentCode>" + strCardholderPresentCode + "</CardholderPresentCode><MotoInd>" + strMotoInd + "</MotoInd>" + "</Transaction>" + "</GVPSRequest>";



            try
            {
                string data = "data=" + strXML;

                WebRequest _WebRequest = WebRequest.Create(bankaApi.HostName);
                _WebRequest.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                _WebRequest.ContentType = "application/x-www-form-urlencoded";
                _WebRequest.ContentLength = byteArray.Length;

                Stream dataStream = _WebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse _WebResponse = _WebRequest.GetResponse();
                dataStream = _WebResponse.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                //GVPSResponse XML'in değerlerini okuma.
                string XML = responseFromServer;
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(XML);

                XmlElement ReasonCode =
                    xDoc.SelectSingleNode("//GVPSResponse/Transaction/Response/ReasonCode") as XmlElement;
                XmlElement Message =
                    xDoc.SelectSingleNode("//GVPSResponse/Transaction/Response/Message") as XmlElement;


                if (ReasonCode.InnerText == "00")
                {
                    XmlElement xElemRetrefNum = xDoc.SelectSingleNode("//GVPSResponse/Transaction/RetrefNum") as XmlElement;

                    XmlElement xElemAuthCode = xDoc.SelectSingleNode("//GVPSResponse/Transaction/AuthCode") as XmlElement;

                    mesaj.Success = true;
                    mesaj.OnayKodu = xElemAuthCode.InnerText;
                    mesaj.ReferansNo = xElemRetrefNum.InnerText;
                }
                else
                {
                    XmlElement ErrorMsg = xDoc.SelectSingleNode("//GVPSResponse/Transaction/Response/ErrorMsg") as XmlElement;
                    XmlElement SysErrMsg = xDoc.SelectSingleNode("//GVPSResponse/Transaction/Response/SysErrMsg") as XmlElement;

                    mesaj.RedMesaj = ErrorMsg.InnerText;
                    mesaj.RedMesajKodu = SysErrMsg.InnerText;
                }

                return mesaj;
            }
            catch (WebException)
            {
                mesaj.RedMesaj = "Banka ile Bağlantı kurulamadı lütfen tekrar deneyiniz.";
                mesaj.RedMesajKodu = "Bağlantı Hatası";
                return mesaj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string KulaniciAdiKes(string kulanici)
        {
            if (kulanici.Length >= 20)
            {
                kulanici = kulanici.Substring(0, 20);
            }
            return kulanici;
        }

        private string GetSHA1(string SHA1Data)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            string HashedPassword = SHA1Data;
            byte[] hashbytes = Encoding.GetEncoding("ISO-8859-9").GetBytes(HashedPassword);
            byte[] inputbytes = sha.ComputeHash(hashbytes);
            return GetHexaDecimal(inputbytes);
        }

        private string GetHexaDecimal(byte[] bytes)
        {
            StringBuilder s = new StringBuilder();
            int length = bytes.Length;
            for (int n = 0; n <= length - 1; n++)
            {
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));
            }
            return s.ToString();
        }
    }
}
