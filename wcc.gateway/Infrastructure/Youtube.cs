using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.Infrastructure
{
    public class Youtube : Entity
    {
        [Required]
        public string? Url { get; set; }

        public long GameId { get; set; }
    }
}
