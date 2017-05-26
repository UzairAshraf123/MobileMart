using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class ChartViewModel
    {
        public string y { get; set; }
        public string a { get; set; }
        public string b { get; set; }
    }
    public class PaiChartViewModel
    {
        public string label { get; set; }
        public int value { get; set; }
    }
}
