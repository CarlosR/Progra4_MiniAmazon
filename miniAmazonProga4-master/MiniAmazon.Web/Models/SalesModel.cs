namespace MiniAmazon.Web.Models
{
    public class SalesModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public long AccountId { get; set; }
        public long CategoryId { get; set; }

        public string Description { get; set; }

    }
}