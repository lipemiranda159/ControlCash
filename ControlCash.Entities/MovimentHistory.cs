using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCash.Entities
{
    public class MovimentHistory
    {
        public int MovimentHistoryId { get; set; }
        [StringLength(30)]
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
