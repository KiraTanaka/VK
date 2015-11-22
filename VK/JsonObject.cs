using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VK
{
    [Table("popular_video")]
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        [JsonProperty("vid")]
        public int Vid { get; set; }
        [Required]
        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }
        [Required]
        [JsonProperty("views")]
        public int Views { get; set; }
    }
    public class PersonsID
    {
        public List<int> PersonsId { get; set; }
    }
    [JsonObject]
    public class VideoCollection
    {
        private int countvideo;
        [JsonProperty("response")]
        public List<Video> ListVideo { get; set; }
        public int CountVideo { get{return countvideo;} set{countvideo=value;} }
    }
}
