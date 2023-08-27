using System.Text;
using DepartManagment.Application.DependencyInjection;
using DepartManagment.Domain.Entities.ApplicationRole;
using DepartManagment.Domain.Entities.ApplicationUser;
using DepartManagment.Entry.Options;
using DepartManagment.Infrastructure.DependencyInjection;
using DepartManagment.Infrastructure.Models;
using DepartManagment.Persistence;
using DepartManagment.Persistence.DependencyInjection;
using DepartManagment.Presentation.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
//builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddApplicationPart(typeof(DepartManagmentController).Assembly)
    .AddControllersAsServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Jwt jwt = new();
builder.Configuration.GetSection("Jwt").Bind(jwt);
builder.Services.AddMvc();

builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.ConfigureOptions<SwaggerGenOptionsSetup>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audience,
        ValidateAudience = true,
        ValidateIssuer = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
        ValidateIssuerSigningKey = true
    };
});

string CorsPolicyName = "MyPolicy";
builder.Services.ConfigureOptions<CorsPolicyOptionsSetup>();
builder.Services.AddCors();

builder.Services.ConfigureOptions<IdentityOptionsSetup>();

builder.Services.AddIdentity<Employee, ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddTokenProvider<DataProtectorTokenProvider<Employee>>(TokenOptions.DefaultProvider);

builder.Services
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication();



var app = builder.Build();
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
{
    var context = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>()!;

    context.Database.Migrate();

}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }




app.UseCors(CorsPolicyName);
app.MapControllers();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    // Map your library's controllers to the appropriate routes
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});




app.Run();
