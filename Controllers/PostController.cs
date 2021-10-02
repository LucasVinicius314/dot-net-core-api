using System;
using System.Linq;
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
        {"data", MainContext.Instance.Posts},
      };
    }

    [HttpPost]
    public Dictionary<string, dynamic> Create()
    {
      var context = MainContext.Instance;

      context.Posts.Add(new DotNetCoreApi.Post
      {
        Description = Request.Form["description"],
        Title = Request.Form["title"],
        UserId = int.Parse(Request.Form["user_id"]),
      });

      context.SaveChanges();

      return new Dictionary<string, dynamic>{
        {"message", "Post created"},
      };
    }

    [HttpGet("{id}")]
    public Dictionary<string, dynamic> Read(int id)
    {
      return new Dictionary<string, dynamic>{
        {"data", MainContext.Instance.Posts.Find(id)},
      };
    }
  }
}
