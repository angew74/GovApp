using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core.Entity
{
    [Table("Contenuto")]
    public class Contenuto : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Column]
        [Required]
        [MaxLength(256)]
        public string ContentuoCard { get; set; }


        [Column]
        [MaxLength(10)]
        public string Tipo { get; set; }

        public virtual TipoContenuto TipoConenuto {get;set;}
        public virtual Pagina Pagina { get; set; }
        [Column]
        public int PaginaId { get; set; }
        [Column]
        public int TipoContenutoId { get; set; }
    }
}
