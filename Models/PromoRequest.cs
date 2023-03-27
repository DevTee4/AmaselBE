using VendolaCore.Model;

namespace AmaselBE.Model
{
    public class PromoRequest : BaseModel
    {
        public string Title { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public string PromoAuthor { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public string Owner { get; set; }
        public Status Status { get; set; }
        public string Customers { get; set; }
        public string ApprovedBy { get; set; }
        public string Sellers { get; set; }
        public string PromoCode { get; set; }
    }
}