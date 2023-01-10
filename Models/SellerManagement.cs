namespace AmaselBE.Model
{
    public class SellerManagement : BaseModel
    {
        public string SellerRequest { get; set; }
        public string Name { get; set; }
        public string Reciever { get; set; }
        public string Country { get; set; }
        public ProductType ProductType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Verification { get; set; }
        public Attachment Attachments { get; set; }
        public SellerRequestStatus RequestStatus { get; set; }
        public string Response { get; set; }

    }
}