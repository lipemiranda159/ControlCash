using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlCash.Entities.Repository;
using ControlCash.UnitOfWork;
using ControlCash.EF.Repository;
using ControlCash.Entities;
using ControlCash.EF.Mapping;

namespace ControlCash.EF
{
    public class ControlCashDbContext : DbContext, IBaseContext
    {
        public ControlCashDbContext()
            : base("name=ControlCashDb")
        {
            LimitRepository = new LimitRepository(this);
            MovimentHistoryRepository = new MovimentHistoryRepository(this);
            UserRepository = new UserRepository(this);
            WalletRepository = new WalletRepository(this);
            LimitHistoryRepository = new LimitHistoryRepository(this);
        }

        private DbSet<Limit> Limits { get; set; }
        private DbSet<MovimentHistory> MovimentHistory { get; set; }
        private DbSet<User> Users { get; set; }
        private DbSet<Wallet> Wallets { get; set; }
        private DbSet<LimitHistory> LimitHistorys { get; set; }

        public ILimitRepository LimitRepository { get; set; }
        public IMovimentHistoryRepository MovimentHistoryRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IWalletRepository WalletRepository { get; set; }
        public ILimitHistoryRepository LimitHistoryRepository { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LimitMapping());
            modelBuilder.Configurations.Add(new MovimentHistoryMapping());
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new WalletMapping());
            modelBuilder.Configurations.Add(new LimitHistoryMapping());
        }

        public async Task SaveChanges()
        {
            await SaveChangesAsync();
        }
    }
}
