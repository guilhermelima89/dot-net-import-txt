using Api.Data;
using Api.Interfaces;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));

builder.Services.AddScoped<ApplicationDbContext>();

builder.Services.AddScoped<IArquivoService, ArquivoService>();

builder.Services.AddScoped<IArquivoRepository, ArquivoRepository>();
builder.Services.AddScoped<IArquivoErroRepository, ArquivoErroRepository>();
builder.Services.AddScoped<IArquivoStatusProcessamentoRepository, ArquivoStatusProcessamentoRepository>();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("Production",
            builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
    });

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("Production");

app.MapControllers();

app.Run();
