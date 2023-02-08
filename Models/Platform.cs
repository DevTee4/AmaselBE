namespace AmaselBE.Model {
    public class Platform : BaseModel {
        public string Tenant { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public bool Active { get; set; }
        public string MailAddress { get; set; }

    }
}