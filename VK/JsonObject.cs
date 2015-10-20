using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VK
{

    [JsonObject]
    public class Video
    {
        [JsonProperty("vid")]
        public int vid { get; set; }
        [JsonProperty("owner_id")]
        public int owner_id { get; set; }
        [JsonProperty("views")]
        public int views { get; set; }
    }
    [JsonObject]
    public class PersonsID
    {
        [JsonProperty("response")]
        public List<int> PersonsId { get; set; }
    }
    [JsonObject]
    public class VideoCollection
    {
        private int countvideo;
        [JsonProperty("response")]
        public List<Video> ArrayVideo { get; set; }
        public int CountVideo { get{return countvideo;} set{countvideo=value;} }
    }

    //[JsonObject]
    //public class Person
    //{
    //    [JsonProperty("id")]
    //    public int uid { get; set; }
    //    [JsonProperty]
    //    public string first_name { get; set; }
    //    [JsonProperty]
    //    public string last_name { get; set; }
    //}
}
