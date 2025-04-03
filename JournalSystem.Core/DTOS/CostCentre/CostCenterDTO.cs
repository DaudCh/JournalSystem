using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.DTOS.CostCentre
{
    public class CreateCostCenterDTO
    {
        public int CostCenterID { get; set; }
        public string Name { get; set; }
    }
    public class UpdateCostCenterDTO
    {
        public int Id { get; set; }
        public int CostCenterID { get; set; }
        public string Name { get; set; }
    }

    public class CostCenterResponseDTO
    {
        public int Id { get; set; }
        public int CostCenterID { get; set; }
        public string Name { get; set; }
    }
}
