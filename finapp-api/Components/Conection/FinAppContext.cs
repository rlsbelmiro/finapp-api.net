using FinAppApi.Components.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Conection
{
    public class FinAppContext : DbContext
    {
        public FinAppContext(DbContextOptions<FinAppContext> options) : base(options)
        {

        }

        //Entities
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<WalletEntity> Wallets { get; set; }
        public DbSet<CreditCardEntity> CreditCards { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
