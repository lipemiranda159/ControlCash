using ControlCash.Entities;
using ControlCash.Entities.Repository;
using ControlCash.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ControlCash.EF.Repository
{
    public class UserRepository : EntityRepositoryAsync<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
