using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.Infrastructure
{
    [Table("Statistic")]
    public class Statistic : Entity
    {
        [Required]
        [ForeignKey("Player")]
        public long PlayerId { get; set; }

        public int Games { get; set; }

        public int Wins { get; set; }
    }
}
