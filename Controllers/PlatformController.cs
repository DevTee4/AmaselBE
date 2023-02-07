using System.Net;
using System.Net.Mail;
using AmaselBE.Configuration;
using AmaselBE.Services;
using AmaselBE.Model;
using AmaselBE.Lib;
using Microsoft.AspNetCore.Mvc;

namespace AmaselBE.Controllers
{
    public class PlatformController : BaseController
    {
        public PlatformService Service { get; set; }
        public UserService userService { get; set; }
        public UtilService utilService { get; set; }
        private readonly ILogger<PlatformController> _logger;

        public PlatformController(ILogger<PlatformController> logger, Setting setting) : base(setting)
        {
            _logger = logger;
            Service = new PlatformService(setting);
            userService = new UserService(setting);
            utilService = new UtilService(setting);
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
                        var mail = new Mail();
                        mail.Recipient = user.MailAddress;
                        mail.Subject = "Welcome to VENDOLA";
                        mail.Body = "I am happy to welcome you to VENDOLA";
                        utilService.sendMail(mail, nameof(User));
                        // userService.Update(user);
                    });
                }
                return Ok(result);
            }
            return NotFound(new Error { ErrorMsg = "No data recieved", StatusCode = (int)HttpStatusCode.Forbidden });
        }

        // public static string sendMail(string mailFrom, string recipient, string subject, string body)
        // {
        //     var smtpClient = new SmtpClient(ServerName)
        //     {
        //         Port = 587,
        //         Credentials = new NetworkCredential(SmtpUsername, SmtpPassword),
        //         EnableSsl = true,
        //     };
        //     var mailMessage = new MailMessage
        //     {
        //         From = new MailAddress(mailFrom),
        //         Subject = subject,
        //         Body = body,
        //         IsBodyHtml = true,
        //     };
        //     mailMessage.To.Add(recipient);

        //     smtpClient.Send(mailMessage);

        //     // smtpClient.Send("admin@techlivate.com", recipient, "WELCOME", "I am happy to welcome you to VENDOLA");
        //     return "Mail sent successfully";
        // }


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
