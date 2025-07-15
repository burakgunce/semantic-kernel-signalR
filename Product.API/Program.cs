using Products.API.ViewModels;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/best-selling-products", () => new List<Product> {
    new("Ürün 1", 10),
    new("Ürün 2", 20),
    new("Ürün 3", 30),
    new("Ürün 4", 40),
    new("Ürün 5", 50),
    new("Ürün 6", 60),
});

app.Run();
