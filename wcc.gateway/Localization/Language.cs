using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.Localization
{
    [Table("Languages")]
    public class Language : Entity
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The Culture value cannot exceed 10 characters. ")]
        public string? Culture { get; set; }
    }
}
