using ExploreMidwest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploreMidwest.Data.PageRepositories
{
    public interface IPageRepo
    {
        Page GetPage(int pageId);
        List<Page> GetAllPages();
        List<Page> GetUnfinshedPages();
        void AddPage(Page p);
        void RemovePage(int pageId);
        void EditPage(Page pa);
    }
}
