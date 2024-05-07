var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseStaticFiles();
app.MapControllerRoute("default", "{controller=home}/{action=index}");
app.MapControllerRoute("shop", "{controller=}/{action=index}");

app.Run();
