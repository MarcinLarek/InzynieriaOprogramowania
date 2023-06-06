using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ProjektZaliczeniowy.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? UserID { get; set; }
        public virtual IdentityUser? User { get; set; }
        public int CommentedPost { get; set; }
        public string CommentContent { get; set; }

    }
}
