using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    public class Transaction
    {
        public Book book { get; private set; }
        public Borrower borrower { get; private set; }
        public int TransType { get; private set; }

        public Transaction(Book book, Borrower borrower, int transType)
        {
            this.book = book;
            this.borrower = borrower;
            this.TransType = transType;
        }
        
        public void Display()
        {

                if (this.TransType == 0)
                {
                    Console.WriteLine($"The {this.book.Title} book has been borrowed by {this.borrower.Name}");
                }
                else
                {
                    Console.WriteLine($"The {this.book.Title} book has been returnd by {this.borrower.Name}");
                }




        }
    }
}
