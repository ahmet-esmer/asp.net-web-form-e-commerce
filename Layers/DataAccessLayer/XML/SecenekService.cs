using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelLayer;
using System.Xml;

namespace DataAccessLayer.XML
{
    public class SecenekService
    {
        private XmlDocument xd;
        private string xmlFilePath;

        public SecenekService(string fileName)
        {
            xmlFilePath = ConfigLibrary.ConfigHelper.GetPhysicalPath("App_Data\\" + fileName + ".xml");
        }

        public bool Add(string secenek)
        {
            xd = new XmlDocument();
            xd.Load(xmlFilePath);

            XmlElement xe0 = xd.CreateElement("secenek");
            xe0.InnerText = secenek;
            xd.DocumentElement.AppendChild(xe0);

            xd.Save(xmlFilePath);

            return true;
        }

        public void Update(Secenek secenek)
        {
            xd = new XmlDocument();
            xd.Load(xmlFilePath);

            XmlNodeList list = xd.DocumentElement.ChildNodes;
     
            foreach (XmlNode item in list)
            {
                if (item.ChildNodes[0].InnerText == secenek.Name)
                {
                    item.ChildNodes[0].InnerText = secenek.Value;
                    break;
                }
            }

            xd.Save(xmlFilePath);
        }

        public void Delete(string secenek)
        {
            xd = new XmlDocument();
            xd.Load(xmlFilePath);

            XmlNodeList list = xd.DocumentElement.ChildNodes;

            foreach (XmlNode item in list)
            {
                if (item.ChildNodes[0].InnerText == secenek)
                {
                    xd.DocumentElement.RemoveChild(item);
                    xd.Save(xmlFilePath);
                    break;
                }
            }
        }
    }
}
