using Gov.Core.Entity.Elezioni;
using Gov.Core.Contracts;
using Gov.Core.Contracts.Elezioni;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gov.Structure.Services.Elezioni
{
    public class RicalcoloPreferenzeService : EntityService<RicalcoloPreferenze>, IRicalcoloPreferenzeService
    {

        readonly IContext _context;

        public RicalcoloPreferenzeService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<RicalcoloPreferenze>();
        }


        public List<RicalcoloPreferenze> sumCandidato(int tipoelezioneid)
        {

            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid).GroupBy(g => new { g.Candidato.Id, g.Candidato.NomeCandidato, g.Candidato.CognomeCandidato, g.Listaid, g.Lista.Denominazione }).
                    Select(g => new RicalcoloPreferenze { NumeroVoti = g.Sum(i => i.NumeroVoti), Id = g.Key.Id, Candidato = new Candidati { NomeCandidato = g.Key.NomeCandidato, CognomeCandidato = g.Key.CognomeCandidato }, Lista = new Liste { Id = g.Key.Id, Denominazione = g.Key.Denominazione } }).ToList();
            
        }

        public List<RicalcoloPreferenze> sumCandidatoByLista(int tipoelezioneid, int idlista)
        {

           
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Listaid == idlista).GroupBy(g => new { g.Candidato.Id, g.Candidato.NomeCandidato, g.Candidato.CognomeCandidato, g.Listaid, g.Lista.Denominazione }).
                    Select(g => new RicalcoloPreferenze { NumeroVoti = g.Sum(i => i.NumeroVoti), Id = g.Key.Id, Candidato = new Candidati { NomeCandidato = g.Key.NomeCandidato, CognomeCandidato = g.Key.CognomeCandidato }, Lista = new Liste { Id = g.Key.Id, Denominazione = g.Key.Denominazione } }).ToList();
            
        }

        public List<RicalcoloPreferenze> sumCandidatoByListaMunicipio(int tipoelezioneid, int municipio, int idlista)
        {

            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio && x.Listaid == idlista).GroupBy(g => new { g.Candidato.Id, g.Candidato.NomeCandidato, g.Candidato.CognomeCandidato, g.Listaid, g.Lista.Denominazione, g.Municipio }).
                    Select(g => new RicalcoloPreferenze { NumeroVoti = g.Sum(i => i.NumeroVoti), Id = g.Key.Id, Candidato = new Candidati { NomeCandidato = g.Key.NomeCandidato, CognomeCandidato = g.Key.CognomeCandidato }, Lista = new Liste { Id = g.Key.Id, Denominazione = g.Key.Denominazione }, Municipio = g.Key.Municipio }).ToList();
            
        }

        public List<RicalcoloPreferenze> sumCandidatoByMunicipio(int tipoelezioneid, int municipio)
        {

            
               return _dbset.Where(x => x.Tipoelezioneid == tipoelezioneid && x.Municipio == municipio).GroupBy(g => new { g.Candidato.Id, g.Candidato.NomeCandidato, g.Candidato.CognomeCandidato, g.Listaid, g.Lista.Denominazione, g.Municipio }).
                    Select(g => new RicalcoloPreferenze { NumeroVoti = g.Sum(i => i.NumeroVoti), Id = g.Key.Id, Candidato = new Candidati { NomeCandidato = g.Key.NomeCandidato, CognomeCandidato = g.Key.CognomeCandidato }, Lista = new Liste { Id = g.Key.Id, Denominazione = g.Key.Denominazione }, Municipio = g.Key.Municipio }).ToList();
            
        }
    }
}
