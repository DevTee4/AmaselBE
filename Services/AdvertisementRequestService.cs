using AmaselBE.Model;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class AdvertisementRequestService : BaseService<AdvertisementRequest>
    {
        AdvertisementService advertisementService;
        public AdvertisementRequestService(Setting setting, IHttpContextAccessor context, User user, AdvertisementService advertisementService) : base(setting, context.HttpContext, user)
        {
            this.advertisementService = advertisementService;
        }
        public new List<AdvertisementRequest> Save(List<AdvertisementRequest> values)
        {
            var advertToUpdateWithApprove = values.Where(w => w.State == ObjectState.Changed && w.Status == AdvertisementRequestStatus.Approved && !string.IsNullOrWhiteSpace(w.ApprovedBy)).ToList();
            var result = base.Save(values);
            if (advertToUpdateWithApprove.Count > 0)
            {
                var adverts = advertToUpdateWithApprove.Select(s =>
                {
                    var advert = new Advertisement
                    {
                        ValidFrom = s.FromDate,
                        File = s.Image,
                        ValidTo = s.ToDate,
                        AdvertType = s.AdvertType,
                        Url = s.Url,
                        Name = s.Title,
                        Tenant = s.Tenant,
                        Request = s.Id,
                        Seller = s.Seller
                    };
                    return advert;
                }).ToList();
                advertisementService.Save(adverts);
            }
            return result;
        }


    }

}