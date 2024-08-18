namespace OHS_program_api.Application.DTOs.User
{
    public class ListUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
