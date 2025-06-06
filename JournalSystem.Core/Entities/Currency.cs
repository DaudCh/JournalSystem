﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public int CurrencyCode { get; set; }
        public string Name { get; set; }
        public ICollection<JournalEntry> JournalEntries { get; set; }
    }

}
