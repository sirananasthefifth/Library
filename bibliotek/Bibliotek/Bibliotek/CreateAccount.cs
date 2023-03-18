using System;

namespace Library
{	
    public class CreateAccount
    {
        public static void UserCreateAccount()
        {
            string librarianSsnDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\LibrarianInformation\LibrarianSocialSecurityNumbers.txt";
            string userSsnDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserSocialSecurityNumbers.txt";
            string firstNamesDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserFirstNames.txt";
            string lastNamesDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserLastNames.txt";
            string passwordDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserPasswords.txt";
            string loanedBooksDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserLoanedBooks.txt";
            string reservedBooksDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserReservedBooks.txt";

            string ssn;
            string firstName;
            string lastName;
            string password;

            bool succesfulAccountCreation = false;
            
            while (!succesfulAccountCreation)
            {
                Console.WriteLine("Enter social security number YYYYMMDDXXXX");
                ssn = Console.ReadLine();

                foreach (string line in System.IO.File.ReadLines(userSsnDirectory))
                {
                    if (line == ssn)
                    {
                        Console.WriteLine("An account with this social security number already exists");
                        succesfulAccountCreation = false;

                    }
                    else
                    {
                        succesfulAccountCreation = true;
                    }
                }

                foreach (string line in System.IO.File.ReadLines(librarianSsnDirectory))
                {
                    if (line == ssn)
                    {
                        Console.WriteLine("An account with this social security number already exists");
                        succesfulAccountCreation = false;

                    }
                    else
                    {
                        succesfulAccountCreation = true;
                    }
                }


                Console.WriteLine("Enter first name");
                firstName = Console.ReadLine();

                Console.WriteLine("Enter last name");
                lastName = Console.ReadLine();

                Console.WriteLine("Enter password");
                password = Console.ReadLine();


                if (succesfulAccountCreation)
                {
                    File.AppendAllText(userSsnDirectory, string.Format("{0}{1}", ssn, Environment.NewLine));
                    File.AppendAllText(firstNamesDirectory, string.Format("{0}{1}", firstName, Environment.NewLine));
                    File.AppendAllText(lastNamesDirectory, string.Format("{0}{1}", lastName, Environment.NewLine));
                    File.AppendAllText(passwordDirectory, string.Format("{0}{1}", password, Environment.NewLine));
                    File.AppendAllText(loanedBooksDirectory, string.Format("{0}{1}", "0", Environment.NewLine));
                    File.AppendAllText(reservedBooksDirectory, string.Format("{0}{1}", "0", Environment.NewLine));

                }

                Console.Clear();
            }





        }
    }
}
