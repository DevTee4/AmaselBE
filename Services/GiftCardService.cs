using AmaselBE.Model;
using VendolaCore;

namespace AmaselBE.Services
{
    public class GiftCardService : BaseService<GiftCard>
    {
        public GiftCardService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

    }

}