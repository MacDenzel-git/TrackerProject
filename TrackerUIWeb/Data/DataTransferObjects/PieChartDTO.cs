using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataTransferObject
{
    public class PieChartDTO
    {
        public string Statuses { get; set; }     
        public int NumberOfProjects { get; set; }     
        public bool IsDeleted { get; set; }
    }
}
