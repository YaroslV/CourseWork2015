using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;


namespace course.IdentityExtensions
{
    public class CustomPasswordValidator : IIdentityValidator<string>
    {
        public Task<IdentityResult> ValidateAsync(string item)
        {
            if (!IsStrongPassword(item))
                return Task.FromResult(IdentityResult.Failed("Поганий пароль. Хороший пароль містить мінімум 1 велику літеру, 1 маленьку літеру, 1 цифру і жодного символа"));
            else
                return Task.FromResult(IdentityResult.Success);
        }

        private static bool IsStrongPassword(string password)
        {
            // Minimum and Maximum Length of field - 6 to 12 Characters
            if (password.Length < 6 || password.Length > 12)
                return false;

            // Special Characters - Not Allowed
            // Spaces - Not Allowed
            if (!(password.All(c => char.IsLetter(c) || char.IsDigit(c))))
                return false;

            // Numeric Character - At least one character
            if (!password.Any(c => char.IsDigit(c)))
                return false;

            // At least one Capital Letter
            if (!password.Any(c => char.IsUpper(c)))
                return false;           

            return true;
        }
    }
}