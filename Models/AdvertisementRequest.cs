using VendolaCore.Model;

namespace AmaselBE.Model
{
    public class AdvertisementRequest : BaseModel
    {
        public string Title { get; set; }
        public DateTimeOffset FromDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset ToDate { get; set; } = DateTimeOffset.Now;
        public string PaymentInfo { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Pending;
        public string ApprovedBy { get; set; } = string.Empty;
        public string Seller { get; set; } = string.Empty;


    }
}