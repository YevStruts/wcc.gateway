using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models.Schedule
{
    public class ScheduleModel
    {
        public string TournamentId { get; set; }

        public List<ScheduleGameModel> Games { get; set; }
    }
}
