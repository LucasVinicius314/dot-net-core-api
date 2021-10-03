using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace DotNetCoreApi
{
  public class JwtMiddleware
  {
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      if (context.Request.Path == "/User/Login")
        await _next(context);
      else
        try
        {
          var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

          if (token == null)
            throw new Exception("Invalid access token");

          attachUserToContext(context, token);

          await _next(context);
        }
        catch (System.Exception)
        {
          context.Response.StatusCode = 401;
          await context.Response.WriteAsJsonAsync(
            new Dictionary<string, dynamic>{
            {"message", "Invalid access token"},
            }
          );
        }
    }

    private void attachUserToContext(HttpContext context, string token)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecret"));

      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
      }, out SecurityToken validatedToken);

      var jwtToken = (JwtSecurityToken)validatedToken;
      var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

      context.Items["User"] = MainContext.Instance.Users.Find(userId);
    }

    public static string generateJwtToken(User user)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JwtSecret"));
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[] { new System.Security.Claims.Claim("id", user.Id.ToString()) }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}