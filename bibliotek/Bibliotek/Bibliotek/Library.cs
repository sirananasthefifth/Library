using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.X86;

namespace Library
{
    public class Library
    {
        

        public static void UserPage(User currentUser)
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

            //Get Loaned Books
            List<Book> loanedBooks = new List<Book>();
            List<Book> reservedBooks = new List<Book>();



            //User Menu

            while (true)
            {
                loanedBooks = GetLoanedBooks(currentUser.lineNumber, loanedBooksDirectory, ISBNDirectory, titleDirectory, authorDirectory, genreDirectory);
                reservedBooks = GetReservedBooks(currentUser.lineNumber, reservedBooksDirectory, ISBNDirectory, titleDirectory, authorDirectory, genreDirectory);

                if (reservedBooks != null)
                {
                    foreach(Book book in reservedBooks)
                    {
                        
                        int quantity = Convert.ToInt32(File.ReadLines(quantityDirectory).Skip(book.lineNumber - 1).Take(1).First());
                        if (quantity > 0)
                        {
                            Console.WriteLine($"{book.title} which you have reserved is now available. Would you like to loan it?");
                            Console.WriteLine("If you don't loan your reservation will be removed.");
                            Console.WriteLine("(1) Yes");
                            Console.WriteLine("(2) No");
                            string input = Console.ReadLine();

                            if (input == "1")
                            {
                                string currentLoans = File.ReadLines(loanedBooksDirectory).Skip(currentUser.lineNumber - 1).Take(1).First();

                                if (currentLoans == "0")
                                {
                                    TextSystem.lineChanger(book.ISBN, loanedBooksDirectory, currentUser.lineNumber);

                                    quantity--;
                                    string newQuantity = Convert.ToString(quantity);

                                    TextSystem.lineChanger(newQuantity, quantityDirectory, book.lineNumber);

                                }
                                else
                                {
                                    TextSystem.lineChanger(currentLoans + "." + book.ISBN, loanedBooksDirectory, currentUser.lineNumber);

                                    quantity--;
                                    string newQuantity = Convert.ToString(quantity);

                                    TextSystem.lineChanger(newQuantity, quantityDirectory, book.lineNumber);
                                }

                                Console.WriteLine($"You are currently loaning {book.title} by {book.author}.");
                            }


                        }
                    }
                    if (reservedBooks == null)
                    {
                        TextSystem.lineChanger("0", reservedBooksDirectory, currentUser.lineNumber);
                    }

                }


                bool validInput = false;

                while (!validInput)
                {
                    Console.WriteLine($"Name: {currentUser.firstName} {currentUser.lastName}");
                    Console.WriteLine($"Social Security Number: {currentUser.socialSecurityNumber}");
                    Console.WriteLine($"Password: {currentUser.password}");
                    
                    
                    if (loanedBooks == null)
                    {
                        Console.WriteLine("No loaned books.");
                    }
                    else
                    {
                        foreach (Book book in loanedBooks)
                        {
                            Console.WriteLine("Loaned Books:");
                            Console.WriteLine(book.title);
                            
                        }
                    }
                    
                    

                    Console.WriteLine("What would you like to do?");

                    validInput = true;

                    Console.WriteLine("(1) Search Books");
                    Console.WriteLine("(2) Change Password");
                    Console.WriteLine("(3) Return Books");

                    int userInput = Convert.ToInt32(Console.ReadLine());

                    Console.Clear();

                    switch (userInput)
                    {
                        case 1:
                            Library.Search(titleDirectory, authorDirectory, genreDirectory, ISBNDirectory, currentUser, quantityDirectory, loanedBooksDirectory, loanedBooks);
                            break;

                        case 2:
                            Library.ChangePassword(currentUser, passwordDirectory);
                            break;

                        case 3:
                            Library.ReturnBooks(currentUser, loanedBooksDirectory, loanedBooks, quantityDirectory);
                            break;

                        default:
                            Console.WriteLine("Incorrect input");
                            validInput = false;
                            break;
                    }
                }

            }
        }
        public static void WriteBookInfo(int lineNumber, string titleDirectory, string authorDirectory, string genreDirectory, string ISBNDirectory)
        {
            Console.WriteLine();

            string title = File.ReadLines(titleDirectory).Skip(lineNumber - 1).Take(1).First();
            string author = File.ReadLines(authorDirectory).Skip(lineNumber - 1).Take(1).First();
            string genre = File.ReadLines(genreDirectory).Skip(lineNumber - 1).Take(1).First();
            string ISBN = File.ReadLines(ISBNDirectory).Skip(lineNumber - 1).Take(1).First();

            Console.WriteLine("Title: " + title);
            Console.WriteLine("Author: " + author);
            Console.WriteLine("Genre: " + genre);
            Console.WriteLine("ISBN: " + ISBN);

            Console.WriteLine();
        }
        public static void Search(string titleDirectory, string authorDirectory, string genreDirectory, string ISBNDirectory, User currentUser, string quantityDirectory, string loanedBooksDirectory, List<Book> loanedBooks)
        {
            

            string search;

            string title;
            string author;
            string genre;
            string ISBN;

            int lineNumber;

            Console.WriteLine("Search for a book, author, genre or type in an ISBN:");
            search =  Console.ReadLine();

            lineNumber = 0;
            foreach (string line in System.IO.File.ReadLines(titleDirectory))
            {
                lineNumber++;
                if (line.ToUpper() == search.ToUpper())
                {
                    WriteBookInfo(lineNumber, titleDirectory, authorDirectory, genreDirectory, ISBNDirectory);
                }
            }

            lineNumber = 0;
            foreach (string line in System.IO.File.ReadLines(authorDirectory))
            {
                lineNumber++;
                if (line.ToUpper() == search.ToUpper())
                {
                    WriteBookInfo(lineNumber, titleDirectory, authorDirectory, genreDirectory, ISBNDirectory);

                }
            }

            lineNumber = 0;
            foreach (string line in System.IO.File.ReadLines(genreDirectory))
            {
                lineNumber++;
                if (line.ToUpper() == search.ToUpper())
                {
                    WriteBookInfo(lineNumber, titleDirectory, authorDirectory, genreDirectory, ISBNDirectory);

                }
            }

            lineNumber = 0;
            foreach (string line in System.IO.File.ReadLines(ISBNDirectory))
            {
                lineNumber++;
                if (line.ToUpper() == search.ToUpper())
                {
                    WriteBookInfo(lineNumber, titleDirectory, authorDirectory, genreDirectory, ISBNDirectory);

                }
            }

            Console.WriteLine("(1) Search");
            Console.WriteLine("(2) Loan book");

            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Library.Search(titleDirectory, authorDirectory, genreDirectory, ISBNDirectory, currentUser, quantityDirectory, loanedBooksDirectory, loanedBooks);
                    break;

                case "2":
                    Library.Loan(currentUser, titleDirectory, authorDirectory, ISBNDirectory, quantityDirectory, loanedBooksDirectory, loanedBooks);
                    break;
            }


        }
        public static void Loan(User currentUser, string titleDirectory, string authorDirectory, string ISBNDirectory, string quantityDirectory, string loanedBooksDirectory, List<Book> loanedBooks)
        {
            

            Console.WriteLine("Copy and paste ISBN-number of the book you want to loan.");
            string ISBN = Console.ReadLine();

            bool currentlyLending = false;
            if (loanedBooks != null)
            {
                foreach (Book book in loanedBooks)
                {
                    if (ISBN == book.ISBN)
                    {
                        currentlyLending = true;
                    }
                }
            }
            

            if (currentlyLending)
            {
                Console.WriteLine("You are already lending this book.");
            }
            else
            {
                int lineNumber = 0;
                foreach (string line in System.IO.File.ReadLines(ISBNDirectory))
                {
                    lineNumber++;
                    if (line == ISBN)
                    {
                        Console.WriteLine();

                        string title = File.ReadLines(titleDirectory).Skip(lineNumber - 1).Take(1).First();
                        string author = File.ReadLines(authorDirectory).Skip(lineNumber - 1).Take(1).First();
                        int quantity = Convert.ToInt32(File.ReadLines(quantityDirectory).Skip(lineNumber - 1).Take(1).First());


                        Console.WriteLine($"Do you want to loan {title} by {author}?");
                        Console.WriteLine("(1) Yes");
                        Console.WriteLine("(2) No");

                        string input = Console.ReadLine();



                        if (input == "1")
                        {
                            if (quantity == 0)
                            {
                                Reserve(currentUser, ISBN, title, author);
                            }
                            else if (quantity > 0)
                            {
                                string currentLoans = File.ReadLines(loanedBooksDirectory).Skip(currentUser.lineNumber - 1).Take(1).First();

                                if (currentLoans == "0")
                                {
                                    TextSystem.lineChanger(ISBN, loanedBooksDirectory, currentUser.lineNumber);

                                    quantity--;
                                    string newQuantity = Convert.ToString(quantity);

                                    TextSystem.lineChanger(newQuantity, quantityDirectory, lineNumber);

                                }
                                else
                                {
                                    TextSystem.lineChanger(currentLoans + "." + ISBN, loanedBooksDirectory, currentUser.lineNumber);

                                    quantity--;
                                    string newQuantity = Convert.ToString(quantity);

                                    TextSystem.lineChanger(newQuantity, quantityDirectory, lineNumber);
                                }

                                Console.WriteLine($"You are currently loaning {title} by {author}.");
                            }

                        }
                        else if (input == "2")
                        {
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Incorrect input.");
                        }

                    }
                }
            }

            
        }
        public static void Reserve(User currentUser, string ISBN, string title, string author)
        {
            
            string reservedBooksDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserReservedBooks.txt";
            string currentReservations = File.ReadLines(reservedBooksDirectory).Skip(currentUser.lineNumber - 1).Take(1).First();

            bool alreadyReserving = false;

            if (currentReservations != "0")
            {
                string[] reservedBooks = currentReservations.Split(".");
                foreach (string book in reservedBooks)
                {
                    if (book == ISBN)
                    {
                        alreadyReserving = true;
                    }
                }
            }


            if (!alreadyReserving)
            {
                Console.WriteLine("Unfortunately we do not have this book right now.");
                Console.WriteLine("Would you like to reserve it?");
                Console.WriteLine("(1) Yes");
                Console.WriteLine("(2) No");


                string input = Console.ReadLine();




                if (input == "1")
                {

                    if (currentReservations == "0")
                    {

                        TextSystem.lineChanger(ISBN, reservedBooksDirectory, currentUser.lineNumber);

                    }
                    else
                    {
                        TextSystem.lineChanger(currentReservations + "." + ISBN, reservedBooksDirectory, currentUser.lineNumber);

                    }

                    Console.WriteLine($"You are currently reserving {title} by {author}.");
                }
                else if (input == "2")
                {
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Incorrect input.");
                }
            }
            

            

        }
        public static void ChangePassword(User currentUser, string passwordDirectory)
        {
            Console.WriteLine("Write your current password");
            string password = Console.ReadLine();

            Console.WriteLine("Write your new password");
            string newPassword = Console.ReadLine();

            Console.WriteLine("Confirm password");
            string confirmPassword = Console.ReadLine();

            if (confirmPassword == newPassword && password == currentUser.password)
            {
                TextSystem.lineChanger(newPassword, passwordDirectory, currentUser.lineNumber);
                currentUser.password = newPassword;
            }
            else
            {
                Console.WriteLine("Incorrect input.");
            }

        }
        public static List<Book> GetLoanedBooks(int lineNumber, string loanedBooksDirectory, string ISBNDirectory, string titleDirectory, string authorDirectory, string genreDirectory)
        {
            int linenumber;
            
            string loanedBooksRawData = File.ReadLines(loanedBooksDirectory).Skip(lineNumber - 1).Take(1).First();
            
            List<Book> loanedBooks = new List<Book>();
            
            if (loanedBooksRawData == "0")
            {
                return null;
            }
            else
            {
                string[] loanedBooksISBNArray = loanedBooksRawData.Split(".");
                
                for(int i = 0; i < loanedBooksISBNArray.Length; i++)
                {
                    lineNumber = 0;
                    foreach (string line in System.IO.File.ReadLines(ISBNDirectory))
                    {
                        lineNumber++;
                        if (line == loanedBooksISBNArray[i])
                        {
                            string title = File.ReadLines(titleDirectory).Skip(lineNumber - 1).Take(1).First();
                            string author = File.ReadLines(authorDirectory).Skip(lineNumber - 1).Take(1).First();
                            string genre = File.ReadLines(genreDirectory).Skip(lineNumber - 1).Take(1).First();

                            Book newBook = new Book(title, author, genre, loanedBooksISBNArray[i], lineNumber);

                            loanedBooks.Add(newBook);
                        }
                        
                    }
                }
                
                
                return loanedBooks;
            }

        }
        public static List<Book> GetReservedBooks(int lineNumber, string reservedBooksDirectory, string ISBNDirectory, string titleDirectory, string authorDirectory, string genreDirectory)
        {
            int linenumber;

            string reservedBooksRawData = File.ReadLines(reservedBooksDirectory).Skip(lineNumber - 1).Take(1).First();

            List<Book> reservedBooks = new List<Book>();

            if (reservedBooksRawData == "0")
            {
                return null;
            }
            else
            {
                string[] reservedBooksISBNArray = reservedBooksRawData.Split(".");

                for (int i = 0; i < reservedBooksISBNArray.Length; i++)
                {
                    lineNumber = 0;
                    foreach (string line in System.IO.File.ReadLines(ISBNDirectory))
                    {
                        lineNumber++;
                        if (line == reservedBooksISBNArray[i])
                        {
                            string title = File.ReadLines(titleDirectory).Skip(lineNumber - 1).Take(1).First();
                            string author = File.ReadLines(authorDirectory).Skip(lineNumber - 1).Take(1).First();
                            string genre = File.ReadLines(genreDirectory).Skip(lineNumber - 1).Take(1).First();

                            Book newBook = new Book(title, author, genre, reservedBooksISBNArray[i], lineNumber);

                            reservedBooks.Add(newBook);
                        }

                    }
                }


                return reservedBooks;
            }

        }
        public static void ReturnBooks(User currentUser, string loanedBooksDirectory, List<Book> loanedBooks, string quantityDirectory)
        {
            if (loanedBooks != null)
            {
                Console.WriteLine("Your loaned books:");
                foreach (Book book in loanedBooks)
                {
                    Console.WriteLine("Title: " + book.title);
                    Console.WriteLine("Author: " + book.author);
                    Console.WriteLine("Genre: " + book.genre);
                    Console.WriteLine("ISBN: " + book.ISBN);
                    Console.WriteLine();
                }

                Book removedBook = null;
                bool correctInput = false;

                while (!correctInput)
                {
                    Console.WriteLine("Copy and paste the ISBN of the book you'd like to return.");

                    string ISBN = Console.ReadLine();

                    foreach (Book book in loanedBooks)
                    {

                        if (ISBN == book.ISBN)
                        {
                            removedBook = book;
                            correctInput = true;

                        }
                    }
                }

                

                //Remove from list
                loanedBooks.Remove(removedBook);

                //Change quantity
                string quantity = File.ReadLines(quantityDirectory).Skip(removedBook.lineNumber - 1).Take(1).First();
                int newQuantity = Convert.ToInt32(quantity) + 1;
                quantity = Convert.ToString(newQuantity);
                TextSystem.lineChanger(quantity, quantityDirectory, removedBook.lineNumber);

                string loanedBooksRawData = "";

                foreach (Book book in loanedBooks)
                {
                    loanedBooksRawData += book.ISBN + ".";
                }

                if (loanedBooks.Any())
                {
                    
                    //Add remove last character here:
                    loanedBooksRawData = loanedBooksRawData.Remove(loanedBooksRawData.Length - 1);
                }
                else
                {
                    loanedBooksRawData = "0";
                }
                


                TextSystem.lineChanger(loanedBooksRawData, loanedBooksDirectory, currentUser.lineNumber);


                Console.WriteLine("Book succesfully returned.");
            }
            else
            {
                Console.WriteLine("You have no loaned books.");
            }
            

        }

    }
}



