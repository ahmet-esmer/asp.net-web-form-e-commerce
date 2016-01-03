<%@ WebService Language="C#"  Class="CascadingSehirler" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DataAccessLayer;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class CascadingSehirler : System.Web.Services.WebService
{
    public static DataTable dt;

    [WebMethod()]
    public CascadingDropDownNameValue[] sehirler(string knownCategoryValues, string category)
    {
        
        DataTable dt = SehirDB.Sehirler();
        
        List<CascadingDropDownNameValue> liste = new List<CascadingDropDownNameValue>();

        foreach (DataRow dataRow in dt.Rows)
        {
            liste.Add(new CascadingDropDownNameValue(dataRow["Ad"].ToString(), dataRow["IlID"].ToString()));
        }
        return liste.ToArray();
    }



    [WebMethod()]
    public CascadingDropDownNameValue[] ilceler(string knownCategoryValues, string category)
    {
        StringDictionary gelen = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
        int gelenID = int.Parse(gelen["sehir"]);

        DataTable dt = SehirDB.Ilceler(gelenID);
        
        List<CascadingDropDownNameValue> liste = new List<CascadingDropDownNameValue>();

        foreach (DataRow dataRow in dt.Rows)
        {
            liste.Add(new CascadingDropDownNameValue(dataRow["Ad"].ToString(), dataRow["IlceID"].ToString()));
        }
        return liste.ToArray();
    }

    //[WebMethod()]
    //public CascadingDropDownNameValue[] semtler(string knownCategoryValues, string category)
    //{
    //    StringDictionary gelen = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
    //    int gelenID = int.Parse(gelen["ilce"]);
    //    DataTable dt = new DataTable();
    //    dt = DatabaseIslemleri.semtListele(Convert.ToInt32(gelenID));

    //    List<CascadingDropDownNameValue> liste = new List<CascadingDropDownNameValue>();

    //    foreach (DataRow dataRow in dt.Rows)
    //    {
    //        liste.Add(new CascadingDropDownNameValue(dataRow["Ad"].ToString(), dataRow["SemtID"].ToString()));
    //    }
    //    return liste.ToArray();
    //}



}