using e_store.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace e_store.Models
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtMappingValue _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtMappingValue> JWT) {
        _userManager = userManager;
            _jwt = JWT.Value;
        }
        private async Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
               issuer: _jwt.Issuer, // استخدم Issuer الصحيح
    audience: _jwt.Audience, 
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public async Task<AuthModel> RejesterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "there is already accounts with this email" };
            }
            var user=new ApplicationUser { Email = model.Email ,UserName=model.Email };
          var reslut=  await _userManager.CreateAsync(user,model.Password);
            if (!reslut.Succeeded)
            {string error=string.Empty;
              foreach (var e in reslut.Errors)
                {
                    error += e.Description + " ";
                }
              return new AuthModel { Message = error };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var JwtSecurityTokken = await CreateJwtTokenAsync(user);
            return new AuthModel
            {
                Email = user.Email,
                IsAuthenticated = true,
                Message= "user Created successfully",
                UserName = user.UserName,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityTokken),
                ExpiresOn = JwtSecurityTokken.ValidTo
            };


            throw new NotImplementedException();
        }

        public async Task<AuthModel> GetTokenAsync(RegisterModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user,model.Password))
            {
                return new AuthModel { Message = "Wrong in Email or password",
                IsAuthenticated=false,};
            }
            var UserToken = await CreateJwtTokenAsync(user);
            var TheRoles = await _userManager.GetRolesAsync(user);
            return new AuthModel { Email = user.Email, IsAuthenticated = true, Message = "successfully", Roles = TheRoles.ToList(), UserName = user.UserName, Token = new JwtSecurityTokenHandler().WriteToken(UserToken),ExpiresOn=UserToken.ValidTo };


            
        }
    }
}
