using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM
{
    public class Taetigkeiten
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int TransferPrice { get; set; }
        public string toCsvString()
        {
            string csv = ID + ";" + Description + ";" + TransferPrice;
            return csv;
        }
    }
}
