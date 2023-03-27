using VendolaCore.Model;

namespace AmaselBE.Model
{
    public class RatingLevel : BaseModel
    {
        public string Name { get; set; }
        public RatingCriteria Criteria { get; set; }

    }
}