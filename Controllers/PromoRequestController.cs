using System.Net;
using AmaselBE.Services;
using AmaselBE.Model;
using Microsoft.AspNetCore.Mvc;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Controllers
{
    public class PromoRequestController : BaseController
    {
        public PromoRequestService Service { get; set; }


        public PromoRequestController(Setting setting, PromoRequestService service) : base(setting)
        {
            Service = service;
        }
        [HttpGet("GetAll/{skip?}/{limit?}")]
        public IActionResult GetAll(int skip, int limit) => Ok(Service.Get(skip, limit));


        [HttpGet("GetWithId/{param:length(24)}")]
        public IActionResult GetWithId(string param)
        {
            var result = Service.Get(a => a.Id == param);
            return Ok(result);
        }
        [HttpPost("Save")]
        public IActionResult Post([FromBody] List<PromoRequest> values)
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
        [HttpGet("Search/{param}/{skip?}/{limit?}")]
        public ActionResult<List<PromoRequest>> Search(string param, int skip, int limit) => Ok(Service.Search(param, skip, limit));
        [HttpPost("Search/{skip?}/{limit?}")]
        public ActionResult<List<PromoRequest>> Search([FromBody] List<OperatorKeyValue> param, int skip, int limit)
        {
            var result = Service.Search(param, skip, limit);
            return Ok(result);
        }

    }

}
