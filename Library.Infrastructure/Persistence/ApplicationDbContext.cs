using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<BookDetails> BookDetails { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookCopy> Copies { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureBookDetails(modelBuilder);
            ConfigureAuthor(modelBuilder);

            SeedDatabase(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "William Shakespeare" },
                new Author { Id = 2, Name = "Villiam Skakspjut" },
                new Author { Id = 3, Name = "Robert C. Martin" }
                );
            modelBuilder.Entity<BookDetails>().HasData(
                new BookDetails { ID = 1, AuthorID = 1, Title = "Hamlet", ISBN = "1472518381", Description = "Arguably Shakespeare's greatest tragedy" },
                new BookDetails { ID = 2, AuthorID = 1, Title = "King Lear", ISBN = "9780141012292", Description = "King Lear is a tragedy written by William Shakespeare. It depicts the gradual descent into madness of the title character, after he disposes of his kingdom by giving bequests to two of his three daughters egged on by their continual flattery, bringing tragic consequences for all." },
                new BookDetails { ID = 3, AuthorID = 2, Title = "Othello", ISBN = "1853260185", Description = "An intense drama of love, deception, jealousy and destruction." }
            );
        }

        private static void ConfigureAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().Property(x => x.Name).HasMaxLength(55);
        }

        private static void ConfigureBookDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDetails>().HasKey(x => x.ID);
            modelBuilder.Entity<BookDetails>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorID);
        }
    }
}
