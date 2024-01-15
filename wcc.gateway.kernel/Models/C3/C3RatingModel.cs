using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models.C3
{
    public class C3RatingItemModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public int score { get; set; }
    }

    public class C3RatingModel : ResultModel
    {
        public C3RatingModel()
        {
            players = new List<C3RatingItemModel>();
        }
        public List<C3RatingItemModel> players { get; set; }
    }
}
