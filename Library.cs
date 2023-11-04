using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    public class Library
    {
        public List<Book> books { get; private set; }

        public List<Borrower> borrowers { get; private set;}


        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void AddBorrower(Borrower borrower)
        {
            borrowers.Add(borrower);
        }

        public Library()
        {
            books = new List<Book>();
            borrowers = new List<Borrower>();
        }

        public void DisplayAllBooks()
        {
            if (books.Count > 0 && books != null) 
            {
                int i = 0;
                foreach(Book book in books)
                {
                    i++;
                    Console.WriteLine($"Book number {i}:\n");
                    book.Display();
                }
            }
            else
            {
                Console.WriteLine("There isn't any books at the library");
                Console.WriteLine("---------------------------------------");
            }
        }

        public void DisplayAllBorrowers()
        {
            if (borrowers != null && borrowers.Count > 0) 
            {
                int i = 0;
                foreach (Borrower borrower in borrowers)
                {
                    i++;
                    Console.WriteLine($"Customer number {i} : \n");
                    borrower.Display();
                }
            }
            else
            {
                Console.WriteLine("There isn't any customers in the library");
                Console.WriteLine("---------------------------------------");
            }
        }
    }
}
