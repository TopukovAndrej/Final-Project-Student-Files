namespace StudentFiles.Api.Services
{
    public interface IJwtService
    {
        public string GenerateJwtToken(Guid userUid, string userName, string userRole);
    }
}
