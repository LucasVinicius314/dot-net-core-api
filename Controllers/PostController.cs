using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreApi.Controllers
{
  [ApiController]
  [Route("[controller]/[action]", Name = "[controller]_[action]")]
  public class PostController : ControllerBase
  {
    [HttpGet]
    public Dictionary<string, dynamic> All()
    {
      return new Dictionary<string, dynamic>{
        {"data", Post.All()},
      };
    }

    [HttpPost]
    public Dictionary<string, dynamic> Create()
    {
      DotNetCoreApi.Post.Create(new DotNetCoreApi.Post
      {
        Description = Request.Form["description"],
        Title = Request.Form["title"],
        User_id = int.Parse(Request.Form["user_id"]),
      });

      return new Dictionary<string, dynamic>{
        {"message", "Post created"},
      };
    }

    [HttpGet("{id}")]
    public Dictionary<string, dynamic> Read(int id)
    {
      return new Dictionary<string, dynamic>{
        {"data", Post.Read(id)},
      };
    }
  }
}
