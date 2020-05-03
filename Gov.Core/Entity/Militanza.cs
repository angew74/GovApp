using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core
{
    [Table("Militanza")]
    public class Militanza : AuditableEntity<int>
    {
   

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Column]
        public DateTime? DataInizio { get; set; }
        [Column]
        public DateTime? DataFine { get; set; }

        public Partito Partito { get; set; }


    }
}
