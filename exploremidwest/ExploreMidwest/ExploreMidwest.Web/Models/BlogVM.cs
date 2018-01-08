using ExploreMidwest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExploreMidwest.Web.Models
{
    public class BlogVM
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Category BlogCategory { get; set; }
        public ICollection<Tag> BlogTags { get; set; }
        public string Body { get; set; }
        public bool IsFinished { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public string NewCategory { get; set; }
        public string ImageLocation { get; set; }
        public HttpPostedFileBase File { get; set; }

        public List<SelectListItem> CategoryItems { get; set; }       

        public BlogVM()
        {
            CategoryItems = new List<SelectListItem>();
            BlogTags = new List<Tag>();
            BlogCategory = new Category();
        }

        public void SetCategories(List<Category> category)
        {
            category = category.OrderBy(c => c.CategoryType).ToList();
            foreach (var c in category)
            {
                
                CategoryItems.Add(new SelectListItem()
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryType

                });
            }
        }

        

    }
}