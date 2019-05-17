using ControlCash.Entities.Repository;
using ControlCash.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.EF
{
    public interface IBaseContext : IUnitOfWork
    {
        ILimitRepository LimitRepository { get; set; }
        IMovimentHistoryRepository MovimentHistoryRepository { get; set; }
        IUserRepository UserRepository { get; set; }
        IWalletRepository WalletRepository { get; set; }
        ILimitHistoryRepository LimitHistoryRepository { get; set; }
    }
}