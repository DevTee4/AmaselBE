using AmaselBE.Model;
using VendolaCore;

namespace AmaselBE.Services
{
    public class AdvertisementRequestService : BaseService<AdvertisementRequest>
    {
        public AdvertisementRequestService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

    }

}