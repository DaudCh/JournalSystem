using JournalSystem.Core.Entities;

public class JournalLine
{
    public int Id { get; set; }
    public int JournalEntryId { get; set; }
    public int AccountId { get; set; }
    public string Description { get; set; }
    public string Reference { get; set; }
    public float Debit { get; set; }
    public float Credit { get; set; }
    public int CostCenterId { get; set; }
    public int DimensionId { get; set; }

   
    public JournalEntry JournalEntry { get; set; }
    public Account Account { get; set; }
    public CostCenter CostCenter { get; set; }
    public Dimension Dimension { get; set; }
}