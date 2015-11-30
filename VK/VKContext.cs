using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VK
{
    public class VKContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Video> PopularVideo { get; set; }
        public DbSet<AccessTokenDB> AccessToken { get; set; }
    }
}