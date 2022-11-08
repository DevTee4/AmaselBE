using AmaselBE.Configuration;
using AmaselBE.Model;

namespace AmaselBE.Services
{
    public class AuditTrailService : BaseService<AuditTrail>
    {
        public AuditTrailService(Setting setting) : base(setting)
        {

        }

    }

}