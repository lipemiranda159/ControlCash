using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.Entities
{
    public class Limit
    {
        public int LimitId { get; set; }
        [StringLength(30)]
        public string TagDescription { get; set; }
        public decimal LimitValue { get; set; }
    }
}
