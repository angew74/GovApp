using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Identity
{
    [Table("UserExtended")]
    public partial class UserExtended
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column]
        [MaxLength(1)]
        public string Sesso { get; set; }
        [Required]
        [Column]
        [MaxLength(150)]
        public string Nome { get; set; }
        [Required]
        [Column]
        [MaxLength(150)]
        public string Cognome { get; set; }
        [Required]
        [Column]
        [MaxLength(16)]
        public string Codicefiscale { get; set; }
        [Required]
        [Column]      
        public int Userid { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
