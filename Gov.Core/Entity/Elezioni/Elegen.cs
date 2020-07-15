using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Elegen")]
    public partial class Elegen : Entity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Idtipoelezione { get; set; }
        [Required]
        [Column]
        public int Numerosezioni { get; set; }
       
        [Column]
        public int? Numeroliste { get; set; }
        [Required]
        [Column]
        public int Annoelezione { get; set; }
        [Required]
        [Column]
        public string Giornocostituzione { get; set; }
        [Required]
        [Column]
        public string Giornovotazione1 { get; set; }
        [Required]
        [Column]
        public string Giornovotazione2 { get; set; }

        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
    }
}
