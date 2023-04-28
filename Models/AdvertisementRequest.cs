using VendolaCore.Model;

namespace AmaselBE.Model
{
    public class AdvertisementRequest : BaseModel
    {
        public AdvertType AdvertType { get; set; } = AdvertType.Product;
        public string Url { get; set; } = string.Empty;//Only Applicable to Product
        public string Title { get; set; }
        public DateTimeOffset FromDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset ToDate { get; set; } = DateTimeOffset.Now;
        public string PaymentInfo { get; set; } = string.Empty;
        public AdvertisementRequestStatus Status { get; set; } = AdvertisementRequestStatus.Pending;
        public string ApprovedBy { get; set; } = string.Empty;
        public string Seller { get; set; } = string.Empty;
        public Attachment Image { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}