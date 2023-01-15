using System.Net;
using AmaselBE.Configuration;
using AmaselBE.Services;
using AmaselBE.Model;
using AmaselBE.Lib;
using Microsoft.AspNetCore.Mvc;

namespace AmaselBE.Controllers
{
    public class FeedbackController : BaseController
    {
        public FeedbackService Service { get; set; }

        public AuthUserService authUserService { get; set; }

        public FeedbackController(Setting setting) : base(setting)
        {
            Service = new FeedbackService(setting);
            authUserService = new AuthUserService(setting);
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

        [HttpGet("GetWithCredential/{username}/{password}")]
        public IActionResult GetWithCredential(string username, string password)
        {
            var result = new List<Feedback>();
            var resultKey = this.authUserService.Get(a => a.MailAddress == username && a.Password == password);
            if (resultKey.Count > 0)
            {
                resultKey.ForEach(res =>
                {
                    var feedBk = Service.Get(a => a.Code == res.Code);
                    result.AddRange(feedBk);
                });
            }
            return Ok(result);
        }

        [HttpPost("Save")]
        public IActionResult Post([FromBody] List<Feedback> values)
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
        public ActionResult<List<AuthUser>> Search(string param) => Ok(Service.Search(param));

    }

}
