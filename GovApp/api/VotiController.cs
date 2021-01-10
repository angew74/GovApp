using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Core.Entity;
using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using Gov.Core.Identity;
using Gov.Structure.Config;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;
using Gov.Structure.Contracts.Helpers;
using Gov.Structure.Identity;
using GovApp.Helpers;
using GovApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GovApp.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VotiController : ControllerBase
    {
        private readonly ILogger<VotiController> _logger;
        private readonly UserStore _utentiService;
        private readonly IPaginaService _paginaService;
        private readonly IContenutoService _contenutoService;
        private readonly IListaService _listaService;
        private readonly IRoleStore<ApplicationRole> _roleService;
        private readonly ISezioneService _sezioneService;
        private readonly IOptions<ElezioneConfig> _elezioneConfig;
        private readonly IBusinessRules _businessRules;
        private readonly IAbilitazioniService _abilitazioniService;
        private readonly IAffluenzaService _affluenzaService;
        private readonly IIscrittiService _iscrittiService;
        private readonly ISindacoService _sindacoService;
        private readonly IVotiListaService _votiListaService;
        private readonly IVotiSindacoService _votiSindacoService;
        private readonly IVotiLoader _votiLoader;

        public VotiController(ILogger<VotiController> logger, IPaginaService paginaService, IListaService listaService, IContenutoService contenutoService, IRoleStore<ApplicationRole> roleService, ISezioneService sezioneService, IOptions<ElezioneConfig> elezioneConfig, IBusinessRules businessRules, IAbilitazioniService abilitazioniService, IAffluenzaService affluenzaService, ISindacoService sindacoService, IIscrittiService iscrittiService, UserStore utentiService, IVotiSindacoService votiSindacoService, IVotiListaService votiListaService, IVotiLoader votiLoader)
        {
            _logger = logger;
            _paginaService = paginaService;
            _listaService = listaService;
            _contenutoService = contenutoService;
            _roleService = roleService;
            _sezioneService = sezioneService;
            _elezioneConfig = elezioneConfig;
            _businessRules = businessRules;
            _abilitazioniService = abilitazioniService;
            _affluenzaService = affluenzaService;
            _sindacoService = sindacoService;
            _iscrittiService = iscrittiService;
            _utentiService = utentiService;
            _votiListaService = votiListaService;
            _votiSindacoService = votiSindacoService;
            _votiLoader = votiLoader;
        }

        public class Input
        {
            public ResearchSezione researchsezione { get; set; }
            public VotiModel voti { get; set; }

        }


        [HttpPost("/voti/carica")]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public IActionResult Carica([FromBody] Input input)
        {
            VotiModel json = new VotiModel();
            ErrorModel error = new ErrorModel();
            ResearchSezione model = input.researchsezione;
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            try
            {
                int s = int.Parse(model.sezione);
                Affluenze afp = _affluenzaService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                if (afp.Affluenza3 != 1)
                {
                    error.errMsg = "ultima affluenza per la sezione numero " + model.sezione + " non inserita";
                    return BadRequest(error);
                }
                var votiSindaco = _votiSindacoService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                if(votiSindaco.Count > 0 && model.tipo == "VL")
                {
                    error.errMsg = "voti già inseriti per la sezione numero " + model.sezione + " utilizzare modifica";
                    return BadRequest(error);
                }
                json.Votanti = afp.Votantitotali3.ToString();
                json.Iscritti = afp.Iscritti.Iscrittitotaligen.ToString();
                json.NumeroSezione = input.researchsezione.sezione;
                json.Tipo = input.researchsezione.tipo;
                if (model.tipo == "VL")
                {
                    var sindaci = _sindacoService.findAllByTipoelezioneId(tipoelezioneid);
                    json.Sindaci = _votiLoader.ConvertToJsonSindaciEmpty(sindaci, input.researchsezione.sezione, input.researchsezione.tipo);
                    var liste = _listaService.findAllByTipoelezioneId(tipoelezioneid);                   
                }
                else
                {
                   var votiLista = _votiListaService.findBySezioneNumerosezioneAndTipoelezioneId(s,tipoelezioneid);                            
                    json.Sindaci = _votiLoader.ConvertToJsonSindaci(votiSindaco,s, input.researchsezione.tipo, tipoelezioneid);
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
    }
}
