using VendolaCore.Model;

namespace AmaselBE.Model
{
    public class GiftCard : BaseModel
    {
        public decimal Amount { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ValidFrom { get; set; }
        public string PaymentInfo { get; set; }
        public string Key { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
    }
}
