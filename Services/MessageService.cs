using AmaselBE.Configuration;
using AmaselBE.Model;

namespace AmaselBE.Services
{
    public class MessageService : BaseService<Message>
    {
        public MessageService(Setting setting) : base(setting)
        {

        }

    }

}