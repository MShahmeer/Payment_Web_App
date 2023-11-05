using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Here the instance of PaymentDetailContext is created by passing the value for its contructor parameter which are the DB provider and the corresponding DB connection string. Now the created Instance of DbContext is shared among all the controller having the same constructor parameter of the same type PaymentDetailContext

//If the concept of Dependency Injection would not be there then we had to create the instance of DbContext class in each of the controller by passing the Db provider and corresponding connection string
builder.Services.AddDbContext<PaymentDetailsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader
());

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
