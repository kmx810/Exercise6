using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise6.Models;


public class Product
{
    [Key]
    public int IdProduct { get; set; }

    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string Description { get; set; }

    [Column(TypeName = "numeric(25, 2)")]
    public decimal Price { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<ProductWarehouse> ProductWarehouses { get; set; }
}