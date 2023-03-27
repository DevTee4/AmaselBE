using VendolaCore.Model;

namespace AmaselBE.Model
{
    public class Feedback : BaseModel
    {
        public string Title { get; set; }
        public Message Message { get; set; }

    }
}