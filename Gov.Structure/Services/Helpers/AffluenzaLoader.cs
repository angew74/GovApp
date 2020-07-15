using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using Gov.Structure.Config;
using Gov.Structure.Contracts.Elezioni;
using Gov.Structure.Contracts.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Structure.Services.Helpers
{
    public class AffluenzaLoader : IAffluenzaLoader
    {
        private readonly IIscrittiService _iscrittiService;
        private readonly ILogger _logger;
        private IOptions<ElezioneConfig> _config;
        public AffluenzaLoader(IIscrittiService iscrittiService, IOptions<ElezioneConfig> config, ILogger<AffluenzaLoader> logger)
        {
            _iscrittiService = iscrittiService;
            _config = config;
            _logger = logger;
        }
        public RicalcoliAffluenza affluenzaSplit(Affluenze a, String tipoInterrogazione)
        {
            RicalcoliAffluenza r = new RicalcoliAffluenza();
            int tipoelezioneid = int.Parse(_config.Value.tipoElezioneId);
            try
            {
                Iscritti i = _iscrittiService.findByTipoelezioneIdAndSezioneNumerosezione(tipoelezioneid,(int) a.Sezione.Numerosezione);
                switch (tipoInterrogazione)
                {
                    case "AF1":
                        r.AffluenzaMaschi =(int) a.Votantimaschi1;
                        r.AffluenzaFemmine = (int)a.Votantifemmine1;
                        r.AffluenzaTotale = (int)a.Votantitotali1;
                        break;
                    case "AF2":
                        r.AffluenzaMaschi =(int) a.Votantimaschi2;
                        r.AffluenzaFemmine = (int)a.Votantifemmine2;
                        r.AffluenzaTotale =(int) a.Votantitotali2;
                        break;
                    case "CHI":
                        r.AffluenzaMaschi = (int)a.Votantimaschi3;
                        r.AffluenzaFemmine = (int)a.Votantifemmine3;
                        r.AffluenzaTotale =(int) a.Votantitotali3;
                        break;
                }
                r.IscrittiFemmine = i.Iscrittifemminegen;
                r.IscrittiMaschi = i.Iscrittimaschigen;
                r.IscrittiTotale =(int) i.Iscrittitotaligen;
                r.Municipio = i.Municipio;
                r.PercentualeMaschi = calculatePercentage(r.AffluenzaMaschi, r.IscrittiMaschi);
                r.PercentualeFemmine = calculatePercentage(r.AffluenzaFemmine, r.IscrittiFemmine);
                r.PercentualeTotale = calculatePercentage(r.AffluenzaTotale,(double) r.IscrittiTotale);
                r.Sezione = a.Sezione.Numerosezione.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
            return r;
        }

        public String calculatePercentage(double obtained, double total)
        {
            double percentage = obtained * 100 / total;
           return Math.Round(percentage, 2, MidpointRounding.AwayFromZero).ToString();          
        }

        public RicalcoloAperturaCostituzione costituzioneSplit(Affluenze a, String tipoInterrogazione)
        {
            RicalcoloAperturaCostituzione r = new RicalcoloAperturaCostituzione();
            int tipoelezioneid = int.Parse(_config.Value.tipoElezioneId);
            try
            {
                Iscritti i = _iscrittiService.findByTipoelezioneIdAndSezioneNumerosezione(tipoelezioneid,(int) a.Sezione.Numerosezione);
                r.iscrittiFemmine =(int) i.Iscrittifemminegen;
                r.iscrittiMaschi =(int) i.Iscrittimaschigen;
                r.iscrittiTotali =(int) i.Iscrittitotaligen;
                r.Municipio =(int) i.IdsezioneNavigation.Municipio;
                r.Sezione = (int)a.Sezione.Numerosezione;
                switch (tipoInterrogazione)
                {
                    case "CO1":
                        r.Status = "Costituita";
                        break;
                    case "AP1":
                        r.Status ="Aperta";
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
            return r;

        }

        public AffluenzaModel convertToJson(Affluenze a, Iscritti i, String tipo)
        {
            AffluenzaModel json = new AffluenzaModel();
            switch (tipo)
            {
                case "1A":
                    json.VotantiFemmine =(int) a.Votantifemmine1;
                    json.VotantiMaschi=(int) a.Votantimaschi1;
                    json.VotantiTotali = (int)a.Votantitotali1;                
                    break;
                case "2A":
                    json.VotantiFemmine =(int) a.Votantifemmine2;
                    json.VotantiMaschi= (int)a.Votantimaschi2;
                    json.VotantiTotali = (int)a.Votantitotali2;                  
                    break;
                case "3C":
                    json.VotantiFemmine = (int)a.Votantifemmine3;
                    json.VotantiMaschi= (int)a.Votantimaschi3;
                    json.VotantiTotali = (int)a.Votantitotali3;                  
                    break;
            }
            json.IscrittiMaschi = (int)i.Iscrittimaschigen;
            json.IscrittiFemmine = (int)i.Iscrittifemminegen;
            json.IscrittiTotali = (int)i.Iscrittitotaligen;        
            return json;
        }
    }
}
