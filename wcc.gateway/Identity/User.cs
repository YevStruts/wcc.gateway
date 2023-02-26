﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using wcc.gateway.Infrastructure;

namespace wcc.gateway.Identity
{
    [Table("users")]
    public class User : Entity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The ExternalId value cannot exceed 100 characters. ")]
        public string ExternalId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Username value cannot exceed 100 characters. ")]
        public string? Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Avatar value cannot exceed 100 characters. ")]
        public string? Avatar { get; set; }

        [Required]
        [StringLength(2048, ErrorMessage = "The Token value cannot exceed 2048 characters. ")]
        public string? Token { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "The Code value cannot exceed 128 characters. ")]
        public string? Code { get; set; }

        public virtual Player player { get; set; }
    }
}
