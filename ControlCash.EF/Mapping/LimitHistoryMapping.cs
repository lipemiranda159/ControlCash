using ControlCash.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.EF.Mapping
{
    public class LimitHistoryMapping : EntityTypeConfiguration<LimitHistory>
    {
        public LimitHistoryMapping()
        {
            this.ToTable("dbo.TbLimitHistory");
        }
    }
}
