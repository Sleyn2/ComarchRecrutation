using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Model;

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
            using(var sf = df.CreateSessionFactory())
                using(var session = sf.OpenSession())
                using(var trans = session.BeginTransaction())
            {
                session.SaveOrUpdate(book);
                trans.Commit();
            }
        }
        public  
    }
}
