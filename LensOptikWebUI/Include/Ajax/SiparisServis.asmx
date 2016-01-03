<%@ WebService Language="C#" Class="SiparisServis" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]


public class SiparisServis  : WebService 
{

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    
}