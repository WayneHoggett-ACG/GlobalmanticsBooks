using Microsoft.EntityFrameworkCore;

// Add services to the container
var builder = WebApplication.CreateBuilder(args);

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

app.MapGet("/books", async (BookDb db) =>
    await db.Books.ToListAsync());

app.MapGet("/books/{id}", async (int id, BookDb db) =>
    await db.Books.FindAsync(id)
        is Book book
            ? Results.Ok(book)
            : Results.NotFound());

app.MapPost("/book/add", async (Book book, BookDb db) =>
{
    db.Books.Add(book);
    await db.SaveChangesAsync();

    return Results.Created($"/books/{book.Id}", book);
});

app.MapPut("/books/update/{id}", async (int id, Book inputBook, BookDb db) =>
{
    var book = await db.Books.FindAsync(id);

    if (book is null) return Results.NotFound();

    book.Title = inputBook.Title;
    book.Image = inputBook.Image;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/books/delete/{id}", async (int id, BookDb db) =>
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
