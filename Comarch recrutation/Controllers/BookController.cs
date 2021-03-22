using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Model;

namespace Comarch_recrutation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private string dbFile = @"C:\temp\test4.db";

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return DataRepository.SelectAll(dbFile);
        }

        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return DataRepository.Select(dbFile, id);
        }

        [HttpGet("{firstName}/{lastName}")]
        public IEnumerable<Book> Get(string firstName, string lastName)
        {
            //zastosowane filtrowanie LINQ where
            return DataRepository.SelectAll(dbFile)
                .Where(b => b.AuthorLastName == lastName && b.AuthorFirstName == firstName);
        }

        /*Test Code dla POST:
        {
        "title": "Krzyżacy",
        "authorFirstName": "Henryk",
        "authorLastName": "Sienkiewicz"
        }
        */
        [HttpPost]
        public void Post([FromBody] Book book)
        {
            DataRepository.AddBook(dbFile, book);
        }


        /*Test code dla PUT:
         api/book/3

        {
        "authorFirstName": "Bolesław",
        "authorLastName": "Prus"
        } 
        */
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Book book)
        {
            DataRepository.UpdateName(dbFile, id, book.AuthorFirstName, book.AuthorLastName);
        }

        /*
        Test adress:
        api/book/3
         */
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            DataRepository.Delete(dbFile, id);
        }
    }
}
