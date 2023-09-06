using Angular.NET.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.NET.Database.SQLiteCommon
{
    /// <summary>
    /// Database context
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=data.db");
        }

        /// <summary>
        /// Database set of RfidData object
        /// </summary>
        public DbSet<RfidData> DbSetRfidData { get; set; }
    }
}
