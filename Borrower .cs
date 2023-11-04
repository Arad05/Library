using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    public class Borrower
    {
        static int idCounter = 0;

        public string Name { get; private set; }

        public int ID { get; private set; }

        public List<Book> Books { get; private set; }

        public Borrower(string name)
        {
            this.Name = name;
            this.ID = idCounter++;
            Books = new List<Book>();
            
        }

        public void Borrowing(Book book, Borrower borrower)
        {
            if (!book.IsBorrowed)
            {
                book.Borrow(borrower);
                Books.Add(book);
            }
            else
            {
                Console.WriteLine("The book is already borrowed");
            }
        }

        public void Remove(Book book) 
        {
            if (Books.Contains(book))
            {
                Books.Remove(book);
                book.Return();
            }


        }



        public void Display()
        {
            Console.WriteLine($"Name : {this.Name} \nId : {this.ID}");
            if(Books.Count > 0)
            {
                for (int i = 0; i < Books.Count; i++)
                {
                    Console.WriteLine($"Book{i + 1} : {Books[i].Title}");
                }
            }
            else
            {
                Console.WriteLine($"{this.Name} does not have any books to display");
            }
            Console.WriteLine("---------------------------------------");

        }

    }
}
