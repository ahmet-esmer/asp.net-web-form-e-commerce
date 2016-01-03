using System;
using System.Linq;
using System.Xml.Linq;
using ConfigLibrary;
using LoggerLibrary.Abstract;

namespace LoggerLibrary
{
    public class LogManager
    {
        private LoggerBase headLogger;
        private LoggerBase tailLogger;
        private static string location = null;
        private static LogManager instance, instanceMail, instanceText, instanceEvent, instanceDB;

        public static LogManager Default
        {
            get
            {
                if (instance == null)
                {
                    lock (new object())
                    {
                        if (instance == null)
                            instance = new LogManager();
                    }
                }

                return instance;
            }
        }
        public static LogManager Mail
        {
            get
            {
                location = "MailLogger";

                if (instanceMail == null)
                {
                    lock (new object())
                    {
                        if (instanceMail == null)
                            instanceMail = new LogManager();
                    }
                }

                return instanceMail;
            }
        }
        public static LogManager Text
        {
            get
            {
                location = "TextLogger";

                if (instanceText == null)
                {
                    lock (new object())
                    {
                        if (instanceText == null)
                            instanceText = new LogManager();
                    }
                }
                return instanceText;
            }
        }
        public static LogManager Event
        {
            get
            {
                location = "EventLogger";

                if (instanceEvent == null)
                {
                    lock (new object())
                    {
                        if (instanceEvent == null)
                            instanceEvent = new LogManager();
                    }
                }

                return instanceEvent;
            }
        }
        public static LogManager SqlDB
        {
            get
            {
                location = "DbLogger";

                if (instanceDB == null)
                {
                    lock (new object())
                    {
                        if (instanceDB == null)
                            instanceDB = new LogManager();
                    }
                }

                return instanceDB;
            }
        }

        private LogManager()
        {
            ReadConfig();
        }

        private void ReadConfig()
        {

            XElement doc = XElement.Load(ConfigHelper.GetPhysicalPath(@"Config\LogConfig.xml"));

            var loggers = from p in doc.Elements("loggerClass")
                          orderby p.Value == location descending
                          select new
                          {
                              Class = p.Value,
                          };

            foreach (var logger in loggers)
            {
                LoggerBase logInstance = LogFactory.GetInstens(logger.Class);

                if (headLogger == null)
                    headLogger = logInstance;

                if (tailLogger != null)
                    tailLogger.NextLogger = logInstance;

                tailLogger = logInstance;
            }
        }

        public void Write(string baslik, string mesaj)
        {
            if (headLogger != null)
            {
                Action<string, string> logStrHandler = new Action<string, string>(headLogger.Write);
                logStrHandler.BeginInvoke(baslik, mesaj, null, null);
            }
        }

        public void Write(string baslik, Exception mesaj)
        {
            if (headLogger != null)
            {
                Action<string, Exception> logStrExHandler = new Action<string, Exception>(headLogger.Write);
                logStrExHandler.BeginInvoke(baslik, mesaj, null, null);
            }
        }

        public void Write(Exception mesaj)
        {
            if (headLogger != null)
            {
                Action<Exception> logExHandler = new Action<Exception>(headLogger.Write);
                logExHandler.BeginInvoke(mesaj, null, null);
            }
        }
    }




      /// <summary>
        ///     <description>Index Vip Method</description>
        ///     <author>Fatih GÜNEŞ</author>
        ///     <createDate>20.04.2012</createDate>
        /// </summary>
        /// <param name="sSolrServer">sSolrServer parameter</param>
        /// <param name="bClean">bClean parameter</param>
        /// <param name="bOptimize"> bOptimize parameter</param>
        /// <param name="bDelta"> bDelta parameter</param>
        /// <updates>
        ///     <update>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </update>
        /// </updates>
        /// <reviews>
        ///     <review>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </review>
        /// </reviews>
        public void IndexVip(string sSolrServer, bool bClean = false, bool bOptimize = true, bool bDelta = false) {
            var connectionx = new SolrConnection(sSolrServer) { Timeout = 600000 };
            Startup.Init<RealtyFields>(connectionx);
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<RealtyFields>>();

            if (CheckSolrStatus(sSolrServer) > 0) {
                if (bDelta) {
                    System.Console.WriteLine(string.Format("Tamamlanmamis is var:{0}", sSolrServer));
                    return;
                }

                solr.Commit();
                SendError(DateTime.Now, string.Format("Full import baslamadi:{0}", sSolrServer));
            }

            if (bClean) {
                solr.Delete(SolrQuery.All);
            }

            System.Console.WriteLine(string.Format("start:{0}", DateTime.Now));

            // Getting Last Index
            DateTime dtStartTime = DateTime.Now;
            DateTime dtLastIndexTime = DateTime.MinValue;
            string sRegistryKey = sSolrServer.Replace("http://", string.Empty);
            object objLastIndex = Registry.CurrentUser.GetValue(sRegistryKey);
            if (objLastIndex != null) {
                dtLastIndexTime = Convert.ToDateTime(objLastIndex);
            }

            // silinecek
            int nErrorCount = 0;
            int nProcessed = 0;
            int nInitialPageSize = 1000;
            int nPageSize = nInitialPageSize;

            // delta delete 
            List<vwRealtySolrVip> clsData;
            List<vwRealtySolrVipDeleted> clsDeleted = null;
            using (Realty clsRealty = new Realty()) {
                if (bDelta) {
                    clsDeleted = clsRealty.DeleteForSolrVip(dtLastIndexTime);
                    System.Console.WriteLine(string.Format("delete start:{0} count:{1}", DateTime.Now, clsDeleted.Count));
                    solr.Delete(from a in clsDeleted
                                select new RealtyFields {
                                    RealtyID = a.RealtyID
                                });
                    solr.Commit();
                }

                System.Console.WriteLine(string.Format("data start:{0}", DateTime.Now));

                if (bDelta) {
                    System.Console.WriteLine(string.Format("delta from :{0}", dtLastIndexTime));
                    clsData = clsRealty.FillForVipSolr(dtLastIndexTime);
                }
                else {
                    clsData = clsRealty.FillForVipSolr();
                }
            }

            System.Console.WriteLine(string.Format("data end:{0} count:{1}", DateTime.Now, clsData.Count));

            if (clsData.Count > 0) {
                do {
                    try {
                        solr.Add(
                            from a in clsData.Skip(nProcessed).Take(nPageSize)
                            select
                                new RealtyFields {
                                    RealtyID = a.RealtyID,
                                    RealtyFirmID = a.RealtyFirmID,
                                    RealtyNo = a.RealtyNo,
                                    RealtyTitle = a.RealtyTitle,
                                    RealtyDescription = Helper.Helper.SetRegex(a.RealtyDescription),
                                    RealtyCategory = a.RealtyCategory,
                                    RealtyCategoryID = a.RealtyCategoryID,
                                    RealtyMainCategory = a.RealtyMainCategory,
                                    RealtyMainCategoryID = a.RealtyMainCategoryID,
                                    RealtyBathRoom = a.RealtyBathroom,
                                    RealtyLivingRoom = a.RealtyLivingRoom,
                                    RealtyCityID = a.RealtyCityID,
                                    RealtyCityDescription = a.RealtyCityDescription,
                                    RealtyCountyDescription = a.RealtyCountyDescription,
                                    RealtyCountyID = a.RealtyCountyID,
                                    RealtyDistrictDescription = a.RealtyDistrictDescription,
                                    RealtyDistrictID = a.RealtyDistrictID,
                                    RealtyAreaIDs = string.IsNullOrEmpty(a.RealtyAreaIDs) ? null : a.RealtyAreaIDs.Split(',').ToArray().ToList(),
                                    RealtyPriceTL = a.RealtyPriceTL.HasValue ? (double)a.RealtyPriceTL.Value : 0.0,
                                    RealtyPrice = (double)a.RealtyPrice,
                                    RealtyPriceCurrency = a.RealtyPriceCurrency,
                                    RealtyPriceCurrencyID = a.RealtyPriceCurrencyID,
                                    RealtyPriceShow = a.RealtyPriceShow,
                                    RealtyRoomInfo = a.RealtyRoomInfo,
                                    RealtyRoom = a.RealtyRoom,
                                    RealtySqm = a.RealtySqm,
                                    RealtyCreatedDateTime = a.RealtyCreatedDateTime,
                                    RealtyUpdatedDateTime = a.RealtyUpdatedDateTime,
                                    RealtyStartDateTime = a.RealtyStartDateTime,
                                    RealtyEndDateTime = a.RealtyEndDateTime,
                                    RealtySortOrder = a.RealtySortOrder.ConvertTo<double>(),
                                    RealtyProductBackgroundWithColor = a.RealtyProductBackgroundWithColor.ToString(),
                                    RealtyProductBoldTitle = a.RealtyProductBoldTitle.ToString(),
                                    RealtyProductCount = a.RealtyProductCount,
                                    RealtyActivate = a.RealtyActivate,
                                    RealtyActivateID = a.RealtyActivateID.ToString(),
                                    RealtyAdvertiseOwner = a.RealtyAdvertiseOwner,
                                    RealtyAdvertiseOwnerID = a.RealtyAdvertiseOwnerID,
                                    RealtyAge = a.RealtyAge,
                                    RealtyAttributeInIDs = string.IsNullOrEmpty(a.RealtyAttributeInIDs) ? null : a.RealtyAttributeInIDs.Split(',').ToArray().ToList(),
                                    RealtyAttributeLocationIDs = string.IsNullOrEmpty(a.RealtyAttributeLocationIDs) ? null : a.RealtyAttributeLocationIDs.Split(',').ToArray().ToList(),
                                    RealtyAttributeOutIDs = string.IsNullOrEmpty(a.RealtyAttributeOutIDs) ? null : a.RealtyAttributeOutIDs.Split(',').ToArray().ToList(),
                                    RealtyBuildID = a.RealtyBuildID,
                                    RealtyBuildStateID = a.RealtyBuildStateID,
                                    RealtyCreditID = a.RealtyCreditID,
                                    RealtyFileCount = a.RealtyFileCount,
                                    RealtyFirmRealEstateOrganizationID = a.RealtyFirmRealEstateOrganizationID.ToString(),
                                    RealtyFirmTypeID = a.RealtyFirmTypeID,
                                    RealtyFirmUserID = a.RealtyFirmUserID,
                                    RealtyFlatReceivedID = a.RealtyFlatReceivedID,
                                    RealtyRegisterID = a.RealtyRegisterID,
                                    RealtyFloorCount = a.RealtyFloorCount,
                                    RealtyFloorID = a.RealtyFloorID,
                                    RealtyFuelID = a.RealtyFuelID,
                                    RealtyHeatingID = a.RealtyHeatingID,
                                    RealtyImage = a.RealtyImage,
                                    RealtyIsPopulated = a.RealtyIsPopulated,
                                    RealtyIsStudentOrSingle = a.RealtyIsStudentOrSingle,
                                    RealtyKeywords = a.RealtyKeywords,
                                    RealtyMultimediaIDs = string.IsNullOrEmpty(a.RealtyMultimediaIDs) ? null : a.RealtyMultimediaIDs.Split(',').ToArray().ToList(),
                                    RealtyProjectID = a.RealtyProjectID,
                                    RealtyPublishID = a.RealtyPublishID.ToString(),
                                    RealtyResidenceID = a.RealtyResidenceID.ToString(),
                                    RealtyStar = a.RealtyStar,
                                    RealtySubCategory = a.RealtySubCategory,
                                    RealtySubCategoryID = a.RealtySubCategoryID,
                                    RealtyTimeShareTerm = a.RealtyTimeShareTerm.ToString(),
                                    RealtyUsageID = a.RealtyUsageID,
                                    RealtyVideo = a.RealtyVideo,
                                    RealtyPlace = a.RealtyPlace,
                                    RealtyBarterID = a.RealtyBarterID,
                                    //RealtyHasVIPPacket = Convert.ToBoolean(a.RealtyHasVIPPacket)
                                    RealtyMapLatitude = a.RealtyMapLatitude.ToString(),
                                    RealtyMapLongtitude = a.RealtyMapLongtitude.ToString()
                                });

                        if (bClean && !bDelta) {
                            solr.Commit();
                        }
                    }
                    catch {
                        if (nPageSize > 1) {
                            System.Console.WriteLine(string.Format("Error in {0} decrease pagesize", nProcessed));
                            nPageSize = nPageSize / 10;
                            continue;
                        }

                        nErrorCount++;
                        nPageSize = nInitialPageSize;
                        System.Console.WriteLine(@"Pagesize set to default");
                        nProcessed++;
                        continue;
                    }

                    System.Console.WriteLine(@"{0} {1}", nProcessed, DateTime.Now);
                    nProcessed += nPageSize;
                } while (nProcessed < clsData.Count);

                solr.Commit();

                if (bOptimize) {
                    solr.Optimize();
                }

                // Setting write lastindextime
                Registry.CurrentUser.SetValue(sRegistryKey, dtStartTime);

                // Send Status Mail
                SendStatus(DateTime.Now, bDelta, clsData.Count, clsDeleted == null ? 0 : clsDeleted.Count, nErrorCount, sSolrServer);
            }

            System.Console.WriteLine(string.Format("end:{0} count:{1} error:{2}", DateTime.Now, clsData.Count, nErrorCount));
        }

        /// <summary>
        /// Default Indexer
        /// </summary>
        /// <typeparam name="T">type of solr object</typeparam>
        /// <param name="sSolrServer">solr server to be indexed</param>
        public void Index<T>(string sSolrServer) {
            var connectionx = new SolrConnection(sSolrServer) { Timeout = 600000 };
            Startup.Init<POISolrFields>(connectionx);
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<POISolrFields>>();

            if (CheckSolrStatus(sSolrServer) > 0) {
                solr.Commit();
            }

            // silinecek
            int nErrorCount = 0;
            int nProcessed = 0;
            const int nInitialPageSize = 10000;
            int nPageSize = nInitialPageSize;

            // clean
            solr.Delete(SolrQuery.All);

            using (POI clsPoi = new POI()) {
                var clsData = clsPoi.FillForSolr();

                System.Console.WriteLine(string.Format("data end:{0} count:{1}", DateTime.Now, clsData.Count));

                if (clsData.Count > 0) {
                    do {
                        try {
                            solr.Add(from a in clsData.Skip(nProcessed).Take(nPageSize)
                                     select new POISolrFields {
                                         POIID = a.POIID.ToString(),
                                         POICategoryID = a.POICategoryID ?? 0,
                                         POISubCategoryID = a.POISubCategoryID,
                                         POICityID = a.POICityID ?? 0,
                                         POICountyID = a.POICountyID ?? 0,
                                         POIDistrictID = a.POIDistrictID,
                                         POIName = a.POIName,
                                         POIAddress = a.POIAddress,
                                         POIPoints = a.POIPoints
                                     });
                        }
                        catch {
                            if (nPageSize > 1) {
                                System.Console.WriteLine(string.Format("Error in {0} decrease pagesize", nProcessed));
                                nPageSize = nPageSize / 10;
                                continue;
                            }

                            nErrorCount++;
                            nPageSize = nInitialPageSize;
                            System.Console.WriteLine(@"Pagesize set to default");
                            nProcessed++;
                            continue;
                        }

                        System.Console.WriteLine(@"{0} {1}", nProcessed, DateTime.Now);
                        nProcessed += nPageSize;
                    } while (nProcessed < clsData.Count);

                    solr.Commit();
                    solr.Optimize();
                }
            }
        }

        public void IndexProject(string sSolrServer) {
            var connectionx = new SolrConnection(sSolrServer) { Timeout = 600000 };
            Startup.Init<ProjectSolrFields>(connectionx);
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<ProjectSolrFields>>();

            // silinecek
            int nErrorCount = 0;
            int nProcessed = 0;
            const int nInitialPageSize = 10000;
            int nPageSize = nInitialPageSize;

            // clean
            solr.Delete(SolrQuery.All);

            using (Project clsProject = new Project()) {
                var clsData = clsProject.FillForProjectSolr();

                System.Console.WriteLine(string.Format("data end:{0} count:{1}", DateTime.Now, clsData.Count));

                if (clsData.Count > 0) {
                    do {
                        try {
                            solr.Add(from a in clsData.Skip(nProcessed).Take(nPageSize)
                                     select new ProjectSolrFields {
                                         ProjectID = a.ProjectID.ToString(),
                                         ProjectCategoryID = a.ProjectCategoryID ?? 0,
                                         ProjectCityID = a.ProjectCityID ?? 0,
                                         ProjectCityDescription = a.ProjectCityDescription,
                                         ProjectCountyID = a.ProjectCountyID ?? 0,
                                         ProjectCountyDescription = a.ProjectCountyDescription,
                                         ProjectDistrictID = a.ProjectDistrictID ?? 0,
                                         ProjectDistrictDescription = a.ProjectDistrictDescription,
                                         ProjectAreaIDs = a.ProjectAreaIDs.Split(',').ToList(),
                                         ProjectAreas = a.ProjectAreas.Split(',').ToList(),
                                         ProjectAddress = a.ProjectAddress,
                                         ProjectPlace = a.ProjectPlace,
                                         ProjectTitle = a.ProjectTitle,
                                         ProjectDescription = a.ProjectDescription,
                                         ProjectAbout = a.ProjectAbout,
                                         ProjectFirmID = a.ProjectFirmID ?? 0,
                                         ProjectFirmLogo = a.ProjectFirmLogo,
                                         ProjectFirmName = a.ProjectFirmName,
                                         ProjectBanner = a.ProjectBanner,
                                         ProjectCompletionDateTime = a.ProjectCompletionDateTime,
                                         ProjectHasPacket = Convert.ToBoolean(a.ProjectHasPacket),
                                         ProjectRealtyPriceTL = !string.IsNullOrEmpty(a.ProjectRealtyPriceTL) ? a.ProjectRealtyPriceTL.Split(',').Where(s => !string.IsNullOrEmpty(s)).Select(s => (decimal?)Convert.ToDecimal(s)).ToList() : (List<decimal?>)null,
                                         ProjectRealtyPriceCurrencyID = !string.IsNullOrEmpty(a.ProjectRealtyPriceCurrencyID) ? Convert.ToInt32(a.ProjectRealtyPriceCurrencyID) : (int?)null,
                                         ProjectRoom = !string.IsNullOrEmpty(a.ProjectRoom) ? a.ProjectRoom.Split(',').ToList() : null,
                                         ProjectSqm = !string.IsNullOrEmpty(a.ProjectSqm) ? a.ProjectSqm.Split(',').Select(s => Convert.ToInt32(s)).ToList() : null,
                                         ProjectPoints = a.ProjectPoints,
                                         RealtySortOrder = a.RealtySortOrder.ConvertTo<double>()
                                     });
                        }
                        catch {
                            if (nPageSize > 1) {
                                System.Console.WriteLine(string.Format("Error in {0} decrease pagesize", nProcessed));
                                nPageSize = nPageSize / 10;
                                continue;
                            }

                            nErrorCount++;
                            nPageSize = nInitialPageSize;
                            System.Console.WriteLine(@"Pagesize set to default");
                            nProcessed++;
                            continue;
                        }

                        System.Console.WriteLine(@"{0} {1}", nProcessed, DateTime.Now);
                        nProcessed += nPageSize;
                    } while (nProcessed < clsData.Count);

                    solr.Commit();
                    solr.Optimize();
                }
            }
        }

        /// <summary>
        ///     <description>Get Exception Message</description>
        ///     <author>Ahmet Esmer</author>
        ///     <createDate>12.02.2013</createDate>
        /// </summary>
        /// <param name="sExceptionMessage">sExceptionMessage for checking</param>
        /// <returns>null or string value</returns>
        /// <updates>
        ///     <update>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </update>
        /// </updates>
        /// <reviews>
        ///     <review>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </review>
        /// </reviews>
        public static string GetMessageTitle(string sExceptionMessage) {
            string sMessage = string.Empty;
            try {
                sMessage = Helper.Helper.ExtractString(sExceptionMessage, "<h1>", "</h1>");
            }
            catch {
            }
            return sMessage;
        }


        /// <summary>
        ///     <description>IndexProjectVip method</description>
        ///     <author>Fatih GÜNEŞ</author>
        ///     <createDate>01.06.2012</createDate>
        /// </summary>
        /// <param name="sSolrServer">s Solr Server</param>
        /// <updates>
        ///     <update>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </update>
        /// </updates>
        /// <reviews>
        ///     <review>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </review>
        /// </reviews>
        public void IndexProjectVip(string sSolrServer) {
            var connectionx = new SolrConnection(sSolrServer) { Timeout = 600000 };
            Startup.Init<ProjectSolrFields>(connectionx);
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<ProjectSolrFields>>();

            // silinecek
            int nErrorCount = 0;
            int nProcessed = 0;
            const int nInitialPageSize = 1000;
            int nPageSize = nInitialPageSize;

            // clean
            solr.Delete(SolrQuery.All);

            using (Project clsProject = new Project()) {
                var clsData = clsProject.FillProjectSolrVip();

                System.Console.WriteLine(string.Format("data end:{0} count:{1}", DateTime.Now, clsData.Count));

                if (clsData.Count > 0) {
                    do {
                        try {
                            solr.Add(from a in clsData.Skip(nProcessed).Take(nPageSize)
                                     select new ProjectSolrFields {
                                         ProjectID = a.ProjectID.ToString(),
                                         ProjectCategoryID = a.ProjectCategoryID ?? 0,
                                         ProjectCityID = a.ProjectCityID ?? 0,
                                         ProjectCityDescription = a.ProjectCityDescription,
                                         ProjectCountyID = a.ProjectCountyID ?? 0,
                                         ProjectCountyDescription = a.ProjectCountyDescription,
                                         ProjectDistrictID = a.ProjectDistrictID ?? 0,
                                         ProjectDistrictDescription = a.ProjectDistrictDescription,
                                         ProjectAreaIDs = a.ProjectAreaIDs.Split(',').ToList(),
                                         ProjectAreas = a.ProjectAreas.Split(',').ToList(),
                                         ProjectAddress = a.ProjectAddress,
                                         ProjectPlace = a.ProjectPlace,
                                         ProjectTitle = a.ProjectTitle,
                                         ProjectDescription = a.ProjectDescription,
                                         ProjectAbout = a.ProjectAbout,
                                         ProjectFirmID = a.ProjectFirmID ?? 0,
                                         ProjectFirmLogo = a.ProjectFirmLogo,
                                         ProjectFirmName = a.ProjectFirmName,
                                         ProjectBanner = a.ProjectBanner,
                                         ProjectCompletionDateTime = a.ProjectCompletionDateTime,
                                         ProjectHasPacket = Convert.ToBoolean(a.ProjectHasPacket),
                                         ProjectRealtyPriceTL = !string.IsNullOrEmpty(a.ProjectRealtyPriceTL) ? a.ProjectRealtyPriceTL.Split(',').Where(s => !string.IsNullOrEmpty(s)).Select(s => (decimal?)Convert.ToDecimal(s)).ToList() : (List<decimal?>)null,
                                         ProjectRealtyPriceCurrencyID = !string.IsNullOrEmpty(a.ProjectRealtyPriceCurrencyID) ? Convert.ToInt32(a.ProjectRealtyPriceCurrencyID) : (int?)null,
                                         ProjectRoom = !string.IsNullOrEmpty(a.ProjectRoom) ? a.ProjectRoom.Split(',').ToList() : null,
                                         ProjectSqm = !string.IsNullOrEmpty(a.ProjectSqm) ? a.ProjectSqm.Split(',').Select(s => Convert.ToInt32(s)).ToList() : null,
                                         ProjectPoints = a.ProjectPoints
                                     });
                        }
                        catch {
                            if (nPageSize > 1) {
                                System.Console.WriteLine(string.Format("Error in {0} decrease pagesize", nProcessed));
                                nPageSize = nPageSize / 10;
                                continue;
                            }

                            nErrorCount++;
                            nPageSize = nInitialPageSize;
                            System.Console.WriteLine(@"Pagesize set to default");
                            nProcessed++;
                            continue;
                        }

                        System.Console.WriteLine(@"{0} {1}", nProcessed, DateTime.Now);
                        nProcessed += nPageSize;
                    } while (nProcessed < clsData.Count);

                    solr.Commit();
                    solr.Optimize();
                }
            }
        }

        /// <summary>
        /// Send Status mail of import
        /// </summary>
        /// <param name="dtStatus">Index time</param>
        /// <param name="bDelta">Is Delta</param>
        /// <param name="nCOunt">Total count</param>
        /// <param name="nDeleted">how many deleted</param>
        /// <param name="nError">how many error</param>
        /// <param name="sSolrServer">solr server to be indexed</param>
        protected void SendStatus(DateTime dtStatus, bool bDelta, int nCOunt, int nDeleted, int nError, string sSolrServer) {
            string sBody =
                string.Format(
                @"Server:{5}<br>
                Solr Status<Br>
                SentDate:{0}<br>
                Count:{1}<br>
                Deleted:{2}<br>
                Error:{3}<br>
                Delta:{4}<br>",
                dtStatus,
                nCOunt,
                nDeleted,
                nError,
                bDelta,
                sSolrServer);
            using (eMail clsMail = new eMail(ConfigurationManager.AppSettings["emailServer"])) {
                clsMail.SendMail(ConfigurationManager.AppSettings["emailFrom"], "nkubilay@hurriyetemlak.com;bbickici@hurriyetemlak.com;ssendere@hurriyet.com.tr", string.Empty, string.Empty, "Solr", sBody, string.Empty, ConfigurationManager.AppSettings["emailDisplayName"]);
            }


            /*    string sServerName = sSolrServer.Replace("http://", "");
                sServerName = sServerName.Substring(0, sServerName.IndexOf('/'));

                string sMessage = string.Format("\"Response\": \"{0}\", \"ServerIp\":\"{1}\"");

                StreamWriter streamWriter = new StreamWriter(string.Format(@"\\ha-emlak-fs\SystemCheck\{0}.log", sServerName.Replace(".", string.Empty)));
                streamWriter.WriteLine();
                streamWriter.Close();*/
        }

        /// <summary>
        /// Send Error Message
        /// </summary>
        /// <param name="dtStatus">Index Time</param>
        /// <param name="sError">Error Message</param>
        protected void SendError(DateTime dtStatus, string sError) {
            string sBody =
                string.Format(
                @"Solr Status<Br>
                SentDate:{0}<br>
                Error:{1}<br>",
                dtStatus,
                sError);
            using (eMail clsMail = new eMail(ConfigurationManager.AppSettings["emailServer"])) {
                clsMail.SendMail(ConfigurationManager.AppSettings["emailFrom"], "nkubilay@hurriyetemlak.com;bbickici@hurriyetemlak.com;ssendere@hurriyet.com.tr", string.Empty, string.Empty, "Solr", sBody, string.Empty, ConfigurationManager.AppSettings["emailDisplayName"]);
            }
        }

        /// <summary>
        /// Checking solr status for unfinished index
        /// </summary>
        /// <param name="sSolrServer">Solr Server</param>
        /// <returns>returns >0 if pending docs exists</returns>
        protected int CheckSolrStatus(string sSolrServer) {
            string sStatUri = string.Format("{0}{1}{2}", sSolrServer, sSolrServer[sSolrServer.Length - 1] == '/' ? string.Empty : "/", "admin/stats.jsp");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Request(sStatUri, string.Empty));

            int nPendingDocs = int.Parse(xmlDocument.SelectSingleNode("solr/solr-info/UPDATEHANDLER/entry/stats/stat[@name='docsPending']").InnerText);
            int nPendingAdds = int.Parse(xmlDocument.SelectSingleNode("solr/solr-info/UPDATEHANDLER/entry/stats/stat[@name='adds']").InnerText);
            int nPendingDeletesById = int.Parse(xmlDocument.SelectSingleNode("solr/solr-info/UPDATEHANDLER/entry/stats/stat[@name='deletesById']").InnerText);
            int nPendingDeletesByQuery = int.Parse(xmlDocument.SelectSingleNode("solr/solr-info/UPDATEHANDLER/entry/stats/stat[@name='deletesByQuery']").InnerText);

            return nPendingDocs + nPendingAdds + nPendingDeletesById + nPendingDeletesByQuery;
        }

        /// <summary>
        /// Post a request to solr
        /// </summary>
        /// <param name="requestUri">uri for the request</param>
        /// <param name="postContents">request content data to post</param>
        /// <returns>solr response</returns>
        private string Request(string requestUri, string postContents) {
            string response = string.Empty;
            try {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUri);

                //do not delete!!!! read comment on region WINCLIENTPORTS
                //req.ServicePoint.BindIPEndPointDelegate = new BindIPEndPoint(BindIPEndPointCallback);

                // send post content if exists
                if (!string.IsNullOrEmpty(postContents)) {
                    req.Method = WebRequestMethods.Http.Post;
                    req.ContentType = "text/xml";
                    req.Headers.Add(HttpRequestHeader.ContentEncoding, "UTF-8");
                    req.Headers.Add(HttpRequestHeader.CacheControl, "no-cache, must-revalidate");
                    StreamWriter writer = new StreamWriter(req.GetRequestStream());
                    writer.WriteLine(postContents);
                    writer.Close();
                }

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                StreamReader read = new StreamReader(resp.GetResponseStream());
                response = read.ReadToEnd();
                resp.Close();
                return response;
            }
            catch (WebException e) {
                throw;
            }
            catch (Exception e) {
                throw;
            }

            // return response;
        }



        /// <summary>
        ///     <description>Index Vip Method</description>
        ///     <author>Fatih GÜNEŞ</author>
        ///     <createDate>20.04.2012</createDate>
        /// </summary>
        /// <param name="sSolrServer">sSolrServer parameter</param>
        /// <param name="bClean">bClean parameter</param>
        /// <param name="bOptimize"> bOptimize parameter</param>
        /// <param name="bDelta"> bDelta parameter</param>
        /// <updates>
        ///     <update>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </update>
        /// </updates>
        /// <reviews>
        ///     <review>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </review>
        /// </reviews>
        public void IndexVip(string sSolrServer, bool bClean = false, bool bOptimize = true, bool bDelta = false) {
            var connectionx = new SolrConnection(sSolrServer) { Timeout = 600000 };
            Startup.Init<RealtyFields>(connectionx);
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<RealtyFields>>();

            if (CheckSolrStatus(sSolrServer) > 0) {
                if (bDelta) {
                    System.Console.WriteLine(string.Format("Tamamlanmamis is var:{0}", sSolrServer));
                    return;
                }

                solr.Commit();
                SendError(DateTime.Now, string.Format("Full import baslamadi:{0}", sSolrServer));
            }

            if (bClean) {
                solr.Delete(SolrQuery.All);
            }

            System.Console.WriteLine(string.Format("start:{0}", DateTime.Now));

            // Getting Last Index
            DateTime dtStartTime = DateTime.Now;
            DateTime dtLastIndexTime = DateTime.MinValue;
            string sRegistryKey = sSolrServer.Replace("http://", string.Empty);
            object objLastIndex = Registry.CurrentUser.GetValue(sRegistryKey);
            if (objLastIndex != null) {
                dtLastIndexTime = Convert.ToDateTime(objLastIndex);
            }

            // silinecek
            int nErrorCount = 0;
            int nProcessed = 0;
            int nInitialPageSize = 1000;
            int nPageSize = nInitialPageSize;

            // delta delete 
            List<vwRealtySolrVip> clsData;
            List<vwRealtySolrVipDeleted> clsDeleted = null;
            using (Realty clsRealty = new Realty()) {
                if (bDelta) {
                    clsDeleted = clsRealty.DeleteForSolrVip(dtLastIndexTime);
                    System.Console.WriteLine(string.Format("delete start:{0} count:{1}", DateTime.Now, clsDeleted.Count));
                    solr.Delete(from a in clsDeleted
                                select new RealtyFields {
                                    RealtyID = a.RealtyID
                                });
                    solr.Commit();
                }

                System.Console.WriteLine(string.Format("data start:{0}", DateTime.Now));

                if (bDelta) {
                    System.Console.WriteLine(string.Format("delta from :{0}", dtLastIndexTime));
                    clsData = clsRealty.FillForVipSolr(dtLastIndexTime);
                }
                else {
                    clsData = clsRealty.FillForVipSolr();
                }
            }

            System.Console.WriteLine(string.Format("data end:{0} count:{1}", DateTime.Now, clsData.Count));

            if (clsData.Count > 0) {
                do {
                    try {
                        solr.Add(
                            from a in clsData.Skip(nProcessed).Take(nPageSize)
                            select
                                new RealtyFields {
                                    RealtyID = a.RealtyID,
                                    RealtyFirmID = a.RealtyFirmID,
                                    RealtyNo = a.RealtyNo,
                                    RealtyTitle = a.RealtyTitle,
                                    RealtyDescription = Helper.Helper.SetRegex(a.RealtyDescription),
                                    RealtyCategory = a.RealtyCategory,
                                    RealtyCategoryID = a.RealtyCategoryID,
                                    RealtyMainCategory = a.RealtyMainCategory,
                                    RealtyMainCategoryID = a.RealtyMainCategoryID,
                                    RealtyBathRoom = a.RealtyBathroom,
                                    RealtyLivingRoom = a.RealtyLivingRoom,
                                    RealtyCityID = a.RealtyCityID,
                                    RealtyCityDescription = a.RealtyCityDescription,
                                    RealtyCountyDescription = a.RealtyCountyDescription,
                                    RealtyCountyID = a.RealtyCountyID,
                                    RealtyDistrictDescription = a.RealtyDistrictDescription,
                                    RealtyDistrictID = a.RealtyDistrictID,
                                    RealtyAreaIDs = string.IsNullOrEmpty(a.RealtyAreaIDs) ? null : a.RealtyAreaIDs.Split(',').ToArray().ToList(),
                                    RealtyPriceTL = a.RealtyPriceTL.HasValue ? (double)a.RealtyPriceTL.Value : 0.0,
                                    RealtyPrice = (double)a.RealtyPrice,
                                    RealtyPriceCurrency = a.RealtyPriceCurrency,
                                    RealtyPriceCurrencyID = a.RealtyPriceCurrencyID,
                                    RealtyPriceShow = a.RealtyPriceShow,
                                    RealtyRoomInfo = a.RealtyRoomInfo,
                                    RealtyRoom = a.RealtyRoom,
                                    RealtySqm = a.RealtySqm,
                                    RealtyCreatedDateTime = a.RealtyCreatedDateTime,
                                    RealtyUpdatedDateTime = a.RealtyUpdatedDateTime,
                                    RealtyStartDateTime = a.RealtyStartDateTime,
                                    RealtyEndDateTime = a.RealtyEndDateTime,
                                    RealtySortOrder = a.RealtySortOrder.ConvertTo<double>(),
                                    RealtyProductBackgroundWithColor = a.RealtyProductBackgroundWithColor.ToString(),
                                    RealtyProductBoldTitle = a.RealtyProductBoldTitle.ToString(),
                                    RealtyProductCount = a.RealtyProductCount,
                                    RealtyActivate = a.RealtyActivate,
                                    RealtyActivateID = a.RealtyActivateID.ToString(),
                                    RealtyAdvertiseOwner = a.RealtyAdvertiseOwner,
                                    RealtyAdvertiseOwnerID = a.RealtyAdvertiseOwnerID,
                                    RealtyAge = a.RealtyAge,
                                    RealtyAttributeInIDs = string.IsNullOrEmpty(a.RealtyAttributeInIDs) ? null : a.RealtyAttributeInIDs.Split(',').ToArray().ToList(),
                                    RealtyAttributeLocationIDs = string.IsNullOrEmpty(a.RealtyAttributeLocationIDs) ? null : a.RealtyAttributeLocationIDs.Split(',').ToArray().ToList(),
                                    RealtyAttributeOutIDs = string.IsNullOrEmpty(a.RealtyAttributeOutIDs) ? null : a.RealtyAttributeOutIDs.Split(',').ToArray().ToList(),
                                    RealtyBuildID = a.RealtyBuildID,
                                    RealtyBuildStateID = a.RealtyBuildStateID,
                                    RealtyCreditID = a.RealtyCreditID,
                                    RealtyFileCount = a.RealtyFileCount,
                                    RealtyFirmRealEstateOrganizationID = a.RealtyFirmRealEstateOrganizationID.ToString(),
                                    RealtyFirmTypeID = a.RealtyFirmTypeID,
                                    RealtyFirmUserID = a.RealtyFirmUserID,
                                    RealtyFlatReceivedID = a.RealtyFlatReceivedID,
                                    RealtyRegisterID = a.RealtyRegisterID,
                                    RealtyFloorCount = a.RealtyFloorCount,
                                    RealtyFloorID = a.RealtyFloorID,
                                    RealtyFuelID = a.RealtyFuelID,
                                    RealtyHeatingID = a.RealtyHeatingID,
                                    RealtyImage = a.RealtyImage,
                                    RealtyIsPopulated = a.RealtyIsPopulated,
                                    RealtyIsStudentOrSingle = a.RealtyIsStudentOrSingle,
                                    RealtyKeywords = a.RealtyKeywords,
                                    RealtyMultimediaIDs = string.IsNullOrEmpty(a.RealtyMultimediaIDs) ? null : a.RealtyMultimediaIDs.Split(',').ToArray().ToList(),
                                    RealtyProjectID = a.RealtyProjectID,
                                    RealtyPublishID = a.RealtyPublishID.ToString(),
                                    RealtyResidenceID = a.RealtyResidenceID.ToString(),
                                    RealtyStar = a.RealtyStar,
                                    RealtySubCategory = a.RealtySubCategory,
                                    RealtySubCategoryID = a.RealtySubCategoryID,
                                    RealtyTimeShareTerm = a.RealtyTimeShareTerm.ToString(),
                                    RealtyUsageID = a.RealtyUsageID,
                                    RealtyVideo = a.RealtyVideo,
                                    RealtyPlace = a.RealtyPlace,
                                    RealtyBarterID = a.RealtyBarterID,
                                    //RealtyHasVIPPacket = Convert.ToBoolean(a.RealtyHasVIPPacket)
                                    RealtyMapLatitude = a.RealtyMapLatitude.ToString(),
                                    RealtyMapLongtitude = a.RealtyMapLongtitude.ToString()
                                });

                        if (bClean && !bDelta) {
                            solr.Commit();
                        }
                    }
                    catch {
                        if (nPageSize > 1) {
                            System.Console.WriteLine(string.Format("Error in {0} decrease pagesize", nProcessed));
                            nPageSize = nPageSize / 10;
                            continue;
                        }

                        nErrorCount++;
                        nPageSize = nInitialPageSize;
                        System.Console.WriteLine(@"Pagesize set to default");
                        nProcessed++;
                        continue;
                    }

                    System.Console.WriteLine(@"{0} {1}", nProcessed, DateTime.Now);
                    nProcessed += nPageSize;
                } while (nProcessed < clsData.Count);

                solr.Commit();

                if (bOptimize) {
                    solr.Optimize();
                }

                // Setting write lastindextime
                Registry.CurrentUser.SetValue(sRegistryKey, dtStartTime);

                // Send Status Mail
                SendStatus(DateTime.Now, bDelta, clsData.Count, clsDeleted == null ? 0 : clsDeleted.Count, nErrorCount, sSolrServer);
            }

            System.Console.WriteLine(string.Format("end:{0} count:{1} error:{2}", DateTime.Now, clsData.Count, nErrorCount));
        }

        /// <summary>
        /// Default Indexer
        /// </summary>
        /// <typeparam name="T">type of solr object</typeparam>
        /// <param name="sSolrServer">solr server to be indexed</param>
        public void Index<T>(string sSolrServer) {
            var connectionx = new SolrConnection(sSolrServer) { Timeout = 600000 };
            Startup.Init<POISolrFields>(connectionx);
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<POISolrFields>>();

            if (CheckSolrStatus(sSolrServer) > 0) {
                solr.Commit();
            }

            // silinecek
            int nErrorCount = 0;
            int nProcessed = 0;
            const int nInitialPageSize = 10000;
            int nPageSize = nInitialPageSize;

            // clean
            solr.Delete(SolrQuery.All);

            using (POI clsPoi = new POI()) {
                var clsData = clsPoi.FillForSolr();

                System.Console.WriteLine(string.Format("data end:{0} count:{1}", DateTime.Now, clsData.Count));

                if (clsData.Count > 0) {
                    do {
                        try {
                            solr.Add(from a in clsData.Skip(nProcessed).Take(nPageSize)
                                     select new POISolrFields {
                                         POIID = a.POIID.ToString(),
                                         POICategoryID = a.POICategoryID ?? 0,
                                         POISubCategoryID = a.POISubCategoryID,
                                         POICityID = a.POICityID ?? 0,
                                         POICountyID = a.POICountyID ?? 0,
                                         POIDistrictID = a.POIDistrictID,
                                         POIName = a.POIName,
                                         POIAddress = a.POIAddress,
                                         POIPoints = a.POIPoints
                                     });
                        }
                        catch {
                            if (nPageSize > 1) {
                                System.Console.WriteLine(string.Format("Error in {0} decrease pagesize", nProcessed));
                                nPageSize = nPageSize / 10;
                                continue;
                            }

                            nErrorCount++;
                            nPageSize = nInitialPageSize;
                            System.Console.WriteLine(@"Pagesize set to default");
                            nProcessed++;
                            continue;
                        }

                        System.Console.WriteLine(@"{0} {1}", nProcessed, DateTime.Now);
                        nProcessed += nPageSize;
                    } while (nProcessed < clsData.Count);

                    solr.Commit();
                    solr.Optimize();
                }
            }
        }

        public void IndexProject(string sSolrServer) {
            var connectionx = new SolrConnection(sSolrServer) { Timeout = 600000 };
            Startup.Init<ProjectSolrFields>(connectionx);
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<ProjectSolrFields>>();

            // silinecek
            int nErrorCount = 0;
            int nProcessed = 0;
            const int nInitialPageSize = 10000;
            int nPageSize = nInitialPageSize;

            // clean
            solr.Delete(SolrQuery.All);

            using (Project clsProject = new Project()) {
                var clsData = clsProject.FillForProjectSolr();

                System.Console.WriteLine(string.Format("data end:{0} count:{1}", DateTime.Now, clsData.Count));

                if (clsData.Count > 0) {
                    do {
                        try {
                            solr.Add(from a in clsData.Skip(nProcessed).Take(nPageSize)
                                     select new ProjectSolrFields {
                                         ProjectID = a.ProjectID.ToString(),
                                         ProjectCategoryID = a.ProjectCategoryID ?? 0,
                                         ProjectCityID = a.ProjectCityID ?? 0,
                                         ProjectCityDescription = a.ProjectCityDescription,
                                         ProjectCountyID = a.ProjectCountyID ?? 0,
                                         ProjectCountyDescription = a.ProjectCountyDescription,
                                         ProjectDistrictID = a.ProjectDistrictID ?? 0,
                                         ProjectDistrictDescription = a.ProjectDistrictDescription,
                                         ProjectAreaIDs = a.ProjectAreaIDs.Split(',').ToList(),
                                         ProjectAreas = a.ProjectAreas.Split(',').ToList(),
                                         ProjectAddress = a.ProjectAddress,
                                         ProjectPlace = a.ProjectPlace,
                                         ProjectTitle = a.ProjectTitle,
                                         ProjectDescription = a.ProjectDescription,
                                         ProjectAbout = a.ProjectAbout,
                                         ProjectFirmID = a.ProjectFirmID ?? 0,
                                         ProjectFirmLogo = a.ProjectFirmLogo,
                                         ProjectFirmName = a.ProjectFirmName,
                                         ProjectBanner = a.ProjectBanner,
                                         ProjectCompletionDateTime = a.ProjectCompletionDateTime,
                                         ProjectHasPacket = Convert.ToBoolean(a.ProjectHasPacket),
                                         ProjectRealtyPriceTL = !string.IsNullOrEmpty(a.ProjectRealtyPriceTL) ? a.ProjectRealtyPriceTL.Split(',').Where(s => !string.IsNullOrEmpty(s)).Select(s => (decimal?)Convert.ToDecimal(s)).ToList() : (List<decimal?>)null,
                                         ProjectRealtyPriceCurrencyID = !string.IsNullOrEmpty(a.ProjectRealtyPriceCurrencyID) ? Convert.ToInt32(a.ProjectRealtyPriceCurrencyID) : (int?)null,
                                         ProjectRoom = !string.IsNullOrEmpty(a.ProjectRoom) ? a.ProjectRoom.Split(',').ToList() : null,
                                         ProjectSqm = !string.IsNullOrEmpty(a.ProjectSqm) ? a.ProjectSqm.Split(',').Select(s => Convert.ToInt32(s)).ToList() : null,
                                         ProjectPoints = a.ProjectPoints
                                     });
                        }
                        catch {
                            if (nPageSize > 1) {
                                System.Console.WriteLine(string.Format("Error in {0} decrease pagesize", nProcessed));
                                nPageSize = nPageSize / 10;
                                continue;
                            }

                            nErrorCount++;
                            nPageSize = nInitialPageSize;
                            System.Console.WriteLine(@"Pagesize set to default");
                            nProcessed++;
                            continue;
                        }

                        System.Console.WriteLine(@"{0} {1}", nProcessed, DateTime.Now);
                        nProcessed += nPageSize;
                    } while (nProcessed < clsData.Count);

                    solr.Commit();
                    solr.Optimize();
                }
            }
        }

        /// <summary>
        ///     <description>Get Exception Message</description>
        ///     <author>Ahmet Esmer</author>
        ///     <createDate>12.02.2013</createDate>
        /// </summary>
        /// <param name="sExceptionMessage">sExceptionMessage for checking</param>
        /// <returns>null or string value</returns>
        /// <updates>
        ///     <update>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </update>
        /// </updates>
        /// <reviews>
        ///     <review>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </review>
        /// </reviews>
        public static string GetMessageTitle(string sExceptionMessage) {
            string sMessage = string.Empty;
            try {
                sMessage = Helper.Helper.ExtractString(sExceptionMessage, "<h1>", "</h1>");
            }
            catch {
            }
            return sMessage;
        }



        /// <summary>
        ///     <description>IndexProjectVip method</description>
        ///     <author>Fatih GÜNEŞ</author>
        ///     <createDate>01.06.2012</createDate>
        /// </summary>
        /// <param name="sSolrServer">s Solr Server</param>
        /// <updates>
        ///     <update>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </update>
        /// </updates>
        /// <reviews>
        ///     <review>
        ///         <description></description>    
        ///         <author></author>
        ///         <date></date>
        ///     </review>
        /// </reviews>
        public void IndexProjectVip(string sSolrServer) {
            var connectionx = new SolrConnection(sSolrServer) { Timeout = 600000 };
            Startup.Init<ProjectSolrFields>(connectionx);
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<ProjectSolrFields>>();

            // silinecek
            int nErrorCount = 0;
            int nProcessed = 0;
            const int nInitialPageSize = 1000;
            int nPageSize = nInitialPageSize;

            // clean
            solr.Delete(SolrQuery.All);

            using (Project clsProject = new Project()) {
                var clsData = clsProject.FillProjectSolrVip();

                System.Console.WriteLine(string.Format("data end:{0} count:{1}", DateTime.Now, clsData.Count));

                if (clsData.Count > 0) {
                    do {
                        try {
                            solr.Add(from a in clsData.Skip(nProcessed).Take(nPageSize)
                                     select new ProjectSolrFields {
                                         ProjectID = a.ProjectID.ToString(),
                                         ProjectCategoryID = a.ProjectCategoryID ?? 0,
                                         ProjectCityID = a.ProjectCityID ?? 0,
                                         ProjectCityDescription = a.ProjectCityDescription,
                                         ProjectCountyID = a.ProjectCountyID ?? 0,
                                         ProjectCountyDescription = a.ProjectCountyDescription,
                                         ProjectDistrictID = a.ProjectDistrictID ?? 0,
                                         ProjectDistrictDescription = a.ProjectDistrictDescription,
                                         ProjectAreaIDs = a.ProjectAreaIDs.Split(',').ToList(),
                                         ProjectAreas = a.ProjectAreas.Split(',').ToList(),
                                         ProjectAddress = a.ProjectAddress,
                                         ProjectPlace = a.ProjectPlace,
                                         ProjectTitle = a.ProjectTitle,
                                         ProjectDescription = a.ProjectDescription,
                                         ProjectAbout = a.ProjectAbout,
                                         ProjectFirmID = a.ProjectFirmID ?? 0,
                                         ProjectFirmLogo = a.ProjectFirmLogo,
                                         ProjectFirmName = a.ProjectFirmName,
                                         ProjectBanner = a.ProjectBanner,
                                         ProjectCompletionDateTime = a.ProjectCompletionDateTime,
                                         ProjectHasPacket = Convert.ToBoolean(a.ProjectHasPacket),
                                         ProjectRealtyPriceTL = !string.IsNullOrEmpty(a.ProjectRealtyPriceTL) ? a.ProjectRealtyPriceTL.Split(',').Where(s => !string.IsNullOrEmpty(s)).Select(s => (decimal?)Convert.ToDecimal(s)).ToList() : (List<decimal?>)null,
                                         ProjectRealtyPriceCurrencyID = !string.IsNullOrEmpty(a.ProjectRealtyPriceCurrencyID) ? Convert.ToInt32(a.ProjectRealtyPriceCurrencyID) : (int?)null,
                                         ProjectRoom = !string.IsNullOrEmpty(a.ProjectRoom) ? a.ProjectRoom.Split(',').ToList() : null,
                                         ProjectSqm = !string.IsNullOrEmpty(a.ProjectSqm) ? a.ProjectSqm.Split(',').Select(s => Convert.ToInt32(s)).ToList() : null,
                                         ProjectPoints = a.ProjectPoints
                                     });
                        }
                        catch {
                            if (nPageSize > 1) {
                                System.Console.WriteLine(string.Format("Error in {0} decrease pagesize", nProcessed));
                                nPageSize = nPageSize / 10;
                                continue;
                            }

                            nErrorCount++;
                            nPageSize = nInitialPageSize;
                            System.Console.WriteLine(@"Pagesize set to default");
                            nProcessed++;
                            continue;
                        }

                        System.Console.WriteLine(@"{0} {1}", nProcessed, DateTime.Now);
                        nProcessed += nPageSize;
                    } while (nProcessed < clsData.Count);

                    solr.Commit();
                    solr.Optimize();
                }
            }
        }

        /// <summary>
        /// Send Status mail of import
        /// </summary>
        /// <param name="dtStatus">Index time</param>
        /// <param name="bDelta">Is Delta</param>
        /// <param name="nCOunt">Total count</param>
        /// <param name="nDeleted">how many deleted</param>
        /// <param name="nError">how many error</param>
        /// <param name="sSolrServer">solr server to be indexed</param>
        protected void SendStatus(DateTime dtStatus, bool bDelta, int nCOunt, int nDeleted, int nError, string sSolrServer) {
            string sBody =
                string.Format(
                @"Server:{5}<br>
                Solr Status<Br>
                SentDate:{0}<br>
                Count:{1}<br>
                Deleted:{2}<br>
                Error:{3}<br>
                Delta:{4}<br>",
                dtStatus,
                nCOunt,
                nDeleted,
                nError,
                bDelta,
                sSolrServer);
            using (eMail clsMail = new eMail(ConfigurationManager.AppSettings["emailServer"])) {
                clsMail.SendMail(ConfigurationManager.AppSettings["emailFrom"], "nkubilay@hurriyetemlak.com;bbickici@hurriyetemlak.com;ssendere@hurriyet.com.tr", string.Empty, string.Empty, "Solr", sBody, string.Empty, ConfigurationManager.AppSettings["emailDisplayName"]);
            }


            /*    string sServerName = sSolrServer.Replace("http://", "");
                sServerName = sServerName.Substring(0, sServerName.IndexOf('/'));

                string sMessage = string.Format("\"Response\": \"{0}\", \"ServerIp\":\"{1}\"");

                StreamWriter streamWriter = new StreamWriter(string.Format(@"\\ha-emlak-fs\SystemCheck\{0}.log", sServerName.Replace(".", string.Empty)));
                streamWriter.WriteLine();
                streamWriter.Close();*/
        }

        /// <summary>
        /// Send Error Message
        /// </summary>
        /// <param name="dtStatus">Index Time</param>
        /// <param name="sError">Error Message</param>
        protected void SendError(DateTime dtStatus, string sError) {
            string sBody =
                string.Format(
                @"Solr Status<Br>
                SentDate:{0}<br>
                Error:{1}<br>",
                dtStatus,
                sError);
            using (eMail clsMail = new eMail(ConfigurationManager.AppSettings["emailServer"])) {
                clsMail.SendMail(ConfigurationManager.AppSettings["emailFrom"], "nkubilay@hurriyetemlak.com;bbickici@hurriyetemlak.com;ssendere@hurriyet.com.tr", string.Empty, string.Empty, "Solr", sBody, string.Empty, ConfigurationManager.AppSettings["emailDisplayName"]);
            }
        }

        /// <summary>
        /// Checking solr status for unfinished index
        /// </summary>
        /// <param name="sSolrServer">Solr Server</param>
        /// <returns>returns >0 if pending docs exists</returns>
        protected int CheckSolrStatus(string sSolrServer) {
            string sStatUri = string.Format("{0}{1}{2}", sSolrServer, sSolrServer[sSolrServer.Length - 1] == '/' ? string.Empty : "/", "admin/stats?stats=true&cat=UPDATEHANDLER");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Request(sStatUri, string.Empty));

            int nPendingDocs = int.Parse(xmlDocument.SelectSingleNode("response/lst[@name='solr-mbeans']/lst[@name='UPDATEHANDLER']/lst[@name='updateHandler']/lst[@name='stats']/long[@name='docsPending']").InnerText);
            int nPendingAdds = int.Parse(xmlDocument.SelectSingleNode("response/lst[@name='solr-mbeans']/lst[@name='UPDATEHANDLER']/lst[@name='updateHandler']/lst[@name='stats']/long[@name='adds']").InnerText);
            int nPendingDeletesById = int.Parse(xmlDocument.SelectSingleNode("response/lst[@name='solr-mbeans']/lst[@name='UPDATEHANDLER']/lst[@name='updateHandler']/lst[@name='stats']/long[@name='deletesById']").InnerText);
            int nPendingDeletesByQuery = int.Parse(xmlDocument.SelectSingleNode("response/lst[@name='solr-mbeans']/lst[@name='UPDATEHANDLER']/lst[@name='updateHandler']/lst[@name='stats']/long[@name='deletesByQuery']").InnerText);

            return nPendingDocs + nPendingAdds + nPendingDeletesById + nPendingDeletesByQuery;
        }

        /// <summary>
        /// Post a request to solr
        /// </summary>
        /// <param name="requestUri">uri for the request</param>
        /// <param name="postContents">request content data to post</param>
        /// <returns>solr response</returns>
        private string Request(string requestUri, string postContents) {
            string response = string.Empty;
            try {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUri);

                //do not delete!!!! read comment on region WINCLIENTPORTS
                //req.ServicePoint.BindIPEndPointDelegate = new BindIPEndPoint(BindIPEndPointCallback);

                // send post content if exists
                if (!string.IsNullOrEmpty(postContents)) {
                    req.Method = WebRequestMethods.Http.Post;
                    req.ContentType = "text/xml";
                    req.Headers.Add(HttpRequestHeader.ContentEncoding, "UTF-8");
                    req.Headers.Add(HttpRequestHeader.CacheControl, "no-cache, must-revalidate");
                    StreamWriter writer = new StreamWriter(req.GetRequestStream());
                    writer.WriteLine(postContents);
                    writer.Close();
                }

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                StreamReader read = new StreamReader(resp.GetResponseStream());
                response = read.ReadToEnd();
                resp.Close();
                return response;
            }
            catch (WebException e) {
                throw;
            }
            catch (Exception e) {
                throw;
            }

            // return response;
        }
}
