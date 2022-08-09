

namespace S2STestApp.FakeData
{
    using S2STestApp.Data;
    using S2STestApp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;

    class Program
    {
        static void Main(string[] args)
        {
            string dbName = "Books.db";
            if(File.Exists(dbName))
            {
                File.Delete(dbName);
            }
            using(var dbContext = new BooksContext())
            {
                //Ensure database is created
                dbContext.Database.EnsureCreated();
                if(!dbContext.Books.Any())
                {
                    using(var r = new StreamReader("books.json"))
                    {
                        string json = r.ReadToEnd();
                        var source = JsonSerializer.Deserialize<List<Book>>(json);

                        dbContext.Books.AddRange(source);
                        
                    }
                    dbContext.SaveChanges();
                }

                foreach(var b in dbContext.Books)
                {
                    Console.WriteLine($"BookId={b.Id}\tTitle={b.Title}\t{b.Author}\tYear={b.Year}");
                }
            }
            Console.ReadLine();
        }
    }
}
