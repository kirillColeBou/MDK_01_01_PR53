using Microsoft.EntityFrameworkCore;

namespace PermDynamics_Тепляков.Classes
{
    public class ChartContext : DbContext
    {
        public DbSet<ChartsData> ChartsData { get; set; }

        public ChartContext()
        {
            Database.EnsureCreated();
            ChartsData.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySql("server=localhost;Database=pr53;uid=root;");
    }
}
