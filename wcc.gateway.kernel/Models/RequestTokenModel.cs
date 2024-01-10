using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models
{
    public class RequestTokenModel
    {
        public string Username { get; set; }
        public string BearerToken { get; set; }
    }
}
