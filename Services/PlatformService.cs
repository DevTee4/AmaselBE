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
        public PlatformService(Setting setting, IHttpContextAccessor context, User user) : base(setting, context.HttpContext, user)
        {

        }

        public bool PlatformEmpty()
        {
            var result = Count() > 0 ? false : true;
            return result;
        }
        public List<User> SaveUsers(List<User> values, string token = "")
        {
            //create User on Gateway
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    token = GetToken();
                }
                var response = HttpWebRequest<AuthUser>.Post($"{Setting.AuthenticationURL}/Auth/Save/{nameof(Platform)}", values, token);
                var vendolaCore = new VendolaCore.VendolaCore();
                values.ForEach(s =>
                {
                    var found = values.Find(f => f.Id == s.Id);
                    // if (string.IsNullOrEmpty(found.Token))
                    // {
                    //     found.Token = vendolaCore.GenerateJWT(s);
                    // }
                    found.Password = "";
                });
                return values;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
        public new List<Platform> Save(List<Platform> values, string token)
        {
            var authUsers = new List<User>();
            values.ForEach(platform =>
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
                user.PhoneNumber = platform.PhoneNumber;
                authUsers.Add(user);
            });
            var result = base.Save(values);
            SaveUsers(authUsers, token);
            return result;
        }

        public static void SeedDB(Setting setting1, HttpContext context)
        {
            var platformService = new PlatformService(setting1, new HttpContextAccessor { HttpContext = context }, null);
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
                platformService.Save(new List<Platform> { platform }, VendolaCore.VendolaCore.DefaultToken);
            }
        }
    }

}