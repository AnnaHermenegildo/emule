using Emule.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);


// ?? Registrar os API clients com HttpClient
builder.Services.AddHttpClient<UsuarioApiClient>();
builder.Services.AddHttpClient<PlanoApiClient>();
builder.Services.AddHttpClient<AssinaturaApiClient>();
builder.Services.AddHttpClient<BandaApiClient>();
builder.Services.AddHttpClient<MusicaApiClient>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
