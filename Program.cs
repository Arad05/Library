using System.Linq.Expressions;
using System.Runtime.InteropServices;
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
                Console.WriteLine("See all books : Books");
                Console.WriteLine("See all customers : Customers");
                Console.WriteLine("Add new book to the library: Add book");
                Console.WriteLine("Add new customer : Add customer");
                Console.WriteLine("Exit the library : Exit");
                Console.WriteLine("---------------------------------------");
                input = Console.ReadLine();
                choice = input.Substring(0, 1).ToUpper() + input.Substring(1);
                Console.WriteLine("---------------------------------------");
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
                        string search1 = Console.ReadLine();
                        Console.WriteLine("---------------------------------------");
                        int count3 = 0;
                        foreach (Borrower b in lib.borrowers)
                        {
                            if (b.Name == search1)
                            {
                                b.Display();
                                count3++;
                            }
                            else if (b.ID == int.Parse(search1))
                            {
                                b.Display();
                                count3++;
                            }
                        }
                        if (count3 == 0)
                        {
                            Console.WriteLine("The library doesn't have any custumers yet");
                            Console.WriteLine("---------------------------------------");
                        }
                            break;

                    case "Remove book":
                        try
                        {
                            Console.WriteLine("Whice customer do you wahnt to remove?");
                            Console.WriteLine("---------------------------------------");
                            foreach (Borrower b in lib.borrowers)
                            {
                                b.Display();
                            }
                            Console.WriteLine("Enter the name of the customer or the name his ID of the customer to delet him");
                            string input1 = Console.ReadLine();
                            string search2 = input1.Substring(0, 1).ToUpper() + input1.Substring(1);
                            Console.WriteLine("---------------------------------------");
                            int count4 = 0;
                            for (int i = 0; i < lib.borrowers.Count; i++)
                            {
                                Borrower b = lib.borrowers[i];
                                if (b.Name == search2 || (int.TryParse(search2, out int id) && b.ID == id))
                                {
                                    for (int j = 0; j < b.Books.Count; j++)
                                    {
                                        b.Remove(b.Books[j]);
                                    }
                                    lib.borrowers.RemoveAt(i);
                                    count4++;
                                }
                            }
                            if (count4 == 0)
                            {
                                Console.WriteLine("Customer not found");
                            }
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
                            
                            Console.WriteLine("Whice customer do you wahnt to remove?");
                            Console.WriteLine("---------------------------------------");
                            foreach (Borrower b in lib.borrowers)
                            {
                                b.Display();
                                
                            }

                            Console.WriteLine("Enter the name of the customer or his ID to remove him from this library");
                            string input2 = Console.ReadLine();
                            string search3 = input2.Substring(0, 1).ToUpper() + input2.Substring(1);
                            Console.WriteLine("---------------------------------------");
                            int count5 = 0;
                            int count6 = 0;
                            int maxId = 0;
                            foreach (Borrower b1 in lib.borrowers)
                            {
                                if (b1.Name == search3)
                                {
                                    
                                    count6++;
                                }
                                if(b1.ID>maxId)
                                    maxId = b1.ID;

                            }
                            if (count6 > 1)
                            {
                                int id = 0;
                                Console.WriteLine($"There are more then one customer whit the name {search3} please enter ID insted");
                                while (!(id >= 0) && !(id <= maxId))
                                {

                                    try
                                    {
                                        id = int.Parse(Console.ReadLine());
                                        Console.WriteLine("---------------------------------------");
                                    }
                                    catch (SystemException)
                                    {
                                        Console.WriteLine("Invalid input or ID out of range. Please enter a valid ID:");
                                    }
                                }

                                search3 = id.ToString();
                            }
                            for (int i = 0; i < lib.borrowers.Count; i++)
                            {
                                Borrower b = lib.borrowers[i];
                                
                                if (b.Name == search3 )
                                {
                                    while (b.Books.Count >= 0)
                                    {
                                        b.Remove(b.Books[0]);
                                    }
                                    lib.borrowers.RemoveAt(i);
                                    count5++;
                                    break;

                                }
                                else if (b.ID == int.Parse(search3))
                                {
                                    while (b.Books.Count >= 0)
                                    {
                                        b.Remove(b.Books[0]);
                                    }
                                    lib.borrowers.RemoveAt(i);
                                    count5++;
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
                                            borrower1.Remove(book);
                                            if (book.IsBorrowed && book.Borrower == borrower1 && count2 == 0)
                                            {
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