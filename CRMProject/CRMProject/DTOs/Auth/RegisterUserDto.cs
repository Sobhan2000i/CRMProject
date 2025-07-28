namespace CRMProject.DTOs.Auth
{
    public sealed record RegisterUserDto
    {
        public required string UserName { get; init; }
        public required string Name { get; init; }
        public required string Password { get; init; }
        public required string ConfirmPassword { get; init; }
    }

}
