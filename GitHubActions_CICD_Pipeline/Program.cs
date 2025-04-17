var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:80"); // Add this line

// Add services
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<NasaService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
