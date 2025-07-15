using Products.API.ViewModels;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/best-selling-products", () => new List<Product> {
    new("�r�n 1", 10),
    new("�r�n 2", 20),
    new("�r�n 3", 30),
    new("�r�n 4", 40),
    new("�r�n 5", 50),
    new("�r�n 6", 60),
});

app.Run();
