using Microsoft.AspNetCore.Mvc;
using VendolaCore;

namespace AmaselBE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        // public BaseService Service { get; set; }
        public Setting setting { get; set; }
        public BaseController(Setting setting)
        {
            this.setting = setting;
        }
        public string GetUrlFromRequest()
        {
            var p = Request.Headers["Referer"].ToString();
            var k = p.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1];
            var t = k.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (string.IsNullOrWhiteSpace(t[0]))
            {
                return null;
            }
            return t[0];
        }
    }
}