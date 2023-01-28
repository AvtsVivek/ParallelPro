var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(9874));

// Add services to the container.
builder.Services.AddSingleton<ICustomerService, CustomerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/customers", async (ICustomerService provider) =>
    {
        await Task.Delay(3000);
        provider.GetCustomers();
    })
    .WithName("GetCustomers");

app.MapGet("/customers/{id}", async (ICustomerService provider, int id) =>
    {
        await Task.Delay(1000);
        return provider.GetCustomers().FirstOrDefault(p => p.Id == id);
    })
    .WithName("GetCustomerById");

app.MapGet("/customers/ids", 
    (ICustomerService provider) => provider.GetCustomers().Select(p => p.Id).ToList())
    .WithName("GetAllCustomerIds");

app.Run();
