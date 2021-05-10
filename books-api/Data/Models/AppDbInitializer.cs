using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace books_api.Data.Models
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book
                    {
                        Title = "First Book",
                        Description = "First Description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-5),
                        Rate = 3,
                        Genre = "Biography",
                        CoverUrl = "URL",
                        DateAdded = DateTime.Now.AddDays(20)
                    },
                    new Book
                    {
                        Title = "Second Book",
                        Description = "First Description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-2),
                        Rate = 4,
                        Genre = "Fiction",
                        CoverUrl = "URL",
                        DateAdded = DateTime.Now.AddDays(10)
                    },
                    new Book
                    {
                        Title = "Third Book",
                        Description = "Third Description",
                        IsRead = false,
                        Genre = "Novel",
                        CoverUrl = "URL",
                        DateAdded = DateTime.Now.AddDays(20)
                    }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
