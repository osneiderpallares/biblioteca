using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApiBiblioteca.Context;
using WebApiBiblioteca.Mapper;
using WebApiBiblioteca.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biblioteca App", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped<LibrosServices>();
builder.Services.AddScoped<AutoresServices>();
builder.Services.AddScoped<ParametrosServices>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("localDbConextion") + ";Encrypt=False"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
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
