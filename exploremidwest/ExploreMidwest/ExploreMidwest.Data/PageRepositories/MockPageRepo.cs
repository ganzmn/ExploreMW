using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExploreMidwest.Model;

namespace ExploreMidwest.Data.PageRepositories
{
    public class MockPageRepo : IPageRepo
    {
        private static List<Page> _pages = new List<Page>
        {
            new Page
            {
                PageId = 1,
                Title = "About Us",
                Body = "Explore Midwest was established to connect the world with the sites and activities that the Midwest region has to offer.",
                Url = "http://localhost:8080/aboutus",
                IsInNavigation = true,
                IsFinished = true
            },
            new Page
            {
                PageId = 2,
                Title = "Contact Us",
                Body = "If you have any questions, please reach out to us at: CustomerService@exploreMW.com",
                Url = "http://localhost:8080/contactus",
                IsInNavigation = true,
                IsFinished = true,
            },
            new Page
            {
                PageId = 3,
                Title = "Attractions",
                Body = "Attraction list to be added",
                Url = "http://localhost:8080/attractions",
                IsInNavigation = false,
                IsFinished = false,
            },
            new Page
            {
                PageId = 4,
                Title = "Minnesota",
                Body = "Minnesota offers an endless array of things to do on vacation. Outdoor pursuits include fishing and boating, " +
                "great golf, and some of the country’s best bike trails. There are excellent museums of all types, and options for " +
                "live theater abound. Numerous wineries, breweries and distilleries are open for tours and tastings. And shopping is always nearby, " +
                "including Mall of America, the state’s most-visited attraction.",
                Url = "http://localhost:8080/contactus",
                IsInNavigation = true,
                IsFinished = true,
            },
            new Page
            {
                PageId = 5,
                Title = "Wisconsin",
                Body = "Wisconsin offers a wide variety of things to do and endless opportunities to have fun. " +
                "Whether you enjoy scouting waterfalls and fishing spots or exploring cities packed with sports, " +
                "arts and culture, you'll find it here!",
                Url = "http://localhost:8080/contactus",
                IsInNavigation = true,
                IsFinished = true,
            },
            new Page
            {
                PageId = 6,
                Title = "North Dakota",
                Body = "From legendary outdoor adventures to the bright lights and excitement of our casinos, " +
                "there are a variety of things to see and do during your visit to North Dakota. Whether you’re " +
                "searching for history, family fun, shopping, arts and culture or nightlife, you’ve come to the right place.",
                Url = "http://localhost:8080/contactus",
                IsInNavigation = true,
                IsFinished = true,
            },
            new Page
            {
                PageId = 7,
                Title = "South Dakota",
                Body = "I suppose you could go check out Mount Rushmore",
                Url = "http://localhost:8080/contactus",
                IsInNavigation = true,
                IsFinished = true,
            },
            new Page
            {
                PageId = 8,
                Title = "Iowa",
                Body = "sits between the Missouri and Mississippi rivers. It’s known for its landscape of rolling " +
                "plains and cornfields. Landmarks in the capital, Des Moines, include the gold-domed, 19th-century " +
                "State Capitol Building, Pappajohn Sculpture Park and the Des Moines Art Center, noted for its contemporary " +
                "collections. The city of Cedar Rapids' Museum of Art has paintings by native Iowan Grant Wood.",
                Url = "http://localhost:8080/contactus",
                IsInNavigation = true,
                IsFinished = true,
            },
            new Page
            {
                PageId = 9,
                Title = "Illinois",
                Body = " Nicknamed 'the Prairie State', it's marked by farmland, forests, rolling hills and wetlands. Chicago, one of the largest cities in the U.S, is in the northeast on the shores of Lake Michigan. It’s famous for its skyscrapers, such as sleek, 1,451-ft. Willis Tower and the neo-Gothic Tribune Tower.",
                Url = "http://localhost:8080/contactus",
                IsInNavigation = true,
                IsFinished = true,
            }
        };


        public void AddPage(Page p)
        {
            _pages.Add(p);
        }

        public void EditPage(Page pa)
        {
            var found = _pages.FirstOrDefault(p => p.PageId == pa.PageId);

            if (found != null)
                found = pa;
        }

        public List<Page> GetAllPages()
        {
            return _pages;
        }

        public Page GetPage(int pageId)
        {
            return _pages.FirstOrDefault(p => p.PageId == pageId);
        }

        public List<Page> GetUnfinshedPages()
        {
            return _pages.Where(p => p.IsFinished == false).ToList();
        }

        public void RemovePage(int pageId)
        {
            _pages.RemoveAll(p => p.PageId == pageId);
        }
    }
}
