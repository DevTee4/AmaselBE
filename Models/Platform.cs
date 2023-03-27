using VendolaCore.Model;
namespace AmaselBE.Model
{
    public class Platform : User
    {
        public string Tenant { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
    }
}
