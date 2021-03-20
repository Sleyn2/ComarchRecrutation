
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using System.IO;
using NHibernate.Tool.hbm2ddl;


namespace Comarch_recrutation
{
    internal class DataFactory
    {
        string _dbFile = string.Empty;
        bool _overwriteExisting = false;
        public DataFactory(string dbFile, bool overwriteexisting)
        {
            _dbFile = dbFile;
            _overwriteExisting = overwriteexisting;
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard.UsingFile(_dbFile)
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DataFactory>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private void BuildSchema(Configuration config)
        {
            if(_overwriteExisting)
            {
                if (File.Exists(_dbFile))
                    File.Delete(_dbFile);

                var se = new SchemaExport(config);
                se.Create(false, true);
            }
        }
    }
}
