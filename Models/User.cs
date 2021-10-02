using System.Collections.Generic;

using FreeSql.DataAnnotations;

namespace DotNetCoreApi
{
  [Table(Name = "Users")]
  public class User
  {
    [Column(IsIdentity = true, IsPrimary = true)]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public ICollection<Post> Posts { get; set; }

    public static List<User> All()
    {
      return Connection.fsql.Select<User>().ToList();
    }

    public static void Create(User user)
    {
      Connection.fsql.Insert<User>(user).ExecuteIdentity();
    }

    public static User Read(int id)
    {
      return Connection.fsql.Select<User>().Where(e => e.Id == id).ToOne();
    }
  }
}
