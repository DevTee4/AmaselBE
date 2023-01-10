namespace AmaselBE.Model
{
    public class AdvertisementRequest : BaseModel
    {
        public string Title { get; set; }
        public DateTimeOffset FromDate { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset ToDate { get; set; }
        public string PaymentInfo { get; set; }
        public Status Status { get; set; }
        public string ApprovedBy { get; set; }
        public string Seller { get; set; }
        public string Response { get; set; }
    }
}