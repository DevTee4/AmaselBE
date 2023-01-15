namespace AmaselBE.Model
{
    public class ProductUploadRequest : BaseModel
    {
        public string Seller { get; set; }
        public Status Status { get; set;}
        // public DateTimeOffset CreatedAt { get; set; }
        public Attachment Attachments { get; set; }
    }
}