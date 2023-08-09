namespace B1_Task2.Model
{
    public class DataEntry
    {
        public int Id { get; set; }
        
        public int FileId { get; set; }
        
        public int AccountCode { get; set; }
        
        public decimal BeginningDebitBalance { get; set; }
        
        public decimal BeginningCreditBalance { get; set; }
        
        public decimal DebitTurnover { get; set; }
        
        public decimal CreditTurnover { get; set; }
        
        public decimal EndingDebitBalance { get; set; }
        
        public decimal EndingCreditBalance { get; set; }
    }
}
