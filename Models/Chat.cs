namespace AmaselBE.Model
{
    public class Chat : BaseModel
    {
        public string Message { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public DateTimeOffset Date { get; set; }
        public UserType SenderType { get; set; }
        public UserType ReceiverType { get; set; }
        public Attachment Attachments { get; set; }

    }
}