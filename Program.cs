namespace ConsoleApp16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book dror = new Book("Rain world tutorial","Doron Dororon Peak", 2019);
            dror.Display();

            Borrower shalev = new Borrower("Shalev");

            shalev.Borrowing(dror,shalev);

            shalev.Display();
            dror.Display();

            shalev.Remove(dror);

            shalev.Display();
            dror.Display();
        }
    }
}