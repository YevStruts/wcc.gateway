using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.Infrastructure
{
    [Table("Country")]
    public class Country : Entity
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public List<Player> Players { get; set; }
    }
}
