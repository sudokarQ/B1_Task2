using System.Collections.Generic;

namespace B1_Task2.Model
{
    public class Bank
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public List<File> Files { get; set; }

    }
}
