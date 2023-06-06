using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ProjektZaliczeniowy.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? UserID { get; set; }
        public virtual IdentityUser? User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }

    }
}
