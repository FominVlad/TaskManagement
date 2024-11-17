using System.Reflection;
using Microsoft.OpenApi.Models;
using TaskManagement;
using TaskManagement.BL;
using TaskManagement.Data;
using TaskManagement.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "TaskManagement API",
            Version = "v1",
        });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddRepositories();

builder.Services.AddServices();

builder.Services.AddFilters();

builder.Services.AddServiceBus();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();