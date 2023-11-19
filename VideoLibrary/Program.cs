using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Data;
using System.Data.SqlClient;
using VideoLibrary.Data;
using VideoLibrary.Data.Interfaces;
using VideoLibrary.Service;
using VideoLibrary.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var corsBuilder = new CorsPolicyBuilder();
corsBuilder.AllowAnyHeader();
corsBuilder.AllowAnyMethod();
corsBuilder.AllowAnyOrigin();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowedOrigins", corsBuilder.Build());
});

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(cs));
builder.Services.AddTransient<IVideoService, VideoService>();
builder.Services.AddTransient<IVideoRepo, VideoRepo>();
builder.Services.Configure<IISServerOptions>(options => {
    options.MaxRequestBodySize = int.MaxValue;
});
builder.Services.Configure<KestrelServerOptions>(options => {
    options.Limits.MaxRequestBodySize = int.MaxValue;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowedOrigins");

app.Run();
