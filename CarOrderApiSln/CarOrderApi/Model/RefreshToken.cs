using Microsoft.AspNetCore.Identity;

namespace CarOrderApi.Model
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}
