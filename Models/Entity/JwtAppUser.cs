namespace JwtWebTokenExample.Models.Entity
{
    public class JwtAppUser
    {
        public int LogicalRef { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
