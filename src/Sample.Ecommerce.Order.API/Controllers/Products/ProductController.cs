using Microsoft.AspNetCore.Mvc;
using Sample.Ecommerce.Order.API.Moldes;
using Sample.Ecommerce.Order.Core.Products;
using System.ComponentModel.DataAnnotations;

namespace Sample.Ecommerce.Order.API.Controllers.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]   
    public async Task<ProductResponse> CreateProduct(
        [FromBody,Required] ProductCommand command,
        CancellationToken cancellation = default)
    {

        var product = new Product(command.Name, command.Price, command.Description);

        _productRepository.Add(product);
        
        await _productRepository.UnitOfWork.SaveChangesEntity(cancellation)
            .ConfigureAwait(false );

        return new ProductResponse(product.Id, "Produto criado.");
    }

}