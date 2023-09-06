using Angular.NET.Database.Entities;
using Microsoft.Extensions.Hosting;

namespace Angular.NET.Database
{
    public interface IDatabaseService : IHostedService
    {
        /// <summary>
        /// Add user asynchronously to sqlite database
        /// </summary>
        /// <returns></returns>
        public Task AddUserAsync(RfidData data);

        /// <summary>
        /// Remove user asynchronously from sqlite database
        /// </summary>
        /// <returns>Bool value</returns>
        public Task<bool> RemoveUserAsync(int id);

        /// <summary>
        /// Get all users from the database 
        /// </summary>
        /// <returns>List of users</returns>
        public Task<List<RfidData>> GetAllUsersAsync();

        /// <summary>
        /// Perform check if user has access
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<bool> UserHasAccessAsync(string value);
    }
}