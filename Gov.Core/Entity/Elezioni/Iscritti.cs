using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Iscritti")]
    public partial class Iscritti : Entity<int>
    {
        public Iscritti()
        {
            Affluenze = new HashSet<Affluenze>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Idtipoelezione { get; set; }
        [Required]
        [Column]
        public int Idsezione { get; set; }
      
        [Column]
        public int? Idtiposezione { get; set; }
        [Required]
        [Column]
        public int Municipio { get; set; }
        [Required]
        [Column]
        public int Collegiocamera { get; set; }
        [Required]
        [Column]
        public int Collegiosenato { get; set; }
        [Required]
        [Column]
        public int Collegioprovinciale { get; set; }
        [Required]
        [Column]
        public int Cabina { get; set; }
        [Required]
        [Column]
        public int Iscrittimaschi { get; set; }
        [Required]
        [Column]
        public int Iscrittifemmine { get; set; }
        [Required]
        [Column]
        public int Iscrittitotali { get; set; }
        [Required]
        [Column]
        public int Iscrittimaschiue { get; set; }
        [Required]
        [Column]
        public int Iscrittifemmineue { get; set; }
        [Required]
        [Column]
        public int Iscrittitotaliue { get; set; }
        [Required]
        [Column]
        public int Iscrittimaschigen { get; set; }
        [Required]
        [Column]
        public int Iscrittifemminegen { get; set; }
        [Required]
        [Column]
        public int Iscrittitotaligen { get; set; }

        public virtual Sezioni IdsezioneNavigation { get; set; }
        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
        public virtual Tiposezione IdtiposezioneNavigation { get; set; }
        public virtual ICollection<Affluenze> Affluenze { get; set; }
    }
}
