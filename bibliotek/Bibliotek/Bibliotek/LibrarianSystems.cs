using System;
using System.Runtime.Intrinsics.X86;

namespace Library
{
    public class LibrarianSystems
    {
        public static void Menu()
        {
            //User Directories
            string ssnDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserSocialSecurityNumbers.txt";
            string firstNamesDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserFirstNames.txt";
            string lastNamesDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserLastNames.txt";
            string passwordDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserPasswords.txt";
            string loanedBooksDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserLoanedBooks.txt";
            string reservedBooksDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserReservedBooks.txt";

            //Book Directories
            string titleDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\Books\BookTitle.txt";
            string authorDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\Books\BookAuthor.txt";
            string genreDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\Books\BookGenre.txt";
            string ISBNDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\Books\BookISBN.txt";
            string quantityDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\Books\BookQuantity.txt";

            //Librarian Menu
            Console.WriteLine("What would you like to do?");

            while (true)
            {
                bool validInput = false;

                while (!validInput)
                {
                    validInput = true;

                    Console.WriteLine("(1) List Users");
                    Console.WriteLine("(2) List Books");
                    Console.WriteLine("(3) Add Book");
                    Console.WriteLine("(4) Edit Book");
                    Console.WriteLine("(5) Add User");
                    Console.WriteLine("(6) Edit User");





                    int userInput = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    switch (userInput)
                    {
                        case 1:
                            LibrarianSystems.ListUsers(ssnDirectory, firstNamesDirectory, lastNamesDirectory, passwordDirectory);
                            break;

                        case 2:
                            LibrarianSystems.ListBooks(titleDirectory, authorDirectory, genreDirectory, ISBNDirectory, quantityDirectory);
                            break;

                        case 3:
                            LibrarianSystems.AddBook(titleDirectory, authorDirectory, genreDirectory, ISBNDirectory, quantityDirectory);
                            break;

                        case 4:
                            LibrarianSystems.EditBook(titleDirectory, authorDirectory, genreDirectory, ISBNDirectory, quantityDirectory);
                            break;

                        case 5:
                            CreateAccount.UserCreateAccount();
                            break;

                        case 6:
                            LibrarianSystems.EditUser(ssnDirectory, firstNamesDirectory, lastNamesDirectory, passwordDirectory);
                            break;

                        

                        

                        default:
                            Console.WriteLine("Incorrect input");
                            validInput = false;
                            break;
                    }
                }
            }

            
        }
        public static void ListUsers(string ssnDirectory, string firstNamesDirectory, string lastNamesDirectory, string passwordDirectory)
        {
            
            string ssn;
            string firstName;
            string lastName;
            string password;
            string loanedBooks;
            string reservedBooks;

            int lineNumber = 0;
            foreach (string line in System.IO.File.ReadLines(ssnDirectory))
            {
                lineNumber++;

                firstName = File.ReadLines(firstNamesDirectory).Skip(lineNumber - 1).Take(1).First();
                lastName = File.ReadLines(lastNamesDirectory).Skip(lineNumber - 1).Take(1).First();
                ssn = File.ReadLines(ssnDirectory).Skip(lineNumber - 1).Take(1).First();
                password = File.ReadLines(passwordDirectory).Skip(lineNumber - 1).Take(1).First();
                


                Console.WriteLine("First name: " + firstName);
                Console.WriteLine("Last name: " + lastName);
                Console.WriteLine("Social Security Number: " + ssn);
                Console.WriteLine("Password: " + password);
                

                Console.WriteLine();
            }
        }
        public static void ListBooks(string titleDirectory, string authorDirectory, string genreDirectory, string ISBNDirectory, string quantityDirectory)
        {
            string title;
            string author;
            string genre;
            string ISBN;
            string quantity;

            int lineNumber = 0;
            foreach (string line in System.IO.File.ReadLines(titleDirectory))
            {
                lineNumber++;

                title = File.ReadLines(titleDirectory).Skip(lineNumber - 1).Take(1).First();
                author = File.ReadLines(authorDirectory).Skip(lineNumber - 1).Take(1).First();
                genre = File.ReadLines(genreDirectory).Skip(lineNumber - 1).Take(1).First();
                ISBN = File.ReadLines(ISBNDirectory).Skip(lineNumber - 1).Take(1).First();
                quantity = File.ReadLines(quantityDirectory).Skip(lineNumber - 1).Take(1).First();
                
                Console.WriteLine("Title: " + title);
                Console.WriteLine("Author: " + author);
                Console.WriteLine("Genre: " + genre);
                Console.WriteLine($"ISBN: " + ISBN);
                Console.WriteLine("Quantity: " + quantity);

                Console.WriteLine();
            }

        }
        public static void AddBook(string titleDirectory, string authorDirectory, string genreDirectory, string ISBNDirectory, string quantityDirectory)
        {
            bool sucessfulBookAddition = false;

            while (!sucessfulBookAddition)
            {

                Console.WriteLine("Enter Title");
                string title = Console.ReadLine();

                Console.WriteLine("Enter Author");
                string author = Console.ReadLine();

                Console.WriteLine("Enter Genre");
                string genre = Console.ReadLine().ToUpper();

                Console.WriteLine("Enter ISBN ");
                string ISBN = Console.ReadLine();

                Console.WriteLine("Enter Quantity");
                string quantity = Console.ReadLine();

                foreach (string line in System.IO.File.ReadLines(ISBNDirectory))
                {
                    if (line == ISBN)
                    {
                        Console.WriteLine("A book with this ISBN already exists in the database");
                        sucessfulBookAddition = false;

                    }
                    else
                    {
                        sucessfulBookAddition = true;
                    }
                }

                if (sucessfulBookAddition)
                {
                    File.AppendAllText(titleDirectory, string.Format("{0}{1}", title, Environment.NewLine));
                    File.AppendAllText(authorDirectory, string.Format("{0}{1}", author, Environment.NewLine));
                    File.AppendAllText(genreDirectory, string.Format("{0}{1}", genre, Environment.NewLine));
                    File.AppendAllText(ISBNDirectory, string.Format("{0}{1}", ISBN, Environment.NewLine));
                    File.AppendAllText(quantityDirectory, string.Format("{0}{1}", quantity, Environment.NewLine));
                }

                Console.Clear();

                Console.WriteLine("Book was successfully added.");
            }
        }
        public static void EditBook(string titleDirectory, string authorDirectory, string genreDirectory, string ISBNDirectory, string quantityDirectory)
        {
            LibrarianSystems.ListBooks(titleDirectory, authorDirectory, genreDirectory, ISBNDirectory, quantityDirectory);

            Console.WriteLine("Copy and paste the ISBN of the book you'd like to edit.");

            string ISBN = Console.ReadLine();

            int lineNumber = 0;
            foreach (string book in System.IO.File.ReadLines(ISBNDirectory))
            {
                lineNumber++;

                if (book == ISBN)
                {
                    string title = File.ReadLines(titleDirectory).Skip(lineNumber - 1).Take(1).First();
                    string author = File.ReadLines(authorDirectory).Skip(lineNumber - 1).Take(1).First();
                    string genre = File.ReadLines(genreDirectory).Skip(lineNumber - 1).Take(1).First();
                    string quantity = File.ReadLines(quantityDirectory).Skip(lineNumber - 1).Take(1).First();

                    Console.WriteLine("Title: " + title);
                    Console.WriteLine("Author: " + author);
                    Console.WriteLine("Genre: " + genre);
                    Console.WriteLine("ISBN: " + ISBN);
                    Console.WriteLine("Quantity: " + quantity);


                    Console.WriteLine("What would you like to edit?");

                    Console.WriteLine("(1) Title");
                    Console.WriteLine("(2) Author");
                    Console.WriteLine("(3) Genre");
                    Console.WriteLine("(4) ISBN");
                    Console.WriteLine("(5) Quantity");

                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("Write new title:");
                            title = Console.ReadLine();
                            TextSystem.lineChanger(title, titleDirectory, lineNumber);
                            break;
                        case "2":
                            Console.WriteLine("Write new author:");
                            author = Console.ReadLine();
                            TextSystem.lineChanger(author, authorDirectory, lineNumber);
                            break;
                        case "3":
                            Console.WriteLine("Write new genre:");
                            genre = Console.ReadLine();
                            TextSystem.lineChanger(genre, genreDirectory, lineNumber);
                            break;
                        case "4":
                            Console.WriteLine("Write new ISBN:");
                            ISBN = Console.ReadLine();
                            TextSystem.lineChanger(ISBN, ISBNDirectory, lineNumber);
                            break;
                        case "5":
                            Console.WriteLine("Write new quantity:");
                            quantity = Console.ReadLine();
                            TextSystem.lineChanger(quantity, quantityDirectory, lineNumber);
                            break;
                        default:
                            Console.WriteLine("Incorrect input.");
                            break;
                    }

                }


            }

            
        }
        public static void EditUser(string ssnDirectory, string firstNamesDirectory, string lastNamesDirectory, string passwordDirectory)
        {
            LibrarianSystems.ListUsers(ssnDirectory, firstNamesDirectory, lastNamesDirectory, passwordDirectory);

            Console.WriteLine("Copy and paste the SSN of the user you'd like to edit.");

            string ssn = Console.ReadLine();

            int lineNumber = 0;
            foreach (string person in System.IO.File.ReadLines(ssnDirectory))
            {
                lineNumber++;

                if (person == ssn)
                {
                    string firstName = File.ReadLines(firstNamesDirectory).Skip(lineNumber - 1).Take(1).First();
                    string lastName = File.ReadLines(lastNamesDirectory).Skip(lineNumber - 1).Take(1).First();
                    string password = File.ReadLines(passwordDirectory).Skip(lineNumber - 1).Take(1).First();

                    Console.WriteLine("First name: " + firstName);
                    Console.WriteLine("Last name: " + lastName);
                    Console.WriteLine("Password: " + password);
                    Console.WriteLine("Social Security Number: " + ssn);

                    Console.WriteLine();

                    Console.WriteLine("What would you like to edit?");

                    Console.WriteLine();

                    Console.WriteLine("(1) First name");
                    Console.WriteLine("(2) Last name");
                    Console.WriteLine("(3) Password");
                    Console.WriteLine("(4) Social Security Number");


                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.WriteLine("Write new first name:");
                            firstName = Console.ReadLine();
                            TextSystem.lineChanger(firstName, firstNamesDirectory, lineNumber);
                            break;
                        case "2":
                            Console.WriteLine("Write new last name:");
                            lastName = Console.ReadLine();
                            TextSystem.lineChanger(lastName, lastNamesDirectory, lineNumber);
                            break;
                        case "3":
                            Console.WriteLine("Write new password:");
                            password = Console.ReadLine();
                            TextSystem.lineChanger(password, passwordDirectory, lineNumber);
                            break;
                        case "4":
                            Console.WriteLine("Write new ssn:");
                            ssn = Console.ReadLine();
                            TextSystem.lineChanger(ssn, ssnDirectory, lineNumber);
                            break;
                        
                        default:
                            Console.WriteLine("Incorrect input.");
                            break;
                    }

                }


            }


        }


    }
}



