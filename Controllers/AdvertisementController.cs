using System.Net;
using AmaselBE.Services;
using AmaselBE.Model;
using Microsoft.AspNetCore.Mvc;
using VendolaCore;

namespace AmaselBE.Controllers
{
    public class AdvertisementController : BaseController
    {
        public AdvertisementService Service { get; set; }


        public AdvertisementController(Setting setting, AdvertisementService service) : base(setting)
        {
            Service = service;
        }
        [HttpGet("GetAll/{skip?}/{limit?}")]
        public IActionResult GetAll()
        {

            return Ok(Service.Get());

        }

        [HttpGet("GetWithId/{param:length(24)}")]
        public IActionResult GetWithId(string param)
        {
            var result = Service.Get(a => a.Id == param);
            return Ok(result);
        }



        [HttpPost("Save")]
        public IActionResult Post([FromBody] List<Advertisement> values)
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


    }

}
