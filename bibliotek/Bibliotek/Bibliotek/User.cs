using System.Runtime.Intrinsics.X86;

namespace Library
{
    public class User
    {
        public string userType;
        public string firstName;
        public string lastName;
        public string socialSecurityNumber;
        public int lineNumber;
        public string password;

        public User(string userType, string firstName, string lastName, string socialSecurityNumber, int lineNumber, string password)
        {
            this.userType = userType;
            this.firstName = firstName;
            this.lastName = lastName;
            this.socialSecurityNumber = socialSecurityNumber;
            this.lineNumber = lineNumber;
            this.password = password;
        }

        public static User GetUser(string socialSecurityNumber)
        {

            string userSsnDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserSocialSecurityNumbers.txt";
            string userFirstNamesDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserFirstNames.txt";
            string userLastNamesDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserLastNames.txt";
            string userPasswordDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\UserInformation\UserPasswords.txt";

            string librarianSsnDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\LibrarianInformation\LibrarianSocialSecurityNumbers.txt";
            string librarianFirstNamesDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\LibrarianInformation\LibrarianFirstNames.txt";
            string librarianLastNamesDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\LibrarianInformation\LibrarianLastNames.txt";
            string librarianPasswordDirectory = @"C:\Users\emil.embretsen\Desktop\bibliotek\Bibliotek\Bibliotek\LibrarianInformation\LibrarianPasswords.txt";

            string type = null;
            string firstName = null;
            string lastName = null;
            string password = null;
            int userLineNumber = 0;
            
            
            int lineNumber = 0;
            foreach (string line in System.IO.File.ReadLines(userSsnDirectory))
            {
                lineNumber++;
                if (line == socialSecurityNumber)
                {
                    type = "USER";
                    firstName = File.ReadLines(userFirstNamesDirectory).Skip(lineNumber - 1).Take(1).First();
                    lastName = File.ReadLines(userLastNamesDirectory).Skip(lineNumber - 1).Take(1).First();
                    password = File.ReadLines(userPasswordDirectory).Skip(lineNumber - 1).Take(1).First();
                    userLineNumber = lineNumber;
                }
            }
            lineNumber = 0;
            foreach (string line in System.IO.File.ReadLines(librarianSsnDirectory))
            {
                lineNumber++;
                if (line == socialSecurityNumber)
                {
                    type = "LIBRARIAN";
                    firstName = File.ReadLines(librarianFirstNamesDirectory).Skip(lineNumber - 1).Take(1).First();
                    lastName = File.ReadLines(librarianLastNamesDirectory).Skip(lineNumber - 1).Take(1).First();
                    password = File.ReadLines(librarianPasswordDirectory).Skip(lineNumber - 1).Take(1).First();
                    userLineNumber = lineNumber;

                }

            }

            User currentUser = new User(type, firstName, lastName, socialSecurityNumber, userLineNumber, password);

            return currentUser;
        }
    }

}