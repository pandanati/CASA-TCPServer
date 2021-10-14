using CASABookClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CASA_TCPServer
{
    class BookCatalog
    {
        public static readonly List<Book> bookList1 = new List<Book>()
        {
            new Book(){Author = "Natalie", ISBN = "123ABC", PageNumber = 5, Title = "Ohh"},
            new Book(){Author = "Pedro", ISBN = "1234ABCD", PageNumber = 102, Title = "Noo"},
            new Book(){Author = "Tom", ISBN = "ABCD1234", PageNumber = 105, Title = "The Way Of An Alien"}
        };
        
        public List<Book> GetAll()
        {
            List<Book> result = new List<Book>(bookList1);
            return result;
        }

        public Book GetByISBN(string isbn)
        {
            return bookList1.Find(x => x.ISBN == isbn);

        }

        public void Save(Book newBook)
        {
            bookList1.Add(newBook);
        }

    }




}
