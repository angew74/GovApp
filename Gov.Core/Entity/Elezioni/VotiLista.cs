﻿using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Voti_Lista")]
    public partial class VotiLista : AuditableEntity<int>
    {
        public VotiLista()
        {
            ProfiloVoti = new HashSet<ProfiloVoti>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Listaid { get; set; }
        [Required]
        [Column]
        public int Sezioneid { get; set; }
        [Required]
        [Column]
        public int Tipoelezioneid { get; set; }
      
        [Column]
        public int? Municipio { get; set; }
        [Required]
        [Column]
        public int Voti { get; set; }
        [Required]
        [Column]
        public int Votigeneraliid { get; set; }

        [Required]
        [Column]
        public int VotiSindacoid { get; set; }


        public virtual Liste Lista { get; set; }
        public virtual Sezioni Sezione { get; set; }
        public virtual Tipoelezione Tipoelezione { get; set; }
        public virtual VotiGenerali Votigenerali { get; set; }
        public virtual VotiSindaco VotiSindaco { get; set; }
        public virtual ICollection<ProfiloVoti> ProfiloVoti { get; set; }
    }
}
