using JournalSystem.Core.DTOS.Currency;
using JournalSystem.Core.DTOS.JournalLines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.DTOS.JournalEntry
{
    public class JournalEntryDTO
    {
        public int Id { get; set; }
        public int JournalNumber { get; set; }
        public string JournalType { get; set; }
        public string Period { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Status { get; set; }
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        
        public float TotalDebit { get; set; }
        public float TotalCredit { get; set; }
        public List<JournalLineDTO> JournalLines { get; set; } = new();
        public float ExchangeRate { get; set; }
    }


    public class CreateJournalEntryDTO
    {
        public int JournalNumber { get; set; }
        public string JournalType { get; set; }
        public string Status { get; set; } = "Draft";
        public string Period { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public int CurrencyId { get; set; }
        public float ExchangeRate { get; set; }
        public ICollection<CreateJournalLineDTO> JournalLines { get; set; }

    }


    public class UpdateJournalEntryDTO
    {
        public int Id { get; set; }
        public string JournalType { get; set; }
        public int JournalNumber { get; set; }
        public string Period { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public string Status { get; set; }
        public int CurrencyId { get; set; }
        public float ExchangeRate { get; set; }
        public ICollection<CreateJournalLineDTO> JournalLines { get; set; } = new List<CreateJournalLineDTO>();
    }

    public class JournalEntryWithLinesDTO
    {
        public int Id { get; set; }
        public int JournalNumber { get; set; }
        public string JournalType { get; set; }
        public DateTime PostingDate { get; set; }
        public ICollection<JournalLine> JournalLines { get; set; } 
    }

}
