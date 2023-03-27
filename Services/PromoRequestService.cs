using AmaselBE.Model;
using VendolaCore;

namespace AmaselBE.Services
{
    public class PromoRequestService : BaseService<PromoRequest>
    {
        public PromoRequestService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

    }

}