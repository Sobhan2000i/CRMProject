namespace CRMProject.Services
{
    public sealed partial class TokenProvider
    {
        public sealed record RefreshTokenDto(string RefreshToken);
    }
}
