
using Microsoft.EntityFrameworkCore;
using Resort_Web.Data;
using Resort_Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//     .WriteTo.File("log/villalogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

// builder.Host.UseSerilog();

var PgConn = builder.Configuration["ConnectionString:Resort"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(PgConn));

builder.Services.AddControllers(option => {
    option.ReturnHttpNotAcceptable=true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingConfig));

// builder.Services.AddSingleton<ILogging, Logging>();

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
