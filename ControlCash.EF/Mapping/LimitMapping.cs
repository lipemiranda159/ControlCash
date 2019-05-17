using ControlCash.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.EF.Mapping
{
    public class LimitMapping : EntityTypeConfiguration<Limit>
    {
        public LimitMapping()
        {
            this.ToTable("dbo.TbLimit");
        }
    }
}
