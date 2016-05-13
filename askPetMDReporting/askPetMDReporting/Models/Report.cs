using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace askPetMDReporting.Models
{
    public class Report
    {
        [JsonProperty("total_count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public Results[] Results { get; set; }
    }
}
