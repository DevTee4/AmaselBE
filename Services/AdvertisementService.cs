using AmaselBE.Model;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class AdvertisementService : BaseService<Advertisement>
    {
        public AdvertisementService(Setting setting, IHttpContextAccessor context, User user) : base(setting, context.HttpContext, user)
        {

        }

        public new List<Advertisement> Save(List<Advertisement> values)
        {
            foreach (var item in values)
            {
                if (item.State == VendolaCore.Model.ObjectState.New)
                {
                    item.File.Url = VendolaCore.VendolaCore.SaveFile(item.File, item.Id, Setting);
                }
                else
                {
                    if (item.File.State == VendolaCore.Model.ObjectState.Changed)
                    {
                        item.File.Url = VendolaCore.VendolaCore.SaveFile(item.File, item.Id, Setting);
                    }
                }
            }
            var result = base.Save(values);
            return result;
        }

    }

}