using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransferDemo.API.Models;

namespace TransferDemo.API.Infraestructure.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasData(new Account { AccountHolder = "Gustavo Arriola", AccountNumber = "12345678", Amount = 10000000 });
            builder.HasData(new Account { AccountHolder = "Juan Pérez", AccountNumber = "87654321", Amount = 15000000 });
        }
    }
}
