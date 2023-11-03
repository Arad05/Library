using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    public class Book
    {
        public string Title { get; private set; }

        public string Author { get; private set; }

        public bool IsBorrowed { get; private set; }

        public int Year {  get; private set; }

        public Borrower Borrower { get; set; }

        public Book(string title, string author, int year)
        {
            this.Year = year;
            this.Title = title;
            this.Author = author;
            this.IsBorrowed = false;
        }

        public void Display() 
        {
            if (!this.IsBorrowed)
            {
                Console.WriteLine($"Book Title : {this.Title} \nBook author : {this.Author} \nPublished year : {this.Year} \nBook is available");
                Console.WriteLine("--------------------------------------");
            }

            else
            {
                Console.WriteLine($"Book Title : {this.Title} \nBook author : {this.Author} \nPublished year : {this.Year} \nBook is not available. Borrower : {this.Borrower.Name}");
                Console.WriteLine("--------------------------------------");
            }
        }

        public void Borrow(Borrower borrower)
        {
            this.Borrower = borrower;
            IsBorrowed =true;
        }

        public void Return()
        {
            this.IsBorrowed = false;
            this.Borrower = null;
        }
    }
}
