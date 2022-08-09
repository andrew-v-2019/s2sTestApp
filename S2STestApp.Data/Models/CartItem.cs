namespace S2STestApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CartItem
    {
        [Key]
        public int Id { get; set; }


        public string UserData { get; set; }

        public int BookId { get; set; }

        public DateTime Date { get; set; }
    }
}
