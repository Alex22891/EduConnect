using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduConnect
{
    public class LicensesKeys
    {
        public int Id { get; set; }
        public string LicenseKey { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Username { get; set; }
    }
}
