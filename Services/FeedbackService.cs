using AmaselBE.Model;
using VendolaCore;

namespace AmaselBE.Services
{
    public class FeedbackService : BaseService<Feedback>
    {
        public FeedbackService(Setting setting, IHttpContextAccessor context) : base(setting, context.HttpContext)
        {

        }

    }

}