using Sample.Ecommerce.Core.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Sample.Ecommerce.Order.API.Moldes.Orders;

public record OrderCommand
{
    public Guid IdClient { get; set; }
    public Guid IdProduct { get; set; }
    public DateTime Date { get; set; }

    [Required]
    public BaseAddress DeliveryAddress { get; set; } = null!;

    [Required]
    public BaseBankAccount BankAccount { get; set; } = null!;

    public int Amount { get; set; }
    public decimal Value { get; set; }
}
