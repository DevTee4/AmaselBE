using AmaselBE.Model;
using VendolaCore;

namespace AmaselBE.Services
{
    public class AdvertisementService : BaseService<Advertisement>
    {
        public AdvertisementService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

    }

}