using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VK
{
    [JsonObject]
    public class Person
    {
        [JsonProperty("id")]
        public int uid { get; set; }
        [JsonProperty]
        public string first_name { get; set; }
        [JsonProperty]
        public string last_name { get; set; }
    }
    [JsonObject]
    public class Video
    {
        [JsonProperty]
        public int vid { get; set; }
        [JsonProperty]
        public int owner_id { get; set; }
        //[JsonProperty]
        //public string title { get; set; }
        //[JsonProperty]
        //public string description { get; set; }
        //[JsonProperty]
        //public int duration { get; set; }
        //[JsonProperty]
        //public string link { get; set; }
        //[JsonProperty]
        //public int date { get; set; }
        [JsonProperty]
        public int views { get; set; }
        //[JsonProperty]
        //public string image { get; set; }
        //[JsonProperty]
        //public string image_medium { get; set; }
        //[JsonProperty]
        //public int comments { get; set; }
        //[JsonProperty]
        //public string player { get; set; }
    }
    [JsonObject]
    public class PersonID
    {
        [JsonProperty]
        public int uid { get; set; }
    }
    [JsonObject]
    public class PersonsID
    {
        [JsonProperty]
        public int[] response { get; set; }
    }
    [JsonObject]
    p class VideoCollection
    {
        private int countvideo;
        private Video[] response;
        public int CountVideo { get{return countvideo;} set{countvideo=value;} }
        [JsonProperty]
        public Video[] ArrayVideo { get{return response;} set{response=value;} }
    }
}
