using PizzaEnterprise.Application;
using PizzaEnterprise.Infrastructure;
using PizzaEnterprise.Infrastructure.Persistence;
using PizzaEnterprise.Infrastructure.Persistence.Seeds;
using PizzaEnterprise.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Pizza Enterprise API", 
        Version = "v1",
        Description = "API for Pizza Enterprise - Online ordering and delivery management system"
    });
});

// Add Application services (MediatR, AutoMapper, FluentValidation)
builder.Services.AddApplicationServices();

// Add Infrastructure services (EF Core, Repositories, UnitOfWork, AS400)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Seed database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
    await DataSeeder.SeedAsync(context);
}

// Configure the HTTP request pipeline
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
