namespace S2STestApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using S2STestApp.Data.Models;
    using System.Reflection;

    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Books.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<CartItem>().ToTable("CartItems");
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<CartItem>(entity =>
                    {
                        entity.HasKey(e => e.Id);
                    });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
            .Property(b => b.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<CartItem>()
          .Property(x => x.Id)
          .ValueGeneratedOnAdd();
        }
    }

}
