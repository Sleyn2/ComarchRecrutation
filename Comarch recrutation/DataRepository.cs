using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Model;
using NHibernate.Linq;

namespace Comarch_recrutation
{
    public class DataRepository
    {
        public static void Create(string dbFile)
        {
            DataFactory df = new DataFactory(dbFile, true);
            using(var sf = df.CreateSessionFactory())
            { sf.Close(); }
        }

        public static void AddBook(string dbFile, Book book)
        {
            DataFactory df = new DataFactory(dbFile, false);

                using (var sf = df.CreateSessionFactory())
                using (var session = sf.OpenSession())
                using (var trans = session.BeginTransaction())
                {
                    session.SaveOrUpdate(book);
                    trans.Commit();
                if (!IsThere(dbFile, book))
                    DelLast(dbFile);
                }
        }
        private static bool IsThere(string dbFile, Book book)
        {
            int n = 0;
            DataFactory df = new DataFactory(dbFile, false);
            using (var sf = df.CreateSessionFactory())
            using (var session = sf.OpenSession())
            using (var trans = session.BeginTransaction())
            {
                n = session.Query<Book>()
                    .Where(b => b.Title == book.Title)
                    .Count();
            }
            if (n == 0 || n ==1)
                return false;
            else return true;
        }
        private static void DelLast(string dbFile)
        {
            DataFactory df = new DataFactory(dbFile, false);
            using (var sf = df.CreateSessionFactory())
            using (var session = sf.OpenSession())
            using (var trans = session.BeginTransaction())
            {
                int n = session.Query<Book>().Count();
                n -= 1;
                session.Query<Book>().Where(b => b.Id == n).Delete();
            }
        }
        public static IList<Book> Select(string dbFile, string LastName)
        {
            DataFactory df = new DataFactory(dbFile, false);
            using (var sf = df.CreateSessionFactory())
            using (var session = sf.OpenSession())
            using (var trans = session.BeginTransaction())
            {
                return session.Query<Book>().Where(b => b.AuthorLastName == LastName).ToList();
            }
        }
        public static Book Select(string dbFile, int id)
        {
            DataFactory df = new DataFactory(dbFile, false);
            using (var sf = df.CreateSessionFactory())
            using (var session = sf.OpenSession())
            using (var trans = session.BeginTransaction())
            {
                IList<Book> temp = session.Query<Book>().Where(b => b.Id == id).ToList();
                return temp.First();
            }
        }
        public static IList<Book> SelectAll(string dbFile)
        {
            DataFactory df = new DataFactory(dbFile, false);
            using (var sf = df.CreateSessionFactory())
            using (var session = sf.OpenSession())
            using (var trans = session.BeginTransaction())
            {
                return session.Query<Book>().ToList();
            }
        }
        public static void Delete(string dbFile, string FirstName)
        {
            DataFactory df = new DataFactory(dbFile, false);
            using (var sf = df.CreateSessionFactory())
            using (var session = sf.OpenSession())
            using (var trans = session.BeginTransaction())
            {
                session.Query<Book>().Where(b => b.AuthorFirstName == FirstName).Delete();
                trans.Commit();
            }
        }
        public static void Delete(string dbFile, int id)
        {
            DataFactory df = new DataFactory(dbFile, false);
            using (var sf = df.CreateSessionFactory())
            using (var session = sf.OpenSession())
            using (var trans = session.BeginTransaction())
            {
                session.Query<Book>().Where(b => b.Id == id).Delete();
                trans.Commit();
            }
        }
        public static void UpdateName(string dbFile, string title , string newFirstName, string newLastName)
        {
            DataFactory df = new DataFactory(dbFile, false);
            using (var sf = df.CreateSessionFactory())
            using (var session = sf.OpenSession())
            using (var trans = session.BeginTransaction())
            {
                session.Query<Book>().Where(b => b.Title == title)
                    .Update(b => new Book { AuthorFirstName = newFirstName, AuthorLastName = newLastName });
                trans.Commit();
            }
        }
        public static void UpdateName(string dbFile, int id , string newFirstName, string newLastName)
        {
            DataFactory df = new DataFactory(dbFile, false);
            using (var sf = df.CreateSessionFactory())
            using (var session = sf.OpenSession())
            using (var trans = session.BeginTransaction())
            {
                session.Query<Book>().Where(b => b.Id == id)
                    .Update(b => new Book { AuthorFirstName = newFirstName, AuthorLastName = newLastName });
                trans.Commit();
            }
        }
    }
}
