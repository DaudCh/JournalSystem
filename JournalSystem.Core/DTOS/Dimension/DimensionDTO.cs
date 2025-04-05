using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.DTOS.Dimension
{
    public class CreateDimensionDTO
    {
        public string Name { get; set; }

    }

    public class UpdateDimensionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class DimensionResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
