using AmaselBE.Model;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class PromoService : BaseService<Promo>
    {
        public PromoService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

        public new List<Promo> Save(List<Promo> values)
        {
            base.Save(values);
            return values;
        }

    }

}