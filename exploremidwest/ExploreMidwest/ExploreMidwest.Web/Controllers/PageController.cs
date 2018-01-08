using ExploreMidwest.Data.PageRepositories;
using ExploreMidwest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ExploreMidwest.Web.Controllers
{
    public class PageController : ApiController
    {
        IPageRepo repo = PageRepoFactory.Create();
        [Route("pages")]
        public List<Page> GetAll()
        {
            return repo.GetAllPages();
        }
    }
}