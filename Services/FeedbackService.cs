using AmaselBE.Configuration;
using AmaselBE.Model;

namespace AmaselBE.Services
{
    public class FeedbackService : BaseService<Feedback>
    {
        public FeedbackService(Setting setting) : base(setting)
        {

        }

    }

}