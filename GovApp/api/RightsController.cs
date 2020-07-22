using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using Gov.Structure.Config;
using Gov.Structure.Contracts.Elezioni;
using GovApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GovApp.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RightsController : ControllerBase
    {


        private readonly ILogger<RightsController> _logger;
        private readonly IOptions<ComunicazioneConfig> _mailConfig;
        private readonly IOptions<PagingConfig> _pagingConfig;
        private readonly IOptions<IdentityOptions> _identityOptions;
        private readonly IAbilitazioniService _abilitazioniService;
        private readonly IOptions<ElezioneConfig> _elezioneConfig;
        private readonly IEmailSender _emailSender;


        public RightsController(ILogger<RightsController> logger, IEmailSender emailSender, IOptions<ComunicazioneConfig> config, IOptions<PagingConfig> pagingConfig, IOptions<IdentityOptions> identityOptions, IAbilitazioniService abilitazioniService, IOptions<ElezioneConfig> elezioneConfig)
        {
            _logger = logger;
            _emailSender = emailSender;
            _mailConfig = config;
            _pagingConfig = pagingConfig;
            _identityOptions = identityOptions;
            _abilitazioniService = abilitazioniService;
            _elezioneConfig = elezioneConfig;
        }

        public class Input
        {
           public AbilitazioniModel right { get; set; }
        }

            [Authorize]
        [HttpGet("rightsfilters")]
        public async Task<IActionResult> GetRightsFilters(string page, string filtro, [FromQuery(Name = "fitriarray[]")] string[] filtriarray)
        {

            ErrorModel error = new ErrorModel();
            List<AbilitazioniModel> rightsmodel = new List<AbilitazioniModel>();
            try
            {
                int tipoElezione = int.Parse(_elezioneConfig.Value.tipoelezioneid);
                int take = int.Parse(_pagingConfig.Value.perPage);
                int skip = take * (int.Parse(page) - 1);
                List<FaseElezione> rights = new List<FaseElezione>();
                if (filtriarray.Length > 0)
                {
                    foreach (string t in filtriarray)
                    {
                        switch (t.ToLower())
                        {
                            case "codice":
                                rights.Add(_abilitazioniService.findByCodiceAndTipoelezioneId(filtro,tipoElezione));
                                break;
                            case "categoria":
                                rights.AddRange(_abilitazioniService.findByCategoria(filtro,tipoElezione, skip, take));
                                break;
                            case "descrizione":
                                rights.AddRange(_abilitazioniService.findByDescrizioneLike(filtro,tipoElezione, skip, take));
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    rights.AddRange(_abilitazioniService.findAllByTipoelezioneId(tipoElezione, skip, take));
                }

                if (rights == null)
                {
                    return Ok(rightsmodel);
                }
                rightsmodel = rights.Distinct().Select(x => new AbilitazioniModel
                {
                    id = x.Id.ToString(),
                    categoria = x.Categoria,
                    codice = x.Codice,
                    descrizione = x.Descrizione,
                    abilitata = x.IsAbilitata
                  
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }
            return await System.Threading.Tasks.Task.FromResult(Ok(rightsmodel));
        }

        [Authorize]
        [HttpGet("rights")]
        public async Task<IActionResult> GetRights(string page, string ordinaPer, [FromQuery] bool ordinaDesc, string filter, [FromQuery(Name = "fitriarray[]")] string[] filtriarray)
        {

            ErrorModel error = new ErrorModel();
            List<AbilitazioniModel> rightsmodel = new List<AbilitazioniModel>();
            try
            {
                int take = int.Parse(_pagingConfig.Value.perPage);
                int skip = take * (int.Parse(page) - 1);
                List<FaseElezione> rights = new List<FaseElezione>();
                int tipoElezione = int.Parse(_elezioneConfig.Value.tipoelezioneid);
                rights = _abilitazioniService.getRightsSortingBy(skip, take, ordinaPer, ordinaDesc, filter, filtriarray);
                if (rights == null)
                {
                    return Ok(rightsmodel);
                }
                rightsmodel = rights.Select(x => new AbilitazioniModel
                {
                    id = x.Id.ToString(),
                    categoria = x.Categoria,
                    codice = x.Codice,
                    descrizione = x.Descrizione,
                    abilitata = x.IsAbilitata
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }
            return await System.Threading.Tasks.Task.FromResult(Ok(rightsmodel));
        }

        [Authorize]
        [HttpGet("pagination")]
        public async Task<IActionResult> GetPagination(string type, string page, string ordinaPer, bool ordinaDesc, string filter, [FromQuery(Name = "fitriarray[]")] string[] filtriarray)
        {
            PaginationModel model = new PaginationModel();
            switch (type)
            {
                case "rights":
                    model = GetBasePaginationRights();
                    model.currentPage = page;
                    if (!string.IsNullOrEmpty(ordinaPer))
                    { model.sortBy = ordinaPer; }
                    if (ordinaDesc == true)
                    { model.sortDesc = ordinaDesc; }
                    if (string.IsNullOrEmpty(filter))
                    { model.totalRows = _abilitazioniService.GetRightsCount().ToString(); }
                    else { _abilitazioniService.GetRightsCountLike(filter, filtriarray).ToString(); }
                    break;
            }

            return await System.Threading.Tasks.Task.FromResult(Ok(model));
        }


        [Authorize]
        [HttpGet("paginationlike")]
        public async Task<IActionResult> GetPaginationLike(string type, string page, string filtro, [FromQuery(Name = "fitriarray[]")] string[] filtriarray)
        {
            PaginationModel model = new PaginationModel();
            switch (type)
            {
                case "rights":
                    model = GetBasePaginationRights();
                    model.currentPage = page;
                    model.totalRows = _abilitazioniService.GetRightsCountLike(filtro, filtriarray).ToString();
                    break;
            }

            return await System.Threading.Tasks.Task.FromResult(Ok(model));
        }


        private PaginationModel GetBasePaginationRights()
        {
            PaginationModel model = new PaginationModel();
            List<PaginationModel.Field> fields = new List<PaginationModel.Field>();
            PaginationModel.Field field = new PaginationModel.Field();
            field.key = "codice";
            field.label = "codice";
            field.sortable = true;
            field.sortDirection = "desc";
            fields.Add(field);
            PaginationModel.Field field1 = new PaginationModel.Field();
            field1.key = "descrizione";
            field1.label = "descrizione";
            field1.sortable = true;
            field1.sortDirection = "desc";
            field1.cssclass = "text-center";
            fields.Add(field1);
            PaginationModel.Field field3 = new PaginationModel.Field();
            field3.key = "categoria";
            field3.label = "categoria";
            field3.sortable = true;
            field3.sortDirection = "desc";
            field3.cssclass = "text-center";
            fields.Add(field3);
            PaginationModel.Field field2 = new PaginationModel.Field();
            field2.key = "abilitata";
            field2.label = "abilitata";
            field2.sortable = false;
            field2.sortByFormatted = true;
            field2.filterByFormatted = false;
            field2.cssclass = "text-center";
            field2.formatter = "activeRight";
            fields.Add(field2);            
            PaginationModel.Field field4 = new PaginationModel.Field();
            field4.key = "updateRight";
            field4.label = "";
            fields.Add(field4);       
            model.perPage = _pagingConfig.Value.perPage;
            List<int> options = new List<int> { 5, 10, 15 };
            model.pageOptions = options.ToArray();
            model.sortBy = "codice";
            model.sortDesc = false;
            model.sortDirection = "asc";
            model.filter = "";
            List<PaginationModel.Options> f = new List<PaginationModel.Options>();
            PaginationModel.Options opt = new PaginationModel.Options
            {
                item = "codice",
                name = "codice"
            };
            PaginationModel.Options opt1 = new PaginationModel.Options
            {
                item = "descrizione",
                name = "descrizione"
            };
            PaginationModel.Options opt2 = new PaginationModel.Options
            {
                item = "categoria",
                name = "categoria"
            };
            f.Add(opt);
            f.Add(opt1);
            model.filterOn = f.ToArray();
            model.infoMal = new PaginationModel.InfoModale { content = "", id = "info-modal", title = "" };
            model.fields = fields.ToArray();
            return model;
        }

        [HttpPost("rightssorting")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetRightsSorting([FromBody] SortingModel model)
        {
            ErrorModel error = new ErrorModel();
            List<AbilitazioniModel> rightsmodel = new List<AbilitazioniModel>();
            string[] filtriarray = new string[0];
            if (!ModelState.IsValid)
            {
                error.errMsg = "Errore di validazione";
                return BadRequest(error);
            }
            try
            {

                int take = int.Parse(_pagingConfig.Value.perPage);
                int skip = take * (int.Parse(model.currentPage.ToString()) - 1);
                var rights = _abilitazioniService.getRightsSortingBy(skip,take, model.sortBy, model.sortDesc, model.filter, filtriarray);
                if (rights == null)
                {
                    return Ok(rightsmodel);
                }
                rightsmodel = rights.Select(x => new AbilitazioniModel
                {
                    id = x.Id.ToString(),
                    categoria = x.Categoria,
                    codice = x.Codice,
                    descrizione = x.Descrizione,
                    abilitata = x.IsAbilitata
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }
            return await System.Threading.Tasks.Task.FromResult(Ok(rightsmodel));
        }


        [Authorize]
        [HttpPost("update")]
        [IgnoreAntiforgeryToken]
        public IActionResult Update([FromBody] AbilitazioniModel model)
        {
          
            ErrorModel error = new ErrorModel();
            if (!ModelState.IsValid)
            {
                error.errMsg = "Errore di validazione";
                return BadRequest(error);
            }
            try
            {
                var right =  _abilitazioniService.findFaseElezioneById(int.Parse(model.id));
                if (right == null)
                {
                    _logger.LogError("Impossibile aggiornare abilitazione codice  " + model.codice + " . codice non esistente");
                    error.errMsg = "Impossibile aggiornare abilitazione codice  " + model.codice + " . codice non esistente";
                    return BadRequest(error);
                }
                if (model.abilitata)
                { right.Abilitata = 0; }
                else { right.Abilitata = 1; }
                _abilitazioniService.Update(right);

            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }          
            return Ok(model);
        }

    }
}
