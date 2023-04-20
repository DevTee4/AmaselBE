using AmaselBE.Model;
using VendolaCore;

namespace AmaselBE.Services
{
    public class AdvertisementService : BaseService<Advertisement>
    {
        public AdvertisementService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

        public new List<Advertisement> Save(List<Advertisement> values)
        {
            var prefix = Context.Request.IsHttps ? "https://" : "http://";
            prefix = !Setting.IsProduction ? prefix + Context.Request.Host.Host + "/" : Setting.GatewayURL;
            foreach (var item in values)
            {
                if (item.State == VendolaCore.Model.ObjectState.New)
                {
                    item.Image.Path = prefix + VendolaCore.VendolaCore.SaveFile(item.Image, item.Id);
                }
                else
                {
                    if (item.Image.State == VendolaCore.Model.ObjectState.Changed)
                    {
                        item.Image.Path = prefix + VendolaCore.VendolaCore.SaveFile(item.Image, item.Id);
                    }
                }
            }
            var result = base.Save(values);
            return result;
        }

    }

}