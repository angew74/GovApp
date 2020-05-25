using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Identity
{
    
    [Table("UserTokens")]
    public partial class Usertokens : IdentityUserToken<int>
    {
      /*  [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }*/       
        [Required]
        [Column]
        public override string LoginProvider { get; set; }
        [Required]
        [Column]
        public override string Name { get; set; }
        [Required]
        [Column]
        public override string Value { get; set; }
   
    }
}
