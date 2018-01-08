using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploreMidwest.Data.PageRepositories
{
    public static class PageRepoFactory
    {
        public static IPageRepo Create()
        {
            string mode = ConfigurationManager.AppSettings["Page"].ToString();

            switch (mode)
            {
                case "EF":
                    return new EFPageRepo();
                case "Mock":
                    return new MockPageRepo();
                default:
                    throw new Exception("Mode value in app config is not valid.");
            }
        }
    }
}
