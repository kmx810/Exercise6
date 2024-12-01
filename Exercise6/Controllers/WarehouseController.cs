using Exercise6.Data;
using Exercise6.DTOs;
using Exercise6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Exercise6.Controllers;


[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly WarehouseContext _context;

    public WarehouseController(WarehouseContext context)
    {
        _context = context;
    }

    [HttpPost("add-product")]
    public async Task<IActionResult> AddProductToWarehouse([FromBody] ProductWarehouseRequest request)
    {
        if (request.Amount <= 0) return BadRequest("Amount must be greater than 0.");
        
        var product = await _context.Products.FindAsync(request.IdProduct);
        if (product == null) return NotFound("Product not found.");

        var warehouse = await _context.Warehouses.FindAsync(request.IdWarehouse);
        if (warehouse == null) return NotFound("Warehouse not found.");

        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.IdProduct == request.IdProduct && o.Amount == request.Amount && o.CreatedAt < request.RequestDate);

        if (order == null) return BadRequest("No matching order found.");
        if (_context.ProductWarehouses.Any(pw => pw.IdOrder == order.IdOrder)) return BadRequest("Order already fulfilled.");

        order.FulfilledAt = DateTime.UtcNow;
        
        var productWarehouse = new ProductWarehouse
        {
            IdWarehouse = request.IdWarehouse,
            IdProduct = request.IdProduct,
            IdOrder = order.IdOrder,
            Amount = request.Amount,
            Price = product.Price * request.Amount,
            CreatedAt = DateTime.UtcNow
        };
        
        await _context.ProductWarehouses.AddAsync(productWarehouse);
        await _context.SaveChangesAsync();

        return Ok(productWarehouse.IdProductWarehouse);
    }
}
