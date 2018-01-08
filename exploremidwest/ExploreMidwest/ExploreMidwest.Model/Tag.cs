using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploreMidwest.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<Blog> Blog { get; set;}

        public Tag()
        {
            Blog = new HashSet<Blog>();
        }
    }
}
