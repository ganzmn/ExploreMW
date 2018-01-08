using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploreMidwest.Model
{
    public class Page
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public bool IsInNavigation { get; set; }
        public bool IsFinished { get; set; }
    }
}
