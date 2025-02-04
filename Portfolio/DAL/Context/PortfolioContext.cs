using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Entities;  // User sınıfı burada olduğu için bu namespace'i de ekliyoruz

namespace Portfolio.DAL.Context
{
    public class PortfolioContext : IdentityDbContext<User>  // User sınıfı burada kullanılıyor
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options)
            : base(options)
        {
        }

        public PortfolioContext()
        {
        }

        // OnConfiguring metodu ile veritabanı sağlayıcısını burada da ekleyebilirsiniz.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DEMIR11\\SQLEXPRESS;Initial Catalog=PortfolioDB;Integrated Security=True;");
            }
        }

        // DbSet'ler burada yer alıyor
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Portfolios> Portfolios { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
