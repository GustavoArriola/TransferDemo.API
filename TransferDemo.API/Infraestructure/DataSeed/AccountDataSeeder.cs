using System.Collections.Generic;
using System.Linq;
using TransferDemo.API.Models;

namespace TransferDemo.API.Infraestructure.DataSeed
{
    public class AccountDataSeeder
    {
        private readonly TransferDbContext _db;

        public AccountDataSeeder(TransferDbContext transferDbContext)
        {
            _db = transferDbContext;
        }

        //Carga la información inicial de las cuentas.
        public void Seed()
        {
            if (!_db.Accounts.Any())
            {
                var accounts = new List<Account>()
                {
                    new Account { AccountHolder = "Gustavo Arriola", AccountNumber = "12345678", Amount = 10000000 },
                    new Account { AccountHolder = "Juan Pérez", AccountNumber = "87654321", Amount = 15000000 }
                };

                _db.Accounts.AddRange(accounts);
                _db.SaveChanges();
            }
        }
    }
}
