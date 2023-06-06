using Microsoft.AspNetCore.Identity;
using System.Data;

namespace ProjektZaliczeniowy.Models
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public IFormFile Image { get; set; }

    }
}
