using Gov.Core.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Gov.Structure.Extensions
{
   public class GeneratePassword
    {
        public static string GetOne(IdentityOptions options)
        {
            PasswordOptions passwordOptions = options.Password;
           
                if (passwordOptions == null)
                    return null;                           
                bool requireDigit = passwordOptions.RequireDigit;
                bool requireLowercase = passwordOptions.RequireLowercase;
                bool requireUppercase = passwordOptions.RequireUppercase;               
                string randomPassword = string.Empty;
                int passwordLength = passwordOptions.RequiredLength;

                Random random = new Random();
                while (randomPassword.Length != passwordLength)
                {
                    int randomNumber = random.Next(48, 122);  // >= 48 && < 122 
                    if (randomNumber == 95 || randomNumber == 96) continue;  // != 95, 96 _'

                    char c = Convert.ToChar(randomNumber);

                    if (requireDigit)
                        if (char.IsDigit(c))
                            requireDigit = false;

                    if (requireLowercase)
                        if (char.IsLower(c))
                            requireLowercase = false;

                    if (requireUppercase)
                        if (char.IsUpper(c))
                            requireUppercase = false;                    

                    randomPassword += c;
                }

                if (requireDigit)
                    randomPassword += Convert.ToChar(random.Next(48, 58));  // 0-9

                if (requireLowercase)
                    randomPassword += Convert.ToChar(random.Next(97, 123));  // a-z

                if (requireUppercase)
                    randomPassword += Convert.ToChar(random.Next(65, 91));  // A-Z
                
            
            return randomPassword;
        }
    }
}
