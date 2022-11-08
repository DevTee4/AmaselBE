using AmaselBE.Configuration;
using AmaselBE.Model;

namespace AmaselBE.Services
{
    public class ChatService : BaseService<Chat>
    {
        public ChatService(Setting setting) : base(setting)
        {

        }

    }

}