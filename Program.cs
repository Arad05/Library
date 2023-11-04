using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using static System.Reflection.Metadata.BlobBuilder;

namespace ConsoleApp16
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Library lib = new Library();
            string input;
            string choice;
            do
            {
                Console.WriteLine("What do you want to do at the libarary?");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("See more functions : More functions\n");
                Console.WriteLine("See all books : Books\n");
                Console.WriteLine("See all customers : Customers\n");
                Console.WriteLine("Exit the library : Exit");
                Console.WriteLine("---------------------------------------");
                input = Console.ReadLine();
                choice = input.Substring(0, 1).ToUpper() + input.Substring(1);
                Console.WriteLine("---------------------------------------");
                switch (choice)
                {
                    case "More functions":
                        Console.WriteLine("Add new book to the library: Add book\n");
                        Console.WriteLine("Add new customer : Add customer\n");
                        Console.WriteLine("Find specific book : Find book\n");
                        Console.WriteLine("Find specific customer : Find customer\n");
                        Console.WriteLine("Remove book from the library : Remove book\n");
                        Console.WriteLine("Remove customer from the library : Remove customer\n");
                        Console.WriteLine("Borrow book from the library : Borrow book\n");
                        Console.WriteLine("Return book to the library : Return book\n");
                        Console.WriteLine("Show all transactions :Transactions");
                        Console.WriteLine("Show transations by customer or by book : Transaction\n");
                        Console.WriteLine("---------------------------------------");
                        break;
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
                            Console.WriteLine("---------------------------------------");
                            Console.WriteLine("Enter the author");
                            string author = Console.ReadLine();
                            Console.WriteLine("---------------------------------------");
                            Console.WriteLine("Enter the year of publision");
                            int year = int.Parse(Console.ReadLine());
                            Console.WriteLine("---------------------------------------");
                            if(title.Length <= 0 || author.Length <= 0||(title.Length <= 0 && author.Length <= 0))
                            {
                                Console.WriteLine("The name and the title of the book need to be at least one character");
                                break;
                            }
                            Book book = new Book(title, author, year);
                            lib.AddBook(book);
                        }
                        catch (SystemException)
                        {
                            Console.WriteLine("you entered a wrong value");
                            Console.WriteLine("---------------------------------------");
                        }
                        break;

                    case "Add customer":
                        Console.WriteLine("Enter the name of the costumer");
                        string name = Console.ReadLine();
                        string inp = name.Substring(0, 1).ToUpper() + name.Substring(1);
                        Borrower borrower = new Borrower(inp);
                        Console.WriteLine("---------------------------------------");
                        lib.AddBorrower(borrower);
                        break;

                    case "Find book":
                        Console.WriteLine("Enter the name of the book or the name of the author of the book to find it");
                        string search = Console.ReadLine();
                        Console.WriteLine("---------------------------------------");
                        int count = 0;
                        if (search.Length == 0)
                        {
                            Console.WriteLine("invalid input");
                            break;
                        }
                        foreach (Book book in lib.books)
                        {
                            if (book.Title == search)
                            {
                                book.Display();
                                count++;
                            }
                            else if (book.Author == search)
                            {
                                book.Display();
                                count++;
                            }
                        }
                        if(count == 0)
                        {
                            Console.WriteLine("The library doesn't have the book you want");
                            Console.WriteLine("---------------------------------------");
                        }
                        break;

                    case "Find customer":
                        Console.WriteLine("Enter the name of the customer or his ID of to find him");
                        string input4 = Console.ReadLine();
                        string search1 = input4.Substring(0, 1).ToUpper() + input4.Substring(1);
                        Console.WriteLine("---------------------------------------");
                        foreach (Borrower b in lib.borrowers)
                        {
                            if (b.Name == search1 || b.ID.ToString() == search1)
                            {
                                b.Display();

                            }
                        }
                        if (lib.borrowers.Count==0)
                        {
                            Console.WriteLine("The library doesn't have any custumers yet");
                            Console.WriteLine("---------------------------------------");
                        }
                            break;

                    case "Remove book":
                        try
                        {
                            Console.WriteLine("Which book do you want to remove?");
                            Console.WriteLine("---------------------------------------");
                            foreach (Book b in lib.books)
                            {
                                b.Display();
                            }
                            int count6 = 0;
                            Console.WriteLine("Enter the Title of the book you want to remove:");
                            string inputT = Console.ReadLine();
                            Console.WriteLine("---------------------------------------");
                            Console.WriteLine("Enter the Author of the book you want to remove:");
                            string inputA = Console.ReadLine();
                            Console.WriteLine("---------------------------------------");
                            Console.WriteLine("Enter the Year of the book you want to remove:");
                            int inputY;
                            if (!int.TryParse(Console.ReadLine(), out inputY))
                            {
                                Console.WriteLine("Invalid input for Year");
                                Console.WriteLine("---------------------------------------");
                                break;
                            }
                            Console.WriteLine("---------------------------------------");

                            for (int i = 0; i < lib.books.Count; i++)
                            {
                                Book b = lib.books[i];

                                if (b.Title == inputT && b.Author == inputA && b.Year == inputY)
                                {
                                    if (!b.IsBorrowed)
                                    {
                                        Console.WriteLine($"{b.Title} has been removed from the library");
                                        lib.books.RemoveAt(i);
                                        count6++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The book is currently borrowed and cannot be removed yet.");
                                    }
                                    break;
                                }
                            }

                            if (count6 == 0)
                            {
                                Console.WriteLine("Book not found");
                            }
                            Console.WriteLine("---------------------------------------");
                        }
                        catch (SystemException)
                        {
                            Console.WriteLine("One or more of your inputs were wrong");
                            Console.WriteLine("---------------------------------------");
                        }
                        break;


                    case "Remove customer":
                        try
                        {
                            Console.WriteLine("Which customer do you want to remove?");
                            Console.WriteLine("---------------------------------------");
                            foreach (Borrower b in lib.borrowers)
                            {
                                b.Display();
                            }

                            Console.WriteLine("Enter the ID of the customer to remove:");
                            int input2 = int.Parse(Console.ReadLine());
                            Console.WriteLine("---------------------------------------");
                            int count5 = 0;

                            for (int i = 0; i < lib.borrowers.Count; i++)
                            {
                                Borrower b = lib.borrowers[i];

                                if (b.ID == input2)
                                {
                                    while (b.Books.Count > 0)
                                    {
                                        b.Remove(b.Books[0]);
                                    }
                                    lib.borrowers.RemoveAt(i);
                                    count5++;
                                    Console.WriteLine($"{b.Name} has been removed from the library");
                                    break;
                                }
                            }

                            if (count5 == 0)
                            {
                                Console.WriteLine("Customer not found");
                            }

                            Console.WriteLine("---------------------------------------");
                        }
                        catch (SystemException)
                        {
                            Console.WriteLine("One or more of your inputs were wrong");
                            Console.WriteLine("---------------------------------------");
                        }
                        break;


                    case "Borrow book":
                        if (lib.books.Count > 0)
                        {
                            try
                            {
                                Console.WriteLine("Enter the name of the customer that wants to borrow a book");
                                string borrowerName = Console.ReadLine();
                                string borrowerNameR = borrowerName.Substring(0, 1).ToUpper() + borrowerName.Substring(1);
                                Console.WriteLine("---------------------------------------");
                                Console.WriteLine("Enter the name of the book");
                                string bookTitle = Console.ReadLine();
                                Console.WriteLine("---------------------------------------");

                                int borrowerIndex = lib.borrowers.FindIndex(b => b.Name == borrowerNameR);
                                if (borrowerIndex != -1)
                                {
                                    Borrower borrower1 = lib.borrowers[borrowerIndex];
                                    string borrowerName1 = borrower1.Name.Substring(0, 1).ToUpper() + borrower1.Name.Substring(1);
                                    int count1 = 0;
                                    foreach (Book book in lib.books)
                                        if (bookTitle == book.Title)
                                        {
                                            if (!book.IsBorrowed&& count1==0)
                                            {
                                                borrower1.Borrowing(book, borrower1);
                                                count1++;
                                                Console.WriteLine($"{borrowerName1} borrowed {book.Title}");
                                                Transaction trn = new Transaction(book, borrower1,0);
                                                lib.Transactions.Add(trn);
                                                break;
                                            }
                                            else if(count1>0)
                                            {
                                                Console.WriteLine("The book is already borrowed by someone else");
                                            }

                                        }
                                    if (count1==0)
                                    {
                                        Console.WriteLine($"The book {bookTitle} is not available");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Customer not found");
                                }
                            }
                            catch (SystemException)
                            {
                                Console.WriteLine("One or more of your inputs were wrong");
                            }
                            Console.WriteLine("---------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("there aren't any books at the library yet");
                            Console.WriteLine("---------------------------------------");
                        }

                        break;

                    case "Return book":
                        try
                        {
                            Console.WriteLine("Enter the name of the customer that wants to return the book");
                            string borrowerName1 = Console.ReadLine();
                            string borrowerNameR1 = borrowerName1.Substring(0, 1).ToUpper() + borrowerName1.Substring(1);
                            Console.WriteLine("---------------------------------------");
                            Console.WriteLine("Enter the name of the book");
                            string bookTitle1 = Console.ReadLine();
                            Console.WriteLine("---------------------------------------");

                            int borrowerIndex1 = lib.borrowers.FindIndex(b => b.Name == borrowerNameR1);
                            if (borrowerIndex1 != -1)
                            {
                                Borrower borrower1 = lib.borrowers[borrowerIndex1];
                                string borrowerName2 = borrower1.Name.Substring(0, 1).ToUpper() + borrower1.Name.Substring(1);
                                int count2 = 0;
                                if (lib.books.Count > 0)
                                {
                                    foreach (Book book in lib.books)
                                    {
                                        if (bookTitle1 == book.Title)
                                        {

                                            if (book.IsBorrowed && book.Borrower == borrower1 && count2 == 0)
                                            {
                                                borrower1.Remove(book);
                                                Transaction trn1 = new Transaction(book, borrower1, 1);
                                                lib.Transactions.Add(trn1);
                                                Console.WriteLine($"{borrowerName1} returnd {book.Title}");
                                                count2++;
                                            }
                                            else
                                            {
                                                Console.WriteLine($"{borrowerName2} does not have the book {bookTitle1}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{borrowerName2} does not have the book {bookTitle1}");
                                        }

                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"{borrowerName2} does not have the book {bookTitle1}");
                                }



                            }
                            else
                            {
                                Console.WriteLine("Customer not found");
                            }
                        }
                        catch (SystemException)
                        {
                            Console.WriteLine("One or more of your inputs were wrong");
                        }
                        Console.WriteLine("---------------------------------------");
                        break;

                    case "Transaction":
                        Console.WriteLine("If you want to see transactions by the title of a book, write 'book'. Otherwise, write 'customer'.");
                        string input5 = Console.ReadLine();
                        string ssearch = input5.Substring(0, 1).ToUpper() + input5.Substring(1);
                        if (input5 == "book")
                        {
                            Console.WriteLine("Enter the Title of the book:");
                            string inputT1 = Console.ReadLine();
                            Console.WriteLine("---------------------------------------");
                            Console.WriteLine("Enter the Author of the book:");
                            string inputA1 = Console.ReadLine();
                            Console.WriteLine("---------------------------------------");
                            Console.WriteLine("Enter the Year of the book:");
                            int inputY1;
                            if (!int.TryParse(Console.ReadLine(), out inputY1))
                            {
                                Console.WriteLine("Invalid input for Year");
                                Console.WriteLine("---------------------------------------");
                                break;
                            }
                            Console.WriteLine("---------------------------------------");
                            bool foundTransaction = false;
                            foreach (Transaction trn in lib.Transactions)
                            {
                                if (trn.book != null && trn.book.Title == inputT1 && trn.book.Author == inputA1 && trn.book.Year == inputY1)
                                {
                                    trn.Display();
                                    foundTransaction = true;
                                }
                            }
                            if (!foundTransaction)
                            {
                                Console.WriteLine("No transactions found for the specified book");
                            }
                            Console.WriteLine("---------------------------------------");
                        }
                        else if (input5 == "customer")
                        {
                            Console.WriteLine("Enter the name of the customer:");
                            string inputC = Console.ReadLine();
                            Console.WriteLine("---------------------------------------");
                            bool foundTransaction = false;
                            foreach (Transaction trn in lib.Transactions)
                            {
                                if (trn.borrower != null && trn.borrower.Name == inputC)
                                {
                                    trn.Display();
                                    foundTransaction = true;
                                }
                            }
                            if (!foundTransaction)
                            {
                                Console.WriteLine("No transactions found for the specified customer");
                            }
                            Console.WriteLine("---------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter either 'book' or 'customer'.");
                            Console.WriteLine("---------------------------------------");
                        }
                        break;



                    case "Transactions":
                        foreach (Transaction trn in lib.Transactions)
                        {
                                trn.Display();

                        }
                        if (lib.Transactions.Count == 0)
                        {
                            Console.WriteLine("The library does not had any transactions yet");
                        }
                        Console.WriteLine("---------------------------------------");
                        break;

                    case "Exit":
                        Console.WriteLine("Good bye");
                        Console.WriteLine("---------------------------------------");
                        break;

                    default:
                        Console.WriteLine("Not a valid opthion");
                        Console.WriteLine("---------------------------------------");
                        break;
                }
            } while (choice != "Exit");

        }
    }
}