using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExploreMidwest.Model;
using System.Data.Entity.Migrations;

namespace ExploreMidwest.Data.PageRepositories
{
    public class EFPageRepo : IPageRepo
    {
        ExploreMidwestDBContext context = new ExploreMidwestDBContext();

        public void AddPage(Page p)
        {
            context.Page.Add(p);
            context.SaveChanges();
        }

        public void EditPage(Page pa)
        {
            context.Page.AddOrUpdate(pa);
            context.SaveChanges();
        }

        public List<Page> GetAllPages()
        {
            var pages = from p in context.Page
                        select p;
            return pages.ToList();
        }

        public Page GetPage(int pageId)
        {
            var page = (from p in context.Page where p.PageId == pageId select p).FirstOrDefault();
            return page;
        }

        public List<Page> GetUnfinshedPages()
        {
            return context.Page.Where(p => p.IsFinished == false).ToList();
        }

        public void RemovePage(int pageId)
        {
            var page = (from p in context.Page where p.PageId == pageId select p).FirstOrDefault();
            context.Page.Remove(page);
            context.SaveChanges();
        }
    }
}
