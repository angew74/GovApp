﻿using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Gov.Core
{
    [Table("Premier")]
    public class Premier : AuditableEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Column]
        [MaxLength(256)]
        [Required]
        public string Nome { get; set; }
        [Column]
        [MaxLength(256)]
        [Required]
        public string Cognome { get; set; }
        [Column]
        [Required]
        public DateTime DataNascita { get; set; }

        public List<Militanza> Militanze { get; set; }

    }
}
