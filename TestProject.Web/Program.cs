using MediatR;
using Microsoft.EntityFrameworkCore;
using TestProject.Web;
using TestProject.Web.Application;
using TestProject.Web.Components;
using TestProject.Web.Core;
using TestProject.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddDbContext<UserContext>(opt =>
{
  var connection = builder.Configuration.GetConnectionString("AppDb");
  opt.UseMySql(connection, ServerVersion.AutoDetect(connection));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddMediatR(opt =>
{
  opt.RegisterServicesFromAssemblyContaining<Program>();
});
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserValidation<,>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.AddUserEndpoints();

app.Run();


public partial class Program;