using ApiProcessamento.Config;
using Microsoft.EntityFrameworkCore;
using ApiProcessamento.Data;
using ApiProcessamento.Repositories.Interfaces;
using ApiProcessamento.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
}
);

builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlite("Data Source=app.db"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<SensorRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
}*/


app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "documentacao";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiProcessamento v1");
}
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
