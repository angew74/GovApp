using Gov.Core.Identity;
using Gov.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Entity.Elezioni
{
    [Table("Users_Sezioni")]
    public partial class UsersSezioni : Entity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        [Required]
        [Column]
        public int UserId { get; set; }
      
        [Column]
        public int? Sezioneid { get; set; }
       
        [Column]
        public int? Idtipoelezione { get; set; }

        public virtual Tipoelezione IdtipoelezioneNavigation { get; set; }
        public virtual Sezioni Sezione { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
