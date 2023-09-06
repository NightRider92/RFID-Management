using Angular.NET.Database;
using Angular.NET.Database.Entities;
using Angular.NET.Database.SQLiteCommon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Angular.NET.Controllers
{
    /// <summary>
    /// Database controller logger
    /// </summary>
    /// 
    [ApiController]
    [Route("/api/v1/database")]
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> logger;
        private readonly IDatabaseService dbService;

        public DatabaseController(ILogger<DatabaseController> _logger, IDatabaseService _dbService)
        {
            this.logger = _logger;
            this.dbService = _dbService;
        }

        /// <summary>
        /// Insert user data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("insert-data")]
        public async Task<JsonResult> Insert([FromBody] RfidControllerData data)
        {
            try
            {
                RfidData entityData = new RfidData()
                {
                    ID = data.ID,
                    Username = data.Username,
                    Value = data.Value,
                    CreatedTime = data.CreatedTime
                };

                await this.dbService.AddUserAsync(entityData);
                return new JsonResult($"Data: {JsonConvert.SerializeObject(data)} has been added to database");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                return new JsonResult(ex);
            }
        }

        /// <summary>
        /// Remove user data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpDelete("remove-data")]
        public async Task<JsonResult> Remove([FromQuery] int id)
        {
            try
            {
                if (await this.dbService.RemoveUserAsync(id))
                    return new JsonResult($"Entity with id = {id} has been removed from the database");
                return new JsonResult($"Entity with id = {id} does not exist in the database");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                return new JsonResult(ex);
            }
        }

        /// <summary>
        /// Remove user data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpGet("request-all-data")]
        public async Task<JsonResult> RequestAllData()
        {
            try
            {
                List<RfidData> data = await dbService.GetAllUsersAsync();
                return new JsonResult(data);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                return new JsonResult(ex);
            }
        }
    }
}
