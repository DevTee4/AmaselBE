namespace AmaselBE.Model
{
    public class MailLog : BaseModel
    {
        public string Entity { get; set; }
        public DateTimeOffset Time { get; set; }
        public string Content { get; set; }
        public string Error { get; set; } = "";
    }
}