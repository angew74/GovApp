using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Tipo_Sezione")]
    public partial class Tipoelezione : Entity<int>
    {
        public Tipoelezione()
        {
            AggregazioneInterrogazioni = new HashSet<AggregazioneInterrogazioni>();
            Candidati = new HashSet<Candidati>();
            Raggruppamenti = new HashSet<Raggruppamento>();
            Elegen = new HashSet<Elegen>();
            Iscritti = new HashSet<Iscritti>();
            Liste = new HashSet<Liste>();
            Matrice = new HashSet<Matrice>();
            Plessi = new HashSet<Plessi>();
            ProfiloVoti = new HashSet<ProfiloVoti>();
            RicalcoliAffluenza = new HashSet<RicalcoliAffluenza>();
            RicalcoliAperturaCostituzione = new HashSet<RicalcoloAperturaCostituzione>();
            RicalcoloPreferenze = new HashSet<RicalcoloPreferenze>();
            RicalcoloVotiCoalizioni = new HashSet<RicalcoloVotiRaggruppamento>();
            RicalcoloVotiLista = new HashSet<RicalcoloVotiLista>();
            RicalcoloVotiSindaco = new HashSet<RicalcoloVotiSindaco>();
            Sezioni = new HashSet<Sezioni>();
            Sindaci = new HashSet<Sindaci>();
            TipoInterrogazione = new HashSet<TipoInterrogazione>();
            TipoRicalcolo = new HashSet<TipoRicalcolo>();
            TipoRicalcoloAggregazione = new HashSet<TipoRicalcoloAggregazione>();
            UsersSezioni = new HashSet<UsersSezioni>();
            VotiGenerali = new HashSet<VotiGenerali>();
            VotiLista = new HashSet<VotiLista>();
            VotiPeferenzeStorico = new HashSet<VotiPeferenzeStorico>();
            VotiPreferenze = new HashSet<VotiPreferenze>();
            VotiSindaco = new HashSet<VotiSindaco>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public string Descrizione { get; set; }
        [Required]
        [Column]
        public string Dataelezione { get; set; }

        public virtual ICollection<AggregazioneInterrogazioni> AggregazioneInterrogazioni { get; set; }
        public virtual ICollection<Candidati> Candidati { get; set; }
        public virtual ICollection<Raggruppamento> Raggruppamenti { get; set; }
        public virtual ICollection<Elegen> Elegen { get; set; }
        public virtual ICollection<Iscritti> Iscritti { get; set; }
        public virtual ICollection<Liste> Liste { get; set; }
        public virtual ICollection<Matrice> Matrice { get; set; }
        public virtual ICollection<Plessi> Plessi { get; set; }
        public virtual ICollection<ProfiloVoti> ProfiloVoti { get; set; }
        public virtual ICollection<RicalcoliAffluenza> RicalcoliAffluenza { get; set; }
        public virtual ICollection<RicalcoloAperturaCostituzione> RicalcoliAperturaCostituzione { get; set; }
        public virtual ICollection<RicalcoloPreferenze> RicalcoloPreferenze { get; set; }
        public virtual ICollection<RicalcoloVotiRaggruppamento> RicalcoloVotiCoalizioni { get; set; }
        public virtual ICollection<RicalcoloVotiLista> RicalcoloVotiLista { get; set; }
        public virtual ICollection<RicalcoloVotiSindaco> RicalcoloVotiSindaco { get; set; }
        public virtual ICollection<Sezioni> Sezioni { get; set; }
        public virtual ICollection<Sindaci> Sindaci { get; set; }
        public virtual ICollection<TipoInterrogazione> TipoInterrogazione { get; set; }
        public virtual ICollection<TipoRicalcolo> TipoRicalcolo { get; set; }
        public virtual ICollection<TipoRicalcoloAggregazione> TipoRicalcoloAggregazione { get; set; }
        public virtual ICollection<UsersSezioni> UsersSezioni { get; set; }
        public virtual ICollection<VotiGenerali> VotiGenerali { get; set; }
        public virtual ICollection<VotiLista> VotiLista { get; set; }
        public virtual ICollection<VotiPeferenzeStorico> VotiPeferenzeStorico { get; set; }
        public virtual ICollection<VotiPreferenze> VotiPreferenze { get; set; }
        public virtual ICollection<VotiSindaco> VotiSindaco { get; set; }
    }
}
