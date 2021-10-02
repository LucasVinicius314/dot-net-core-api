using System.Collections.Generic;

using FreeSql.DataAnnotations;

namespace DotNetCoreApi
{
  [Table(Name = "Posts")]
  public class Post
  {
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int User_id { get; set; }

    public static List<Post> All()
    {
      return Connection.fsql.Select<Post>().ToList();
    }

    public static void Create(Post post)
    {
      Connection.fsql.Insert<Post>(post).ExecuteIdentity();
    }

    public static Post Read(int id)
    {
      return Connection.fsql.Select<Post>().Where(e => e.Id == id).ToOne();
    }

    public static void Update(int id, Post post)
    {
      Connection.fsql.Update<Post>(id).Set(e => post).ExecuteAffrows();
    }

    public static void Delete(int id)
    {
      Connection.fsql.Delete<Post>(id);
    }
  }
}
