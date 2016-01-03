using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ModelLayer
{
    public enum SiparisDurum
    {
        OnayBekliyor = 0,
        SiparisHazirlaniyor = 1,
        KargoyaVerildi = 2,
        IptalEdildi = 3,
        OdemeOnayiBekliyor = 4,
        TedarikEdilemedi = 5,
        IptalEdilecek = 6,
        TedarikSurecinde = 7,
        TedarikEdilemiyor = 8
    }
}