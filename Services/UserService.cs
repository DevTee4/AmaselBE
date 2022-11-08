using AmaselBE.Configuration;
using AmaselBE.Model;

namespace AmaselBE.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(Setting setting) : base(setting)
        {

        }

    }

}