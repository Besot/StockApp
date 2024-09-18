namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now; 
        public int? StockId { get; set; }
        public Stock? Stocks { get; set;}
        public int Likes { get; set; } = 0;  // Added field for like count
    }
}
