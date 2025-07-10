using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.NaCekanju; // Enum

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public PaymentMethod PaymentMethod { get; set; } // Enum

        public string? DeliveryAddress { get; set; }

        public string? ContactPhone { get; set; }

        public string? Note { get; set; }

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal TotalAmount => Items.Sum(i => i.Price * i.Quantity);

        public string Currency { get; set; } = "EUR";
    }

    public enum OrderStatus
    {
        NaCekanju,
        UPripremi,
        Zavrsena
    }

    public enum PaymentMethod
    {
        Gotovina,
        Kartica,
        Online
    }
}


