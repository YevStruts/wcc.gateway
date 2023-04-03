﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.Infrastructure
{
    public class PlayerRatingView
    {
        public long Id { get; set; }
        public long PlayersId { get; set; }
        public long RatingId { get; set; }
        public int Position { get; set; }
        public int Progress { get; set; }
        public int TotalPoints { get; set; }
    }
}