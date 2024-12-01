namespace Exercise6.DTOs;

public class ProductWarehouseRequest
{
    public int IdProduct { get; set; }
    public int IdWarehouse { get; set; }
    public int Amount { get; set; }
    public DateTime RequestDate { get; set; }
}