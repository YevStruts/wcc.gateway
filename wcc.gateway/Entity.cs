using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }
    }
}
