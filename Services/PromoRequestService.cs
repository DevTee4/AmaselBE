using AmaselBE.Model;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class PromoRequestService : BaseService<PromoRequest>
    {
        public PromoRequestService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

    }

}