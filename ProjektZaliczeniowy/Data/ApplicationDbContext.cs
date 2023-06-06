using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektZaliczeniowy.Models;

namespace ProjektZaliczeniowy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProjektZaliczeniowy.Models.Post> Post { get; set; }
        public DbSet<ProjektZaliczeniowy.Models.Comment> Comment { get; set; }
        public DbSet<ProjektZaliczeniowy.Models.Profile> Profile { get; set; }
    }
}