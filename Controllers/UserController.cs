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
        {"data", DotNetCoreApi.User.All()},
      };
    }

    [HttpPost]
    public Dictionary<string, dynamic> Create()
    {
      DotNetCoreApi.User.Create(new DotNetCoreApi.User
      {
        Email = Request.Form["email"],
        Password = Request.Form["password"],
        Username = Request.Form["username"],
      });

      return new Dictionary<string, dynamic>{
        {"message", "User created"},
      };
    }

    [HttpGet("{id}")]
    public Dictionary<string, dynamic> Read(int id)
    {
      return new Dictionary<string, dynamic>{
        {"data", DotNetCoreApi.User.Read(id)},
      };
    }
  }
}
