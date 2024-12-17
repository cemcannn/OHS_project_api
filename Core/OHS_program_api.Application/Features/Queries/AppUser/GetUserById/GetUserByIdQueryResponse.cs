namespace OHS_program_api.Application.Features.Queries.AppUser.GetUserById
{
    public class GetUserByIdQueryResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
