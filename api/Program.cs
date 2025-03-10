using Microsoft.EntityFrameworkCore;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using OpenTelemetry.Resources;

// Add services to the container
var builder = WebApplication.CreateBuilder(args);

// Add OpenTelemetry and configure it to use Azure Monitor if APPLICATIONINSIGHTS_CONNECTION_STRING is not null or empty
if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING"))) {
    builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource =>
    {
        resource.AddService(
            serviceName: "Globalmantics Books API"
        );
    })
    .UseAzureMonitor();
}
//Set Environment Variable APPLICATIONINSIGHTS_CONNECTION_STRING

// Use SQL Server if the connection string is present, otherwise use in-memory database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddDbContext<BookDb>(opt => opt.UseInMemoryDatabase("BookList"));
}
else
{
    builder.Services.AddDbContext<BookDb>(opt => opt.UseSqlServer(connectionString));
}

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<BookDb>();
    DbInitializer.Initialize(context);
}

// Define the routes, PREFIX_URL_PATH should start with a / if defined
var prefixUrlPath = Environment.GetEnvironmentVariable("PREFIX_URL_PATH") ?? string.Empty;

var pathRoute = app.MapGet($"{prefixUrlPath}/", () =>
{
    return Results.Ok("API is operational.");
});

app.MapGet($"{prefixUrlPath}/books", async (BookDb db) =>
    await db.Books.ToListAsync());

app.MapGet($"{prefixUrlPath}/books/{{id}}", async (int id, BookDb db) =>
    await db.Books.FindAsync(id)
        is Book book
            ? Results.Ok(book)
            : Results.NotFound());

app.MapPost($"{prefixUrlPath}/book/add", async (Book book, BookDb db) =>
{
    db.Books.Add(book);
    await db.SaveChangesAsync();

    return Results.Created($"/books/{book.Id}", book);
});

app.MapPut($"{prefixUrlPath}/books/update/{{id}}", async (int id, Book inputBook, BookDb db) =>
{
    var book = await db.Books.FindAsync(id);

    if (book is null) return Results.NotFound();

    book.Title = inputBook.Title;
    book.Image = inputBook.Image;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete($"{prefixUrlPath}/books/delete/{{id}}", async (int id, BookDb db) =>
{
    if (await db.Books.FindAsync(id) is Book book)
    {
        db.Books.Remove(book);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();
