namespace AmaselBE.Model
{
    public class Rating : BaseModel
    {
        public RatingType RatingType { get; set; }
        public User User { get; set; }
        public string Owner { get; set; }
        public string Rate { get; set; }

    }
}