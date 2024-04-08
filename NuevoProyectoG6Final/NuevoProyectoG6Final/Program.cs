using Microsoft.EntityFrameworkCore;
using Vet.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Inyeccion de Dependencas
builder.Services.AddDbContext<VetContext>(options => options.UseSqlServer("name=ConnVetDB"));

//****Identity****
var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");
builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultUI();
builder.Services.AddRazorPages();
//****Identity****

//Politica de contrasenas
builder.Services.Configure<IdentityOptions>(options =>
{
    //Longitud Contrasena
    options.Password.RequiredLength = 6;
    //Para que no requiera un caracter especial
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    //No quiero caracteres unicos
    options.Password.RequiredUniqueChars = 0;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

//****Identity****
app.UseAuthorization();
app.UseAuthentication();
//****Identity****

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//****Identity****
app.MapRazorPages();
//****Identity****

app.Run();
