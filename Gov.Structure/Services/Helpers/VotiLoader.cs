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

        public MunicipioModel ConvertToJsonLista(List<VotiLista> l, VotiGenerali votiGenerali)
        {
            List<VotiListaModel> listaJsons = new List<VotiListaModel>();
            MunicipioModel municipioModel = new MunicipioModel();
            DatiModel json = new DatiModel
            {
                Bianche = votiGenerali.Bianche.ToString(),
                Contestate = votiGenerali.Contestate.ToString(),
                Nulle = votiGenerali.Nulle.ToString(),
                totaleValide = votiGenerali.TotaleValide.ToString(),
                soloSindaco = votiGenerali.SoloSindaco.ToString(),
                valideListe = (votiGenerali.TotaleValide - votiGenerali.SoloSindaco).ToString(),
                NumeroSezione = votiGenerali.Sezione.Numerosezione.ToString(),
                Totale = votiGenerali.Totale.ToString(),
                Votanti = votiGenerali.Totale.ToString(),
                Tipo = "Lista sezione numero " + votiGenerali.Sezione.Numerosezione.ToString(),
                Iscritti = votiGenerali.Sezione.Iscritti.Iscrittitotaligen.ToString()
            };
            foreach (VotiLista v in l)
            {
                VotiListaModel j = new VotiListaModel();
                j.Id = v.Id;
                j.Denominazione = v.Lista.Denominazione;
                j.Progressivo = v.Lista.Progressivo;
                j.votiLista = v.Voti.ToString();
                j.numeroSezione = votiGenerali.Sezione.Numerosezione;
                j.idLista = v.Listaid;
                listaJsons.Add(j);
            }
            json.Liste = listaJsons;
            municipioModel.Municipio = "99";
            municipioModel.dati = json;
            return municipioModel;
        }


        public List<VotiLista> prepareVoti(List<VotiListaModel> list, string user, int tipoElezione)
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
            List<VotiLista> votiList = new List<VotiLista>();
            DateTime oggi = DateTime.Now;
            foreach (VotiListaModel l in list)
            {
                VotiLista v = _votiListaService.findById((int)l.Id);
                v.Voti = int.Parse(l.votiLista);
                votiList.Add(v);
            }
            return votiList;
        }

        public List<VotiSindacoModel> ConvertToJsonSindaci(List<VotiSindaco> l, int sezione, string tipo, int tipoelezioneid)
        {
            List<VotiSindacoModel> sindaciJsons = new List<VotiSindacoModel>();
            VotiModel json = new VotiModel();
            VotiGenerali v = _votiGeneraliService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, tipoelezioneid);
            int votanti = (int)_affluenzaService.findBySezioneNumerosezioneAndSezioneTipoelezioneIdAndAffluenza3(sezione, tipoelezioneid, 1).Votantitotali3;
            json.Bianche = v.Bianche.ToString();
            json.Nulle = v.Nulle.ToString();
            json.Contestate = v.Contestate.ToString();
            json.soloSindaco = v.SoloSindaco.ToString();
            json.Totale = v.Totale.ToString();
            json.totaleValide = v.TotaleValide.ToString();
            json.Votanti = votanti.ToString();
            json.Id = v.Id;
            int totaleListe = (int)(v.TotaleValide - v.SoloSindaco);
            json.valideListe = totaleListe.ToString();
            foreach (VotiSindaco vs in l)
            {
                VotiSindacoModel j = new VotiSindacoModel();
                j.Id = vs.Sindaco.Id.ToString();
                j.Progressivo = vs.Sindaco.Progressivo.ToString();
                j.totaleSindaco = vs.Votigenerali.Totale.ToString();
                j.Cognome = vs.Sindaco.Cognome.ToString();
                j.Nome = vs.Sindaco.Nome;
                j.Progressivo = vs.Sindaco.Progressivo.ToString();
                j.IsCoalizione = "S";
                j.SoloSindaco = vs.NumeroVotiSoloSindaco.ToString();
                j.NumeroSezione = sezione.ToString();
                j.Tipo = tipo;
                j.IdSindaco = vs.Sindaco.Id.ToString();
                List<VotiListaModel> votiListaModels = new List<VotiListaModel>();
                foreach (var vl in vs.VotiLista)
                {
                    VotiListaModel votiLista = new VotiListaModel();
                    votiLista.Denominazione = vl.Lista.Denominazione;
                    votiLista.Id = vl.Id;
                    votiLista.idLista = vl.Lista.Id;
                    votiLista.idSindaco = vs.Sindaco.Id;
                    votiLista.numeroSezione = sezione;
                    votiLista.Progressivo = vl.Lista.Progressivo;
                    votiLista.Tipo = tipo;
                    votiLista.votiLista = vl.Voti.ToString();
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
                foreach (var lista in vs.Liste)
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

        public List<VotiSindaco> prepareVotiSindaco(List<VotiSindacoModel> list, string user, int tipoelezioneid)
        {
            List<VotiSindaco> votiList = new List<VotiSindaco>();
            VotiGenerali vg = _votiGeneraliService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(list[0].NumeroSezione), tipoelezioneid);
            foreach (VotiSindacoModel l in list)
            {
                VotiSindaco v = new VotiSindaco();
                v.NumeroVoti = int.Parse(l.totaleSindaco);
                v.NumeroVotiSoloSindaco = int.Parse(l.SoloSindaco);
                v.Sezioneid = int.Parse(l.NumeroSezione);
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
                VotiSindaco v = _votiSindacoService.findBySindacoIdAndSezioneNumerosezioneAndTipoelezioneId(int.Parse(l.Id), int.Parse(l.NumeroSezione), tipoelezioneid);
                v.NumeroVoti = int.Parse(l.totaleSindaco);
                v.NumeroVotiSoloSindaco = int.Parse(l.SoloSindaco);
                v.Votigenerali = vg;
                votiList.Add(v);
            }
            return votiList;
        }
        public VotiGenerali prepareVotiG(VotiModel form, int tipoelezioneid)
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

        public VotiGenerali prepareVotiGR(VotiModel form, string user, int tipoelezioneid)
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

        public List<VotiSindaco> prepareVoti(VotiModel form, int tipoelezioneid)
        {
            List<VotiLista> votiListe = new List<VotiLista>();
            List<VotiSindaco> votiSindaci = new List<VotiSindaco>();
            var sindaci = form.Sindaci;
            int sezioneid = _sezioneService.findByNumerosezioneAndTipoelezioneId(int.Parse(form.NumeroSezione), tipoelezioneid).Id;
            VotiGenerali v = new VotiGenerali();
            v.Bianche = int.Parse(form.Bianche);
            v.Contestate = int.Parse(form.Contestate);
            v.Nulle = int.Parse(form.Nulle);
            v.Sezioneid = sezioneid;
            v.SoloSindaco = int.Parse(form.soloSindaco);
            v.Totale = int.Parse(form.Totale);
            v.Municipio = int.Parse(form.Municipio);
            v.TotaleValide = int.Parse(form.totaleValide);
            v.Tipoelezioneid = tipoelezioneid;
            v.Iscritti = int.Parse(form.Iscritti);
            foreach (VotiSindacoModel s in sindaci)
            {
                VotiSindaco votiSindaco = new VotiSindaco();
                votiSindaco.NumeroVoti = int.Parse(s.totaleSindaco);
                votiSindaco.NumeroVotiSoloSindaco = int.Parse(s.SoloSindaco);
                votiSindaco.Sezioneid = sezioneid;
                votiSindaco.Tipoelezioneid = tipoelezioneid;
                votiSindaco.Sindacoid = int.Parse(s.IdSindaco);
                votiSindaco.Municipio = int.Parse(form.Municipio);
                votiSindaco.Votigenerali = v;
                foreach (VotiListaModel u in s.Liste)
                {
                    VotiLista votiLista = new VotiLista();
                    votiLista.Voti = int.Parse(u.votiLista);
                    votiLista.Sezioneid = sezioneid;
                    votiLista.Tipoelezioneid = tipoelezioneid;
                    int listaid = u.idLista;
                    votiLista.Listaid = listaid;
                    votiLista.Municipio = int.Parse(form.Municipio);
                    votiLista.Votigenerali = v;
                    votiListe.Add(votiLista);
                }
                votiSindaco.VotiLista = votiListe;
                votiSindaci.Add(votiSindaco);
            }
            return votiSindaci;
        }
        public List<VotiLista> prepareVotiListaR(List<VotiSindacoModel> sindaci, int tipoelezioneid, string user, List<VotiListaModel> l)
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
                    v.Voti = int.Parse(l[0].votiLista);
                    v.Votigeneraliid = vg.Id;
                    listVoti.Add(v);
                }
            }
            return listVoti;
        }

        public MunicipioModel ConvertToJsonListaMunicipio(List<RicalcoloVotiLista> listas)
        {
            List<VotiListaModel> listaJsons = new List<VotiListaModel>();          
            MunicipioModel municipio = new MunicipioModel();
            string tipo = "";
            if(listas.FirstOrDefault().Municipio != 99)
            {
                tipo = "Lista Municipio " + listas.FirstOrDefault().Municipio.ToString();
            }
            else { tipo = "Lista tutto il Comune"; }
            DatiModel json = new DatiModel
            {
                Tipo = tipo,    
                Totale = listas.FirstOrDefault().VotantiPervenute.ToString(),
                municipio = listas.FirstOrDefault().Municipio.ToString(),
                Votanti = listas.FirstOrDefault().VotantiPervenute.ToString(),
                Iscritti = listas.FirstOrDefault().IscrittiPervenute.ToString(),
                SezioniPervenute = listas.FirstOrDefault().NumeroSezioni.ToString(),
                PercentualeVotanti = listas.FirstOrDefault().PercentualeVotantiPervenute.ToString()
            };          
            municipio.Municipio = listas.FirstOrDefault().Municipio.ToString();            
            foreach (RicalcoloVotiLista v in listas)
            {
                VotiListaModel j = new VotiListaModel();
                j.Id = v.Id;
                j.Denominazione = v.IdlistaNavigation.Denominazione;
                j.votiLista = v.NumeroVoti.ToString();
                j.perecentualeLista = CalculatePercentage(v.NumeroVoti, v.VotantiPervenute);
                j.idLista = v.Idlista;
                listaJsons.Add(j);
            }
            json.Liste = listaJsons;
            municipio.dati = json;
            return municipio;
        }

        public MunicipioModel ConvertToJsonSindacoRicalcolo(List<RicalcoloVotiSindaco> ricalcoloVotiSindacos, Voti voti)
        {
            List<VotiSindacoModel> sindacoJsons = new List<VotiSindacoModel>();
            MunicipioModel municipio = new MunicipioModel();
            string tipo = "";
            if (voti.Municipio.ToString() == "99")
            { tipo = "Sindaco tutto il Comune"; }
            else { tipo = "Sindaco Municipio " + voti.Municipio.ToString(); }
            DatiModel json = new DatiModel
            {
                Bianche = voti.Bianche.ToString(),
                Tipo = tipo,
                Contestate = voti.Contestate.ToString(),
                Nulle = voti.Nulle.ToString(),
                totaleValide = voti.TotaleValide.ToString(),
                soloSindaco = voti.SoloSindaco.ToString(),
                valideListe = (voti.TotaleValide - voti.SoloSindaco).ToString(),
                municipio = voti.Municipio.ToString(),
                Totale = voti.Totale.ToString(),
                Votanti = voti.Totale.ToString(),
                Iscritti = voti.Iscritti.ToString(),
                SezioniPervenute = voti.SezioniPervenute.ToString()
            };
            municipio.Municipio = voti.Municipio.ToString();
            int totalevotivalidi = voti.TotaleValide;
            foreach (var v in ricalcoloVotiSindacos)
            {
                VotiSindacoModel j = new VotiSindacoModel();
                j.Cognome = v.Sindaco.Cognome;
                j.Nome = v.Sindaco.Nome;
                j.IdSindaco = v.Sindacoid.ToString();
                j.totaleSindaco = v.NumeroVoti.ToString();
                j.percentualeTotale = CalculatePercentage(v.NumeroVoti, totalevotivalidi);
                j.SoloSindaco = v.NumeroVotiSoloSindaco.ToString();              
                sindacoJsons.Add(j);               
            }
            json.Sindaci = sindacoJsons;
            municipio.dati = json;
            return municipio;
        }
        public MunicipioModel ConvertToJsonListeRicalcolo(List<RicalcoloVotiLista> ricalcoloVotiListas, Voti voti)
        {
            List<VotiListaModel> listaJsons = new List<VotiListaModel>();
            MunicipioModel municipio = new MunicipioModel();
            string tipo = "";
            if (voti.Municipio.ToString() == "99")
            { tipo = "Lista tutto il Comune"; } 
            else { tipo = "Lista Municipio " + voti.Municipio.ToString(); }
            DatiModel json = new DatiModel
            {
                Bianche = voti.Bianche.ToString(),
                Tipo = tipo,
                Contestate = voti.Contestate.ToString(),
                Nulle = voti.Nulle.ToString(),
                totaleValide = voti.TotaleValide.ToString(),
                soloSindaco = voti.SoloSindaco.ToString(),
                valideListe = (voti.TotaleValide - voti.SoloSindaco).ToString(),
                municipio = voti.Municipio.ToString(),
                Totale = voti.Totale.ToString(),
                Votanti = voti.Totale.ToString(),
                Iscritti = voti.Iscritti.ToString(),
                SezioniPervenute = voti.SezioniPervenute.ToString()
            };
            municipio.Municipio = voti.Municipio.ToString();
            int totalevotivalidi = voti.TotaleValide;
            foreach (var v in ricalcoloVotiListas)
            {
                VotiListaModel j = new VotiListaModel();               
                j.Denominazione = v.Denominazione;                
                j.votiLista = v.NumeroVoti.ToString();
                j.perecentualeLista = CalculatePercentage(v.NumeroVoti, totalevotivalidi);
                j.idLista = v.Idlista;                  
                listaJsons.Add(j);
            }
            json.Liste = listaJsons;
            municipio.dati = json;
            return municipio;
        }

        internal static string CalculatePercentage(int valid, int total)
        {
            double percent = (double)(valid * 100) / total;
            Double dc = Math.Round((Double)percent, 2);
            return dc.ToString();
        }

        public List<RicalcoloVotiLista> ConvertToListeRicalcolo(DatiModel json, int tipoelezioneid, int totaleSezioni, int tiporicalcolo)
        {
            List<RicalcoloVotiLista> ricalcoloVotiListas = new List<RicalcoloVotiLista>();
            foreach(var lista in json.Liste)
            {
                RicalcoloVotiLista r = new RicalcoloVotiLista
                {
                    Denominazione = lista.Denominazione,
                    Idlista = lista.idLista,
                    Idtipoelezione = tipoelezioneid,
                    Idtiporicalcolo = tiporicalcolo,
                    IscrittiPervenute =int.Parse(json.Iscritti),
                    Municipio = int.Parse(json.municipio),
                    NumeroSezioni = int.Parse(json.SezioniPervenute),
                    TotaleSezioni = totaleSezioni,
                    NumeroVoti = int.Parse(lista.votiLista),
                    VotantiPervenute = int.Parse(json.Votanti),
                    PercentualeVotantiPervenute = CalculatePercentage(int.Parse(json.Votanti), int.Parse(json.Iscritti)),
                    PercentualeSezioniPervenute = CalculatePercentage(int.Parse(json.SezioniPervenute),totaleSezioni),
                    PercentualeVoti = lista.perecentualeLista                  
                   
                };
                ricalcoloVotiListas.Add(r);
            }
            return ricalcoloVotiListas;
        }

        public List<RicalcoloVotiSindaco> ConvertToSindacoRicalcolo(DatiModel json, int tipoelezioneid, int totalesezioni, int tiporicalcolo)
        {
            List<RicalcoloVotiSindaco> ricalcoloVotiSindacos = new List<RicalcoloVotiSindaco>();
            foreach(var sindaco in json.Sindaci)
            {
                RicalcoloVotiSindaco r = new RicalcoloVotiSindaco
                {
                    IscrittiPervenute =int.Parse(json.Iscritti),
                    Municipio = int.Parse(json.municipio),
                    VotantiPervenute = int.Parse(json.Votanti),
                    Tipoelezioneid = tipoelezioneid,
                    Tiporicalcoloid = tiporicalcolo,
                    TotaleSezioni = totalesezioni,
                    Sindacoid = int.Parse(sindaco.IdSindaco),
                    NumeroVoti = int.Parse(sindaco.totaleSindaco),
                    NumeroSezioni = totalesezioni,                                     
                    NumeroVotiSoloSindaco =int.Parse(sindaco.SoloSindaco),
                    PercentualeVotantiPervenute = CalculatePercentage(int.Parse(json.Votanti), int.Parse(json.Iscritti)),
                    PercentualeSezioniPervenute = CalculatePercentage(int.Parse(json.SezioniPervenute), totalesezioni),
                    PercentualeVoti = sindaco.percentualeTotale

                };
                ricalcoloVotiSindacos.Add(r);
            }
            return ricalcoloVotiSindacos;
        }
    }
}
