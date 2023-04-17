using System.ComponentModel.DataAnnotations.Schema;

namespace wcc.gateway.Identity
{
    [Table("Roles")]
    public class Role : Entity
    {
        public string Name { get; set; }
    }
}
