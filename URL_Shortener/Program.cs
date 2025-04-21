using URL_Shortener.Models;
using URL_Shortener.Services;
using URL_Shortener.Repositories;
using Microsoft.EntityFrameworkCore;

var UrlShortenerOrigins = "_urlShortenerOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: UrlShortenerOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUrlShortenerRepository, UrlShortenerRepository>();
builder.Services.AddScoped<IUrlShorteningService, UrlShorteningService>();

var connectionString =
    builder.Configuration.GetConnectionString("UrlShortenerDatabase")
        ?? throw new InvalidOperationException("Connection string"
        + "'UrlShortenerDatabase' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(UrlShortenerOrigins);

app.MapControllers();

app.Run();
