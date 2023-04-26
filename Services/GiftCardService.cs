using AmaselBE.Model;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class GiftCardService : BaseService<GiftCard>
    {
        public GiftCardService(Setting setting, IHttpContextAccessor context, User user) : base(setting, context.HttpContext, user)
        {

        }

    }

}