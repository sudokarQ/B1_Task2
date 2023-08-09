using B1_Task2.Model;

using Microsoft.EntityFrameworkCore;

namespace B1_Task2
{
    public class SqlContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<DataEntry> DataEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=B1_Test;Integrated Security=True");
        }
    }

}
