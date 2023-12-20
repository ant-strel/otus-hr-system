using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {
        }
        public ApplicationUser(string password)
        {
            _rawPassword = password;
        }

        private readonly string _rawPassword;
        public string GetRawPassword()
        {
            return _rawPassword;
        }
    }
}
