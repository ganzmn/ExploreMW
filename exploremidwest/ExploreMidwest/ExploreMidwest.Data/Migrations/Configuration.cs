namespace ExploreMidwest.Data.Migrations
{
    using ExploreMidwest.Model;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExploreMidwest.Data.ExploreMidwestDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExploreMidwest.Data.ExploreMidwestDBContext context)
        {
            var userMgr = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleMgr.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleMgr.Create(role);
            }

            if (!userMgr.Users.Any(u => u.UserName == "manager"))
            {
                var user = new IdentityUser()
                {
                    UserName = "manager"
                };
                userMgr.Create(user, "testing1234");
            }
            var findmanager = userMgr.FindByName("manager");
            // create the user with the manager class
            if (!userMgr.IsInRole(findmanager.Id, "manager"))
            {
                userMgr.AddToRole(findmanager.Id, "manager");
            }


            if (!roleMgr.RoleExists("admin"))
            {
                roleMgr.Create(new IdentityRole() { Name = "admin" });
            }

            if (!userMgr.Users.Any(u => u.UserName == "admin"))
            {
                var user = new IdentityUser()
                {
                    UserName = "admin"
                };
                userMgr.Create(user, "testing123");
            }
            var finduser = userMgr.FindByName("admin");
            // create the user with the manager class
            if (!userMgr.IsInRole(finduser.Id, "admin"))
            {
                userMgr.AddToRole(finduser.Id, "admin");
            }


            context.Tags.AddOrUpdate(t => t.TagName,
                new Tag
                {
                    TagName = "Fall"
                },
                new Tag
                {
                    TagName = "BeautifulFall"
                },
                new Tag
                {
                    TagName = "SummerHoliday"
                },
                new Tag
                {
                    TagName = "SummerGateAway"
                },
                new Tag
                {
                    TagName = "BestGateAway"
                },
                new Tag
                {
                    TagName = "WhoNeedsVenice"
                },
                new Tag
                {
                    TagName = "CornFarm"
                },
                new Tag
                {
                    TagName = "TenThousandLakes"
                }

            );

            context.Category.AddOrUpdate(c => c.CategoryType,
                new Category
                {
                    CategoryType = "Michigan"
                },
                new Category
                {
                    CategoryType = "Wisconsin"
                },
                new Category
                {
                    CategoryType = "Minnesota"
                },
                new Category
                {
                    CategoryType = "Illinois"
                }
                );
            context.SaveChanges();

            context.Blog.AddOrUpdate(b => b.Title,
                new Blog
                {
                    Title = "Keweenaw Peninsula",
                    Body = "<p>The only things crowding Michigan's Keweenaw Peninsula in autumn are miles of coastline, " +
                            "fall color and Lake Superior lore.</p><p>#Fall #BeautifulFall</p>",
                    BlogCategory = context.Category.FirstOrDefault(c => c.CategoryType == "Michigan"),
                    BlogTags = new List<Tag>
                    {
                        context.Tags.FirstOrDefault(t => t.TagName == "Fall"),
                        context.Tags.FirstOrDefault(t => t.TagName ==  "BeautifulFall")
                    },
                    IsFinished = true,
                    IsDeleted = false,
                    Author = "admin",
                    ImageLocation = "images/greatLakes.jpg",
                    Date = DateTime.Parse("10/26/2017"),
                },
                new Blog
                {
                    Title = "Cana Island Lighthouse ",
                    Body = "<p>The 100-step climb to the top of Cana Island Lighthouse in Bailey's Harbor rewards visitors with " +
                           "some of the best views of Lake Michigan's Door County shoreline</p><p>#SummerHoliday #SummerGateAway</p>",
                    BlogCategory = context.Category.FirstOrDefault(c => c.CategoryType == "Wisconsin"),

                    BlogTags = new List<Tag>
                    {
                        context.Tags.FirstOrDefault(t => t.TagName == "SummerHoliday"),
                        context.Tags.FirstOrDefault(t => t.TagName == "SummerGateAway")
                    },
                    IsFinished = true,
                    IsDeleted = false,
                    Author = "admin",
                    ImageLocation = "images/canaisland7_2006.jpg",
                    Date = DateTime.Parse("7/04/2017")

                },
                new Blog
                {
                    Title = "Best Bets for Fall Fun in Stillwater",
                    Body = "<p>Who needs Venice, Italy? Get cozy in the cushioned seats of a Venetian gondola powered by " +
                            "a gondolier wearing a striped shirt and straw hat. For optimal romance, book a dinner package " +
                            "through the Dock Cafe and time a 45-minute gondola ride to watch the moon rise over the St. Croix River</p><p>#BestGateAway #WhoNeedsVenice</p>",
                    BlogCategory = context.Category.FirstOrDefault(c => c.CategoryType == "Minnesota"),
                    BlogTags = new List<Tag>
                    {
                        context.Tags.FirstOrDefault(t => t.TagName == "BestGateAway"),
                        context.Tags.FirstOrDefault(t => t.TagName == "WhoNeedsVenice")
                    },
                    IsFinished = true,
                    IsDeleted = false,
                    Author = "admin",
                    ImageLocation = "images/stillwaterliftbridge2.jpg",
                    Date = DateTime.Parse("05/10/2017")
                },
                new Blog
                {
                    Title = "Get Lost in These Midwest Corn Mazes",
                    Body = "<p>As if pulling off huge harvests and supplying food to the world wasn't enough, " +
                          "Midwest farmers also have an artsy side. Elaborate corn mazes put their design skills on full display. " +
                          "Bonus: Navigating through one is a crazy-fun way to spend an autumn afternoon</p><p>#CornFarm</p>",
                    BlogCategory = context.Category.FirstOrDefault(c => c.CategoryType == "Wisconsin"),
                    BlogTags = new List<Tag>
                    {
                        context.Tags.FirstOrDefault(t => t.TagName == "CornFarm")
                    },
                    IsFinished = true,
                    IsDeleted = false,
                    Author = "admin",
                    ImageLocation = "images/corn maze.jpg",
                    Date = DateTime.Parse("10/29/2017")

                },
                new Blog
                {
                    Title = "The Trick to Planning the Perfect Fall Getaway",
                    Body = "Time your autumn escape just right with these handy fall color reports " +
                           "from state tourism and natural resources groups.</p><p>#TenThousandLakes</p>",
                    BlogCategory = context.Category.FirstOrDefault(c => c.CategoryType == "Minnesota"),
                    BlogTags = new List<Tag>
                    {
                        context.Tags.FirstOrDefault(t => t.TagName == "TenThousandLakes")
                    },
                    IsFinished = true,
                    IsDeleted = false,
                    Author = "admin",
                    ImageLocation = "images/leaves.jpg",
                    Date = DateTime.Parse("03/22/2017")
                },
                new Blog
                {
                    Title = "The good life along Illinois’ Shawnee Hills Wine Trail",
                    Body = "<p>Becky Schneider pours a taste of Golden Oak Aged Reserve in the sun-washed tasting room" +
                           " at Pomona Winery near Alto Pass, Illinois, and watches as Anne Pitts sips it. " +
                           " “I used to have a problem calling wine creamy until this one,” Becky says, " +
                           " grinning as a familiar look of wonder spreads across Anne’s face and adding, " +
                           " “I’ve never tasted anything like it. I steam a lot of veggies in it; I throw it in pasta sauces</p><p></p>",
                    BlogCategory = context.Category.FirstOrDefault(c => c.CategoryType == "Illinois"),
                    BlogTags = new List<Tag>
                    {
                        context.Tags.FirstOrDefault(t => t.TagName == "WineTrail")
                    },
                    IsFinished = false,
                    IsDeleted = false,
                    Author = "Manager",
                    ImageLocation = "images/cheese.jpg",
                    Date = DateTime.Parse("08/02/2017")

                }

                );

            context.Page.AddOrUpdate(p => p.Title,
                new Page
                {
                    Title = "About Us",
                    Body = "Explore Midwest was established to connect the world with the sites and activities that the Midwest region has to offer.",
                    Url = "http://localhost:8080/aboutus",
                    IsInNavigation = true,
                    IsFinished = true
                },
                new Page
                {
                    Title = "Contact Us",
                    Body = "If you have any questions, please reach out to us at: CustomerService@exploreMW.com",
                    Url = "http://localhost:8080/contactus",
                    IsInNavigation = true,
                    IsFinished = true,
                },
                new Page
                {
                    Title = "Attractions",
                    Body = "Attraction list to be added",
                    Url = "http://localhost:8080/attractions",
                    IsInNavigation = false,
                    IsFinished = false,
                },
                new Page
                {
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
                    Title = "South Dakota",
                    Body = "I suppose you could go check out Mount Rushmore",
                    Url = "http://localhost:8080/contactus",
                    IsInNavigation = true,
                    IsFinished = true,
                },
                new Page
                {
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
                    Title = "Illinois",
                    Body = " Nicknamed 'the Prairie State', it's marked by farmland, forests, rolling hills and wetlands. Chicago, one of the largest cities in the U.S, is in the northeast on the shores of Lake Michigan. It’s famous for its skyscrapers, such as sleek, 1,451-ft. Willis Tower and the neo-Gothic Tribune Tower.",
                    Url = "http://localhost:8080/contactus",
                    IsInNavigation = true,
                    IsFinished = true,
                });


        }
    }
}
