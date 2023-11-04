using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Transactions;

namespace ConsoleApp16
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Book dror = new Book("Rain world tutorial","Doron Dororon Peak", 2019);
            //dror.Display();

            //Borrower shalev = new Borrower("Shalev");

            //shalev.Borrowing(dror,shalev);

            //shalev.Display();
            //dror.Display();

            //shalev.Remove(dror);

            //shalev.Display();
            //dror.Display();

            //lib.DisplayAllBooks();
            //lib.DisplayAllBorrowers();
            Library lib = new Library();
            string input;
            string choice;
            //lib.DisplayAllBorrowers();
            do
            {
                Console.WriteLine("What do you want to do at the libarary");
                input = Console.ReadLine();
                choice = input.Substring(0, 1).ToUpper() + input.Substring(1);
                Console.WriteLine("--------------------------------------");
                switch (choice)
                {
                    case "Books":
                        lib.DisplayAllBooks();
                        break;

                    case "Customers":
                        lib.DisplayAllBorrowers();
                        break;

                    case "Add book":
                        try
                        {
                            Console.WriteLine("Enter the Title of the book");
                            string title = Console.ReadLine();
                            Console.WriteLine("--------------------------------------");
                            Console.WriteLine("Enter the author");
                            string author = Console.ReadLine();
                            Console.WriteLine("--------------------------------------");
                            Console.WriteLine("Enter the year of publision");
                            int year = int.Parse(Console.ReadLine());
                            Console.WriteLine("--------------------------------------");
                            Book book = new Book(title, author, year);
                            lib.AddBook(book);
                        }
                        catch (SystemException)
                        {
                            Console.WriteLine("you entered a wrong value");
                            Console.WriteLine("--------------------------------------");
                        }
                        break;

                    case "Add customer":
                        Console.WriteLine("Enter the name of the costumer");
                        Borrower borrower = new Borrower(Console.ReadLine());
                        Console.WriteLine("--------------------------------------");
                        lib.AddBorrower(borrower);
                        break;

                    case "Find book":
                        Console.WriteLine("Enter the name of the book or the name of the author of the book to find it");
                        string search = Console.ReadLine();
                        Console.WriteLine("--------------------------------------");
                        foreach (Book book in lib.books)
                        {
                            if (book.Title == search)
                            {
                                book.Display();
                            }
                            else if (book.Author == search)
                            {
                                book.Display();
                            }
                        }
                        break;

                    case "Borrow":
                        try
                        {
                            Console.WriteLine("Enter the name of the customer that wants to borrow a book");
                            string borrowerName = Console.ReadLine();
                            Console.WriteLine("--------------------------------------");
                            Console.WriteLine("Enter the name of the book");
                            string bookTitle = Console.ReadLine();
                            Console.WriteLine("--------------------------------------");

                            int borrowerIndex = lib.borrowers.FindIndex(b => b.Name == borrowerName);
                            if (borrowerIndex != -1)
                            {
                                Borrower borrower1 = lib.borrowers[borrowerIndex];
                                foreach (Book book in lib.books)
                                    if (bookTitle == book.Title)
                                        borrower1.Borrowing(book, borrower1); // Pass the book instance to the Borrowing method
                            }
                            else
                            {
                                Console.WriteLine("Borrower not found");
                            }
                        }
                        catch (SystemException)
                        {
                            Console.WriteLine("An error occurred while borrowing the book");
                        }
                        Console.WriteLine("--------------------------------------");
                        break;




                    case "Exit":
                        Console.WriteLine("Good bye");
                        Console.WriteLine("--------------------------------------");
                        break;
                    default:
                        Console.WriteLine("Not a valid opthion");
                        Console.WriteLine("--------------------------------------");
                        break;
                }

            } while (choice != "Exit");

        }
    }
}