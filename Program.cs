using DevTrackR.API.Persistence;
using DevTrackR.API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DevTrackRcs");
// builder.Services
// .AddDbContext<DevTrackRContext>(o => o.UseSqlServer(connectionString));

builder.Services
      .AddDbContext<DevTrackRContext>(o => o.UseInMemoryDatabase("DevTrackR"));

builder.Services.AddScoped<IPackageRepository, PackageRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevTrackR.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Rafael Difini Dev Jr",
            Email = "rafaeldifini@live.com",
        }
    });

    var xmlPath = Path.Combine(AppContext.BaseDirectory, "DevTrackR.API.xml");
    o.IncludeXmlComments(xmlPath);

});

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
