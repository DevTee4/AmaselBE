using AmaselBE.Configuration;
using Microsoft.AspNetCore.Mvc;
namespace AmaselBE.Controllers
{
    [Route("api/[controller]")]
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