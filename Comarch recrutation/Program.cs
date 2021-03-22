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
            AddBooks();
            //zmiana autora "Lalki"
            DataRepository.UpdateName(dbFile, "Lalka", "Karol", "Kowalski");
            //usuniêcie b³êdnego rekordu "Lalki"
            //DataRepository.Delete(dbFile, 3);


            CreateHostBuilder(args).Build().Run();
        }

        public static void AddBooks()
        {
            string dbFile = @"C:\temp\test4.db";
            DataRepository.Create(dbFile);
            Book book = new Book
            {
                Title = "Ostatnie ¿yczenie",
                AuthorFirstName = "Andrzej",
                AuthorLastName = "Sapkowski"
            };
            //book 2 dodane w celu test czy nie dodaje podwójnie rekordów do ju¿ istniej¹cej
            //bazy danych (2 uruchomienie programu)
            Book book2 = new Book
            {
                Title = "Krew Elfów",
                AuthorFirstName = "Andrzej",
                AuthorLastName = "Sapkowski"
            };
            //book 3 dodane w celu testowania modyfikacji danych oraz ich usuwania
            Book book3 = new Book
            {
                Title = "Lalka",
                AuthorFirstName = "Boles³aw",
                AuthorLastName = "Prus"
            };

            DataRepository.AddBook(dbFile, book);
            DataRepository.AddBook(dbFile, book2);
            DataRepository.AddBook(dbFile, book3);

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
