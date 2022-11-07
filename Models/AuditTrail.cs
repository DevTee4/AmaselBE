namespace AmaselBE.Model
{
    public class AuditTrail : BaseModel
    {
        public string PrevObject { get; set; }
        public string Name { get; set; }
        public string MailAddress { get; set; }
        public string NewObject { get; set; }
        public UserType UserType { get; set; }

    }
}
