﻿using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Voti_GeneraliStorico")]
    public partial class VotiGeneraliStorico : AuditableEntity<int>
    {
        public VotiGeneraliStorico()
        {
            ProfiloVoti = new HashSet<ProfiloVoti>();
            VotiListaStorico = new HashSet<VotiListaStorico>();
            VotiSindacoStorico = new HashSet<VotiSindacoStorico>();
        }

        [Key]         
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Sezioneid { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }
        [Required]
        [Column]
        public int Municipio { get; set; }
        [Required]
        [Column]
        public int Contestate { get; set; }
        [Required]
        [Column]
        public int Bianche { get; set; }
        [Required]
        [Column]
        public int Nulle { get; set; }
        [Required]
        [Column]
        public int TotaleValide { get; set; }
        [Required]
        [Column]
        public int SoloSindaco { get; set; }
        [Required]
        [Column]
        public int Totale { get; set; }
        [Required]
        [Column]
        public int Iscritti { get; set; }
        [Required]
        [Column]
        public DateTime DataOperazioneOld { get; set; }
        [Required]
        [Column]
        public string UtenteOperazioneOld { get; set; }
     

        public virtual Sezioni Sezione { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }

        public virtual ICollection<ProfiloVoti> ProfiloVoti { get; set; }
        public virtual ICollection<VotiListaStorico> VotiListaStorico { get; set; }
        public virtual ICollection<VotiSindacoStorico> VotiSindacoStorico { get; set; }
    }
}
