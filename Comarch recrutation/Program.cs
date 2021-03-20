using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Mapping;
using NHibernate.Model;
using Comarch_recrutation;

namespace Comarch_recrutation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dbFile = @"C:\temp\test4.db";
            DataRepository.Create(dbFile);

            Book book = new Book
            {
                Title = "Ostatnie zyczenie",
                AuthorFirstName = "Andrzej",
                AuthorLastName = "Sapkowski"
            };
            DataRepository.AddBook(dbFile, book);
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
