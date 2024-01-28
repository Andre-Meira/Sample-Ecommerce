using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Ecommerce.Core.Domain;
using Sample.Ecommerce.Order.API.Moldes.Inventorys;
using Sample.Ecommerce.Order.Core.Inventorys.Events;
using Sample.Ecommerce.Order.Core.Inventorys.EventStream;
using Sample.Ecommerce.Order.Core.Products;
using System.ComponentModel.DataAnnotations;

namespace Sample.Ecommerce.Order.API.Controllers.Inventorys;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IStockRepository _stockRepository;

    public InventoryController(
        IProductRepository productRepository, 
        IStockRepository stockRepository)
    {
        _productRepository = productRepository;
        _stockRepository = stockRepository;
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(InventoryResponse), StatusCodes.Status200OK)]    
    public async Task<InventoryResponse> InventoryCreate(InventoryCreateCommand command,
        CancellationToken cancellationToken = default)
    {
        Product? product = await _productRepository.Get(command.IdProduct, cancellationToken)
            .ConfigureAwait(false);

        if (product is null) throw new DomainException("Produto não encontrado.");

        var inventory = new InventoryCreated(product, command.Amount);
        await _stockRepository.Include(inventory).ConfigureAwait(false);

        return new InventoryResponse(inventory.IdCorrelation, "Inventario criado.");
    }

    [HttpPost("Include/{Id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(InventoryResponse), StatusCodes.Status200OK)]
    public async Task<InventoryResponse> InventoryInclude([FromBody,Required] int amount,
        Guid id, CancellationToken cancellationToken)
    {
        var inventory = new InventorynIcreased(id, amount);
        await _stockRepository.Include(inventory, cancellationToken).ConfigureAwait(false);

        return new InventoryResponse(inventory.IdCorrelation, "Produto adicionado");
    }
}
