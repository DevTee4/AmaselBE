using System.Net;
using AmaselBE.Services;
using AmaselBE.Model;
using Microsoft.AspNetCore.Mvc;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Controllers
{
    public class PlatformController : BaseController
    {
        public PlatformService Service { get; set; }
        private readonly ILogger<PlatformController> _logger;

        public PlatformController(ILogger<PlatformController> logger, Setting setting, PlatformService service) : base(setting)
        {
            _logger = logger;
            Service = service;
        }

        [HttpGet("GetAll/{skip?}/{limit?}")]
        public IActionResult GetAll(int skip, int limit) => Ok(Service.Get(skip, limit));

        [HttpGet("GetWithId/{param:length(24)}")]
        public IActionResult GetWithId(string param) => Ok(Service.Get(a => a.Id == param));

        [HttpGet("TestIn")]
        public IActionResult TestIn()
        {
            return Ok("Gotcha ....");
        }

        [HttpGet("PlatformEmpty")]
        public IActionResult PlatformEmpty()
        {
            return Ok(Service.PlatformEmpty());
        }


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

        [HttpPost("SaveUsers")]
        public IActionResult PostUser([FromBody] List<User> values)
        {
            if (values != null)
            {
                var result = Service.SaveUsers(values);
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

        [HttpGet("Search/{param}/{skip?}/{limit?}")]
        public ActionResult<List<Platform>> Search(string param, int skip, int limit) => Ok(Service.Search(param, skip, limit));
        [HttpPost("Search/{skip?}/{limit?}")]
        public ActionResult<List<Platform>> Search([FromBody] List<OperatorKeyValue> param, int skip, int limit)
        {
            var result = Service.Search(param, skip, limit);
            return Ok(result);
        }
    }
}
