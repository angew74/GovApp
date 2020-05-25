using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Identity
{
    [Table("UserClaims")]
    public partial class Userclaims :IdentityUserClaim<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }       
        [Required]
        [Column]
        public override string ClaimType { get; set; }
        [Required]
        [Column]
        public override string ClaimValue { get; set; }
    }
}
