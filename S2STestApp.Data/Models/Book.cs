


namespace S2STestApp.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Book
    {

        [Key]
        public int Id { get; set; }


        public string Author { get; set; }

        public string Title { get; set; }


        public int Year { get; set; }
    }
}
