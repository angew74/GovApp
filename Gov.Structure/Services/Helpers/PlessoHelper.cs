using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using Gov.Structure.Config;
using Gov.Structure.Contracts.Elezioni;
using Gov.Structure.Contracts.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Helpers
{
    public class PlessoHelper : IPlessoHelper
    {
        private IOptions<ElezioneConfig> _config;
        private ISezioneService _sezioneService;

        public PlessoHelper(IOptions<ElezioneConfig> config, ISezioneService sezioneService)
        {
            _config = config;
            _sezioneService = sezioneService;
        }



        public List<PlessoModel> Convert(List<Plessi> pp,string username,int userid)
        {
            List<PlessoModel> ppj = new List<PlessoModel>();
            int tipoelezioneid = int.Parse(_config.Value.tipoelezioneid);
            foreach (Plessi p in pp)           
            {
                List<Sezioni> s = _sezioneService.findByPlessoIdAndTipoelezioneId((int)p.Id, tipoelezioneid);
                foreach (Sezioni su in s)
                {
                    PlessoModel j = new PlessoModel();
                    j.Descrizione = p.Descrizione;
                    j.Ubicazione = p.Ubicazione;
                    j.Numero =(int) p.Id;
                    j.UserName = username;
                    j.UserId = userid;
                    j.Municipio = su.Municipio.ToString();
                    j.Cabina = su.Cabina.ToString();
                    j.Sezione =su.Numerosezione.ToString();
                    ppj.Add(j);
                }
            }
            return ppj;
        }

        public List<PlessoModel> ConvertFromSezioni(List<Sezioni> pp, string username, int userid)
        {
            List<PlessoModel> ppj = new List<PlessoModel>();
            int tipoelezioneid = int.Parse(_config.Value.tipoelezioneid);
            foreach (Sezioni s in pp)
            {
                PlessoModel j = new PlessoModel();
                j.Descrizione = s.IdplessoNavigation.Descrizione;
                j.Ubicazione = s.IdplessoNavigation.Ubicazione;
                j.Numero =(int) s.Idplesso;
                j.UserName = username;
                j.UserId = userid;
                j.Municipio = s.Municipio.ToString();
                j.Cabina = s.Cabina.ToString();
                j.Sezione = s.Numerosezione.ToString();
                ppj.Add(j);
            
        }
            return ppj;
        }
    }
}
