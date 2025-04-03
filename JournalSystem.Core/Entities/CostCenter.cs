﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Entities
{
    public class CostCenter
    {
        public int Id { get; set; }
        public int CostCenterID { get; set; }
        public string Name { get; set; }

        
        public ICollection<JournalLine> JournalLines { get; set; }
    }

}
