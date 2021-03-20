using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.Model
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Id(x => x.Id)
                .Column("Id")
                .GeneratedBy.Guid();
            Map(x => x.Title);
            Map(x => x.AuthorFirstName);
            Map(x => x.AuthorLastName);

        }
    }
}
