using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect
{
    public class Normative
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string SportName { get; set; }
        public int YearComplete { get; set; }
        public string TrainerName { get; set; }
        public string NormsAtTheBeginning { get; set; }
        public string NormsAtTheEndOf { get; set; }
        public string SurrenderRate { get; set; }
        public string Comment { get; set; }
    }
}
