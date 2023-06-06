using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ProjektZaliczeniowy.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string? UserID { get; set; }
        public virtual IdentityUser? User { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Image { get; set; }

    }
}
