using AmaselBE.Configuration;
using AmaselBE.Model;

namespace AmaselBE.Services
{
    public class MailLogService : BaseService<MailLog>
    {
        public MailLogService(Setting setting) : base(setting)
        {

        }
    }

}