using CRMProject.DataBase;
using CRMProject.DTOs.Auth;
using CRMProject.DTOs.Users;
using CRMProject.Entities;
using CRMProject.Services;
using CRMProject.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace CRMProject.Controllers
{
    [ApiController]
    [Route("auth")]
    [AllowAnonymous]
    public sealed class AuthController(UserManager<IdentityUser> userManager
    , ApplicationDbContext applicationDbContext
    , ApplicationIdentityDbContext applicationIdentityDbContext
    , TokenProvider tokenProvider
    , IOptions<JwtAuthOptions> options
    ) : ControllerBase
    {
        private readonly JwtAuthOptions jwtAuthOptions = options.Value;

        [HttpPost("register")]
        public async Task<ActionResult<AccessTokenDto>> Register(RegisterUserDto registerUserDto)
        {
            using IDbContextTransaction transaction = await applicationIdentityDbContext.Database.BeginTransactionAsync();

            applicationDbContext.Database.SetDbConnection(applicationIdentityDbContext.Database.GetDbConnection());
            await applicationDbContext.Database.UseTransactionAsync(transaction.GetDbTransaction());

            var identityUser = new IdentityUser
            {
                UserName = registerUserDto.UserName
            };

            IdentityResult identityResult = await userManager.CreateAsync(identityUser, registerUserDto.Password);

            if (!identityResult.Succeeded)
            {
                var extensions = new Dictionary<string, object?>
            {
                {
                    "errors",
                    identityResult.Errors.ToDictionary(e => e.Code , e => e.Description)

                }
            };
                return Problem(
                    detail: "Unable to register user, please try again",
                    statusCode: StatusCodes.Status400BadRequest,
                    extensions: extensions
                    );
            }

            User user = registerUserDto.ToEntity();
            user.IdentityId = identityUser.Id;

            applicationDbContext.Users.Add(user);
            await applicationDbContext.SaveChangesAsync();

            var tokenReuest = new TokenRequest(identityUser.Id, identityUser.UserName);
            var accessTokens = tokenProvider.Create(tokenReuest);

           
            await applicationDbContext.SaveChangesAsync();

            await transaction.CommitAsync();


            return Ok(accessTokens);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccessTokenDto>> Login(
            LoginUserDto loginUserDto)
        {

            IdentityUser? identityUser = await userManager.FindByNameAsync(loginUserDto.UserName);

            if (identityUser is null || !await userManager.CheckPasswordAsync(identityUser, loginUserDto.Password))
            {
                return Unauthorized();
            }

            var tokenRequest = new TokenRequest(identityUser.Id, identityUser.UserName!);
            AccessTokenDto accessToken = tokenProvider.Create(tokenRequest);

            return Ok(accessToken);
        }
    }
}
