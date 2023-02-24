using RecorridoHistoricoApi.Data;
using RecorridoHistoricoApi.Repositories.Abstract;
using RecorridoHistoricoApi.Repositories.Impl;
using RecorridoHistoricoApi.Services;
using RecorridoHistoricoApi.Services.Abstract;
using RecorridoHistoricoApi.Services.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
        .UseSnakeCaseNamingConvention());

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddScoped<ITiposRepository, TipoRepository>();
builder.Services.AddScoped<IHorariosRepository, HorariosRepository>();
builder.Services.AddScoped<IFechasManualesRepository, FechasManualesRepository>();
builder.Services.AddScoped<IRecorridosRepository, RecorridoRepository>();
builder.Services.AddScoped<IDisponibilidadesRepository, DisponibilidadesRepository>();
builder.Services.AddScoped<IDashboardEdecanesRepository, DashboardEdecanesRepository>();

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
