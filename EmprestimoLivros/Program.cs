using EmprestimoLivros.Data;
using EmprestimoLivros.Services.EmprestimoService;
using EmprestimoLivros.Services.LoginService;
using EmprestimoLivros.Services.SenhaServices;
using EmprestimoLivros.Services.SessaoService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Conex�o com o banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 4, 0)) 
    );
});

//Comunica��o da interface de Service de Autentica��o
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


//Comunica��o da Interface com a Service 

builder.Services.AddScoped<ILoginInterface, LoginService>();
builder.Services.AddScoped<ISenhaInterface, SenhaService>();
builder.Services.AddScoped<ISessaoInterface, SessaoService>();
builder.Services.AddScoped<IEmprestimoInterface, EmprestimoService>();


//Informa��es de sess�o de autentica��o
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Faz parte da Sess�o de autentica��o
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
