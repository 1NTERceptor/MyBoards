using Microsoft.EntityFrameworkCore;
using StackOverflow.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StackOverflowContext>(
        option => option
        .UseSqlServer(builder.Configuration.GetConnectionString("StackOverflowConnectionString"))
    );

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program)); // Scans for mapping profiles in the assembly

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
