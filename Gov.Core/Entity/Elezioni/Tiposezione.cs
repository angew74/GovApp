using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Tipo_Sezione")]
    public partial class Tiposezione : Entity<int>
    {
        public Tiposezione()
        {
            Iscritti = new HashSet<Iscritti>();
            Sezioni = new HashSet<Sezioni>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public string Descrizione { get; set; }
        [Required]
        [Column]
        public string Codicesezione { get; set; }

        public virtual ICollection<Iscritti> Iscritti { get; set; }
        public virtual ICollection<Sezioni> Sezioni { get; set; }
    }
}
