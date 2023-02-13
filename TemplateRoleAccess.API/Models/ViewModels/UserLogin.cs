namespace TemplateRoleAccess.API.Models.ViewModels
{
    public class UserLoginVM
    {
        public string NIK { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
