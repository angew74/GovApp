using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovApp.Models
{
    public class SignInResultMock : SignInResult
    {


        //
        // Riepilogo:
        //     Returns an Microsoft.AspNetCore.Identity.IdentityResult indicating a successful
        //     identity operation.
        //
        // Valori restituiti:
        //     An Microsoft.AspNetCore.Identity.IdentityResult indicating a successful operation.
        public static IdentityResult Success { get; set; }
        //
        // Riepilogo:
        //     An System.Collections.Generic.IEnumerable`1 of Microsoft.AspNetCore.Identity.IdentityErrors
        //     containing an errors that occurred during the identity operation.
        public IEnumerable<IdentityError> Errors { get; set; }
        //
        // Riepilogo:
        //     Flag indicating whether if the operation succeeded or not.
        public bool Succeeded { get; set; }

    }
}
