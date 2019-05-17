using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [StringLength(50)]
        public string UserIdentifier { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
