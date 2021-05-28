using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DHT11
{
    class PostgresDBContext : DbContext
    {
        public PostgresDBContext() : base(nameOrConnectionString: "PostgreSQL") { }

        public DbSet<SensorRecord> list_record { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

    }
}
