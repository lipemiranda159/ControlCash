using ControlCash.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.EF.Mapping
{
    public class WalletMapping : EntityTypeConfiguration<Wallet>
    {
        public WalletMapping()
        {
            this.ToTable("dbo.TbWallet");
        }
    }
}
