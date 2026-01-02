using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryApp.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [StringLength(200)]
        public string Address { get; set; }
        
        [StringLength(20)]
        public string Phone { get; set; }
        
        [StringLength(100)]
        public string ImageUrl { get; set; }
        
        [Range(0, 5)]
        public double Rating { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DeliveryFee { get; set; }
        
        public int DeliveryTime { get; set; } // minutes
        
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }

    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(200)]
        public string Description { get; set; }
        
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }

    public class MenuItem
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [StringLength(200)]
        public string ImageUrl { get; set; }
        
        public bool IsAvailable { get; set; } = true;
        
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        
        [Required]
        public string SessionId { get; set; }
        
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        
        public int Quantity { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }
        
        [Required]
        [StringLength(20)]
        [Phone]
        public string Phone { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Address { get; set; }
        
        [StringLength(500)]
        public string Notes { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        
        public int Quantity { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }

    public enum OrderStatus
    {
        Pending,
        Confirmed,
        Preparing,
        OnTheWay,
        Delivered,
        Cancelled
    }
}