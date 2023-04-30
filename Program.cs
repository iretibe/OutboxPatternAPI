using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OutboxPatternAPI.BackgroundServices;
using OutboxPatternAPI;
using OutboxPatternAPI.Data;
using OutboxPatternAPI.EmailOutboxService;
using OutboxPatternAPI.Helper;
using OutboxPatternAPI.Models;
using OutboxPatternAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register AppDbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService>();

var emailSettings = new EmailSettings();
builder.Configuration.Bind(EmailSettings.SectionName, emailSettings);
builder.Services.AddSingleton(Options.Create(emailSettings));
builder.Services.AddSingleton<IMailService, MailService>();

builder.Services.AddScoped<IEmailOutboxService, EmailOutboxService>();

builder.Services.AddSingleton<IEmailBackgroundServices, EmailBackgroundServices>();
builder.Services.AddHostedService<EmailBackgroundService>();

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
