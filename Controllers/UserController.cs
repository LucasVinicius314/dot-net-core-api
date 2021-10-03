using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreApi.Controllers
{
  [ApiController]
  [Route("[controller]/[action]", Name = "[controller]_[action]")]
  public class UserController : ControllerBase
  {
    [HttpGet]
    public Dictionary<string, dynamic> All()
    {
      return new Dictionary<string, dynamic>{
        {"data", MainContext.Instance.Users},
      };
    }

    [HttpPost]
    public Dictionary<string, dynamic> Create()
    {
      var context = MainContext.Instance;

      context.Add<DotNetCoreApi.User>(new DotNetCoreApi.User
      {
        Email = Request.Form["email"],
        Password = Request.Form["password"],
        Username = Request.Form["username"],
      });

      context.SaveChanges();

      return new Dictionary<string, dynamic>{
        {"message", "User created"},
      };
    }

    [HttpGet("{id}")]
    public Dictionary<string, dynamic> Read(int id)
    {
      return new Dictionary<string, dynamic>{
        {"data", MainContext.Instance.Users.Find(id)},
      };
    }

    [HttpPost]
    public Dictionary<string, dynamic> Login()
    {
      var user = MainContext.Instance.Users.Where(user =>
        user.Email == Request.Form["email"] && user.Password == Request.Form["password"]).First();

      if (user != null)
      {
        Response.Headers.Add("Authorization", JwtMiddleware.generateJwtToken(user));
        return new Dictionary<string, dynamic>{
          {"data", user},
        };
      }
      else
      {
        Response.StatusCode = 400;
        return new Dictionary<string, dynamic>{
          {"message", "Invalid login credentials"},
        };
      }
    }
  }
}
