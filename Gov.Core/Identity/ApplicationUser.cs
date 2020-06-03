using Gov.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gov.Core.Identity
{
   
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public ApplicationUser()
        {
            Userclaims = new HashSet<Userclaims>();
            Userlogins = new HashSet<Userlogins>();        
            Usertokens = new HashSet<Usertokens>();
        }
     

        [NotMapped]
        public bool enabled { get; set; }
        public string CustomTag { get; set; }
    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int Id { get; set; }
        [DataMember]
        public string CodiceFiscale { get; set; }
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Cognome { get; set; }
        [DataMember]
        public string Sesso { get; set; }
        [DataMember]
        public string Password { get; set; }
        [Column]
        public DateTime? LockoutEnd { get; set; }
        [Required]
        [Column]
        public override bool LockoutEnabled { get; set; }

        [Column]
        public override int AccessFailedCount { get; set; }
        [Required]
        [Column]
        public DateTime LastModified { get; set; }

        public List<ApplicationRole> Roles { get; set; }
        public virtual ICollection<Userclaims> Userclaims { get; set; }
        public virtual ICollection<Userlogins> Userlogins { get; set; }
          
        public virtual ICollection<Usertokens> Usertokens { get; set; }
    }
}
