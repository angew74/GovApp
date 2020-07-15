using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Matrice")]
    public partial class Matrice : Entity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Idtipoelezione { get; set; }
        [Column]
        public int? Municipio { get; set; }
        [Column]
        public int? Collegiocamera { get; set; }
        [Column]
        public int? Collegiosenato { get; set; }
        [Column]
        public int? Collegioprovinciale { get; set; }
        [Column]
        public int? Numerosezioni { get; set; }
        [Column]
        public int? Iscrittimaschi { get; set; }
        [Column]
        public int? Iscrittifemmine { get; set; }
        [Column]
        public int? Iscrittitotali { get; set; }

        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
    }
}
