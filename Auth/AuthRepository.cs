using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class AuthRepository : IAuthRepository
{
    private readonly DataContext context;

    public AuthRepository(DataContext context)
    {
        this.context = context;
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt )
    {
        using(var hmac = new System.Security.Cryptography.HMACSHA512()){
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>{
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.Username)  
        };
        SymmetricSecurityKey key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY")+" DOTNET DA DEPRESSAO")
        );
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };
        JwtSecurityTokenHandler tokenHandler =  new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        ServiceResponse<int> response = new ServiceResponse<int>();
        if(await userExists(user.Username)){
            response.Success = false;
            response.Message = "User already exists.";
            return response;
        }
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        response.Data = user.Id;
        return response;
    }

     public async Task<ServiceResponse<string>> Login(string username, string password)
    {
       ServiceResponse<string> response =  new ServiceResponse<string>();
       User user = await this.context.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
       if(user == null)
       {
           response.Success = false;
           response.Message = "User not found";
       }
       else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
       {
           response.Success = false;
           response.Message = "Wrong password.";
       }
       else{
           response.Data = CreateToken(user);
       }
       return response;
    }

    public async Task<bool> userExists(string username)
    {
        if(await this.context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower())){
            return true;
        }
        return false;
    }
}