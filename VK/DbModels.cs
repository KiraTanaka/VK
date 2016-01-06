using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace VK
{
    [Table("people")]
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [Required]
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [Required]
        public int UID { get; set; }
    }
    [Table("access_token")]
    public class AccessTokenDB
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AccessToken { get; set; }
    }
}