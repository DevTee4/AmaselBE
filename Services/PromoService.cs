using AmaselBE.Model;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class PromoService : BaseService<Promo>
    {
        public PromoService(Setting setting, IHttpContextAccessor context, User user) : base(setting, context.HttpContext, user)
        {

        }

        public new List<Promo> Save(List<Promo> values)
        {
            base.Save(values);
            return values;
        }

    }

}