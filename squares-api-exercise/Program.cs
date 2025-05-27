using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenXmlPowerTools;
using squares_api_excercise.Data;
using squares_api_excercise.Repositories;
using squares_api_excercise.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SquaresDbContext>(options =>
      options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
          ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));


builder.Services.AddScoped<ISquaresService, SquaresService>();
builder.Services.AddScoped<IPointsService, PointsService>();

builder.Services.AddScoped<ISquaresRepository, SquaresRepository>();
builder.Services.AddScoped<IPointsRepository, PointsRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(filePath, includeControllerXmlComments: true);
});
//C.IncludeXmlComments(XmlPath);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.SerializeAsV2 = true);
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
