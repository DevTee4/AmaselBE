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
    public class AttachmentController : BaseController
    {
        public AttachmentService Service { get; set;} 
        private readonly ILogger<AttachmentController> _logger;

    public AttachmentController(ILogger<AttachmentController> logger,Setting setting):base(setting)
    {
            _logger = logger;
           Service= new AttachmentService(setting);
    }

    [HttpGet("GetAll/{skip?}/{limit?}")]
    public IActionResult GetAll() =>  Ok (Service.Get());
    
    [HttpGet ("GetWithId/{param:length(24)}")]
    public IActionResult GetWithId (string param) => Ok (Service.Get(a=>a.Id==param));
 
    [HttpPost ("Save")]
    public IActionResult Post ([FromBody] List<Attachment> values) {
      if (values != null) {
        var result = Service.Save(values);
        return Ok (result);
      }
      return NotFound (new Error { ErrorMsg = "No data recieved", StatusCode = (int)HttpStatusCode.Forbidden });
    }

 
    [HttpDelete ("Delete/{ids}")]
    public IActionResult Delete ([FromQuery] string  ids) {
      if (string.IsNullOrWhiteSpace (ids)) {
        return NotFound (new Error { ErrorMsg = "Server didnt recieve any delete data", StatusCode = (int)HttpStatusCode.Forbidden});
      }
      Service.Remove(ids);
      return NoContent ();
    }

    [HttpGet("Search/{param}")]
    public ActionResult<List<Attachment>> Search(string param) =>Ok(Service.Search( param));
    
    }
}
