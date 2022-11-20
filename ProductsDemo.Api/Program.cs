using Microsoft.EntityFrameworkCore;
using ProductsDemo.Api.Hubs;
using ProductsDemo.Data.DataAccess;
using ProductsDemo.Logging;
using ProductsDemo.Logging.Interface;
using ProductsDemo.Model.Mapping;
using ProductsDemo.Repository;
using ProductsDemo.Repository.Interface;
using ProductsDemo.Service;
using ProductsDemo.Service.Interface;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("ProductsDB");
builder.Services.AddDbContext<ProductsContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<IProductsService, ProductsService>();
builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
builder.Services.AddAutoMapper(typeof(ProductMapper));
builder.Services.AddSingleton<ILog, Log>();
builder.Services.AddSignalR();
builder.Services.AddHttpLogging(config =>
{
    config.LoggingFields = HttpLoggingFields.All;
});

// Adds Microsoft Identity platform (Azure AD B2C) support to protect this Api
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//         .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
//         .EnableTokenAcquisitionToCallDownstreamApi().AddInMemoryTokenCaches();
// End of the Microsoft Identity platform block    




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();
app.MapControllers();

app.UseEndpoints(routes =>
{
    routes.MapHub<NotificationHub>("/notificationhub");
});
app.Run();
