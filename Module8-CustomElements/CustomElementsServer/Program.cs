var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");
app.MapGet("/", () => "This is the server supplying our Custom element");


app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });


app.Run();
