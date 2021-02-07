using Gov.Core.Entity.Elezioni;
using Gov.Core.Identity;
using Gov.Core.Contracts.Elezioni;
using Gov.Core.Contracts.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Gov.Structure.Services.Helpers
{
    public class BusinessRules : IBusinessRules
    {

        private readonly IAffluenzaService _affluenzaService;
        private readonly IAbilitazioniService _abilitazioniService;
        private readonly ISezioneService _sezioneService;
        private readonly IVotiListaService _votiListaService;
        private readonly IVotiSindacoService _votiSindacoService;
        private readonly IVotiPreferenzeService _votiPreferenzeService;
        private readonly IUserSezioneService _userSezioneService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BusinessRules(IAffluenzaService affluenzaService,IAbilitazioniService abilitazioneService,ISezioneService sezioneService,IVotiListaService votiListaService, IVotiSindacoService votiSindacoService, IVotiPreferenzeService votiPreferenzeService, IUserSezioneService userSezioneService, IHttpContextAccessor httpContextAccessor)
        {
            _affluenzaService = affluenzaService;
            _abilitazioniService = abilitazioneService;
            _sezioneService = sezioneService;
            _votiListaService = votiListaService;
            _votiSindacoService = votiSindacoService;
            _votiPreferenzeService = votiPreferenzeService;
            _userSezioneService = userSezioneService;
            _httpContextAccessor = httpContextAccessor;
        }
        public string getTitoloByFase(string codice, string tipo)
        {
            string titolo = "Inserimento";
            switch (codice)
            {
                case "2A":
                    switch (tipo)
                    {
                        case "I":
                            titolo = "Inserimento 2 Affluenza";
                            break;
                    }
                    break;
                case "R2A":
                    switch (tipo)
                    {
                        case "M":
                            titolo = "Modifica 2 Affluenza";
                            break;
                        case "A":
                            titolo = "Annullamento 2 Affluenza";
                            break;
                    }
                    break;
                case "1A":
                    switch (tipo)
                    {
                        case "I":
                            titolo = "Inserimento 1 Affluenza";
                            break;
                    }
                    break;
                case "R1A":
                    switch (tipo)
                    {
                        case "A":
                            titolo = "Annullamento 1 Affluenza";
                            break;
                        case "M":
                            titolo = "Modifica 1 Affluenza";
                            break;
                    }
                    break;
                case "AP":
                    switch (tipo)
                    {
                        case "I":
                            titolo = "Inserimento Apertura";
                            break;
                    }
                    break;
                case "RAP":
                    switch (tipo)
                    {
                        case "A":
                            titolo = "Annullamento Apertura";
                            break;
                    }
                    break;
                case "CO":
                    switch (tipo)
                    {
                        case "I":
                            titolo = "Inserimento Costituzione";
                            break;
                    }
                    break;
                case "RCO":
                    switch (tipo)
                    {
                        case "A":
                        case "M":
                            titolo = "Annullamento Costituzione";
                            break;
                    }
                    break;
                case "3C":
                    switch (tipo)
                    {
                        case "I":
                            titolo = "Inserimento Chiusura";
                            break;
                        case "M":
                            titolo = "Modifica Chiusura";
                            break;
                    }
                    break;
                case "VL":
                    switch (tipo)
                    {
                        case "I":
                            titolo = "Inserimento Voti Lista";
                            break;
                    }
                    break;
                case "VS":
                    switch (tipo)
                    {
                        case "I":
                            titolo = "Inserimento Voti Coalizione";
                            break;
                    }
                    break;
                case "R3C":
                    switch (tipo)
                    {
                        case "M":
                            titolo = "Modifica Chiusura";
                            break;
                        case "A":
                            titolo = "Annullamento Chiusura";
                            break;
                    }
                    break;
                case "RVL":
                    titolo = "Rettifica Voti Lista";
                    break;
                case "RVS":
                    titolo = "Rettifica Voti Coalizione";
                    break;

                case "RIC":
                    titolo = "Ricalcolo Affluenze";
                    break;
                case "RIL":
                    titolo = "Ricalcolo Voti Lista";
                    break;
                case "RIP":
                    titolo = "Ricalcolo Preferenze";
                    break;
                case "RIA":
                    titolo = "Ricalcolo Costituzione Apertura";
                    break;

            }
            return titolo;
        }

        public bool IsEnabled(string codiceFase, int idtipoelezione)
        {          
            FaseElezione fase = _abilitazioniService.findByCodiceAndTipoelezioneId(codiceFase, idtipoelezione);
            if (fase == null)
            { return false; }
            { return fase.IsAbilitata; }          
        }

        public string IsInsertable(int sezione, string codiceFase, int cabina, int idtipoelezione)
        {
            String message = "";
            if (codiceFase.ToUpper() != "INT")
            {
                FaseElezione fase = _abilitazioniService.findByCodiceAndTipoelezioneId(codiceFase, idtipoelezione);
                if (fase.IsAbilitata == false)
                {
                    return "Funzionalità non abilitata";
                }
            }
            if (cabina != 0)
            {
                Sezioni sezioneControllo = _sezioneService.findByNumerosezioneAndTipoelezioneId(sezione, idtipoelezione);
                if (sezioneControllo != null)
                {
                    if (!(sezioneControllo.Cabina == cabina))
                    {
                        return "cabina sezione non corrette";
                    }
                }
                else
                {
                    return " sezione non corretta";
                }
            }
            ClaimsPrincipal principal = _httpContextAccessor.HttpContext.User;
            if (!principal.HasClaim("admin", "admin"))
            { 
                UsersSezioni usersSezioni = _userSezioneService.findBySezioneIdAndTipoelezioneId(sezione, idtipoelezione); 
                if(usersSezioni.User == null || usersSezioni.User.UserName.ToLower() != principal.Identity.Name)
                {
                    return "sezione non abilitata all'utente";
                }
               
            }           
            Affluenze affluenza = _affluenzaService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, idtipoelezione);
            if (codiceFase.ToUpper() != "INT")
            {
                switch (codiceFase.ToUpper())
                {
                    case "AP":
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Apertura1 != null && affluenza.Apertura1 == 1)
                        {
                            return "Affluenza già inserita usare rettifica";
                        }
                        if ((affluenza.Costituzione1 == null) || (affluenza.Costituzione1 == 0))
                        {
                            return "Manca Costituzione";
                        }
                        break;
                    case "RAP":
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Apertura1 != null && affluenza.Apertura1 == 0)
                        {
                            return "Apertura non inserita usare funzione inserimento";
                        }
                        if (affluenza.Affluenza1 != null && affluenza.Affluenza1 == 1)
                        {
                            return "Affluenza già inserita impossibile annullare";
                        }
                        if ((affluenza.Costituzione1 == null) || (affluenza.Costituzione1 == 0))
                        {
                            return "Manca Costituzione";
                        }
                        if (affluenza.Apertura1 == 0)
                        {
                            return message;
                        }
                        break;
                    case "CO":
                        if (affluenza == null)
                        {
                            return message;
                        }
                        if (affluenza != null)
                        {
                            if (affluenza.Costituzione1 == 1)
                            {
                                return "Costituzione già inserita usare rettitifica";
                            }
                        }
                        if (affluenza.Apertura1 != null && affluenza.Apertura1 == 1)
                        {
                            return "Apertura inserita";
                        }
                        break;
                    case "RCO":
                        if (affluenza == null)
                        {
                            return "Costituzione non effettuata usare inserimento";
                        }
                        if (affluenza.Apertura1 != null && affluenza.Apertura1 == 1)
                        {
                            return "Apertura già inserita impossibile annullare costituzione";
                        }
                        break;
                    case "1A":
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza1 != null && affluenza.Affluenza1 == 1)
                        {
                            return "1 Affluenza già registrata";
                        }
                        if ((affluenza.Apertura1 == null) || (affluenza.Apertura1 == 0))
                        {
                            return "Manca Apertura";
                        }
                        break;
                    case "R1A":
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza1 == null || affluenza.Affluenza1 == 0)
                        {
                            return "1 Affluenza non registrata usare inserimento";
                        }
                        if (affluenza.Affluenza2 != null && affluenza.Affluenza2 == 1)
                        {
                            return "2 Affluenza inserita impossibile annullare";
                        }
                        if ((affluenza.Apertura1 == null) || (affluenza.Apertura1 == 0))
                        {
                            return "Manca Apertura";
                        }
                        break;
                    case "2A":
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza2 != null && affluenza.Affluenza2 == 1)
                        {
                            return "2 Affluenza già registrata";
                        }
                        if ((affluenza.Affluenza1 == null) || (affluenza.Affluenza1 == 0))
                        {
                            return "Manca 1 Affluenza";
                        }
                        break;
                    case "R2A":
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza2 == null || affluenza.Affluenza2 == 0)
                        {
                            return "2 Affluenza non registrata usare inserimento";
                        }
                        if ((affluenza.Affluenza1 == null) || (affluenza.Affluenza1 == 0))
                        {
                            return "Manca 1 Affluenza";
                        }
                        if (affluenza.Affluenza3 != null && affluenza.Affluenza3 == 1)
                        {
                            return "3 Affluenza inserita impossibile annullare";
                        }
                        break;
                    case "3C":
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza3 != null && affluenza.Affluenza3 == 1)
                        {
                            return "Chiusura già registrata";
                        }
                        if ((affluenza.Affluenza2 == null) || (affluenza.Affluenza2 == 0))
                        {
                            return "Manca 2 Affluenza";
                        }
                        break;
                    case "R3C":
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza3 == null || affluenza.Affluenza3 == 0)
                        {
                            return "Chiusura non registrata usare inserimento";
                        }
                        break;
                    case "VL":
                        List<VotiLista> lvoti = _votiListaService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, idtipoelezione);
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza3 == null || affluenza.Affluenza3 == 0)
                        {
                            return "Chiusura non registrata";
                        }
                        if (lvoti != null && lvoti.Count > 0)
                        {
                            return "Scrutinio già registrato usare rettifica";
                        }
                        break;
                    case "RVL":
                        List<VotiLista> lvotir = _votiListaService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, idtipoelezione);
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza3 == null || affluenza.Affluenza3 == 0)
                        {
                            return "Chiusura non registrata";
                        }
                        if ((lvotir == null) || (lvotir.Count() == 0))
                        {
                            return "Scrutinio non registrato usare inserimento";
                        }
                        break;
                    case "VS":
                        List<VotiSindaco> lsindaco = _votiSindacoService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, idtipoelezione);
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza3 == null || affluenza.Affluenza3 == 0)
                        {
                            return "Chiusura non registrata";
                        }
                        if (lsindaco != null && lsindaco.Count() > 0)
                        {
                            return "Scrutinio già registrato usare rettifica";
                        }
                        break;
                    case "RVS":
                        List<VotiSindaco> lvotisindacor = _votiSindacoService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, idtipoelezione);
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza3 == null || affluenza.Affluenza3 == 0)
                        {
                            return "Chiusura non registrata";
                        }
                        if ((lvotisindacor == null) || (lvotisindacor.Count() == 0))
                        {
                            return "Scrutinio non registrato usare inserimento";
                        }
                        break;
                    case "PE":
                        List<VotiLista> lvotip = _votiListaService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, idtipoelezione);
                        List<VotiPreferenze> lpreferenze = _votiPreferenzeService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, idtipoelezione);
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza3 == null && affluenza.Affluenza3 == 0)
                        {
                            return "Chiusura non registrata";
                        }
                        if ((lvotip == null) || (lvotip.Count() == 0))
                        {
                            return "Scrutinio non registrato impossibile inserire preferenze";
                        }
                        if (lpreferenze != null && lpreferenze.Count() > 0)
                        {
                            return "Preferenze già registrate usare rettifica";
                        }
                        break;
                    case "RPE":
                        List<VotiPreferenze> rlpreferenze = _votiPreferenzeService.findBySezioneNumerosezioneAndTipoelezioneId(sezione, idtipoelezione);
                        if (affluenza == null)
                        {
                            return "Sezione non costitutita";
                        }
                        if (affluenza.Affluenza3 == null || affluenza.Affluenza3 == 0)
                        {
                            return "Chiusura non registrata";
                        }
                        if ((rlpreferenze == null) || (rlpreferenze.Count() == 0))
                        {
                            return "Preferenze non registrate usare inserimento";
                        }
                        // todo aggiungere controllo preferenze
                        break;

                    default:
                        return "Attenzione configurazione di sistema errata";
                }
            }
            return message;
        }
    }
}
