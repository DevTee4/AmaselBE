using AmaselBE.Model;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class RatingService : BaseService<Rating>
    {
        public RatingService(Setting setting, IHttpContextAccessor context, User user) : base(setting, context.HttpContext, user)
        {

        }

    }

}