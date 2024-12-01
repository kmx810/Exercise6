using System.ComponentModel.DataAnnotations;

namespace Exercise6.Models;

public class Warehouse
{
    [Key]
    public int IdWarehouse { get; set; }

    [MaxLength(200)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string Address { get; set; }

    public ICollection<ProductWarehouse> ProductWarehouses { get; set; }
}