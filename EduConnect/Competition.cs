using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SportType { get; set; }
        public DateTime EventDate { get; set; }
        public int ParticipantsCount { get; set; }
        public string Results { get; set; }
        public int Year { get; set; }
    }
}
