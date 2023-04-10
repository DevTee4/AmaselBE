using System.Dynamic;
using AmaselBE.Model;
using Newtonsoft.Json.Linq;
using VendolaCore;
using VendolaCore.HttpRequest;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class PlatformService : BaseService<Platform>
    {
        public PlatformService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

        public bool PlatformEmpty()
        {
            var result = Count() > 0 ? false : true;
            return result;
        }

        public new List<Platform> Save(List<Platform> values)
        {
            var result = base.Save(values);
            var authUsers = new List<User>();
            result.ForEach(platform =>
            {
                var user = new User();
                user.MailAddress = platform.MailAddress;
                user.Code = platform.Id;
                user.Name = platform.Name;
                user.UserTypeId = platform.Id;
                user.Password = platform.Password;
                user.UserType = UserType.Platform;
                user.LastCallTime = DateTimeOffset.Now;
                user.Id = platform.Id;
                user.Tenant = platform.Tenant;
                authUsers.Add(user);
            });
            //create User on Gateway
            try
            {
                var response = HttpWebRequest<AuthUser>.Post($"{Setting.AuthenticationURL}/Auth/SaveUsers/{nameof(Platform)}", authUsers, GetToken(), GetTenant());
                var vendolaCore = new VendolaCore.VendolaCore();
                authUsers.ForEach(s =>
                {
                    var found = result.Find(f => f.Id == s.Code);
                    if (string.IsNullOrEmpty(found.Token))
                    {
                        found.Token = vendolaCore.GetTokenFromUser(s);
                    }
                    found.Password = "";
                });
                return result;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        public static void SeedDB(Setting setting1, HttpContext context)
        {
            var platformService = new PlatformService(setting1, new HttpContextAccessor { HttpContext = context });
            platformService.Tenant = "Vendola";
            var res = platformService.Get(s => s.MailAddress == "vendola@gmail.com").Result.ToList();
            if (res.Count == 0)
            {
                var platform = new Platform
                {
                    MailAddress = "vendola@gmail.com",
                    Password = "adfba795fd8a0b127816a8ec28497598",
                    Name = "Vendola",
                    Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    Code = "VEND-001",
                    PhoneNumber = "080",
                    Tenant = "Vendola"
                };
                platformService.Token = new VendolaCore.VendolaCore().GetTokenFromUser(null);
                platformService.Tenant = "Vendola";
                platformService.Save(new List<Platform> { platform });
            }
        }
    }

}