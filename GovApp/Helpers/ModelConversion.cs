using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Helpers
{
    public class ModelConversion
    {
        internal static List<Status> ConvertStatus(Affluenze affluenze)
        {
            List<Status> statuses = new List<Status>();
            Status status1 = new Status();
            status1.id = "CO1";
            status1.nome = "1 Costituzione";
            status1.pervenuto = affluenze.Costituzione1 == 1 ? true : false;
            statuses.Add(status1);      
            Status status2 = new Status();
            status2.id = "CO2";
            status2.nome = "2 Costituzione";
            status2.pervenuto = affluenze.Costituzione2 == 1 ? true : false;
            statuses.Add(status2);
            Status status3 = new Status();
            status3.id = "AP1";
            status3.nome = "1 Apertuta";
            status3.pervenuto = affluenze.Apertura1 == 1 ? true : false;
            statuses.Add(status3);
            Status status4 = new Status();
            status4.id = "AP2";
            status4.nome = "2 Apertura";
            status4.pervenuto = affluenze.Apertura2 == 1 ? true : false;
            statuses.Add(status4);
            Status status5 = new Status();
            status5.id = "AP3";
            status5.nome = "3 Apertura";
            status5.pervenuto = affluenze.Apertura3 == 1 ? true : false;
            statuses.Add(status5);
            Status status6 = new Status();
            status6.id = "AF1";
            status6.nome = "1 Affluenza";
            status6.pervenuto = affluenze.Affluenza1 == 1 ? true : false;
            statuses.Add(status6);
            Status status7 = new Status();
            status7.id = "AF2";
            status7.nome = "2 Affluenza";
            status7.pervenuto = affluenze.Affluenza2 == 1 ? true : false;
            statuses.Add(status7);
            Status status8 = new Status();
            status8.id = "AF3";
            status8.nome = "3 Affluenza";
            status8.pervenuto = affluenze.Affluenza3 == 1 ? true : false;
            statuses.Add(status8);
            Status status9 = new Status();
            status9.id = "AF4";
            status9.nome = "4 Affluenza";
            status9.pervenuto = affluenze.Affluenza4 == 1 ? true : false;
            statuses.Add(status9);
            Status status10 = new Status();
            status10.id = "AF5";
            status10.nome = "5 Affluenza";
            status10.pervenuto = affluenze.Affluenza5 == 1 ? true : false;
            statuses.Add(status10);
            return statuses;

        }

       
    }
}
