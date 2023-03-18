namespace Library
{
    public class LogIn
    {
        public static string LogInPage()
        {
            string currentUser = null;
            bool succesfulLogin = false;

            while (!succesfulLogin)
            {
                Console.WriteLine("(1) User log in");
                Console.WriteLine("(2) Create account");
                Console.WriteLine("(3) Librarian log in");

                int userInput = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                switch (userInput)
                {
                    case 1:
                        currentUser = LogIn.UserLogIn();
                        succesfulLogin = true;
                        break;

                    case 2:
                        CreateAccount.UserCreateAccount();
                        break;

                    case 3:
                        currentUser = LogIn.LibrarianLogIn();
                        succesfulLogin = true;
                        break;

                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
            }
            

            return currentUser;
        }

        public static string UserLogIn()
        {
            string currentUser = null;

            string passwordDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserPasswords.txt";
            string ssnDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserSocialSecurityNumbers.txt";
             
            string ssn;
            int lineNumber;
            bool userFound = false;
            bool succesfulLogIn = false;
            
            while (!succesfulLogIn)
            {
                userFound = false;
                succesfulLogIn = false;

                Console.WriteLine("Enter your social security number YYYYMMDDXXXX:");
                ssn = Console.ReadLine();

                lineNumber = 0;
                foreach (string line in System.IO.File.ReadLines(ssnDirectory))
                {
                    lineNumber++;
                    if (line == ssn)
                    {
                        userFound = true;

                        Console.WriteLine("Enter your password:");
                        string password = Console.ReadLine();

                        string correctPassword = File.ReadLines(passwordDirectory).Skip(lineNumber - 1).Take(1).First();
                        
                        if (password == correctPassword)
                        {
                            Console.Clear();

                            succesfulLogIn = true;
                            currentUser = ssn;

                            Console.WriteLine("Log in succesful");
                        }

                    }
                }


                if (!userFound)
                {
                    Console.Clear();
                    Console.WriteLine("User was not found. Try again.");
                }
                else if (!succesfulLogIn)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong password or social security number");
                }
            }

            return currentUser;

            
        }
        public static string LibrarianLogIn()
        {
            string currentUser = null;

            string passwordDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\LibrarianInformation\LibrarianPasswords.txt";
            string ssnDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\LibrarianInformation\LibrarianSocialSecurityNumbers.txt";

            string ssn;
            int lineNumber;
            bool userFound = false;
            bool succesfulLogIn = false;

            while (!succesfulLogIn)
            {
                userFound = false;
                succesfulLogIn = false;

                Console.WriteLine("Enter your social security number YYYYMMDDXXXX:");
                ssn = Console.ReadLine();

                lineNumber = 0;
                foreach (string line in System.IO.File.ReadLines(ssnDirectory))
                {
                    lineNumber++;
                    if (line == ssn)
                    {
                        userFound = true;

                        Console.WriteLine("Enter your password:");
                        string password = Console.ReadLine();

                        string correctPassword = File.ReadLines(passwordDirectory).Skip(lineNumber - 1).Take(1).First();

                        if (password == correctPassword)
                        {
                            Console.Clear();

                            succesfulLogIn = true;
                            currentUser = ssn;

                            Console.WriteLine("Log in succesful");
                        }

                    }
                }


                if (!userFound)
                {
                    Console.Clear();
                    Console.WriteLine("Librarian was not found. Try again.");
                }
                else if (!succesfulLogIn)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong password or social security number");
                }
            }

            return currentUser;


        }
    }
}