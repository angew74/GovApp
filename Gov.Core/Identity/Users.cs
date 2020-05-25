using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gov.Core.Identity
{
    [Table("Users")]
    public partial class Users
    {
        public Users()
        {
            Userclaims = new HashSet<IdentityUserClaim<int>>();
            Userlogins = new HashSet<IdentityUserLogin<int>>();
            UserRoles = new HashSet<ApplicationUserRole>();
            UsersExtended = new HashSet<UserExtended>();           
            Usertokens = new HashSet<Usertokens>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column]
        [MaxLength(16)]
        public string UserName { get; set; }
        [Required]
        [Column]
        [MaxLength(16)]
        public string NormalizedUserName { get; set; }
        [Required]
        [Column]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [Column]
        [MaxLength(100)]
        public string NormalizedEmail { get; set; }
        [Required]
        [Column]
        public bool EmailConfirmed { get; set; }
        [Required]
        [Column]
        [MaxLength(100)]
        public string PasswordHash { get; set; }
       
        [Column]
        [MaxLength(100)]
        public string SecurityStamp { get; set; }
        
        [Column]
        [MaxLength(100)]
        public string ConcurrencyStamp { get; set; }
       
        [Column]
        public string PhoneNumber { get; set; }
       
        [Column]
       public bool PhoneNumberConfirmed { get; set; }
        [Required]
        [Column]
        public bool TwoFactorEnabled { get; set; }
     
        [Column]
        public DateTime? LockoutEnd { get; set; }
        [Required]
        [Column]
        public bool LockoutEnabled { get; set; }
       
        [Column]
        public int AccessFailedCount { get; set; }
        [Required]
        [Column]
        public DateTime LastModified { get; set; }

        public virtual ICollection<IdentityUserClaim<int>> Userclaims { get; set; }
        public virtual ICollection<IdentityUserLogin<int>> Userlogins { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<UserExtended> UsersExtended { get; set; }    
        public virtual ICollection<Usertokens> Usertokens { get; set; }
    }
}
