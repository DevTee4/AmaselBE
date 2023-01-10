namespace AmaselBE.Model
{
    public class Message : BaseModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set;}
        public string Title { get; set; }
        public Medium Medium { get; set; }
        public Attachment Attachments { get; set; }
    }
}