namespace Task_5.Models
{
    public class BookRequest
    {
        public string Region { get; set; } 
        public int Seed { get; set; }
        public double AvgLikes { get; set; }
        public double AvgReviews { get; set; }
        public int StartIndex { get; set; }
        public int Count { get; set; }
    }

}
