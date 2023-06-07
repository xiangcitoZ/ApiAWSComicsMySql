using ApiAWSComicsMySql.Data;
using ApiAWSComicsMySql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ApiAWSComicsMySql;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString =
            this.Configuration.GetConnectionString("MySql");
        services.AddTransient<RepositoryComics>();
        services.AddDbContext<ComicsContext>
            (options=> options.UseMySql(connectionString
            , ServerVersion.AutoDetect(connectionString)));
        //HABILITAMOS CORS PORQUE DEBEMOS CONFIGURAR EL API
        //CON ESOS PERMISOS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });

    


        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api AWS COMICS ",
                Version = "v1"
                ,
                Description = "Api Comics AWS"



            });
        });

        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseCors(options => options.AllowAnyOrigin());

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("swagger/v1/swagger.json"
                , "Api AWS COMICS v1");
            options.RoutePrefix = "";
        });



        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}