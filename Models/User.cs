using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webapi_dotnet5.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; } 
        public byte[] PasswordSalt { get; set; } 
        // User-Has-Many-Characters this creates another table
        public List<Character> Characters { get; set; }
        [Required]
        public string Role { get; set; } 
    }
}