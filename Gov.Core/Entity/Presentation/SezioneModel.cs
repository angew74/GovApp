﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Entity.Presentation
{
    public class SezioneModel
    {
        public SezioneModel()
        { }

        public int Sezione { get; set; }

        public string Municipio { get; set; }
        public string NumeroSezione { get; set; }
       
        public int Cabina { get; set; }

        public string Tipo { get; set; }

        public string DescrizioneSezione { get; set; }
        public string DescrizioneElezione { get; set; }

        public IscrittiModel Iscritti { get; set; }
        public string UbicazionePlesso { get; set; }
        public string DescrizionePlesso { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }

        public List<Status> StatusSezione { get; set; }
      
    }

    public class Status
    {
        public string nome { get; set; }
        public string id { get; set; }

        public bool pervenuto { get; set; }
        public string user { get; set; }
        public string dataregistrazione { get; set; }
    }
}
