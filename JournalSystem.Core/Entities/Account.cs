using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }

        // Navigation Property
        public ICollection<JournalLine> JournalLines { get; set; }
    }

}
