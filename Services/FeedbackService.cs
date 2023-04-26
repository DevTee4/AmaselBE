using AmaselBE.Model;
using VendolaCore;
using VendolaCore.Model;

namespace AmaselBE.Services
{
    public class FeedbackService : BaseService<Feedback>
    {
        public FeedbackService(Setting setting, IHttpContextAccessor context, User user) : base(setting, context.HttpContext, user)
        {

        }

    }

}