using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExploreMidwest.Model;

namespace ExploreMidwest.Data.BlogRepositories
{
    public class MockBlogRepo : IBlogRepo
    {
        private static List<Blog> _blogs = new List<Blog>
        {
            new Blog
            {
                BlogId = 1,
                Title = "River Rafting",
                Body = "What a great time it is rafting down the rapids in Minnesota. Going so fast makes it hard to keep a smile off my face",
                BlogCategory = new Category
                {
                    CategoryId = 1,
                    CategoryType = "Minnesota"
                },
                BlogTags = new List<Tag>
                {
                    new Tag
                    {
                        TagId = 1,
                        TagName = "#Funtime"
                    },
                    new Tag
                    {
                        TagId = 2,
                        TagName = "#Rivers"
                    },
                    new Tag
                    {
                        TagId = 3,
                        TagName = "#Rafting"
                    }
                },
                IsFinished = true,
                IsDeleted = false,
                Date = DateTime.Parse("10/21/2017"),
            },
            new Blog
            {
                BlogId = 2,
                Title = "Corn Maze",
                Body = "What a wonderful maze I found ",
                BlogCategory = new Category
                {
                    CategoryId = 2,
                    CategoryType = "Iowa"
                },
                BlogTags = new List<Tag>
                {
                    new Tag
                    {
                        TagId = 4,
                        TagName = "#Coolplaces"
                    },
                    new Tag
                    {
                        TagId = 5,
                        TagName = "#Outdoors"
                    },
                    new Tag
                    {
                        TagId = 1,
                        TagName = "#Funtime"
                    }
                },
                IsFinished = true,
                IsDeleted = false,
                Date = DateTime.Parse("8/19/2017"),
            },
            new Blog
            {
                BlogId = 3,
                Title = "Football Game",
                Body = "I went out to a cornhuskers game last night and it was a great time. The place was so packed and so loud everytime they scored thinking about it makes we want to go back right now!",
                BlogCategory = new Category
                {
                    CategoryId = 3,
                    CategoryType = "Nebraska"
                },
                BlogTags = new List<Tag>
                {
                    new Tag
                    {
                        TagId = 1,
                        TagName = "#Goteam"
                    },
                    new Tag
                    {
                        TagId = 2,
                        TagName = "#Bigcity"
                    }
                },
                IsFinished = true,
                IsDeleted = false,
                Date = DateTime.Parse("10/15/2017"),
            },
            new Blog
            {
                BlogId = 4,
                Title = "City Life",
                Body = "Living in Minneapolis always has its challenges. It can be fun too ",
                BlogCategory = new Category
                {
                    CategoryId = 1,
                    CategoryType = "Minnesota"
                },
                BlogTags = new List<Tag>(),
                IsFinished = false,
                IsDeleted = false,
                Date = DateTime.Parse("10/26/2017"),
            }
        };

        public void AddBlog(Blog blog)
        {
            _blogs.Add(blog);
        }

        public void DeleteBlog(int blogId)
        {
            _blogs.RemoveAll(b => b.BlogId == blogId);
        }

        public void EditBlog(Blog blog)
        {
            var b = new Blog();
            foreach(var blo in _blogs)
            {
                if(blo.BlogId == blog.BlogId)
                {
                    blo.Title = blog.Title;
                    blo.Body = blog.Body;
                    blo.BlogCategory = blog.BlogCategory;
                    blo.Date = blog.Date;
                    blo.IsDeleted = blog.IsDeleted;
                    blo.IsFinished = blog.IsFinished;
                    blo.BlogTags = blog.BlogTags;
                }
            }
            b = blog;
        }

        public Blog GetBlogById(int BlogId)
        {
            foreach(var blog in _blogs)
            {
                if(blog.BlogId == BlogId)
                {
                    return blog;
                }
            }
            return new Blog();
        }

        public List<Blog> GetBlogsByCategory(string category)
        {
            return _blogs.Where(b => b.BlogCategory.CategoryType == category).ToList();
        }

        public List<Blog> GetBlogsByDate(string date)
        {
            var day = DateTime.Parse(date);
            return _blogs.Where(b => b.Date == day).ToList();
        }

        public List<Blog> GetBlogsByTag(string tag)
        {
            var toReturn = new List<Blog>();
            foreach(var blog in _blogs)
            {
                foreach(var tags in blog.BlogTags)
                {
                    if(tag == tags.TagName)
                    {
                        toReturn.Add(blog);
                    }
                }
            }
            return toReturn;
        }

        public List<Blog> GetBlogsByTitle(string title)
        {
            return _blogs.Where(b => b.Title == title).ToList();
        }

        public List<Blog> GetNumberOfBlogs(int number, int set)
        {
            return _blogs.Skip(number * set).Take(number).ToList();
        }

        public List<Blog> GetSavedFromAuthor(string author)
        {
            return _blogs.Where(b => b.Author == author).Where(b => b.IsFinished == false).ToList();
        }

        public List<Blog> GetUnpublishedBlogs()
        {
            return _blogs.Where(b => b.IsFinished == false).ToList();
        }
    }
}
