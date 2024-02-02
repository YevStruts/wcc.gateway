using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models.C3
{
    public class C3UsersModel : ResultModel
    {
        public List<C3UserItemModel> users { get; set; }
    }
}
