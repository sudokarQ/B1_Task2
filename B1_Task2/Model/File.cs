using System.Collections.Generic;
using System;

namespace B1_Task2.Model
{
    public class File
    {
        public int Id { get; set; }
        
        public int BankId { get; set; }
        
        public string FileName { get; set; }
        
        public DateTime PeriodStart { get; set; }
        
        public DateTime PeriodEnd { get; set; }
        
        public List<DataEntry> DataEntries { get; set; }

        public override bool Equals(object? obj)
        {
            var b = obj as File;

            return BankId == b.BankId
                && FileName == b.FileName
                && PeriodStart == b.PeriodStart
                && PeriodEnd == b.PeriodEnd;
        }
    }
}
