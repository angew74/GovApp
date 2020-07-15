using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{

    [Table("Ricalcoli_AperturaCostituzione")]
    public partial class RicalcoloAperturaCostituzione : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Idtipoelezione { get; set; }
        [Required]
        [Column]
        public int Idtiporicalcolo { get; set; }
        [Required]
        [Column]
        public int NumeroSezioni { get; set; }
        [Required]
        [Column]
        public int NumeroCostituite { get; set; }
        [Required]
        [Column]
        public string PercentualeCostituite { get; set; }
        [Required]
        [Column]
        public int NumeroAperte { get; set; }
        [Required]
        [Column]
        public string PercentualeAperte { get; set; }
        [Required]
        [Column]
        public int IscrittiTotali { get; set; }
        [Required]
        [Column]
        public int Municipio { get; set; }
     

        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
        public virtual TipoRicalcolo IdtiporicalcoloNavigation { get; set; }

       [NotMapped]
       public int Sezione { get; set; }

        [NotMapped]
       public string Status { get; set; }

        [NotMapped]
       public int iscrittiMaschi { get; set; }

        [NotMapped]
       public int iscrittiFemmine { get; set; }

        [NotMapped]
        public int iscrittiTotali { get; set; }
    }
}
