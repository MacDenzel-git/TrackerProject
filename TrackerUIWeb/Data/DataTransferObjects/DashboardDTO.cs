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
        //public List<ProjectDTO> Projects { get; set; }
    }
}
