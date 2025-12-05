using System.ComponentModel.DataAnnotations;

namespace CarOrderApi.Dtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
