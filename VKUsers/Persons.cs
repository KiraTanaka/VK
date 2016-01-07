using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VK;

namespace VKUsers
{
    public class Persons
    {
        [JsonProperty("response")]
        public List<Person> People { get; set; }
    }
    public class MembersOfGroup
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("users")]
        public List<Person> People { get; set; }
    }
    public class ResponseMembers
    {
        public MembersOfGroup response { get; set; }
    }
    public class ResponseScreenName
    {
        public ResolveScreenName response { get; set; }
    }
    public class ResolveScreenName
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("object_id")]
        public int ObjectId { get; set; }
    }
}
