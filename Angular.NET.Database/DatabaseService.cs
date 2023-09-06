using Angular.NET.Database.Entities;
using Angular.NET.Database.SQLiteCommon;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.NET.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly ILogger<DatabaseService> logger;
        private readonly IServiceProvider serviceProvider;

        private SqliteConnection? sqlConnection;
        private readonly DatabaseContext dbContext;
        private readonly DbContextOptions dbContextOptions;
        public DatabaseService(ILogger<DatabaseService> _logger, IServiceProvider _serviceProvider)
        {
            this.logger = _logger;
            this.logger.LogInformation("Initializing database service");

            this.serviceProvider = _serviceProvider;
            this.dbContext = this.serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();

            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "data.db" };
            this.sqlConnection = new SqliteConnection(connectionStringBuilder.ToString());
            this.dbContextOptions = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(this.sqlConnection).Options;
        }

        /// <inheritdoc />
        public async Task AddUserAsync(RfidData data)
        {
            try
            {
                DatabaseContext dbContext = new DatabaseContext((DbContextOptions<DatabaseContext>)this.dbContextOptions);
                data.Value = data.Value?.ToUpperInvariant();
                dbContext.DbSetRfidData.Add(data);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex) { this.logger.LogError(ex.ToString()); }
        }

        /// <inheritdoc />
        public async Task<bool> RemoveUserAsync(int id)
        {
            try
            {
                DatabaseContext dbContext = new DatabaseContext((DbContextOptions<DatabaseContext>)this.dbContextOptions);
                var entity = dbContext.Find<RfidData>(id);
                if (entity == null) return false;

                dbContext.DbSetRfidData.Remove(entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { this.logger.LogError(ex.ToString()); return false; }
        }

        /// <inheritdoc />
        public Task<List<RfidData>> GetAllUsersAsync()
        {
            try
            {
                DatabaseContext dbContext = new DatabaseContext((DbContextOptions<DatabaseContext>)this.dbContextOptions);
                return Task.FromResult(dbContext.DbSetRfidData.AsEnumerable().ToList());
            }
            catch (Exception ex) { this.logger.LogError(ex.ToString()); return Task.FromResult(new List<RfidData>()); }
        }

        /// <inheritdoc />
        public Task<bool> UserHasAccessAsync(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value)) return Task.FromResult(false); ;
                value = value.Trim().ToUpperInvariant();

                DatabaseContext dbContext = new DatabaseContext((DbContextOptions<DatabaseContext>)this.dbContextOptions);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                //Value won't be null because server does not accept null values
                var entity = dbContext.DbSetRfidData.FirstOrDefault(e => e.Value.Equals(value));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (entity == null) return Task.FromResult(false);
                return Task.FromResult(true);
            }
            catch (Exception ex) { this.logger.LogError(ex.ToString()); return Task.FromResult(false); }
        }

        /// <inheritdoc />
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Starting database service");
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Stopping database service");
            return Task.CompletedTask;
        }
    }
}
