using AmaselBE.Configuration;
using AmaselBE.Model;

namespace AmaselBE.Services
{
    public class AuthUserService : BaseService<AuthUser>
    {
        public AuthUserService(Setting setting) : base(setting)
        {

        }

    }

}