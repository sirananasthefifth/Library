using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to the library!");
            Console.WriteLine();
            
            string currentUserSsn = LogIn.LogInPage();

            User currentUser = User.GetUser(currentUserSsn);

            Console.WriteLine($"Welcome {currentUser.firstName} {currentUser.lastName}");
            
            if (currentUser.userType == "USER")
            {
                Library.UserPage(currentUser);
            }

            if (currentUser.userType == "LIBRARIAN")
            {
                LibrarianSystems.Menu();
            }

            
        }


    }
}