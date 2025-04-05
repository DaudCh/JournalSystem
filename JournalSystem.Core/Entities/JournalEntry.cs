using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Entities
{
    public class JournalEntry
    {
        public int Id { get; set; }
        public int JournalNumber { get; set; }
        public string JournalType { get; set; }
        public string Period { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Status { get; set; }
        public int CurrencyId { get; set; }
        public float ExchangeRate { get; set; }
        public Currency Currency { get; set; }
        public ICollection<JournalLine> JournalLines { get; set; }
    }

}
