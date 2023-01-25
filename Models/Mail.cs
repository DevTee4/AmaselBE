namespace AmaselBE.Model
{
    public class Mail : BaseModel
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}