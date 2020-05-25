using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Core.Enumerators
{
    public enum UserAuditEventType
    {
        Login,
        LogOut,
        FailedLogin,
        Error
    }
}
