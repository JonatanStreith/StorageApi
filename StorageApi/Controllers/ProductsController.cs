using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageApi.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly StorageContext _context;
    public ProductsController(StorageContext context)
    {
        _context = context;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
        return await _context.Product.ToListAsync();
    }

    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Product.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    // PUT: api/Product/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int? id, ProductUpdateDto productUpdate)
    {

        if (productUpdate == null) return BadRequest();

        var product = _context.Product.FirstOrDefault(p => p.Id == id);

        if (product == null) return NotFound();

        product.Name = productUpdate.Name;
        product.Price = productUpdate.Price;
        product.Category = productUpdate.Category;
        product.Shelf = productUpdate.Shelf;
        product.Count = productUpdate.Count;
        product.Description = productUpdate.Description;

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Product
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ProductCreateDto>> PostProduct(ProductCreateDto product)
    {
        var createdProduct = new Product()
        {
            Name = product.Name,
            Price = product.Price,
            Category = product.Category,
            Shelf = product.Shelf,
            Count = product.Count,
            Description = product.Description
        };

        if(createdProduct== null) return BadRequest();

        _context.Product.Add(createdProduct);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = createdProduct.Id }, createdProduct);
    }

    // DELETE: api/Product/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Product.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int? id)
    {
        return _context.Product.Any(e => e.Id == id);
    }
}
