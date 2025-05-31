namespace BookDataGeneratorApp.Models
{
    public class BookViewModel
    {
        public int Index { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Publisher { get; set; }
        public int Likes { get; set; }
        public int Reviews { get; set; }
    }
}