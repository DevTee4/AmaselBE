using System.Net;
using AmaselBE.Services;
using AmaselBE.Model;
using Microsoft.AspNetCore.Mvc;
using VendolaCore;

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
