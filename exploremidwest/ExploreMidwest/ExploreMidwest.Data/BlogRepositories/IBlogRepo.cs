using ExploreMidwest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExploreMidwest.Data.BlogRepositories
{
    public interface IBlogRepo
    {
        List<Blog> GetNumberOfBlogs(int number, int set);
        List<Blog> GetBlogsByTitle(string title);
        List<Blog> GetBlogsByCategory(string category);
        List<Blog> GetBlogsByDate(string date);
        List<Blog> GetBlogsByTag(string tag);
        List<Blog> GetUnpublishedBlogs();
        List<Blog> GetSavedFromAuthor(string author);
        Blog GetBlogById(int BlogId);
        void AddBlog(Blog blog);
        void DeleteBlog(int blogId);
        void EditBlog(Blog blog);
    }
}
