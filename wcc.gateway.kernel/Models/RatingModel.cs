﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models
{
    public class RatingModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string? AvatarUrl { get; set; }
    }
}
