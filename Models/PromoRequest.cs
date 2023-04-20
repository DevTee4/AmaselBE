using VendolaCore.Model;

namespace AmaselBE.Model
{
    public class PromoRequest : BaseModel
    {
        public string Title { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public UserType OwnerType { get; set; } = UserType.Platform;
        public DateTimeOffset ToDate { get; set; }
        public string Owner { get; set; }
        public Status Status { get; set; }
        public List<string> Customers { get; set; }
        public string ApprovedBy { get; set; }
        public List<string> Sellers { get; set; }
        public string PromoCode { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}