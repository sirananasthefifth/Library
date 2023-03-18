namespace Library
{
    public class Book
    {
        public string title;
        public string author;
        public string genre;
        public string ISBN;
        public int lineNumber;


        public Book(string title, string author, string genre, string ISBN, int lineNumber)
        {
            this.title = title;
            this.author = author;
            this.genre = genre;
            this.ISBN = ISBN;
            this.lineNumber = lineNumber;

        }
    }
}