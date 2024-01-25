using Microsoft.EntityFrameworkCore;
using Sample.Ecommerce.Core.Domain.Entity;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Infra.Products;

internal sealed class ProductRepository : IProductRepository
{
    public IUnitOfWork UnitOfWork => _context;
    private readonly ContextEcommerce _context;

    public ProductRepository(ContextEcommerce context) => _context = context;

    public void Add(Product Entity) => _context.Products.Add(Entity);

    public Task<Product?> Get(Guid Id, CancellationToken cancellation = default)
    {
        return _context.Products.FirstOrDefaultAsync(p => p.Id == Id);  
    }

    public IEnumerable<Product> GetAll(int itemsPerPage = 10)
    {
        return _context.Products.Take(itemsPerPage).ToList();
    }
}
