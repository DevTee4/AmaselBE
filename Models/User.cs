namespace AmaselBE.Model
{
    public class User : BaseModel
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string MailAddress { get; set; }
        public bool Active { get; set; }
        public UserType UserType { get; set; }

    }
}
