using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using Gov.Structure.Config;
using Gov.Structure.Contracts.Elezioni;
using Gov.Structure.Contracts.Helpers;
using Gov.Structure.Identity;
using GovApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AndamentoController : ControllerBase
    {
        private readonly ILogger<AndamentoController> _logger;
        private readonly UserStore _utentiService;      
        private readonly ISezioneService _sezioneService;
        private readonly IOptions<ElezioneConfig> _elezioneConfig;
        private readonly IBusinessRules _businessRules;     
        private readonly IAffluenzaService _affluenzaService;
        private readonly IIscrittiService _iscrittiService;
        private readonly ITipoElezioneService _tipoElezioneService;      
        private readonly IMunicipioService _municipioService;     

        public AndamentoController(ILogger<AndamentoController> logger, ISezioneService sezioneService, IOptions<ElezioneConfig> elezioneConfig, IBusinessRules businessRules, IAbilitazioniService abilitazioniService, IAffluenzaService affluenzaService, ITipoElezioneService tipoElezioneService, IIscrittiService iscrittiService, UserStore utentiService, IMunicipioService municipioService, IListaService listaService, IVotiSindacoService votiSindacoService, IVotiPreferenzeService votiPreferenzeService)
        {
            _logger = logger;          
            _sezioneService = sezioneService;
            _elezioneConfig = elezioneConfig;
            _businessRules = businessRules;           
            _affluenzaService = affluenzaService;
            _tipoElezioneService = tipoElezioneService;
            _iscrittiService = iscrittiService;
            _utentiService = utentiService;           
            _municipioService = municipioService;         
        }

        public class Input
        {
            public Research research { get; set; }
            public AndamentoModel anda { get; set; }

            public AffluenzaModel affluenza { get; set; }

        }

        [HttpPost("/Andamento/Apra")]
        [IgnoreAntiforgeryToken]
        public IActionResult Apra([FromBody] Input model)
        {
            ErrorModel error = new ErrorModel();
            SezioneModel sezioneJson = new SezioneModel();
            Affluenze affluenza = new Affluenze();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            AndamentoModel andamento = model.anda;
            try
            {
                int s = int.Parse(andamento.sezione);
                String msg = _businessRules.IsInsertable(s, andamento.tipoAffluenza, 0, tipoelezioneid);
                if (string.IsNullOrEmpty(msg))
                {
                    Tipoelezione t = _tipoElezioneService.findTipoElezioneById(tipoelezioneid);
                    affluenza = _affluenzaService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                    switch (andamento.tipoAffluenza)
                    {
                        case "AP":
                            affluenza.Apertura1 = 1;
                            break;
                        case "CO":
                            if (affluenza == null)
                            {
                                affluenza = new Affluenze();
                                Sezioni sezione = _sezioneService.findByNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                                affluenza.Sezioneid = sezione.Id;
                                Iscritti iscritti = _iscrittiService.findByTipoelezioneIdAndSezioneNumerosezione(tipoelezioneid, s);
                                affluenza.Iscrittiid = iscritti.Id;
                            }
                            affluenza.Costituzione1 = 1;
                            break;
                        default:
                            error.errMsg = "Errrore in banca dati Parametri non validi";
                            return BadRequest(error);
                            break;

                    }
                    sezioneJson.Tipo = andamento.tipoAffluenza;
                    affluenza.Tipoelezioneid = tipoelezioneid;
                    if (andamento.tipoAffluenza == "CO")
                    { _affluenzaService.Create(affluenza); }
                    else { _affluenzaService.Update(affluenza); }
                }
                else
                {
                    error.errMsg = "Errrore in banca dati: " + msg;
                    _logger.LogError("Errrore in banca dati: " + msg);
                    return BadRequest(error);

                }
            }
            catch (Exception ex)
            {
                error.errMsg = "Errrore in banca dati " + ex.Message;
                _logger.LogError("Errrore in banca dati: " + ex.Message);
                return BadRequest(error);
            }
            return Ok(sezioneJson);
        }

        [HttpPost("/Andamento/affluenza")]
        [IgnoreAntiforgeryToken]
        public IActionResult researchAffluenza([FromBody] Input input)
        {
            AffluenzaModel json = new AffluenzaModel();
            ErrorModel error = new ErrorModel();
            Research model = input.research;
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            try
            {
                int s = int.Parse(model.sezione);
                Affluenze afp = _affluenzaService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                if (afp == null)
                {
                    error.errMsg = "Nessuna affluenza per la sezione";
                    return BadRequest(error);
                }
                if (model.tipo == "2A" || model.tipo == "3C" || model.tipo == "R2A" || model.tipo == "R3C")
                {
                    /* imposto votanti 1 affluenza */
                    if (model.tipo == "2A" || model.tipo == "R2A")
                    {
                        json.VotantiFemmineAffP = (int)afp.Votantifemmine1;
                        json.VotantiMaschiAffP = (int)afp.Votantimaschi1;
                        json.VotantiTotaliAffP = (int)afp.Votantitotali1;
                        /* imposto votanti per rettifica*/
                        if (model.tipo == "R2A")
                        {
                            json.VotantiFemmine = afp.Votantifemmine2.ToString();
                            json.VotantiMaschi = afp.Votantimaschi2.ToString();
                            json.VotantiTotali = afp.Votantitotali2.ToString();
                        }
                    }
                    /* imposto votanti 2 affluenza */
                    if (model.tipo == "3C" || model.tipo == "R3C")
                    {
                        json.VotantiFemmineAffP = (int)afp.Votantifemmine2;
                        json.VotantiMaschiAffP = (int)afp.Votantimaschi2;
                        json.VotantiTotaliAffP = (int)afp.Votantitotali2;
                        /* imposto votanti per rettifica */
                        if (model.tipo == "R3C")
                        {
                            json.VotantiFemmine = afp.Votantifemmine3.ToString();
                            json.VotantiMaschi = afp.Votantimaschi3.ToString();
                            json.VotantiTotali = afp.Votantitotali3.ToString();
                        }
                    }
                }
                /* imposto votanti per rettifica */
                if (model.tipo == "R1A")
                {
                    json.VotantiFemmine = afp.Votantifemmine1.ToString();
                    json.VotantiMaschi = afp.Votantimaschi1.ToString();
                    json.VotantiTotali = afp.Votantitotali1.ToString();
                }
                json.NumeroSezione = s;
                json.Tipo = model.tipo;
                /* imposto iscritti per confronti */
                if (!(model.tipo == "CO"))
                {
                    json.IscrittiMaschi = (int)afp.Iscritti.Iscrittimaschigen;
                    json.IscrittiFemmine = (int)afp.Iscritti.Iscrittifemminegen;
                    json.IscrittiTotali = (int)afp.Iscritti.Iscrittitotaligen;
                }
                else
                {
                    Iscritti iscritti = _iscrittiService.findByTipoelezioneIdAndSezioneNumerosezione(tipoelezioneid, s);
                    json.IscrittiMaschi = (int)iscritti.Iscrittimaschigen;
                    json.IscrittiFemmine = (int)iscritti.Iscrittifemminegen;
                    json.IscrittiTotali = (int)iscritti.Iscrittitotaligen;
                }
            }
            catch (Exception ex)
            {
                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok(json);
        }

        [HttpPost("/Andamento/Anda")]
        [IgnoreAntiforgeryToken]
        public IActionResult Anda([FromBody] Input input)
        {
            Affluenze affluenza = new Affluenze();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            AffluenzaModel model = input.affluenza;
            ErrorModel error = new ErrorModel();
            try
            {
                affluenza = _affluenzaService.findBySezioneNumerosezioneAndTipoelezioneId(model.NumeroSezione, tipoelezioneid);
                switch (model.Tipo)
                {
                    case "1A":
                    case "R1A":
                        affluenza.Affluenza1 = 1;
                        affluenza.Votantifemmine1 = int.Parse(model.VotantiFemmine);
                        affluenza.Votantimaschi1 = int.Parse(model.VotantiMaschi);
                        affluenza.Votantitotali1 = int.Parse(model.VotantiTotali);
                        break;
                    case "2A":
                    case "R2A":
                        affluenza.Affluenza2 = 1;
                        affluenza.Votantifemmine2 = int.Parse(model.VotantiFemmine);
                        affluenza.Votantimaschi2 = int.Parse(model.VotantiMaschi);
                        affluenza.Votantitotali2 = int.Parse(model.VotantiTotali);
                        break;
                    case "3C":
                    case "R3C":
                        affluenza.Affluenza3 = 1;
                        affluenza.Votantifemmine3 = int.Parse(model.VotantiFemmine);
                        affluenza.Votantimaschi3 = int.Parse(model.VotantiMaschi);
                        affluenza.Votantitotali3 = int.Parse(model.VotantiTotali);
                        break;
                    default:
                        error.errMsg = "Errore grave: Parametri non validi";
                        _logger.LogError("Errore grave: Parametri non validi");
                        return BadRequest(error);
                        break;
                }
                _affluenzaService.Update(affluenza);
            }
            catch (Exception ex)
            {
                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok(model);
        }


        [HttpPost("/Andamento/Rapra")]
        [IgnoreAntiforgeryToken]
        public IActionResult Rapra([FromBody] Input model)
        {
            ErrorModel error = new ErrorModel();
            SezioneModel sezioneJson = new SezioneModel();
            Affluenze affluenza = new Affluenze();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            AndamentoModel andamento = model.anda;
            try
            {
                int s = int.Parse(andamento.sezione);
                String msg = _businessRules.IsInsertable(s, andamento.tipoAffluenza, 0, tipoelezioneid);
                if (string.IsNullOrEmpty(msg))
                {
                    DateTime oggi = DateTime.Now;
                    Tipoelezione t = _tipoElezioneService.findTipoElezioneById((int)tipoelezioneid);
                    switch (andamento.tipoAffluenza)
                    {
                        case "RAP":
                            affluenza = _affluenzaService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                            affluenza.Apertura1 = 0;
                            break;
                        case "RCO":
                            affluenza.Costituzione1 = 0;
                            break;
                        default:
                            error.errMsg = "Errrore in banca dati Parametri non validi";
                            return BadRequest(error);
                            break;
                    }
                    sezioneJson.Tipo = andamento.tipoAffluenza;
                    affluenza.Tipoelezioneid = tipoelezioneid;
                    _affluenzaService.Update(affluenza);
                }
                else
                {
                    error.errMsg = "Errrore in banca dati: " + msg;
                    _logger.LogError("Errrore in banca dati: " + msg);
                    return BadRequest(error);
                }
            }
            catch (Exception ex)
            {
                error.errMsg = "Errrore in banca dati " + ex.Message;
                _logger.LogError("Errrore in banca dati: " + ex.Message);
                return BadRequest(error);
            }
            return Ok(sezioneJson);
        }

    }
}
