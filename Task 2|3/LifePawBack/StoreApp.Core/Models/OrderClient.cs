using StoreApp.Core.Enums;

namespace StoreApp.Core.Models
{
    public class OrderClient
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public PaymentType PaymentType { get; set; }

        public List<OrderDetailClient> OrderDetails { get; set; }
    }
}
