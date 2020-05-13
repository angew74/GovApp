using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Core.Entity;
using Gov.Structure.Contracts;
using GovApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GovApp.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        private readonly IPaginaService _paginaService;

        public ValuesController(ILogger<ValuesController> logger, IPaginaService paginaService)
        {
            _logger = logger;
            _paginaService = paginaService;
        }

        [HttpGet("/Values/content")]
        public IActionResult GetValues([FromQuery] string type)
        {
            Dictionary<String, String> errors = null;
            List<ContenutoModel> model = new List<ContenutoModel>();
            try
            {
               List<Pagina> pagine = _paginaService.GetByCodice(type);
                if(pagine == null)
                {
                    return Ok();
                }
                else
                {
                    int id = 0;
                    foreach (Pagina p in pagine)
                    {
                        ContenutoModel contenuto = new ContenutoModel();
                        contenuto.ContenutoTitolo = type;
                        id++;
                        contenuto.Id ="collapse" + id.ToString();
                        foreach (Contenuto c in p.Contenuti)
                        {
                            switch (c.Tipo.ToLower())
                            {
                                case "icona":
                                    contenuto.ContenutoIcon = c.ContentuoCard;
                                    break;
                                case "testo":
                                    contenuto.ContenutoTesto = c.ContentuoCard;
                                    break;
                                case "link":
                                    contenuto.ContenutoLink = c.ContentuoCard;
                                    break;
                                case "header":
                                    contenuto.ContenutoHeader = c.ContentuoCard;
                                    break;                                
                            }
                        }
                        model.Add(contenuto);
                    }
                }
            }
            catch (Exception ex)
            {
                errors = new Dictionary<string, string>();
                errors.Add("Errore grave", ex.Message);
                _logger.LogError(errors.First().Value);
                return BadRequest(errors.First().Value);
            }
            return Ok(model);
        }
    }
}