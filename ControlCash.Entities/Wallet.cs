using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public decimal Balance { get; set; }
        public List<User> Users { get; set; }
    }
}
