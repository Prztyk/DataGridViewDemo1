using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeldScanApp
{
    public class PartDTO
    {
        public int Id { get; set; }
        public string PartCode { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
        public string Line { get; set; }
        public List<string> Welds { get; set; }

        public PartDTO()
        {
            Welds = new List<string>();
        }
    }
}
