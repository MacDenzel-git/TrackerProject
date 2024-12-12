 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObject
{
    public class DashboardDTO
    {
        public List<BarChartDTO> BarChart { get; set; }
        public  List<BarChartDTO> PieChart { get; set; }
        public int ExpiringProductsCount { get; set; }
        public int LowLevelCount { get; set; }


    }
}
