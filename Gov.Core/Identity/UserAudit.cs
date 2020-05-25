using Gov.Core.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Identity
{
    [Table("UserAudit")]

    public class UserAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserAuditId { get; set; }
        [Required]
        [Column]
        public int IdUtente { get; set; }
        [Required]
        [Column]
        public int AuditEvent { get; set; }
        [Required]
        [Column]
        public string IpAddress { get; set; }
        [Required]
        [Column]
        public DateTime TimeStamp { get; set; }      

        public static UserAudit CreateAuditEvent(int id, UserAuditEventType auditEventType, string ip)
        {

            UserAudit userAudit = new UserAudit();        
            userAudit.IdUtente = id;
            userAudit.IpAddress = ip;
            userAudit.TimeStamp = DateTime.Now;
            userAudit.AuditEvent = (int)auditEventType;
            return userAudit;
        }

    }
}
