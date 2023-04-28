using VendolaCore.Model;

namespace AmaselBE.Model
{
    public class Advertisement : BaseModel
    {
        public bool Active { get; set; } = false;
        public DateTimeOffset ValidFrom { get; set; } = DateTimeOffset.Now;
        public Attachment File { get; set; }
        public string Name { get; set; }
        public DateTimeOffset ValidTo { get; set; } = DateTimeOffset.Now;
        public AdvertType AdvertType { get; set; } = AdvertType.Product;
        public string Url { get; set; } = string.Empty;
        public string Request { get; set; }
        public string Seller { get; set; } = string.Empty;

    }
}