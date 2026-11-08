
var builder = WebApplication.CreateBuilder(args);

// Añadir los controladores
builder.Services.AddControllers();

// Añadir el soporte a OpenAPI:  https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Contruir la aplicacion
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // 1. Expone el endpoint del documento OpenAPI nativo (ej: /openapi/v1.json)
    app.MapOpenApi();

    // Si instalaste Swashbuckle para seguir usando Swagger UI:
    app.UseSwaggerUI(options =>
    {
        // Le indicamos dónde está el archivo JSON generado por .NET
        options.SwaggerEndpoint("/openapi/v1.json", "RestAPI for 4VGym v1");
    });

}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
