using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Identity
{
    [Table("UserLogins")]
    public partial class Userlogins : IdentityUserLogin<int>
    {
      /*  [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }*/
        [Required]
        [Column]
        public override string LoginProvider { get; set; }
        [Required]
        [Column]
        public override string ProviderKey { get; set; }
        [Required]
        [Column]
        public override string ProviderDisplayName { get; set; }       

    }
}
