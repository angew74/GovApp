using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Sezioni")]
    public partial class Sezioni : Entity<int>
    {
        public Sezioni()
        {
            Affluenze = new HashSet<Affluenze>();
            ProfiloVoti = new HashSet<ProfiloVoti>();
            UsersSezioni = new HashSet<UsersSezioni>();
            VotiGenerali = new HashSet<VotiGenerali>();
            VotiLista = new HashSet<VotiLista>();
            VotiListaStorico = new HashSet<VotiListaStorico>();
            VotiPeferenzeStorico = new HashSet<VotiPeferenzeStorico>();
            VotiPreferenze = new HashSet<VotiPreferenze>();
            VotiSindaco = new HashSet<VotiSindaco>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int Numerosezione { get; set; }
        [Column]
        public int? Idtiposezione { get; set; }
        [Column]
        public int? Idplesso { get; set; }
        [Column]
        public int? Idtipoelezione { get; set; }
        [Column]
        public int? Municipio { get; set; }
        [Required]
        [Column]
        public int Cabina { get; set; }

        public virtual Plessi IdplessoNavigation { get; set; }
        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
        public virtual Tiposezione IdtiposezioneNavigation { get; set; }
        public virtual Iscritti Iscritti { get; set; }
        public virtual ICollection<Affluenze> Affluenze { get; set; }
        public virtual ICollection<ProfiloVoti> ProfiloVoti { get; set; }
        public virtual ICollection<UsersSezioni> UsersSezioni { get; set; }
        public virtual ICollection<VotiGenerali> VotiGenerali { get; set; }
        public virtual ICollection<VotiLista> VotiLista { get; set; }
        public virtual ICollection<VotiListaStorico> VotiListaStorico { get; set; }
        public virtual ICollection<VotiPeferenzeStorico> VotiPeferenzeStorico { get; set; }
        public virtual ICollection<VotiPreferenze> VotiPreferenze { get; set; }
        public virtual ICollection<VotiSindaco> VotiSindaco { get; set; }
    }
}
