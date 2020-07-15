using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{

    [Table("Raggruppamento")]
    public partial class Raggruppamento : Entity<int>
    {
        public Raggruppamento()
        {
            Liste = new HashSet<Liste>();
            RicalcoloVotiCoalizioni = new HashSet<RicalcoloVotiRaggruppamento>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Idtipoelezione { get; set; }
        [Required]
        [Column]
        public string Denominazione { get; set; }
        [Required]
        [Column]
        public string DenominazioneBreve { get; set; }
        [Required]
        [Column]
        public int Sindacoid { get; set; }

        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
        public virtual Sindaci Sindaco { get; set; }
        public virtual ICollection<Liste> Liste { get; set; }
        public virtual ICollection<RicalcoloVotiRaggruppamento> RicalcoloVotiCoalizioni { get; set; }
    }
}
