using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO
{

    public class CostCenterMappingModel
    {
        public int RowNum { get; set; }
        public string Company { get; set; }
        public string HeadQuarter { get; set; }
        public string HeadQuarterCode { get; set; }
        public string Zone { get; set; }
        public string ZoneCode { get; set; }
        public string Region { get; set; }
        public string RegionCode { get; set; }
        public string ProfitCenter { get; set; }
        public string ProfitCenterCode { get; set; }
        public string CostCenterCode { get; set; }
        public string CostCenter { get; set; }
    }
}
