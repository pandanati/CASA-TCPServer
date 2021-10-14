using CASABookClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace CASA_TCPServer
{
    class TCPBookServer
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TcpListener listener = new TcpListener(IPAddress.Loopback, 4646);
            listener.Start();
            Console.WriteLine("The server has now started");

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(() => { ManageClient(socket); });
            }
        }

        public static void ManageClient(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            BookCatalog catalog = new BookCatalog();
            Console.WriteLine("The client had now successfully connected");

            while (true)
            {
                string firstLine = reader.ReadLine();
                string secondLine = reader.ReadLine();
                if (firstLine == "getAll" && string.IsNullOrEmpty(secondLine))
                {
                    List<Book> books = catalog.GetAll();
                    string bookjsonString = JsonSerializer.Serialize(books);

                    writer.WriteLine(bookjsonString);
                    writer.Flush();
                }

                if (firstLine == "get" && BookCatalog.bookList1.Any(x => x.ISBN == secondLine))
                {
                    Book searchedBook = catalog.GetByISBN(secondLine);
                    string bookjsonString = JsonSerializer.Serialize(searchedBook);

                    writer.WriteLine(bookjsonString);
                    writer.Flush();
                }

                if (firstLine == "Save" && !string.IsNullOrEmpty(secondLine))
                {
                    Book inputBook = JsonSerializer.Deserialize<Book>(secondLine);
                    catalog.Save(inputBook);
                    writer.WriteLine("Book was successfully added!");
                    writer.Flush();
                }
            }
        }





    }
}
