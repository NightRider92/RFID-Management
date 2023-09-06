using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular.NET.Database.Entities
{
    /// <summary>
    /// Rfid data
    /// </summary>
    public class RfidData
    {
        public int ID { get; set; }
        public string? Username { get; set; }
        public string? Value { get; set; }
        public string? CreatedTime { get; set; }
    }
}
