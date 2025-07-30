namespace CRMProject.DTOs.Auth
{
    public sealed record LoginUserDto
    {
        public required string UserName { get; init; }
        public required string Password { get; init; }
    }
}
