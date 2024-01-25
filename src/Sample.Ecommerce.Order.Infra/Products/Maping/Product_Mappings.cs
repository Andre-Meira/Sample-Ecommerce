using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Infra.Products.Maping;

internal class Product_Mappings : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("me_produts");

        builder.Property(x => x.Id).HasColumnName("id").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(e => e.Price).HasColumnName("price").IsRequired();
        builder.Property(e => e.Description).HasColumnName("description").IsRequired(false);
    }
}
