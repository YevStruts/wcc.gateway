using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models.Schedule
{
    public class ScheduleModel
    {
        public string? Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string SideA { get; set; }
        public string SideB { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
        public List<string> YouTube { get; set; }
    }
}
