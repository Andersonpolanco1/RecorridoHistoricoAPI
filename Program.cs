using EdecanesV2.Data;
using EdecanesV2.Repositories.Abstract;
using EdecanesV2.Repositories.Impl;
using EdecanesV2.Services;
using EdecanesV2.Services.Abstract;
using EdecanesV2.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddScoped<ITiposRepository, TipoRepository>();
builder.Services.AddScoped<IHorariosRepository, HorariosRepository>();
builder.Services.AddScoped<IFechasManualesRepository, FechasManualesRepository>();
builder.Services.AddScoped<IRecorridosRepository, RecorridoRepository>();
builder.Services.AddScoped<IDisponibilidadesRepository, DisponibilidadesRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
