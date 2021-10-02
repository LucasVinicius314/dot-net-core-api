class Connection
{
  public static IFreeSql fsql = new FreeSql.FreeSqlBuilder()
  .UseConnectionString(FreeSql.DataType.MySql, @"host=localhost; user=root; password=; port=3306; database=dotnet")
  .UseAutoSyncStructure(true) // automatically synchronize the entity structure to the database
  .Build(); // be sure to define as singleton mode
}