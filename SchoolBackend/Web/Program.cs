using Entity.Context.Main;
using Microsoft.EntityFrameworkCore;
using Utilities.AlmacenadorArchivos.implementes;
using Utilities.AlmacenadorArchivos.Interface;
using Web.Extendes;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMapperApp();

// Configuracion de la base de datos
builder.Services.AddDb("PgAdmin", builder.Configuration);
//builder.Services.AddDb("PgAdminLog", builder.Configuration);

// Inyeccion de dependencias de los controladores
builder.Services.AddInject();
builder.Services.AddJwtConfig(builder.Configuration);
builder.Services.AddViewAuthApi();

//Agrega las validaciones genericas
builder.Services.AddHelpersValidation();
builder.Services.AddCustomCors(builder.Configuration);


// azure (AlmacenadorArchivos) local (AlmacenadorLocal) esto para el almacenamiento de las imagenes
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorLocal>();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorLocal>();


// CACHE
builder.Services.AddOutputCache((options) =>
{
    // se puede configurar para minutos, horas dias ..
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(10);
});

var app = builder.Build();


app.UseMiddleware<ProblemDetailsMiddleware>();

/*
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();
    if (dbContext.Database.IsRelational())
    {
        dbContext.Database.Migrate();
    }
}*/

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Endpoint del JSON de Swagger
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");

    // Esto colapsa todos los endpoints
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

// para servir achivos estaticos
app.UseStaticFiles();

// Middleware del cache
app.UseOutputCache();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
