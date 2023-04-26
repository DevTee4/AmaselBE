using AmaselBE.Model;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class PromoRequestService : BaseService<PromoRequest>
    {
        PromoService promoService;
        public PromoRequestService(Setting setting, IHttpContextAccessor context, PromoService promoService, User user) : base(setting, context.HttpContext, user)
        {
            this.promoService = promoService;
        }

        public new List<PromoRequest> Save(List<PromoRequest> values)
        {
            var promotoUpdateWithApprove = values.Where(w => w.State == ObjectState.Changed && w.Status == PromoRequestStatus.Approved && !string.IsNullOrWhiteSpace(w.ApprovedBy)).ToList();
            var result = base.Save(values);
            if (promotoUpdateWithApprove.Count > 0)
            {
                var promos = promotoUpdateWithApprove.Select(s =>
                {
                    var promo = new Promo
                    {
                        Title = s.Name,
                        FromDate = s.FromDate,
                        OwnerType = s.OwnerType,
                        ToDate = s.ToDate,
                        Status = PromoStatus.Pending,
                        Customers = s.Customers,
                        Sellers = s.Sellers,
                        PromoRequest = s.Id
                    };
                    return promo;
                }).ToList();
                promoService.Save(promos);
            }
            return result;
        }

    }

}