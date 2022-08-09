namespace S2STestApp.ViewModels
{
    public class BookViewModel
    {
        public BookViewModel(int id, string author, string name, int year)
        {
            Id = id;
            Author = author;
            Name = name;
            Year = year;
        }

        public int Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
    }
}
