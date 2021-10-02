using System.Collections.Generic;

namespace DotNetCoreApi
{
  public class User
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public List<Post> Posts { get; } = new List<Post>();

    // public static List<User> All()
    // {
    //   return Connection.fsql.Select<User>().ToList();
    // }

    // public static void Create(User user)
    // {
    //   Connection.fsql.Insert<User>(user).ExecuteIdentity();
    // }

    // public static User Read(int id)
    // {
    //   return Connection.fsql.Select<User>().Where(e => e.Id == id).ToOne();
    // }
  }
}