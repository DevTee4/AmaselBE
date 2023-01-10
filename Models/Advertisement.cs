namespace AmaselBE.Model
{
    public class Advertisement : BaseModel
    {
        public bool Active { get; set; }
        public DateTimeOffset ValidFrom { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset ValidTo { get; set; }
        public AdvertType AdvertType { get; set; }
        public string Url { get; set; }
    }
}