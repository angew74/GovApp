using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core
{
    [Table("Partito")]
    public class Partito : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Column]
        [MaxLength(256)]
        public string Nome { get; set; }
        public virtual Coalizione? Coalizione {get;set;}
    }
}
