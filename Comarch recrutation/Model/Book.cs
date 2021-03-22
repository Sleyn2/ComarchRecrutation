using System;


namespace NHibernate.Model
{
    public class Book
    {
        public virtual int Id { get; set; }
        public virtual String Title { get; set; }
        public virtual String AuthorFirstName { get; set; }
        public virtual String AuthorLastName { get; set; }

        public virtual String GetAuthorFullName()
        {
            return $"{AuthorFirstName} {AuthorLastName}";
        }
        public Book() { }
        public Book(string title, string first, string last)
        {
            this.Title = title;
            this.AuthorFirstName = first;
            this.AuthorLastName = last;
        }

    }
}
