namespace StoreApp.Core.Models
{
    public class OrderDetailClient
    {
        public int ProductId { get; set; }

        public double Price { get; set; }

        public string ProductTitle { get; set; } =  string.Empty;

        public int Quantity { get; set; }
    }
}
