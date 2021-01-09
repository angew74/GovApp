using Elezioni.Contracts;
using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using Gov.Structure.Contracts.Elezioni;
using Gov.Structure.Contracts.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Helpers
{
    public class VotiLoader : IVotiLoader
    {
        private readonly IVotiGeneraliService _votiGeneraliService;
        private readonly ISezioneService _sezioneService;
        private readonly IVotiListaService _votiListaService;
        private readonly IVotiSindacoService _votiSindacoService;
        private readonly IAffluenzaService _affluenzaService;

        public VotiLoader(IVotiGeneraliService votiGeneraliService, IAffluenzaService affluenzaService, ISezioneService sezioneService, IVotiListaService votiListaService, IVotiSindacoService votiSindacoService)
        {
            _votiGeneraliService = votiGeneraliService;
            _affluenzaService = affluenzaService;
            _sezioneService = sezioneService;
            _votiListaService = votiListaService;
            _votiSindacoService = votiSindacoService;
        }

        public List<VotiListaModel> ConvertToJson(List<VotiLista> l, int sezione, string tipo)
        {
            List<VotiListaModel> listaJsons = new List<VotiListaModel>();
            VotiModel json = new VotiModel();
            foreach (VotiLista v in l)
            {
                VotiListaModel j = new VotiListaModel();
                j.Id = v.Id;
                j.Denominazione = v.Lista.Denominazione;
                j.Progressivo = v.Lista.Progressivo;
                j.Voti = v.Voti;
                j.numeroSezione = sezione;
                j.Tipo =tipo;
                j.idLista = v.Listaid;
                listaJsons.Add(j);
            }
            return listaJsons;
        }


       
        public List<VotiLista> prepareVoti(List<VotiListaModel> list,string user,int tipoElezione)
        {
            List<VotiLista> votiList = new List<VotiLista>();                 
            Sezioni sezione = _sezioneService.findByNumerosezioneAndTipoelezioneId(list[0].numeroSezione, tipoElezione);
            foreach (VotiListaModel l in list)
            {
                VotiLista v = new VotiLista();      
                v.Sezioneid = sezione.Id;
                v.Tipoelezioneid = tipoElezione;
                v.Listaid = l.idLista;
                votiList.Add(v);
            }
            return votiList;
        }

        public List<VotiLista> prepareVotiR(List<VotiListaModel> list, string user)
        {
            List<VotiLista> votiList = new  List<VotiLista>();
            DateTime oggi = DateTime.Now;            
            foreach (VotiListaModel l in list)
            {
                VotiLista v = _votiListaService.findById((int)l.Id);              
                v.Voti = l.Voti;               
                votiList.Add(v);
            }
            return votiList;
        }


        public List<VotiSindacoModel> ConvertToJsonSindaci(List<VotiSindaco> l, int sezione, string tipo, int tipoelezioneid)
        {
            List<VotiSindacoModel> sindaciJsons = new List<VotiSindacoModel>();
            VotiModel json = new VotiModel();           
            VotiGenerali v = _votiGeneraliService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, tipoelezioneid);
            int votanti =(int)_affluenzaService.findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza3(sezione, tipoelezioneid, 1).Votantitotali3;
            json.Bianche = v.Bianche.ToString();
            json.Nulle=v.Nulle.ToString();
            json.Contestate = v.Contestate.ToString();
            json.soloSindaco=v.SoloSindaco.ToString();
            json.Totale=v.Totale.ToString();
            json.totaleValide=v.TotaleValide.ToString();
            json.Votanti=votanti.ToString();
            json.Id=v.Id;
            int totaleListe =(int) (v.TotaleValide - v.SoloSindaco);
            json.valideListe = totaleListe.ToString();
            foreach (VotiSindaco vs in l)
            {
                VotiSindacoModel j = new VotiSindacoModel();
                j.Id = vs.Sindaco.Id.ToString();            
                j.Progressivo = vs.Sindaco.Progressivo.ToString();
                j.Totale = vs.Votigenerali.Totale.ToString();            
                j.Cognome = vs.Sindaco.Cognome.ToString();
                j.Nome = vs.Sindaco.Nome;            
                j.Progressivo = vs.Sindaco.Progressivo.ToString();             
                j.IsCoalizione = "S";
                j.SoloSindaco = vs.NumeroVotiSoloSindaco.ToString();
                j.NumeroSezione = sezione.ToString();
                j.Tipo = tipo;
                j.IdSindaco = vs.Sindaco.Id.ToString();
                List<VotiListaModel> votiListaModels = new List<VotiListaModel>();
                foreach(var vl in vs.VotiLista)
                {
                    VotiListaModel votiLista = new VotiListaModel();
                    votiLista.Denominazione = vl.Lista.Denominazione;
                    votiLista.Id = vl.Id;
                    votiLista.idLista = vl.Lista.Id;
                    votiLista.idSindaco = vs.Sindaco.Id;
                    votiLista.numeroSezione = sezione;
                    votiLista.Progressivo = vl.Lista.Progressivo;
                    votiLista.Tipo = tipo;
                    votiLista.Voti = vl.Voti;
                    votiListaModels.Add(votiLista);
                }
                j.Liste = votiListaModels;
                sindaciJsons.Add(j);
            }            
            return sindaciJsons;
        }

        public List<VotiSindacoModel> ConvertToJsonSindaciEmpty(List<Sindaci> l, string sezione, string tipo)
        {
            List<VotiSindacoModel> sindaciJsons = new List<VotiSindacoModel>();            
            foreach (Sindaci vs in l)
            {
                VotiSindacoModel j = new VotiSindacoModel();               
                j.Progressivo = vs.Progressivo.ToString();            
                j.Cognome = vs.Cognome.ToString();
                j.Nome = vs.Nome;
                j.Progressivo = vs.Progressivo.ToString();
                j.IsCoalizione = "N";           
                j.NumeroSezione = sezione.ToString();
                j.Tipo = tipo;
                j.soloListe = "";
                j.IdSindaco = vs.Id.ToString();
                List<VotiListaModel> listaModels = new List<VotiListaModel>();
                foreach(var lista in vs.Liste)
                {
                    VotiListaModel jl = new VotiListaModel();
                    jl.Denominazione = lista.Denominazione;
                    jl.Progressivo = lista.Progressivo;
                    jl.numeroSezione = int.Parse(sezione);
                    jl.Tipo = tipo;
                    jl.idLista = lista.Id;
                    listaModels.Add(jl);
                }
                j.Liste = listaModels;
                sindaciJsons.Add(j);
            }
            return sindaciJsons;
        }

        public List<VotiSindaco> prepareVotiSindaco(List<VotiSindacoModel> list, string user,int tipoelezioneid)
        {
            List<VotiSindaco> votiList = new List<VotiSindaco>();          
            VotiGenerali vg = _votiGeneraliService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(list[0].NumeroSezione), tipoelezioneid);
            foreach (VotiSindacoModel l in list)
            {
                VotiSindaco v = new VotiSindaco();                
                v.NumeroVoti =int.Parse(l.Totale);
                v.NumeroVotiSoloSindaco = int.Parse(l.SoloSindaco);               
                v.Sezioneid=int.Parse(l.NumeroSezione);
                v.Tipoelezioneid = tipoelezioneid;           
                v.Votigenerali = vg;
                votiList.Add(v);
            }
            return votiList;
        }

        public List<VotiSindaco> prepareVotiSindacoR(List<VotiSindacoModel> list, VotiGenerali vg, string user, int tipoelezioneid)
        {
            List<VotiSindaco> votiList = new List<VotiSindaco>();          
            foreach (VotiSindacoModel l in list)
            {
                VotiSindaco v = _votiSindacoService.findBySindacoIdAndSezioneNumerosezioneAndTipoelezioneId(int.Parse(l.Id),int.Parse(l.NumeroSezione), tipoelezioneid);              
                v.NumeroVoti = int.Parse(l.Totale);
                v.NumeroVotiSoloSindaco = int.Parse(l.SoloSindaco);                
                v.Votigenerali = vg;
                votiList.Add(v);
            }
            return votiList;
        }

        public VotiGenerali prepareVotiG(VotiModel form,string user,int tipoelezioneid)
        {
           
            Sezioni sezione = _sezioneService.findByNumerosezioneAndTipoelezioneId(int.Parse(form.Sindaci[0].NumeroSezione), tipoelezioneid);
            int sezioneid = _sezioneService.findByNumerosezioneAndTipoelezioneId(int.Parse(form.NumeroSezione), tipoelezioneid).Id;
            VotiGenerali v = new VotiGenerali();
            v.Bianche = int.Parse(form.Bianche);
            v.Contestate = int.Parse(form.Contestate);            
            v.Nulle = int.Parse(form.Nulle);
            v.Sezioneid = sezioneid;
            v.SoloSindaco = int.Parse(form.soloSindaco);
            v.Totale = int.Parse(form.Totale);
            v.TotaleValide = int.Parse(form.totaleValide);
            v.Tipoelezioneid = tipoelezioneid;
          //   v.Municipio = uint.Parse(form.);
            return v;
        }

        public VotiGenerali prepareVotiGR(VotiModel form,string user,int tipoelezioneid)
        {

            DateTime oggi = DateTime.Now;
            VotiGenerali v = _votiGeneraliService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(form.Sindaci[0].NumeroSezione), tipoelezioneid);
            v.Bianche = int.Parse(form.Bianche);
            v.Contestate = int.Parse(form.Contestate);            
            v.Nulle = int.Parse(form.Nulle);
            v.SoloSindaco = int.Parse(form.soloSindaco);
            v.Totale = int.Parse(form.Totale);
            v.TotaleValide = int.Parse(form.totaleValide);
            return v;
        }

        public List<VotiLista> prepareVotiLista(List<VotiSindacoModel> sindaci, int tipoelezioneid, string user, List<VotiListaModel> l)
        {
            List<VotiLista> listVoti = new List<VotiLista>();
            DateTime oggi = DateTime.Now;
            int sezioneid = _sezioneService.findByNumerosezioneAndTipoelezioneId(int.Parse(sindaci[0].NumeroSezione), tipoelezioneid).Id;
            foreach (VotiSindacoModel s in sindaci)
            {
               
                foreach (VotiListaModel u in l)
                {
                    VotiLista v = new VotiLista();                  
                    v.Voti = u.Voti;
                    v.Sezioneid = sezioneid;
                    v.Tipoelezioneid = tipoelezioneid;
                    int listaid = l[0].Id;
                    v.Listaid= listaid;
                    listVoti.Add(v);
                }
            }
            return listVoti;
        }

            public List<VotiLista> prepareVotiListaR(List<VotiSindacoModel> sindaci, int tipoelezioneid,string user, List<VotiListaModel> l)
        {
            List<VotiLista> listVoti = new List<VotiLista>();
            DateTime oggi = DateTime.Now;
            VotiLista v = new VotiLista();          
            foreach (VotiSindacoModel s in sindaci)
            {
              
                VotiGenerali vg = _votiGeneraliService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(sindaci[0].NumeroSezione), tipoelezioneid);
                // todo verificare aggiornamento parziale
                if (l == null)
                {
                    
                }
                else
                {
                    v = _votiListaService.findById((int)l[0].Id);                  
                    v.Voti = l[0].Voti;
                    v.Votigeneraliid = vg.Id;
                    listVoti.Add(v);
                }
            }
            return listVoti;
        }

    }
}
