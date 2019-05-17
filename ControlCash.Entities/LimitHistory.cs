using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.Entities
{
    public class LimitHistory
    {
        public int LimitHistoryId { get; set; }
        [StringLength(30)]
        public string TagDescription { get; set; }
        public short Month { get; set; }
        public short Year { get; set; }
        public decimal Balance { get; set; }
        public decimal LimitValue { get; set; }
    }
}
