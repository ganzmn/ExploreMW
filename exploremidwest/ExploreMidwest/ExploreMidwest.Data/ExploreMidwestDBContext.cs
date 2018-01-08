using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ExploreMidwest.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ExploreMidwest.Data
{
    public class ExploreMidwestDBContext : IdentityDbContext
    {
        public ExploreMidwestDBContext()
            : base("ExploreMidwest"){

        }
        
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
