﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models.C3
{
    public class C3RankModel
    {
        public int RankId { get; set; }
        public List<C3RankItemModel> Items { get; set; }
    }
}
