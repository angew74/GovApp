using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core
{
    [Table("Ministero")]
    public class Ministero : AuditableEntity<int>
    {
   

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
              
        [Column]
        [MaxLength(256)]
        [Required]
        public string Denominazione { get; set; }

        [Column]
        [Required]
        public DateTime DataIstituzione { get; set; }
        [Column]
        [Required]
        public DateTime DataCessazione { get; set; }
        [Column]
        [Required]
        public bool IsSenzaPortafoglio { get; set; }
    }
}
