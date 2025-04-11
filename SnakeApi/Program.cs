using SnakeApi;
using SnakeApi.Models;
using SnakeApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de SnakeDatabaseSettings
builder.Services.Configure<SnakeDatabaseSettings>(
    builder.Configuration.GetSection(nameof(SnakeDatabaseSettings)));

builder.Services.AddSingleton<SnakeDatabaseSettings>(sp =>
    sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<SnakeDatabaseSettings>>().Value);

// Agregar servicio
builder.Services.AddSingleton<ScoreService>();

// Agregar controladores
builder.Services.AddControllers();

// Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll"); // aplica la política de CORS
app.UseAuthorization();

app.MapControllers();

app.Run();
