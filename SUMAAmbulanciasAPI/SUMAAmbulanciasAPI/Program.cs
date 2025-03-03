using SUMAAmbulanciasAPI.Areas.Empleados.Services;
using SUMAAmbulanciasAPI.Areas.Usuarios.Services;
using SUMAAmbulanciasAPI.Areas.Clientes.Services;
using SUMAAmbulanciasAPI.Areas.Membrecias.Services;
using SUMAAmbulanciasAPI.Areas.Ganancias.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<EmpleadosService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<MembreciaService>();
builder.Services.AddScoped<gananciasService>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Permitir cualquier origen
               .AllowAnyMethod() // Permitir cualquier método (GET, POST, etc.)
               .AllowAnyHeader(); // Permitir cualquier cabecera
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Usa la política de CORS
app.UseCors("AllowAllOrigins");

app.UseRouting();

app.MapControllers();

app.Run();
