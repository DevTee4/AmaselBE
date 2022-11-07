namespace AmaselBE.Model
{

    public class BaseModel
    {
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public string Code { get; set; }
    }
}