using System.Net;
using AmaselBE.Configuration;
using AmaselBE.Services;
using AmaselBE.Model;
using AmaselBE.Lib;
using Microsoft.AspNetCore.Mvc;

namespace AmaselBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : BaseController
    {
        public PlatformService Service { get; set; }
        public UserService userService { get; set; }
        private readonly ILogger<PlatformController> _logger;

        public PlatformController(ILogger<PlatformController> logger, Setting setting) : base(setting)
        {
            _logger = logger;
            Service = new PlatformService(setting);
            userService = new UserService(setting);
        }

        [HttpGet("GetAll/{skip?}/{limit?}")]
        public IActionResult GetAll() => Ok(Service.Get());

        [HttpGet("GetWithId/{param:length(24)}")]
        public IActionResult GetWithId(string param) => Ok(Service.Get(a => a.Id == param));

        [HttpPost("Save")]
        public IActionResult Post([FromBody] List<Platform> values)
        {
            if (values != null)
            {
                var result = Service.Save(values);
                return Ok(result);
            }
            return NotFound(new Error { ErrorMsg = "No data recieved", StatusCode = (int)HttpStatusCode.Forbidden });
        }

        [HttpPost("SavePlatformUser")]
        public IActionResult PostUser([FromBody] List<Platform> values)
        {
            if (values != null)
            {
                var result = Service.Save(values);
                List<User> users = new List<User>();
                if (result != null)
                {
                    result.ForEach(platform =>
                      {
                          var user = new User();
                          user.MailAddress = platform.MailAddress;
                          user.Name = platform.Name;
                          user.Active = platform.Active;
                          user.UserType = UserType.Platform;
                          user.Code = platform.Code;
                          users.Add(user);
                          userService.Save(users);
                      });
                }
                return Ok(result);
            }
            return NotFound(new Error { ErrorMsg = "No data recieved", StatusCode = (int)HttpStatusCode.Forbidden });
        }


        [HttpDelete("Delete/{ids}")]
        public IActionResult Delete([FromQuery] string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
            {
                return NotFound(new Error { ErrorMsg = "Server didnt recieve any delete data", StatusCode = (int)HttpStatusCode.Forbidden });
            }
            Service.Remove(ids);
            return NoContent();
        }

        [HttpGet("Search/{param}")]
        public ActionResult<List<Platform>> Search(string param) => Ok(Service.Search(param));

    }
}
