using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalSystem.Core.DTOS.JournalLines
{
    public class JournalLineDTO
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int LineNumber { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public float Debit { get; set; }
        public float Credit { get; set; }
        public int CostCenterId { get; set; }
        public int DimensionId { get; set; }
    }

    public class CreateJournalLineDTO
    {
        public int JournalEntryId { get; set; }
        public int LineNumber { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public float Debit { get; set; }
        public float Credit { get; set; }
        public int CostCenterId { get; set; }
        public int DimensionId { get; set; }
    }

    public class UpdateJournalLineDTO
    {
        public int Id { get; set; }
        public int JournalEntryId { get; set; }
        public int LineNumber { get; set; }
        public int AccountId { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public float Debit { get; set; }
        public float Credit { get; set; }
        public int CostCenterId { get; set; }
        public int DimensionId { get; set; }
    }

    public class JournalEntryLinesDTO
    {
        public int JournalEntryId { get; set; }
        public List<JournalLineDTO> JournalLines { get; set; }
    }

}
