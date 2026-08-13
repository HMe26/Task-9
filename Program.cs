using BikeStoreApp.Data;

ApplicationDbContext db = new();

Console.WriteLine(" 1. All Categories ");

var categories = db.Categories.ToList();

foreach (var category in categories)
{
    Console.WriteLine($"{category.CategoryId} - {category.CategoryName}");
}


Console.WriteLine("\n 2. First Product ");

var firstProduct = db.Products.FirstOrDefault();

if (firstProduct != null)
{
    Console.WriteLine($"{firstProduct.ProductId} - {firstProduct.ProductName}");
}


Console.WriteLine("\n 3. Product by ID ");

int productId = 5;

var product = db.Products.FirstOrDefault(p => p.ProductId == productId);

if (product != null)
{
    Console.WriteLine($"{product.ProductId} - {product.ProductName}");
}
else
{
    Console.WriteLine("Product not found.");
}


Console.WriteLine("\n 4. Products by Model Year ");

short modelYear = 2018;

var productsByYear = db.Products
    .Where(p => p.ModelYear == modelYear)
    .ToList();

foreach (var productItem in productsByYear)
{
    Console.WriteLine(
        $"{productItem.ProductId} - {productItem.ProductName} - {productItem.ModelYear}");
}


Console.WriteLine("\n 5. Customer by ID ");

int customerId = 10;

var customer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);

if (customer != null)
{
    Console.WriteLine(
        $"{customer.CustomerId} - {customer.FirstName} {customer.LastName}");
}
else
{
    Console.WriteLine("Customer not found.");
}


Console.WriteLine("\n 6. Products with Brands ");

var productsWithBrand = db.Products
    .Select(p => new
    {
        p.ProductName,
        BrandName = p.Brand.BrandName
    })
    .ToList();

foreach (var item in productsWithBrand)
{
    Console.WriteLine(
        $"Product: {item.ProductName} - Brand: {item.BrandName}");
}


Console.WriteLine("\n 7. Products Count in Category ");

int categoryId = 1;

var productsCount = db.Products.Count(p => p.CategoryId == categoryId);

Console.WriteLine(
    $"Category {categoryId} contains {productsCount} products.");


Console.WriteLine("\n 8. Total List Price ");

var totalPrice = db.Products
    .Where(p => p.CategoryId == categoryId)
    .Sum(p => p.ListPrice);

Console.WriteLine($"Total list price: {totalPrice:F2}");


Console.WriteLine("\n 9. Average List Price ");

var averagePrice = db.Products.Average(p => p.ListPrice);

Console.WriteLine($"Average list price: {averagePrice:F2}");


Console.WriteLine("\n 10. Completed Orders ");

byte completedStatus = 4;

var completedOrders = db.Orders
    .Where(o => o.OrderStatus == completedStatus)
    .ToList();

foreach (var order in completedOrders)
{
    Console.WriteLine(
        $"Order ID: {order.OrderId} - Customer ID: {order.CustomerId}");
}