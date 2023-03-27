using AmaselBE.Model;
using VendolaCore;

namespace AmaselBE.Services
{
    public class RatingService : BaseService<Rating>
    {
        public RatingService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

    }

}