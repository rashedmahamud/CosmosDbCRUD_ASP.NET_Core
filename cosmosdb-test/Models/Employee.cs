using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cosmosdb_test.Models
{
    public class Employee
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string  Name { get; set; }

        [JsonProperty(PropertyName = "dept")]
        public string  Dept { get; set; }
    }
}
