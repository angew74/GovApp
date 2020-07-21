using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Fase_Elezione")]
    public partial class FaseElezione : Entity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public string Codice { get; set; }
        [Required]
        [Column]
        public string Descrizione { get; set; }
        [Required]
        [Column]
        public int Abilitata { get; set; }
        [Required]
        [Column]
        public int Idtipoelezione { get; set; }

        public string Categoria { get; set; }

        public bool IsAbilitata
        {
            get
            {
                if (Abilitata == 0) { return false; }else { return true; }
                
            }
        }
    }
}
