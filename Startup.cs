using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetCoreApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
      {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
      }));

      services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();


      app.Use(async (context, nextMiddleware) =>
        {
          context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
          context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
          context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
          context.Response.Headers.Add("Access-Control-Max-Age", "86400");

          await nextMiddleware();
        });

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseMiddleware<JwtMiddleware>();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}