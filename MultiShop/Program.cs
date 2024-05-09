using MultiShop.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MultishopContext>();

var app = builder.Build();


app.UseStaticFiles();

app.MapControllerRoute("areas", "{area:exists}/{controller=slider}/{action=index}/{id?}");
app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");


app.Run();
